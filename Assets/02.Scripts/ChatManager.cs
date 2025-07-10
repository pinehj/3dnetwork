using ExitGames.Client.Photon;
using NUnit.Framework;
using Photon.Chat;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    // 채팅 이벤트
    // 0. 서버 로그
    // 1. 서버 접속/해제  (카카오톡 접속/해제)
    // 2. 채널 접속/해제 (카카오톡 채팅방 접속/해제)
    // 3. 메시지 수신(1:1, 오픈채팅)
    // 4. 다른 사람 방 입장/퇴장 (카톡 단독방 입장/퇴장)
    // 5. 친구 이벤트(친구 상태 변화)
    public static ChatManager Instance { get; private set; }
    private ChatClient _client;

    public event Action OnDataChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _client = new ChatClient(this);

        // 디버그 로그 레벨
        _client.DebugOut = DebugLevel.ALL;

        _client.ChatRegion = "asia";

        var auth = new AuthenticationValues("hj");

        _client.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0.0", auth);

    }

    private void Update()
    {
        _client.Service();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SendPublicChatMessage("ㅎㅇ");
        }
    }
    // 포톤챗 내부 로그 발생시 호출되는 함수(필터링 레벨 이상)
    public void DebugReturn(DebugLevel level, string message)
    {
        // unity의 debug.Log 계열로 우회
        switch (level)
        {
            case DebugLevel.ERROR: Debug.LogError(message); break;
            case DebugLevel.WARNING: Debug.LogWarning(message); break;
            default: Debug.Log(message); break;
        }
        Debug.Log(message);
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log($"포톤챗 상태: {state}");
    }

    private const string DEFAULT_GLOBAL_CHANNEL = "global";
    private const string DEFAULT_NOTICE_CHANNEL = "notice";


    public void OnConnected()
    {
        Debug.Log("포톤챗 접속 완료");
        var channelOption = new ChannelCreationOptions();

        channelOption.PublishSubscribers = true;



        //_client.Subscribe("global");                // 채널 1개 구독
        _client.Subscribe(DEFAULT_GLOBAL_CHANNEL, creationOptions:channelOption); // 채널 여러개 구독

    }

    public void OnDisconnected()
    {
        Debug.Log("포톤챗 접속 종료");
    }

    private List<Chat> _chats = new List<Chat>();
    public List<Chat> Chats => _chats;
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        /**
   • sender, messages 가 배열인 이유:
     Photon 은 네트워크 최적화를 위해 같은 프레임(또는 같은 네트워크 패킷) 안에 들어온
     여러 개의 메시지를 한 번에 묶어 전달할 수 있다. 따라서 한 콜백 호출에 n개의
     발신자(senders) 와 메시지(messages)가 함께 온다.
   • messages 는 object[] 이며 주로 string 을 사용하지만 byte[]/JSON 도 가능.
   */

        for (int i = 0; i < messages.Length; i++)
        {
            Debug.Log($"[{channelName}] {senders[i]}: {messages[i]}");

            if (senders[i] == "hj")
            {
                _chats.Add(new Chat(EChatType.Mine, senders[i], messages[i].ToString()));
            }
            else
            {
                _chats.Add(new Chat(EChatType.Other, senders[i], messages[i].ToString()));
            }
        }

        OnDataChanged?.Invoke();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        // channels :이번에 구독 요청한 채널들
        // results : 구독 성공 여부

        for (int i = 0; i < channels.Length; i++)
        {
            Debug.Log($"[포톤챗] 구독 -> {channels[i]} (결과: {results[i]})");
        }

        foreach(var channel in _client.PublicChannels)
        {
            Debug.Log($"현재 구독 중인 채널: {channel.Key}");
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        foreach(var channel in channels)
        {
            Debug.Log($"[포톤챗] 구독취소 -> {channel}");
        }
    }

    public void OnUserSubscribed(string channel, string user)
    {
        // 00님 입장
        Debug.Log($"[PhotonChat] {user} joined {channel}");

        _chats.Add(new Chat(EChatType.System, "system", $"{user} Entered the room"));
        OnDataChanged?.Invoke();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        // 00님 퇴장
        Debug.Log($"[PhotonChat] {user} left {channel}");

        _chats.Add(new Chat(EChatType.System, "system", $"{user} Exited the room"));
        OnDataChanged?.Invoke();
    }

    public void SendPublicChatMessage(string message)
    {
        if(_client == null || _client.CanChat)
        {
            _client.PublishMessage(DEFAULT_GLOBAL_CHANNEL, message);
        }
    }
}

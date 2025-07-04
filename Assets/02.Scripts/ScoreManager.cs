using Photon.Pun;
using Photon;
using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using System;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    [SerializeField] private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            Refresh();
        }
    }
    [SerializeField] private int _killCount;
    public int KillCount
    {
        get
        {
            return _killCount;
        }
        set
        {
            _killCount = value;
            Refresh();
        }
    }

    public int TotalScore
    {
        get
        {
            return _killCount * 5000 + Score;
        }
    }

    public event Action OnDataChanged;
    private Dictionary<string, int> _scoreDict = new Dictionary<string, int>();
    public Dictionary<string, int> ScoreDict => _scoreDict;
    private Dictionary<string, int> _killCountDict = new Dictionary<string, int>();
    public Dictionary<string, int> KillCountDict => _killCountDict;
    private Dictionary<string, int> _totalScoreDict = new Dictionary<string, int>();
    public Dictionary<string, int> TotalScoreDict => _totalScoreDict;

    private void Awake()
    {
        _instance = this;
    }
    public override void OnJoinedRoom()
    {
        Refresh();
    }


    public void Refresh()
    {

        Hashtable hashTable = new Hashtable();
        hashTable.Add("Score", _score);
        hashTable.Add("KillCount", _killCount);
        hashTable.Add("TotalScore", TotalScore);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hashTable);

    }

    public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable hashTable)
    {
        var roomPlayers = PhotonNetwork.PlayerList;
        foreach (Photon.Realtime.Player player in roomPlayers)
        {
            if (player.CustomProperties.ContainsKey("Score"))
            {
                _scoreDict[$"{player.NickName}_{player.ActorNumber}"] = (int)player.CustomProperties["Score"];
            }
            if (player.CustomProperties.ContainsKey("KillCount"))
            {
                _killCountDict[$"{player.NickName}_{player.ActorNumber}"] = (int)player.CustomProperties["KillCount"];
            }
            if (player.CustomProperties.ContainsKey("TotalScore"))
            {
                _totalScoreDict[$"{player.NickName}_{player.ActorNumber}"] = (int)player.CustomProperties["TotalScore"];
            }
        }

        OnDataChanged?.Invoke();
    }
}

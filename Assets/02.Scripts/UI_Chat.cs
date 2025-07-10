using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Chat : MonoBehaviour
{
    public ChatSlot MyChatPrefab;
    public ChatSlot OtherChatPrefab;
    public ChatSlot SystemChatPrefab;

    public Transform ChatContainer;

    public List<ChatSlot> Slots;

    public TMP_InputField InputField;
    private void Start()
    {
        ChatManager.Instance.OnDataChanged += Refresh;
    }

    public void OnSendButtonClicked()
    {
        if (string.IsNullOrEmpty(InputField.text))
        {
            return;
        }
        ChatManager.Instance.SendPublicChatMessage(InputField.text);
        InputField.text = string.Empty;
    }

    public void Refresh()
    {
        for(int i = Slots.Count-1; i>=0; --i)
        {
            Destroy(Slots[i].gameObject);
        }

        Slots.Clear();

        foreach(Chat chat in ChatManager.Instance.Chats)
        {
            switch (chat.Type)
            {
                case EChatType.Mine:
                {
                    ChatSlot newChatSlot = Instantiate(MyChatPrefab, ChatContainer);
                    newChatSlot.Set(chat);
                    Slots.Add(newChatSlot);

                    break;
                }
                case EChatType.Other:
                {
                    ChatSlot newChatSlot = Instantiate(OtherChatPrefab, ChatContainer);
                    newChatSlot.Set(chat);
                    Slots.Add(newChatSlot);

                    break;
                }
                case EChatType.System:
                {
                    ChatSlot newChatSlot = Instantiate(SystemChatPrefab, ChatContainer);
                    newChatSlot.Set(chat);
                    Slots.Add(newChatSlot);

                    break;
                }
            }
        }
    }
}

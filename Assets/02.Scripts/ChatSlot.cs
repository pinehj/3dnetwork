using TMPro;
using UnityEngine;

public class ChatSlot : MonoBehaviour
{
    public TextMeshProUGUI ContentTextUI;
    public TextMeshProUGUI DateTextUI;


    public void Set(Chat chat)
    {
        ContentTextUI.text = $"{chat.Nickname}:\n{chat.Message}";

        if (DateTextUI == null)
        {
            return;
        }
        DateTextUI.text = "nn";
    }
}

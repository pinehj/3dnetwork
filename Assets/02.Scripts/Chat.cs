using UnityEngine;

public enum EChatType
{
    Mine,
    Other,
    System
}
public class Chat
{
    public readonly EChatType Type;
    public readonly string Nickname;
    public readonly string Message;

    public Chat(EChatType type, string nickname, string message)
    {
        if (string.IsNullOrEmpty(nickname))
        {
            throw new System.Exception("Nickname cannot be null or empty");
        }
        if (string.IsNullOrEmpty(message))
        {
            throw new System.Exception("Message cannot be null or empty");
        }
        Type = type;
        Nickname = nickname;
        Message = message;
    }
}

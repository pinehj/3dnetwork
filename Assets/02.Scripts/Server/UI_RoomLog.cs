using TMPro;
using UnityEngine;

public class UI_RoomLog : MonoBehaviour
{
    public TextMeshProUGUI LogTextUI;
    private string _logMessage;
    private void Start()
    {
        RoomManager.Instance.OnPlayerEntered += PlayerEnterLog;
        RoomManager.Instance.OnPlayerExited += PlayerExitLog;
        RoomManager.Instance.OnPlayerDead += PlayerDeathLog;


        Refresh();
    }

    public void Refresh()
    {
        LogTextUI.text = _logMessage;
    }

    public void PlayerEnterLog(string playerName)
    {
        _logMessage += $"\n<color=#00ff00ff>{playerName}</color> <color=blue>Entered</color> the room";
        Refresh();
    }

    public void PlayerExitLog(string playerName)
    {
        _logMessage += $"\n<color=#00ff00ff>{playerName}</color> <color=#800000ff>Exited</color> the room";
        Refresh();
    }

    public void PlayerDeathLog(string playerName, string attackerName)
    {
        _logMessage += $"\n<color=#ffa500ff>{playerName}</color> <color=red>Killed</color> by <color=#ff00ffff>{attackerName}</color>";
        Refresh();
    }
}

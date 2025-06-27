using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player Player { get; private set; }
    public event Action OnInit;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        Player = FindObjectsByType<Player>(FindObjectsSortMode.None).First(player => player.PhotonView.IsMine);
        OnInit?.Invoke();
    }
}

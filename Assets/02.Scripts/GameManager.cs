using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player Player { get; private set; }
    public event Action OnInit;

    [SerializeField] private List<Transform> _spawnPositionList;
    protected override void Awake()
    {
        base.Awake();
    }

    public void Init()
    {
        Player = PhotonNetwork.Instantiate("Player", _spawnPositionList[UnityEngine.Random.Range(0, _spawnPositionList.Count)].position, Quaternion.identity).GetComponent<Player>();
        //Player = FindObjectsByType<Player>(FindObjectsSortMode.None).First(player => player.PhotonView.IsMine);
        OnInit?.Invoke();
    }

    public IEnumerator RespawnPlayer(Player player)
    {
        yield return new WaitForSeconds(5);
        player.PhotonView.RPC(nameof(Player.Respawn), RpcTarget.All, _spawnPositionList[UnityEngine.Random.Range(0, _spawnPositionList.Count)].position);
    }
}

using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Player : MonoBehaviour
{
    public PlayerStat Stat { get; private set; }
    public PlayerState State { get; private set; }
    public Animator Animator { get; private set; }
    public PhotonView PhotonView { get; private set; }
    private Dictionary<Type, PlayerAbility> _abilitiesCache;

    private void Awake()
    {
        PlayerAbility[] abilities = GetComponents<PlayerAbility>();
        _abilitiesCache = new Dictionary<Type, PlayerAbility>();
        foreach(PlayerAbility ability in abilities)
        {
            _abilitiesCache[ability.GetType()] = ability;
        }

        Stat = GetComponent<PlayerStat>();
        State = GetComponent<PlayerState>();
        Animator = GetComponentInChildren<Animator>();
        PhotonView = GetComponent<PhotonView>();
    }

    public T GetAbility<T>() where T: PlayerAbility
    {
        var type = typeof(T);

        if (_abilitiesCache.TryGetValue(type, out PlayerAbility ability))
        {
            return ability as T;
        }

        ability = GetComponent<T>();
        if(ability != null)
        {
            _abilitiesCache[typeof(T)] = ability;
            return ability as T;
        }
        throw new Exception($"어빌리티 {type.Name}을 {gameObject.name}에서 찾을 수 없습니다.");
    }

}

using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerState
{
    [SerializeField] private bool _isRunning;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
        }
    }

    [SerializeField] private bool _isJumping;

    public bool IsJumping
    {
        get
        {
            return _isJumping;
        }
        set
        {
            _isJumping = value;
        }
    }

    [SerializeField] private bool _isDead;

    public bool IsDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            _isDead = value;
        }
    }
}

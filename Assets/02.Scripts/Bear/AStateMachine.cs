using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Die
}
public abstract class AStateMachine<T>
{
    protected T _owner;
    public T Owner => _owner;
    protected Dictionary<EState, AState<T>> _stateDict;
    protected AState<T> _currentState;
    public virtual void Init(T owner)
    {
        _owner = owner;
        InitDict();
        InitStates();
        ChangeState(EState.Idle);
    }

    protected abstract void InitDict();
    protected virtual void InitStates()
    {
        foreach(AState<T> state in _stateDict.Values)
        {
            state.Init(this);
        }
    }
    public virtual void Execute()
    {
        _currentState.Execute();
    }

    public void ChangeState(EState goalState)
    {
        if(_currentState == _stateDict[goalState])
        {
            return;
        }
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = _stateDict[goalState];
        _currentState.Enter();

    }
}

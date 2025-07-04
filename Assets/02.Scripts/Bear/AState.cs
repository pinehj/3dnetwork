using UnityEditor;
using UnityEngine;

public abstract class AState<T>
{
    protected AStateMachine<T> _stateMachine;
    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();
    public virtual void Init(AStateMachine<T> stateMachine)
    {
        _stateMachine = stateMachine;
    }
}

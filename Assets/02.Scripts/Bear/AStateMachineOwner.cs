using UnityEngine;

public abstract class AStateMachineOwner<T> : MonoBehaviour where T : AStateMachineOwner<T>
{
    protected AStateMachine<T> _stateMachine;

    public abstract void InitStateMachine();

    protected virtual void Awake()
    {
        InitStateMachine();
        _stateMachine.Init(this as T);
    }

    protected virtual void Update()
    {
        _stateMachine.Execute();
    }
}

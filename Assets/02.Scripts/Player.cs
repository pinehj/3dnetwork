using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStat Stat;
    public Animator Animator;

    private void Awake()
    {
        Stat = GetComponent<PlayerStat>();
        Animator = GetComponentInChildren<Animator>();
    }
}

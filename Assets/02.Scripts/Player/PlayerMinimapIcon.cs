using UnityEngine;

public class PlayerMinimapIcon : PlayerAbility
{
    [SerializeField] private SpriteRenderer _minimapIcon;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        if (!_owner.PhotonView.IsMine)
        {
            _minimapIcon.color = Color.red;
        }
    }
}

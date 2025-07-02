using Photon.Pun;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }
}

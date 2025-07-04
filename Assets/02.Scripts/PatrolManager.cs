using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PatrolManager : Singleton<PatrolManager>
{
    [SerializeField] private List<Transform> _patrolPointList;
    
    public Transform GetRandomPatrolPoint()
    {
        int randomIndex = Random.Range(0, _patrolPointList.Count);
        return _patrolPointList[randomIndex];
    }
}

using System.Collections;
using UnityEngine;

public class PlayerShake : PlayerAbility
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _strength;
    [SerializeField] private float _duration;

    public IEnumerator ShakeRoutine()
    {
        float elapsedTime = 0f;

        Vector3 startPosition = _target.localPosition;


        Debug.Log("sdfsdf");
        while (elapsedTime <= _duration)
        {
            elapsedTime += Time.deltaTime;
            Vector3 randomPosition = Random.insideUnitSphere * _strength;

            _target.localPosition = randomPosition;
            yield return null;
        }

        _target.localPosition = startPosition;
    }
}

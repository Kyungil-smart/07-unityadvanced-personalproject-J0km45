using UnityEngine;
using System;

public class Target : MonoBehaviour, IHittable
{
    public Transform SpawnPoint { get; private set; }
    public event Action<Target> OnDestroyed;

    public void Init(Transform spawnPoint)
    {
        SpawnPoint = spawnPoint;
    }

    public void OnHit()
    {
        // TODO: 점수 추가
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
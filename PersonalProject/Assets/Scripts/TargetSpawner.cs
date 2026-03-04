using UnityEngine;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints; // 스폰 위치
    [SerializeField] private GameObject _targetPrefab; // 타겟
    [SerializeField] private int _maxTargetCount = 10; // 최대 타겟 수
    [SerializeField] private int _targetCount; // 생성된 타겟 수
    [SerializeField] private GamePresenter _presenter;

    private void Start()
    {
        SpawnTarget();
    }

    void SpawnTarget()
    {
        if (_targetPrefab == null) return;

        while (_targetCount < _maxTargetCount)
        {
            int rand = Random.Range(0, _spawnPoints.Count);

            Transform spawnPoint = _spawnPoints[rand];

            GameObject t = Instantiate(_targetPrefab, spawnPoint.position, Quaternion.identity);
            if (t.TryGetComponent(out Target target))
            {
                _spawnPoints.RemoveAt(rand);
                target.Init(spawnPoint);
                target.OnDestroyed += NotifyDestroy;
                _targetCount++;
            }
        }
    }

    public void NotifyDestroy(Target target)
    {
        target.OnDestroyed -= NotifyDestroy;
        _presenter.AddScore();
        _spawnPoints.Add(target.SpawnPoint);
        _targetCount--;
        SpawnTarget();
    }
}
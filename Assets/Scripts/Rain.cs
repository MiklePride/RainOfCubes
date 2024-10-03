using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private List<Platform> _platforms = new List<Platform>();
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _repeatRate = 1f;

    private Coroutine _coroutine;
    private Queue<Cube> _cubes = new Queue<Cube>();

    private void OnEnable()
    {
        foreach (var platform in _platforms)
        {
            platform.CollisionEntered += OnPutInQueueForReleaseToPool;
        }
    }

    private void Start()
    {
        _coroutine = StartCoroutine(StartRain());
    }

    private void Update()
    {
        if (_cubes.Count > 0)
            TryReleaseToPool();
    }

    private void OnDestroy()
    {
        foreach (var platform in _platforms)
        {
            platform.CollisionEntered -= OnPutInQueueForReleaseToPool;
        }
    }

    private IEnumerator StartRain()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            _spawner.Spawn();

            yield return wait;
        }
    }

    private void OnPutInQueueForReleaseToPool(Cube cube)
    {
        if (!_cubes.Contains(cube))
            _cubes.Enqueue(cube);
    }

    private void TryReleaseToPool()
    {
        Cube cube = _cubes.Peek();

        if (cube.IsTimeOver)
            _spawner.ReturnToPool(_cubes.Dequeue());
    }
}
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    private const float Altitude = 30f;

    [SerializeField] private Cube _prefab;

    private ObjectPool<Cube> _poolOfCubes;
    private int _poolCapacity = 20;
    private int _poolMaxSize = 20;

    private void Awake()
    {
        _poolOfCubes = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet:  (cube) => OnGetCube(cube),
            actionOnRelease: (cube) => OnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnGetCube(Cube cube)
    {
        float minRandomValue = -15;
        float maxRandomValue = 15;
        cube.transform.position = new Vector3(Random.Range(minRandomValue, maxRandomValue + 1), Altitude, Random.Range(minRandomValue, maxRandomValue + 1));

        cube.gameObject.SetActive(true);
        cube.CollisionEntered += OnReturnToPool;
    }

    private void OnRelease(Cube cube)
    {
        cube.CollisionEntered -= OnReturnToPool;
        cube.gameObject.SetActive(false);
    }

    private void OnReturnToPool(Cube cube)
    {
        _poolOfCubes.Release(cube);
    }

    public void Spawn()
    {
        _poolOfCubes.Get();
    }
}

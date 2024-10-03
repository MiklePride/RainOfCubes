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
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnGetCube(Cube cube)
    {
        float minRandomValue = -15;
        float maxRandomValue = 16;

        cube.transform.position = new Vector3(Random.Range(minRandomValue, maxRandomValue), Altitude, Random.Range(minRandomValue, maxRandomValue));
        cube.gameObject.SetActive(true);
    }

    public void Spawn()
    {
        _poolOfCubes.Get();
    }

    public void ReturnToPool(Cube cube)
    {
        _poolOfCubes.Release(cube);
    }
}

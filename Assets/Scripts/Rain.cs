using System.Collections;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _repeatRate = 1f;

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(StartRain());
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
}
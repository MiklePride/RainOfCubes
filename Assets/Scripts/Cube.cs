using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _defaultColor;
    private float _lifeTime;
    private bool _hasBeenCollision;

    public event UnityAction<Cube> CollisionEntered;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
    }

    private void OnEnable()
    {
        _hasBeenCollision = false;
    }

    private void OnDisable()
    {
        _meshRenderer.material.color = _defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
            Die();
    }

    private void Die()
    {
        if (_hasBeenCollision)
            return;

        SetRandomColor();
        StartRandomLifeTime();

        _hasBeenCollision = true;
    }

    private void SetRandomColor()
    {
        float minValue = 0f;
        float maxValue = 1f;
        _meshRenderer.material.color = new Color(Random.Range(minValue, maxValue), Random.Range(minValue, maxValue), Random.Range(minValue, maxValue));
    }

    private void StartRandomLifeTime()
    {
        int minLifeTime = 2;
        int maxLifeTime = 5;
        _lifeTime = Random.Range(minLifeTime, maxLifeTime + 1);

        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(_lifeTime);

        _lifeTime = 0;
        CollisionEntered?.Invoke(this);
    }
}

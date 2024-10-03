using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Color _defaultColor;
    private float _lifeTime;
    private bool _hasBeenCollision;

    public bool HasBeenCollision => _hasBeenCollision;
    public bool IsTimeOver => _lifeTime <= 0;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
    }

    private void OnEnable()
    {
        _hasBeenCollision = false;
    }

    private void Update()
    {
        if (_lifeTime > 0)
            _lifeTime -= Time.deltaTime;
    }

    private void OnDisable()
    {
        _meshRenderer.material.color = _defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            SetRandomColor();
            SetRandomLifeTime();

            _hasBeenCollision = true;
        }
    }

    private void SetRandomColor()
    {
        if (_hasBeenCollision)
            return;

        float minValue = 0f;
        float maxValue = 1f;

        _meshRenderer.material.color = new Color(Random.Range(minValue, maxValue), Random.Range(minValue, maxValue), Random.Range(minValue, maxValue));
    }

    private void SetRandomLifeTime()
    {
        if (_hasBeenCollision)
            return;

        int minLifeTime = 2;
        int maxLifeTime = 6;

        _lifeTime = Random.Range(minLifeTime, maxLifeTime);
    }
}

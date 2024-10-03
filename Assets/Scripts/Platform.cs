using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public event Action<Cube> CollisionEntered;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Cube cube))
        {
            CollisionEntered?.Invoke(cube);
        }
    }
}

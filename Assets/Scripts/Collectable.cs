using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        transform.Rotate(transform.up, _rotationSpeed * Time.deltaTime);
    }
}

using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _height = 1f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _startDelay;

    private float _startY;

    private void Start()
    {
        _startY = transform.position.y;

        StartCoroutine(MoveTrap());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable target))
            target.TakeDamage(_damage);
    }

    private IEnumerator MoveTrap()
    {
        yield return new WaitForSeconds(_startDelay);

        while (true)
        {
            float newY = _startY + Mathf.Sin(Time.time * _speed - _startDelay * _speed) * _height;
            transform.position = new Vector3(
                transform.position.x,
                newY,
                transform.position.z
            );
            yield return null;
        }
    }
}

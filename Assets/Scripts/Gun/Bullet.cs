using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5f;

    private Rigidbody _rb;
    private Coroutine _destroyCoroutine;

    public void Init(float bulletSpeed)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);
        _destroyCoroutine = StartCoroutine(DestroyAwait());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Target hit!");
        StopCoroutine(_destroyCoroutine);
        Destroy(gameObject);
    }

    private IEnumerator DestroyAwait()
    {
        yield return new WaitForSeconds(_lifeTime);
        Debug.Log("Missed!!!");
        Destroy(gameObject);
    }
}

using Cysharp.Threading.Tasks;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy = 1f;
    [SerializeField] private float _speed = 500f;

    public void Awake()
    {
        StartDestroy().Forget();
    }

    public void Update()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * _speed;
    }

    private async UniTask StartDestroy()
    {
        await UniTask.WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }
}

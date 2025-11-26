using UnityEngine;


public class Coin : MonoBehaviour
{
    [SerializeField] private int _score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventBus.Instance.TriggerEvent(new ItemPickedEvent(_score, $"{nameof(Coin)} was picked!!!"));
        Destroy(gameObject);
    }
}

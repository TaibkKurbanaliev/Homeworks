using System;
using TMPro;
using UnityEngine;

public class ItemsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _healsNumber;
    [SerializeField] private TMP_Text _granadesNumber;
    [SerializeField] private TMP_Text _bullets;

    private EventBinding<ItemCountChangedEvent> _itemCountChangedBinding;
    private EventBinding<BulletsChangedEvent> _bulletChangedBinding;

    private void Awake()
    {
        _itemCountChangedBinding = new EventBinding<ItemCountChangedEvent>(OnItemCountChanged);
        _bulletChangedBinding = new EventBinding<BulletsChangedEvent>(OnBulletChanged);
        EventBus<ItemCountChangedEvent>.Register(_itemCountChangedBinding);
        EventBus<BulletsChangedEvent>.Register(_bulletChangedBinding);
    }

    private void OnDestroy()
    {
        EventBus<BulletsChangedEvent>.Deregister(_bulletChangedBinding);
        EventBus<ItemCountChangedEvent>.Deregister(_itemCountChangedBinding);
    }

    private void OnBulletChanged(BulletsChangedEvent @event)
    {
        _bullets.text = @event.Count.ToString();
    }

    private void OnItemCountChanged(ItemCountChangedEvent @event)
    {
        switch (@event.Item)
        {
            case ItemType.Heal:
                _healsNumber.text = @event.Count.ToString();
                break;
            case ItemType.Grenade:
                _granadesNumber.text = @event.Count.ToString();
                break;
            default:
                throw new NotImplementedException();
        }
    }
}

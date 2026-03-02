using System;
using TMPro;
using UnityEngine;

public class ItemsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _healsNumber;
    [SerializeField] private TMP_Text _granadesNumber;

    private EventBinding<ItemCountChangedEvent> _itemCountChangedBinding;

    private void Awake()
    {
        _itemCountChangedBinding = new EventBinding<ItemCountChangedEvent>(OnItemCountChanged);
        EventBus<ItemCountChangedEvent>.Register(_itemCountChangedBinding);
    }

    private void OnDestroy()
    {
        EventBus<ItemCountChangedEvent>.Deregister(_itemCountChangedBinding);
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

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _logMessagePrefab;
    [SerializeField] private GameObject _logger;
    [SerializeField] private Button _showAnalytics;

    private EventManager _eventManager;
    private List<TMP_Text> _logMessages = new();

    public void Init(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void OnEnable()
    {
        _showAnalytics.onClick.AddListener(OnShowAnalyticsClicked);
        _eventManager.OnGameEvent += HandleGameEvent;
    }

    private void OnDisable()
    {
        _showAnalytics.onClick.RemoveListener(OnShowAnalyticsClicked);
        _eventManager.OnGameEvent -= HandleGameEvent;
    }

    public void OnShowAnalyticsClicked()
    {
    }

    private void HandleGameEvent(GameEvent e)
    {
        var message = $"[{e.Time}] LOG {e.Type}: {e.Desctiption}";
        var instance = Instantiate(_logMessagePrefab, _logger.transform);
        instance.text = message;
        _logMessages.Add(instance);
    }
}

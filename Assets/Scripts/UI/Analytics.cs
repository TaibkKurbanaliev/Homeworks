using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Analytics : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private GameObject _container;
    [SerializeField] private TMP_Text _logMessagePrefab;
    [SerializeField] private TMP_Text _numberOfPickedEvents;
    [SerializeField] private TMP_Text _mostFrequentEvent;
    [SerializeField] private Transform _battleStartEventsContrainer;
    [SerializeField] private Transform _lastFiveEventsEventsContrainer;
    [Header("Buttons")]
    [SerializeField] private Button _exit;
    [SerializeField] private Button _show;

    private EventManager _eventManager;

    private List<TMP_Text> _battleStartPrefabs = new();
    private List<TMP_Text> _lastFiveEventsPrefabs = new();


    public void Init(EventManager eventManager)
    {
        _eventManager = eventManager;
    }

    private void OnEnable()
    {
        _show.onClick.AddListener(ShowAnalytics);
        _exit.onClick.AddListener(Hide);
    }


    private void OnDisable()
    {
        _show.onClick.RemoveListener(ShowAnalytics);
        _exit.onClick.RemoveListener(Hide);
    }

    private void ShowAnalytics()
    {
        _container.SetActive(true);
        _show.gameObject.SetActive(false);

        _mostFrequentEvent.text = _eventManager.ShowMostFrequentEvent() == null ? 
                                                                                "None" : 
                                                                                _eventManager.ShowMostFrequentEvent().ToString();

        _numberOfPickedEvents.text = _eventManager.CountPlayerItemPicks().ToString();
        
        foreach (var e in _eventManager.FindAllBattleStartEvents())
        {
            var message = Instantiate(_logMessagePrefab, _battleStartEventsContrainer);
            message.text = $"[{e.Time}] LOG {e.Type}: {e.Desctiption}";
            _battleStartPrefabs.Add(message);
        }

        foreach (var e in _eventManager.LastFiveEventsPeriodTime())
        {
            var message = Instantiate(_logMessagePrefab, _lastFiveEventsEventsContrainer);
            message.text = $"[{e.Time}] LOG {e.Type}: {e.Desctiption}";
            _lastFiveEventsPrefabs.Add(message);
        }

        Time.timeScale = 0f;
    }

    private void Hide()
    {
        _container.SetActive(false);
        _show.gameObject.SetActive(true);

        foreach (var item in _battleStartPrefabs)
            Destroy(item.gameObject);

        foreach (var item in _lastFiveEventsPrefabs)
            Destroy(item.gameObject);

        _lastFiveEventsPrefabs.Clear();
        _battleStartPrefabs.Clear();
        Time.timeScale = 1f;
    }
}

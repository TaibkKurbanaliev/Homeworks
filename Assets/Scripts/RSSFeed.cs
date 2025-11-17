using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RSSFeed : MonoBehaviour
{
    [Header("Feed View")]
    [SerializeField] private NewsItemView _prefab;
    [SerializeField] private TMP_Text _errorMessage;
    [SerializeField] private GameObject _parent;
    [SerializeField] private Image _loadingImage;
    [Header("Feed settings")]
    [SerializeField] private RSSFeedConfig _config;

    [Header("Control Buttons")]
    [SerializeField] private Button _showAll;
    [SerializeField] private Button _reload;
    

    private NewsLoader _newsLoader;
    private List<NewsItem> _newsItems;
    private List<NewsItemView> _newsItemViews = new();
    private Coroutine _showNewsCoroutine;

    private async void Start()
    {
        _newsLoader = new(_config.FileName, _config.FirstLoadDelayInMiliseconds);
        _loadingImage.gameObject.SetActive(true);

        try
        {
            _newsItems = await _newsLoader.LoadNewsAsync();
            _newsItems = _newsItems.OrderBy(news => news.Timestamp).ToList();
            _showNewsCoroutine = StartCoroutine(ShowNewsCoroutine());
            _loadingImage.gameObject.SetActive(false);
        }
        catch (Exception e)
        {
            _errorMessage.gameObject.SetActive(true);
            _errorMessage.text = e.Message;
        }
    }

    private void OnEnable()
    {
        _showAll.onClick.AddListener(OnShowAllNews);
        _reload.onClick.AddListener(OnReload);
    }

    private void OnDisable()
    {
        _showAll.onClick.RemoveListener(OnShowAllNews);
        _reload.onClick.RemoveListener(OnReload);
    }

    public IEnumerator ShowNewsCoroutine()
    {
        foreach (var item in _newsItems)
        {
            var news = Instantiate(_prefab, _parent.transform);
            news.Init(item);
            news.gameObject.SetActive(false);
            _newsItemViews.Add(news);
            yield return new WaitForSeconds(_config.TimeBetweenNewsInstantiate);
        }
    }

    private void OnShowAllNews()
    {
        if (_newsItems == null || _newsItems.Count == 0)
            return;

        if (_showNewsCoroutine != null)
            StopCoroutine(_showNewsCoroutine);

        foreach (var itemView in _newsItemViews)
            Destroy(itemView.gameObject);

        _newsItemViews.Clear();

        foreach (var item in _newsItems)
        {
            var news = Instantiate(_prefab, _parent.transform);
            news.Init(item);
            _newsItemViews.Add(news);
        }
    }

    private void OnReload()
    {
        if (_newsItems == null || _newsItems.Count == 0)
            return;

        foreach (var itemView in _newsItemViews.Where(item => item.gameObject.activeSelf == false))
            itemView.gameObject.SetActive(true);
    }
}

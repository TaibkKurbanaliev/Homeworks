using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _content;
    [SerializeField] private TMP_Text _date;


    public void Init(NewsItem item)
    {
        _title.text = item.Title ?? "Без загаловка";
        _content.text = item.Content ?? "Контент не указан";
        _date.text = item.Timestamp.ToString();
    }
}

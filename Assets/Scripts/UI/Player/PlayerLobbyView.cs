using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLobbyView : MonoBehaviour
{
    [SerializeField] private Image _backgoundColor;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _ready;
    [SerializeField] private TMP_Text _hostOrPlayer;

    private InstanceInfo _info;

    public InstanceInfo Info => _info;

    public void Init(InstanceInfo info)
    {
        _info = info;
        _info.NameChanged += OnNameChanged;
        _info.ReadyChanged += OnReadyChanged;
        _info.ColorChanged += OnColorChanged;

        OnNameChanged(info.Name);
        OnReadyChanged(info.IsReady);
        OnColorChanged(info.Color);

        _hostOrPlayer.text = info.IsLeader ? "Host" : "Player";
    }

    private void OnColorChanged(Color color)
    {
        _backgoundColor.color = color;
    }

    private void OnReadyChanged(bool isReady)
    {
        _ready.text = isReady ? "Ready" : "NotReady";
    }

    private void OnNameChanged(string name)
    {
        _name.text = name;
    }
}

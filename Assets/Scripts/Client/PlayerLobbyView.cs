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

    public void Init(InstanceInfo info)
    {
        _info = info;
        _info.OnNameChanged += _info_OnNameChanged;
        _info.OnReadyChanged += _info_OnReadyChanged;
        _info.OnColorChanged += _info_OnColorChanged;

        _info_OnNameChanged(info.Name);
        _info_OnReadyChanged(info.IsReady);
        _info_OnColorChanged(info.Color);

        _hostOrPlayer.text = info.IsLeader ? "Host" : "Player";
    }

    private void _info_OnColorChanged(Color color)
    {
        color.a = 0.1f;
        _backgoundColor.color = color;
    }

    private void _info_OnReadyChanged(bool isReady)
    {
        _ready.text = isReady ? "Ready" : "NotReady";
    }

    private void _info_OnNameChanged(string name)
    {
        _name.text = name;
    }
}

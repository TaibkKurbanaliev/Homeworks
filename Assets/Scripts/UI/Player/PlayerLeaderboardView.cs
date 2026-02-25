using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLeaderboardView : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _kills;
    [SerializeField] private TMP_Text _deaths;

    private PlayerInfo _playerInfo;
    private InstanceInfo _instanceInfo;

    public void Init(PlayerInfo playerInfo, InstanceInfo instanceInfo)
    {
        _playerInfo = playerInfo;
        _instanceInfo = instanceInfo;

        _kills.text = playerInfo.NumberOfKills.ToString();
        _deaths.text = playerInfo.NumberOfDeaths.ToString();
        _background.color = instanceInfo.Color;
        _name.text = instanceInfo.Name;

        _instanceInfo.NameChanged += OnNameChange;
        _instanceInfo.ColorChanged += OnColorChanged;
        _playerInfo.NumberOfKillsChanged += OnNumberOfKillsChanged;
        _playerInfo.NumberOfDeathsChanged += OnNumberOfDeathChanged;
    }

    private void OnDestroy()
    {
        _instanceInfo.NameChanged -= OnNameChange;
        _instanceInfo.ColorChanged -= OnColorChanged;
        _playerInfo.NumberOfKillsChanged -= OnNumberOfKillsChanged;
        _playerInfo.NumberOfDeathsChanged -= OnNumberOfDeathChanged;
    }

    private void OnNumberOfKillsChanged(int kills)
    {
        _kills.text = kills.ToString();
    }

    private void OnNumberOfDeathChanged(int deaths)
    {
        _deaths.text = deaths.ToString();
    }

    private void OnColorChanged(Color color)
    {
        _background.color = color;
    }

    private void OnNameChange(string name)
    {
        _name.text = name;
    }
}

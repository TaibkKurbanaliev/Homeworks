using Mirror;
using System.Text;
using TMPro;
using UnityEngine;

public class Timer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _time;

    [SyncVar(hook = nameof(OnTimeChanged))] private int _timeInSeconds;

    [Server]
    public void SetTime(int timeInSeconds)
    {
        Debug.Log("Timer");
        _timeInSeconds = timeInSeconds;
    }

    private void OnTimeChanged(int prev, int next)
    {
        var mm = next / 60;
        var ss = next % 60;
        
        _time.text = (mm > 9 ? mm : "0" + mm.ToString()) + ":" + (ss > 9 ? ss.ToString() : "0" + ss.ToString()) ;
    }
}

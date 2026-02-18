using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ButtonAnimation : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioSource _source;

    private void OnValidate()
    {
        if (_source == null)
            _source = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        _source.PlayOneShot(_clickSound);
    }
}

using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModifierView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    public ModifierType Type { get; private set; }

    public void Init(ModifierType type)
    {
        _name.text = type.ToString();
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "RSSFeedConfig", menuName = "Scriptable Objects/RSSFeedConfig")]
public class RSSFeedConfig : ScriptableObject
{
    [field: SerializeField] public string FileName;
    [field: SerializeField] public int FirstLoadDelayInMiliseconds;
    [field: SerializeField] public float TimeBetweenNewsInstantiate;
}

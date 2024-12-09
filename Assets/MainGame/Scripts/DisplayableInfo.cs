using UnityEngine;

[CreateAssetMenu(fileName = "Info", menuName = "DisplayableInfo")]
public class DisplayableInfo : ScriptableObject
{
    public Sprite icon;
    public string title;
    [TextArea]
    public string description;
}

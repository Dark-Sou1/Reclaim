using Giacomo;
using UnityEngine;

public class DisplayInfoOnHover : Interactable2D
{
    public Sprite icon;
    public string title;
    [TextArea]
    public string description;

    protected override void OnCursorEnter()
    {
        DisplayInfoUI.Instance.Show(this, icon, title, description);
    }

    protected override void OnCursorExit()
    {
        DisplayInfoUI.Instance.Hide(this);
    }
}

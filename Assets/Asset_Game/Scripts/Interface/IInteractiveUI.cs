using UnityEngine;

public interface IInteractiveUI
{
    void OnDrag(Vector2 position);
    void EndDrag(Vector2 position);
    void Click(object o);
    void ClickUp(object o);
}
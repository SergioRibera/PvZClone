using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerUpHandler
{
    public bool isInteractable = true;
    IInteractiveUI interactive;
    object ob;
    public void Init(IInteractiveUI _i, object _o = null)
    {
        interactive = _i;
        ob = _o;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isInteractable)
            interactive?.OnDrag(eventData.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (isInteractable)
            interactive?.OnDrag(eventData.position);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractable)
            interactive?.Click(ob);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInteractable)
            interactive?.ClickUp(ob);
    }
}
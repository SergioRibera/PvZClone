using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Color hightPointer;
    public Color lowPointer;
    public PlayerSettings thisPlanta;
    public event OnMouseHover onMouseHover;
    public event OnMouseHover onMouseUp;
    public event OnMouseClick onMouseClick;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = lowPointer;
    }
    void OnMouseDown()
    {
        if (transform.childCount == 0)
            onMouseClick?.Invoke(this);
    }
    private void OnMouseEnter()
    {
        if (transform.childCount == 0)
        {
            GetComponent<SpriteRenderer>().color = hightPointer;
            onMouseHover?.Invoke(this);
        }
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = lowPointer;
    }
    private void OnMouseUp()
    {
        if (transform.childCount == 0)
        {
            GetComponent<SpriteRenderer>().color = lowPointer;
            onMouseUp?.Invoke(this);
        }
    }
}
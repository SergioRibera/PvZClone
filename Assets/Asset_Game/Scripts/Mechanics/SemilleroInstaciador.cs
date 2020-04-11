using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SemilleroInstaciador : MonoBehaviour, IInteractiveUI
{
    public bool Selected;
    public PlayerSettings settings;
    public Cell[] cs;
    public GameObject preview;

    float mZCoordinate;
    Cell currentCell;
    public void Init(PlayerSettings _s)
    {
        settings = _s;
        cs = FindObjectsOfType<Cell>();
        foreach (var c in cs)
        {
            c.onMouseHover += CellHover;
            c.onMouseClick += CellClick;
            c.onMouseUp += CellUp;
        }
        mZCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
    }
    void CellHover(Cell c)
    {
        currentCell = c;
    }
    void CellClick(Cell c)
    {
        InstanciarPlanta(c);
    }
    void CellUp(Cell c)
    {
        InstanciarPlanta(c);
    }
    public void OnDrag(Vector2 position)
    {
        Selected = true;
        if(preview == null)
        {
            preview = Instantiate(new GameObject());
            preview.AddComponent<SpriteRenderer>().sprite = settings.previewSpan;
            preview.transform.position = GetMouseWorldPosition();
        }
        else
        {
            preview.transform.position = GetMouseWorldPosition();
        }
    }
    public void EndDrag(Vector2 position)
    {
        Destroy(preview);
        preview = null;
        /*          Debería instanciar but not found
        if (currentCell != null)
        {
            if (currentCell.transform.childCount == 0)
            {
                if (Selected)
                {
                    GameObject g = Instantiate(settings.prefab);
                    g.transform.SetParent(currentCell.transform);
                    g.transform.localPosition = Vector3.zero;
                    Selected = false;
                }
            }
        }*/
    }
    public void Click(object o)
    {
        Selected = !Selected;
    }
    public void ClickUp(object o)
    {
        Destroy(preview);
        preview = null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        // pixel coordinate (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoordinate;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void InstanciarPlanta(Cell c)
    {
        if (c.transform.childCount == 0)
        {
            if (Selected)
            {
                GameObject g = Instantiate(settings.prefab);
                g.transform.SetParent(c.transform);
                g.transform.localPosition = Vector3.zero;

                var x = int.Parse(c.name.Split('|')[0].Split(':')[1], CultureInfo.InvariantCulture);
                float xc = 1f;

                if(x == 1)
                    xc = 11;
                if (x == 2)
                    xc = 10;
                if (x == 3)
                    xc = 8.3f;
                if (x == 4)
                    xc = 6.7f;
                if (x == 5)
                    xc = 5.5f;
                if (x == 6)
                    xc = 4;
                if (x == 7)
                    xc = 2.5f;
                if (x == 8)
                    xc = 1.2f;

                PlayerSettings ps = new PlayerSettings(settings);
                ps.maxDistance = xc;
                g.GetComponent<PlayerManager>().Init(ps);
                Selected = false;
            }
        }
    }
}
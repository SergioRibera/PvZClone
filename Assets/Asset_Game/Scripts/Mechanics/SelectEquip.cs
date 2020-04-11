using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEquip : MonoBehaviour, IInteractiveUI
{
    public PlayerScriptableObject plantasDB;
    public GameObject[] selectPlantas;
    public Transform showUnlocks;
    public Transform baraja;
    public List<PlayerSettings> selects = new List<PlayerSettings>();
    List<PlayerSettings> ps = new List<PlayerSettings>();

    public static SelectEquip Singleton;
    void Start()
    {
        if (Singleton == null)
            Singleton = this;
        ps = plantasDB.FindPlantsUnlocked();
        for (int i = 0; i < ps.Count; i++)
        {
            GameObject g = Instantiate(new GameObject(ps[i].id + ""));
            g.AddComponent<UnityEngine.UI.Image>().sprite = ps[i].target;
            ps[i].myIndex = i;
            g.AddComponent<DragUI>().Init(this, ps[i]);
            g.transform.SetParent(showUnlocks);
            g.transform.localScale = Vector3.one;
        }
    }
    public void Click(object o)
    {
        PlayerSettings p = (PlayerSettings)o;
        if (p.addToSelect)
        {
            if (selects.Count < 7)
            {
                showUnlocks.Find(p.id + "(Clone)").GetComponent<UnityEngine.UI.Image>().color = new Color(255,255,255,.5f);
                showUnlocks.Find(p.id + "(Clone)").GetComponent<DragUI>().isInteractable = false;
                
                p.addToSelect = false;
                selects.Add(p);
                selectPlantas[p.myIndex].GetComponent<UnityEngine.UI.Image>().sprite = p.target;
                selectPlantas[p.myIndex].GetComponent<UnityEngine.UI.Image>().color = Color.white;
                selectPlantas[p.myIndex].GetComponent<DragUI>().isInteractable = true;
                selectPlantas[p.myIndex].GetComponent<DragUI>().Init(this, p);

                GameObject t = Instantiate(new GameObject(p.id + ""));
                t.AddComponent<UnityEngine.UI.Image>().sprite = p.target;
                SemilleroInstaciador s = t.AddComponent<SemilleroInstaciador>();
                t.AddComponent<DragUI>().Init(s, p);
                s.Init(p);
                t.transform.SetParent(baraja);
                t.transform.localScale = Vector3.one;
            }
        }
        else if(!p.addToSelect)
        {
            selectPlantas[p.myIndex].GetComponent<UnityEngine.UI.Image>().sprite = null;
            selectPlantas[p.myIndex].GetComponent<UnityEngine.UI.Image>().color = new Color(255, 255, 255, .3f);
            selectPlantas[p.myIndex].GetComponent<DragUI>().isInteractable = false;

            Destroy(baraja.Find(p.id + "(Clone)").gameObject);

            showUnlocks.Find(p.id + "(Clone)").GetComponent<UnityEngine.UI.Image>().color = Color.white;
            showUnlocks.Find(p.id + "(Clone)").GetComponent<DragUI>().isInteractable = true;
            selects.Remove(p);
            p.addToSelect = true;
        }
    }

    public void ResetPlants()
    {
        foreach (var p in ps)
        {
            p.addToSelect = true;
        }
    }

    public void EndDrag(Vector2 position)
    {

    }
    public void OnDrag(Vector2 position)
    {

    }
    public void ClickUp(object o)
    {

    }
}
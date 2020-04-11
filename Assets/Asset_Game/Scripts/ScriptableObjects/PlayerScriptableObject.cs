using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plantas DataBase", fileName = "PlantasDataBase")]
public class PlayerScriptableObject : ScriptableObject
{
    public List<PlayerSettings> Plantas = new List<PlayerSettings>();

    public List<PlayerSettings> FindPlantsUnlocked()
    {
        List<PlayerSettings> u = new List<PlayerSettings>();
        foreach (var e in Plantas)
        {
            if (e.unlock)
            {
                PlayerSettings p = new PlayerSettings();
                p.id = e.id;
                p.life = e.life;
                p.type = e.type;
                p.animations = e.animations;
                p.originShoot = e.originShoot;
                p.maxDistance = e.maxDistance;
                p.attackType = e.attackType;
                p.shootCadencia = e.shootCadencia;
                p.maskEnemy = e.maskEnemy;
                p.prefab = e.prefab;
                p.prefabBullet = e.prefabBullet;
                p.previewSpan = e.previewSpan;
                p.target = e.target;
                p.unlock = e.unlock;
                p.addToSelect = e.addToSelect;
                p.myIndex = e.myIndex;
                u.Add(p);
            }
        }
        return u;
    }
    public PlayerSettings FindPlant(int id)
    {
        foreach (var e in Plantas)
        {
            if (e.id == id)
                return e;
        }
        return null;
    }
}
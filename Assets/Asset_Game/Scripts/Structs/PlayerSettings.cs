using System;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    public int id;
    public int life;
    public PlayerType type;
    public RuntimeAnimatorController animations;
    public Vector3 originShoot;
    [Range(0, 11)]
    public float maxDistance;
    public AttackPlayerType attackType;
    public float shootCadencia;
    public LayerMask maskEnemy;
    public GameObject prefab;
    public GameObject prefabBullet;
    public Sprite previewSpan;
    public Sprite target;
    public bool unlock;
    public bool addToSelect;
    public int myIndex;
    public PlayerSettings()
    {

    }
    public PlayerSettings(PlayerSettings e)
    {
        id = e.id;
        life = e.life;
        type = e.type;
        animations = e.animations;
        originShoot = e.originShoot;
        maxDistance = e.maxDistance;
        attackType = e.attackType;
        shootCadencia = e.shootCadencia;
        maskEnemy = e.maskEnemy;
        prefab = e.prefab;
        prefabBullet = e.prefabBullet;
        previewSpan = e.previewSpan;
        target = e.target;
        unlock = e.unlock;
        addToSelect = e.addToSelect;
        myIndex = e.myIndex;
    }
}
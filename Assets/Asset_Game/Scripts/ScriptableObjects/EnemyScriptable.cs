using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy DataBase", fileName = "EnemyDataBase")]
public class EnemyScriptable : ScriptableObject
{
    public List<EnemySettings> Enemies = new List<EnemySettings>();

    public EnemySettings FindEnemyPerType(EnemyType _t)
    {
        foreach (var e in Enemies)
        {
            if (e.type == _t)
            {
                EnemySettings es = new EnemySettings();
                es.type = e.type;
                es.life = e.life;
                es.velocity = e.velocity;
                es.damage = e.damage;
                es.attacVelocity = e.attacVelocity;
                es.attackCadencia = e.attackCadencia;
                es.animations = e.animations;
                return es;
            }
        }
        return null;
    }
}
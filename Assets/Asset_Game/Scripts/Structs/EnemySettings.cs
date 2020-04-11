using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySettings
{
    public EnemyType type;
    public int life;
    public float velocity;
    public int damage;
    public float attacVelocity;
    public float attackCadencia;
    public RuntimeAnimatorController animations;
}
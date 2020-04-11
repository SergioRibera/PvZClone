using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int life;
    public EnemySettings settings;
    public bool left = true;
    public bool move;
    public bool moveToCenter;
    public bool attack;

    public bool isDeath { get { return settings.life == 0; } }
    bool isFinalPosition { get { return transform.position == path.endPos; } }
    bool isCenterPosition { get { if (isFinalPosition) { moveToCenter = true; } return transform.position == path.centerPos; } }

    Animator anim;
    Path path;
    float cadAux = 0;
    public void Init(Path _p, EnemySettings _s)
    {
        path = _p;
        settings = _s;
        life = settings.life;
        move = true;
        anim = TryGetComponent(out anim) ? GetComponent<Animator>() : gameObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = settings.animations;
    }
    void Update()
    {
        if (attack)
        {
            cadAux += Time.deltaTime;
            if (cadAux >= settings.attackCadencia)
            {
                cadAux = 0;
                Attack(planta);
            }
        }
        if (!move) return;
        if (!isDeath)
        {
            if (left)
            {
                transform.position = Vector3.MoveTowards(transform.position, GetNextPath(), Time.deltaTime * settings.velocity);
            }
        }
        if (isCenterPosition)
        {
            Anim("a", true);
            move = false;
            moveToCenter = false;
        }
    }
    Vector3 GetNextPath()
    {
        if (!isFinalPosition && !moveToCenter) return path.endPos;
        else
            return path.centerPos;
    }

    PlayerManager planta;
    void Attack(PlayerManager _p)
    {
        if (!attack)
        {
            planta = _p;
            _p.OnDead += () =>
            {
                move = true;
                attack = false;
                Anim("a", false);
            };
            attack = true;
        }
        if (attack)
        {
            Anim("a", true);
            _p.RecibirDano(settings.damage);
        }
    }

    public void RecibirDano(int d)
    {
        if (life > 0) life -= d;
        if (life <= 0) Dead();
    }
    void Dead()
    {
        move = false;
        attack = false;
        tag = "Untagged";
        Destroy(GetComponent<BoxCollider>());
        gameObject.layer = 0;
        Anim("a", false);
        Anim("d");
        Destroy(gameObject, 3f);
    }

    void Anim(string n, bool b)
    {
        anim.SetBool(n, b);
    }
    void Anim(string n, float f)
    {
        anim.SetFloat(n, f);
    }
    void Anim(string n)
    {
        anim.SetTrigger(n);
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player") {
            move = false;
            Attack(col.GetComponent<PlayerManager>());
        }
    }
}
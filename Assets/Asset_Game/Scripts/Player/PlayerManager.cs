using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ICollision
{
    public int life;
    public PlayerSettings settings;
    public event OnDestroyPlayer OnDead;

    Animator anim;
    float cadAux = 0f;
    public void Init(PlayerSettings _s)
    {
        settings = _s;
        life = settings.life;
        anim = TryGetComponent(out anim) ? GetComponent<Animator>() : gameObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = settings.animations;
    }
    private void FixedUpdate()
    {
        if(settings.attackType == AttackPlayerType.Distance)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + settings.originShoot, transform.right, out hit, settings.maxDistance, settings.maskEnemy))
            {
                Debug.DrawRay(transform.position + settings.originShoot, transform.right * settings.maxDistance, Color.white);
                if (!hit.collider.GetComponent<EnemyManager>().isDeath)
                {
                    cadAux += Time.deltaTime;
                    if (cadAux >= settings.shootCadencia)
                    {
                        cadAux = 0;
                        Shoot();
                    }
                }
            }
        }
    }

    void Shoot()
    {
        GameObject g = Instantiate(settings.prefabBullet);
        g.transform.position = transform.position + settings.originShoot;
        g.GetComponent<Bullet>().Init();
        g.transform.SetParent(transform);
    }
    public void RecibirDano(int d)
    {
        if (life > 0) life -= d;
        if (life <= 0) Dead();
    }
    void Dead()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }

    public void CollisionEnter(GameObject col, string tag)
    {
        if(settings.attackType == AttackPlayerType.Melee)
        {

        }
    }

    public void CollisionStay(GameObject col, string tag)
    {

    }

    public void CollisionExit(GameObject col, string tag)
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity;
    public float timeLife;
    public int damage;
    public bool right;
    Rigidbody rb;
    public void Init()
    {
        if (!GetComponent<Rigidbody>())
            rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = new Vector3(right ? velocity : -velocity, 0, 0);
        Destroy(gameObject, timeLife);
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            col.GetComponent<EnemyManager>().RecibirDano(damage);
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podadora : MonoBehaviour, ICollision
{
    public float velocity;
    public Path path;
    private bool move;
    private Animator anim;

    bool isFinalPosition { get { return transform.position == path.startPos; } }
    public void Init(Path _p)
    {
        path = _p;
        GetComponentInChildren<CollisionDetect>().SetListener(this);
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (!move) return;
        if (!isFinalPosition)
            transform.position = Vector3.MoveTowards(transform.position, path.startPos, Time.deltaTime * velocity);
        else if (isFinalPosition) Destroy(gameObject);
    }

    public void CollisionEnter(GameObject col, string tag)
    {
        if (!move) move = true;
        print(tag);
        if(tag == "Enemy")
        {
            col.GetComponent<EnemyManager>().RecibirDano(100000000);
        }
    }
    public void CollisionExit(GameObject col, string tag)
    {

    }
    public void CollisionStay(GameObject col, string tag)
    {

    }
}
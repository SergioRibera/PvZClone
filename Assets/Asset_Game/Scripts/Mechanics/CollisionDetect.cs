using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    public CollisionPosition collisionPosition;
    ICollision collision;

    public void SetListener(ICollision _c, float pos = 1f)
    {
        collision = _c;
        transform.localPosition = Vector3.zero;
        switch (collisionPosition)
        {
            case CollisionPosition.Left:
                transform.localPosition = new Vector3(-pos, 1f, 1f);
                break;
            case CollisionPosition.Right:
                transform.localPosition = new Vector3(pos, 1f, 1f);
                break;
            case CollisionPosition.Top:
                transform.localPosition = Vector3.up;
                break;
            case CollisionPosition.Bottom:
                transform.localPosition = Vector3.down;
                break;
            case CollisionPosition.x:
                transform.localScale = new Vector3(transform.localScale.x * 2, transform.localScale.y, transform.localScale.z);
                break;
            case CollisionPosition.y:
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
                break;
            case CollisionPosition.z:
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * 3);
                break;
        }
    }
    void OnTriggerEnter(Collider collider) => collision.CollisionEnter(collider.gameObject, collider.tag);
    void OnTriggerExit(Collider collider) => collision.CollisionExit(collider.gameObject, collider.tag);
    void OnTriggerStay(Collider collider) => collision.CollisionStay(collider.gameObject, collider.tag);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollision
{
    void CollisionEnter(GameObject col, string tag);
    void CollisionStay(GameObject col, string tag);
    void CollisionExit(GameObject col, string tag);
}
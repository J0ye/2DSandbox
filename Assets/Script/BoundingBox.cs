using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    protected Collider2D coll;
    void Start()
    {
        coll = GetComponent<Collider2D>();
        coll.isTrigger = true;
    }
}

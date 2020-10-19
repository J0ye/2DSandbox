using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider2D coll = GetComponent<Collider2D>();
        Debug.Log(coll);
        Debug.Log(coll.bounds.extents);
    }
}

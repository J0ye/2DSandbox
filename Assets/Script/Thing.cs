using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thing : MonoBehaviour
{
    [Tooltip("The higher the threshold, the more velocity the thing needs to emit the trail.")]
    public float trailThreshold = 5f;
    protected Vector3 startPos;
    protected Rigidbody2D rb;
    protected TrailRenderer trail;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        if(transform.childCount > 0)
        {
            SetTrail();
        }
    }

    void Update()
    {
        if(trail != null)
        {            
            if(rb.velocity.magnitude > trailThreshold)
            {
                trail.emitting = true;
            }else 
            {
                trail.emitting = false;
            }
        }
    }

    public virtual void ResetPosition()
    {
        Destroy(gameObject);
    }

    private void SetTrail()
    {
        foreach(Transform child in transform)
        {
            if(child.GetComponent<TrailRenderer>() != null)
            {
                trail = child.GetComponent<TrailRenderer>();
            }
        }
    }
}

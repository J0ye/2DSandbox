using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour
{  
    public Collider2D bounds;
    public LayerMask layerMask;
    [Range(0.1f, 100f)]
    public float speed = 2f;

    private RaycastHit2D hit;
    public Transform target;

    // Update is called once per frame
    void Update()
    {        
        Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        if(!IsPointInBounds(mousePos) && target != null)
        {
            if(!target.gameObject.CompareTag("Thing")) target = null; // Set target to null if it is not a random thing
        }

        if(Input.GetButton("Fire1"))    // Left Mouse Button Click
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward, Mathf.Infinity, layerMask);
            if(hit)
            {
                if(hit.transform.gameObject.GetComponent<Thing>() != null)
                {
                    if(!hit.transform.gameObject.CompareTag("Thing") && IsPointInBounds(mousePos))
                    {                        
                        target = hit.transform; // Only set target if it is the playball and within the bounds
                    } else if(hit.transform.gameObject.CompareTag("Thing"))
                    {
                        target = hit.transform; // If it is not the playball set it to the target
                    }
                }
            }

            if(target != null)
            {
                Vector3 targetPos = mousePos - target.position;
                        target.GetComponent<Rigidbody2D>().velocity = targetPos * speed;
            }
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            target = null;
        }
        else if(Input.GetButton("Fire2")) // Right Mouse Button Click
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward, Mathf.Infinity, layerMask);
            if(hit)
            {                
                if(hit.transform.gameObject.GetComponent<Thing>() != null)
                {
                    hit.transform.gameObject.GetComponent<Thing>().ResetPosition();
                }
            }
        }
    }

    private bool IsPointInBounds(Vector3 point)
    {
        return bounds.bounds.Contains(point);
    }
}

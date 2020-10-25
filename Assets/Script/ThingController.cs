using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour
{  
    public Collider2D bounds;
    public LayerMask layerMask;
    [Range(0.1f, 100f)]
    public float speed = 2f;
    public bool debug = false;

    protected RaycastHit2D hit;
    protected Transform target;
    protected Vector3 inputPos = Vector3.zero;

    // Update is called once per frame
    protected virtual void Update()
    {    
        // Position of mouse relative to camera    
        inputPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        CheckForValidGrip(inputPos);
        
        #region Grab thing
        if(Input.GetButton("Fire1"))    // Left Mouse Button Click
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward, Mathf.Infinity, layerMask);
            if(hit)
            {
                // register new target, if its an interactable
                if(!target) SetTarget(hit.transform.gameObject);
            }

            PullTarget(); // Pulls target closer if it exists
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            if (debug) Debug.Log("Removed target");
            // Release target on left mouse button up
            target = null;
        }
        #endregion
        #region Delete/Reset Thing
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
        #endregion
    }

    protected void SetTarget(GameObject go)
    {
        if (debug) Debug.Log("Target: " + hit.collider.gameObject.name);
        if (go.GetComponent<Thing>() != null)
        {
            if (debug) Debug.Log("Sucssefull hit");
            if (!go.gameObject.CompareTag("Thing") && IsPointInBounds(inputPos))
            {
                target = hit.transform; // Only set target if it is the playball and within the bounds
            }
            else if (hit.transform.gameObject.CompareTag("Thing"))
            {
                target = hit.transform; // If it is not the playball set it to the target
            }
        }
    }

    protected void PullTarget()
    {
        if (target != null)
        {
            //Pull the target towards the position of input. Force relative to distance.
            Vector3 targetPos = inputPos - target.position;
            target.GetComponent<Rigidbody2D>().velocity = targetPos * speed;
        }
    }

    protected bool CheckForValidGrip(Vector3 pos)
    {
        if (!IsPointInBounds(pos) && target != null)
        {
            if (!target.gameObject.CompareTag("Thing")) target = null; // Set target to null if it is not a random thing
            if (debug) Debug.Log("Removed target via a false grip check");
            return false;
        }

        return true;
    }

    // Checks if the position is within the bounds
    protected bool IsPointInBounds(Vector3 point)
    {
        if(bounds)
        {
            return bounds.bounds.Contains(point);
        } else 
        {
            Debug.LogWarning("No Bounds set for " + gameObject.name);
            return true;
        }
    }
}

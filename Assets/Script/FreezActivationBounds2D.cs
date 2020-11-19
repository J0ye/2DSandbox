using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FreezActivationBounds2D : MonoBehaviour
{
    public string targetTag = "Player";
    public Transform target;

    private Vector3 fixpoint = Vector3.zero;
    private bool freez = false;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(targetTag))
        {
            // Player enters bounds
            fixpoint = target.position;
            Debug.Log(fixpoint + " saved as target position");
            freez = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Player leaves bounds
            fixpoint = Vector3.zero;
            freez = false;
        }
    }

    private void LateUpdate()
    {
        if(freez && target)
        {
            target.position = fixpoint;
            Debug.Log("Freezing " + target.gameObject.name);
        }
    }
}

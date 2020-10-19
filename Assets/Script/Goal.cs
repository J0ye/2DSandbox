using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : Target
{  
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.gameObject.GetComponent<Thing>() != null)
        {
            other.gameObject.GetComponent<Thing>().ResetPosition();
        }
    }
}

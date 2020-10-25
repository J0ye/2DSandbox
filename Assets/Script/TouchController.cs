using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TouchController : ThingController
{
    protected override void Update()
    {
#if UNITY_EDITOR
        // Normal mouse input controls
        base.Update();
#endif
#if UNITY_ANDROID
        // Touch input controls
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                inputPos = new Vector3(Camera.main.ScreenToWorldPoint(touch.position).x,
                            Camera.main.ScreenToWorldPoint(touch.position).y, 0);
                CheckForValidGrip(inputPos);
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector3.forward, Mathf.Infinity, layerMask);
                if(hit)
                {
                    // register new target, if its an interactable
                    SetTarget(hit.transform.gameObject);
                }
                PullTarget();
            }
        }else if (!Application.isEditor)
        {
            target = null;
            if (debug) Debug.Log("Removed target because of Touch Input");
        }
    }
#endif
}

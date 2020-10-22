using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnviroment : MonoBehaviour
{
    public GravityBoundingBox gbb;
    public GameTarget gt;
    void Start()
    {
        if(gbb == null) SetUpGbb();
        if(gt == null && gbb !=null); 
    }

    private void SetUpGbb()
    {
        foreach(Transform child in transform)
        {
            if(child.gameObject.GetComponent<GravityBoundingBox>() != null)
            {
                gbb = child.gameObject.GetComponent<GravityBoundingBox>();
            }
        }
    }

    private void SetUpGt()
    {
        foreach (Transform child in gbb.transform)
        {
            if(child.gameObject.GetComponent<GameTarget>() != null)
            {
                gt = child.gameObject.GetComponent<GameTarget>();
            }
        }
    }
}

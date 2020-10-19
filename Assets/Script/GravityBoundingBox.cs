using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBoundingBox : BoundingBox
{
    [Range(0.001f,3f)]
    public float strength = 0.1f;
    public Collider2D innerBox;
    [Space]
    public List<GameObject> objs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(innerBox == null)
        {
            foreach(Transform child in transform)
            {
                if(child.gameObject.GetComponent<Collider2D>() != null)   innerBox = child.gameObject.GetComponent<Collider2D>();
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in objs)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;                                    // Set gravity to 0
            if(!innerBox.bounds.Contains(obj.transform.position))   // Only pull the playball towards the center if he isnt already in it
            {
                Vector3 dir =  innerBox.transform.position - obj.transform.position; // Direction towards center of bounds
                float distance = Vector2.Distance(innerBox.transform.position, obj.transform.position);
                rb.AddForce((dir*distance)*strength, ForceMode2D.Impulse);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!CheckListFor(other.gameObject) && other.gameObject.GetComponent<PlayBall>() != null)
        {
            objs.Add(other.gameObject);
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(CheckListFor(other.gameObject))
        {
            objs.Remove(other.gameObject);
            other.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
    }


    private bool CheckListFor(GameObject go)
    {
        foreach (GameObject obj in objs)
        {
            if(go == obj)
            {
                return true;
            }
        }

        return false;
    }
}

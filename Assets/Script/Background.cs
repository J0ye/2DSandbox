using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float parallaxEffect = 0f;
    private float height, startpos;
    void Start()
    {
        startpos = transform.position.y;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = Camera.main.transform.position.y * (1 - parallaxEffect);
        float dist = Camera.main.transform.position.y * parallaxEffect;

        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        if (temp > startpos + height) startpos += height;
        else if (temp < startpos - height) startpos -= height;
    }
}

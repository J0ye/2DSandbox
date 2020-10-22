using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingSpawner : MonoBehaviour
{
    public GameObject thingPrefab;

    private GameObject current; 

    void Start()
    {
        Spawn();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == current)
        {
            current.GetComponent<Rigidbody2D>().isKinematic = false;
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject obj = Instantiate(thingPrefab, transform.position, Quaternion.identity);
        current = obj;
        current.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}

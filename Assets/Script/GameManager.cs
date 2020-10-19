using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public PlayBall playball;
    public GameObject wallPrefab;

    private bool startedClimb = false;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance) Destroy(this);
        if(!Instance) Instance = this;

        if(player == null)
        {
            if(GameObject.Find("Player")) player = GameObject.Find("Player");
            if(GameObject.FindWithTag("Player")) player = GameObject.FindWithTag("Player");
        }
    }

    void LateUpdate()
    {
        if(!playball.gameObject.GetComponent<Renderer>().isVisible && startedClimb)
        {
            GameOver();
        }
    }

    public void RaiseLevel()
    {
        float cameraY = Camera.main.transform.position.y + 7; // Height camera will move to
        Camera.main.transform.DOMoveY(cameraY, 0.3f, true);
        Instantiate(wallPrefab, new Vector3(0, cameraY, 0), Quaternion.identity);
        startedClimb = true;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("TestClimb");
    }
}

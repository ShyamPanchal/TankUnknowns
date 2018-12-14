using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public GameObject timer;
    public GameObject direction;
    public GameObject stats;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public bool isSafe = false;

    private GameObject[] hearts;
    private GameObject floor;
    private GameController gameController;
    private int kills = 0;
    

	// Use this for initialization
	void Start () {
        hearts = new GameObject[] { heart1, heart2, heart3, heart4, heart5};
        floor = GameObject.FindWithTag("floor");
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        direction.transform.LookAt(floor.transform);
        timer.GetComponent<Text>().text = gameController.timer;
        stats.GetComponent<Text>().text = "Kills: "+kills+gameController.stats;
    }

    public void DisplayHealth(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                hearts[i].transform.localScale = new Vector3(0, 0, 0);
            }            
        }
    }

    public void KillSecure()
    {
        kills++;
    }
}

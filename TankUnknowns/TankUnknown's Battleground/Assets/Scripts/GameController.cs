using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour
{

    public string timer = "Time Left Before Bombardment:\n3 : 00";
    public string stats = "\nPlayers Left: 0/0";

    private float seconds = 1f;
    private int minutes = 3;
    private bool countdown = true;
    private int maxPlayers = 0;
    private GameObject[] players;
    private int currentPlayers = 0;
    private float delayKill = 2f;
    private bool delayStart = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (countdown)
        {
            seconds -= Time.deltaTime;
            string sec = Mathf.RoundToInt(seconds).ToString();
            if (seconds < 9.5f)
            {
                sec = "0" + sec;
            }
            timer = "Time Left Before Bombardment:\n" + minutes + " : " + sec;
            if (minutes == 0 && seconds <= 0)
            {
                countdown = false;
                delayStart = true;
                GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bombs");
                for (int i = 0; i < bombs.Length; i++)
                {
                    bombs[i].GetComponent<Rigidbody>().useGravity = true;
                }
            }
            if (seconds <= -0.5)
            {
                seconds = 59.49f;
                minutes--;
            }
        }
        players = GameObject.FindGameObjectsWithTag("Player");
        currentPlayers = players.Length;
        if (currentPlayers > maxPlayers)
        {
            maxPlayers = currentPlayers; 
        }
        stats = "\nPlayers Left: "+currentPlayers+"/"+maxPlayers;
        if (delayStart)
        {
            delayKill -= Time.deltaTime;
            if (delayKill <= 0)
            {
                for (int i = 0; i < currentPlayers; i++)
                {
                    if (!players[i].GetComponent<UIController>().isSafe)
                    {
                        players[i].GetComponent<TankHealth>().TakeDamage(5, null);
                    }
                }
               
            }
        }

    }
}

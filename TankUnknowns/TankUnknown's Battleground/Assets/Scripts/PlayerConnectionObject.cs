using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour {

    public GameObject PlayerPrefab;

    public int health = 100;

	// Use this for initialization
	void Start () {
		if(isLocalPlayer == false)
        {
            return;
        }

        CmdSpawnMyUnit();
	}
	
	// Update is called once per frame
	void Update () {
		if(isLocalPlayer == false)
        {
            return;
        }
        // respawn code here
	}

    ///////////////////// COMMANDS
    
    [Command]
    void CmdSpawnMyUnit()
    {
        GameObject go = Instantiate(PlayerPrefab);
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
public class TankHealth : NetworkBehaviour
{
    public const int maxHealth = 5;
    public GameObject explosion;

    [SyncVar(hook ="OnChangeHealth")]
    public int currentHealth = maxHealth;

    public bool destroyOnDeath;

    // public RectTransform healthBar;

    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        if(isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }
    public void TakeDamage(int amount, GameObject source)
    {

        if (!isServer)
        {
            return;
        }
        currentHealth -= amount;
        GetComponent<UIController>().DisplayHealth(currentHealth);
        if (currentHealth <= 0)
        {
            if (source != null)
            {
                source.GetComponent<UIController>().KillSecure();
            }
            if (destroyOnDeath)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                NetworkServer.Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;

                //called on the Server, will be invoked on the clients
                GetComponent<UIController>().DisplayHealth(currentHealth);

                RpcRespawn();

            }
        }
    }
        void OnChangeHealth(int health)
        {
             // Debug.Log("currentHealth: " + currentHealth);
             // healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
        }
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;
        }
    }

}
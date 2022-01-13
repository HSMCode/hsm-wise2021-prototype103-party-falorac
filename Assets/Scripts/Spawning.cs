using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    // define enemy prefab and spawn interval/delay
    public GameObject[] enemy = new GameObject[2];
    public float delayEnemies = 2.0f;
    public float intervalEnemies = 5.0f;

    // define disguise prefabs (4) and spawn interval/delay
    public GameObject[] disguise = new GameObject[4];
    public float delayDisguise = 1.0f;
    public float intervalDisguise = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // invoke spawning of enemies and disguises
        InvokeRepeating("SpawnEnemy", delayEnemies, intervalEnemies);
        InvokeRepeating("SpawnDisguise", delayDisguise, intervalDisguise);
    }

    private void SpawnEnemy() {
        // choose one of 2 random enemies that can spawn
        int randomEnemy = Random.Range(0, enemy.Length);
        GameObject chosenEnemy = enemy[randomEnemy];

        // instanciate spawning enemy (further away from player, offset on z-axis)
        Vector3 offsetZAxis = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 1.5f);
        GameObject instanceEnemy = Instantiate(chosenEnemy, offsetZAxis, Quaternion.identity, gameObject.transform);
    }     
    private void SpawnDisguise() {
        // choose one of 4 random disguises that can spawn
        int randomDisguise = Random.Range(0, disguise.Length);
        GameObject chosenDisguise = disguise[randomDisguise];

        // instanciate spawning disguise
        GameObject instanceDisguise = Instantiate(chosenDisguise, gameObject.transform.position, Quaternion.identity, gameObject.transform);
    }

    public void StopInvoke() {
        Debug.Log("Spawning stopped.");
        CancelInvoke();
    }

    /*
    to do:
    - randomize spawning interval
    */
}

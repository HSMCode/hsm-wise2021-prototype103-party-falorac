using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    // define enemy prefab and spawn interval/delay
    public GameObject enemy;
    public float delayEnemies = 2.0f;
    public float intervalEnemies = 5.0f;

    // define disguise prefab and spawn interval/delay
    public GameObject disguise;
    public float delayDisguise = 1.0f;
    public float intervalDisguise = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", delayEnemies, intervalEnemies);
        InvokeRepeating("SpawnDisguise", delayDisguise, intervalDisguise);
    }

    private void SpawnEnemy() {
        GameObject instance = Instantiate(enemy, gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
    }     
    private void SpawnDisguise() {
        GameObject instance = Instantiate(disguise, gameObject.transform.position, Quaternion.identity, this.gameObject.transform);
    }

    /*
    to do:
    - randomize spawning interval
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 12f;
    public int detectionAbility;
    public GameObject angryPS;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    void OnTriggerEnter (Collider other) {
        if(other.tag == "Player") {
        }
    }

    // Emit one particle when colliding with player (and play attached sound)
    void ShowAnger() {
        // instantiate particle system, so it survives after parent gets destroyed
        GameObject angry = Instantiate(angryPS, gameObject.transform.position, Quaternion.identity, gameObject.transform);

        // emit number of particles
        angry.GetComponent<ParticleSystem>().Play();

        // play audio clip attached to audio source
        AudioSource playSound = angry.GetComponent<AudioSource>();
        playSound.PlayOneShot(playSound.clip);
        
    }
}

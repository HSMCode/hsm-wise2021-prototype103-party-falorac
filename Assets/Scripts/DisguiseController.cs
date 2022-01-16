using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisguiseController : MonoBehaviour
{
    public float speed = 7f;
    public int disguiseQuality;
    public GameObject sparklesPS;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    // Emit x amount of particles when getting picked up
    void OnPickupEmitParticles(int number) {
        // instantiate particle system, so it survives after parent gets destroyed
        GameObject sparkles = Instantiate(sparklesPS, gameObject.transform.position, gameObject.transform.rotation);

        // emit number of particles
        sparkles.GetComponent<ParticleSystem>().Emit(number);

        // play audio clip attached to audio source
        AudioSource playSound = sparkles.GetComponent<AudioSource>();
        playSound.PlayOneShot(playSound.clip);

        // destroy particle system instance
        Destroy(sparkles, 1);
    }

    /*
    to do:
    - slot restraint, only one disguise per slot?
    */
}

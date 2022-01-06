using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfVision : MonoBehaviour
{
    // destroy colliding objects (enemies and disguises) when they are out of vision
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Disguise")) {
            other.gameObject.SetActive(true);
            Destroy(other.gameObject, 0.5f);
        }
    }
}

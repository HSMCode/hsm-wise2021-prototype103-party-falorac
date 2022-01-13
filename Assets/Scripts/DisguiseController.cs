using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisguiseController : MonoBehaviour
{
    public float speed = 15f;
    public int disguiseQuality;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    /*
    to do:
    - slot restraint, only one disguise per slot?
    */
}

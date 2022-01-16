using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public float speed = 10.0f;
    private GameObject endPoint;
    private bool stillPlaying = true;

    void Start()
    {
        endPoint = GameObject.FindWithTag("EndPoint");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, speed * Time.deltaTime);

        if (this.transform.position.x <= endPoint.transform.position.x && stillPlaying) {
            // tell player character to switch to end-routine
            GameObject.FindWithTag("Player").SendMessage("SwitchDanceRoutine");

            // stop panning textures
            GameObject[] panningObjects = GameObject.FindGameObjectsWithTag("PanningTextures");
            for (int i = 0; i < panningObjects.Length; i++) {
                panningObjects[i].SendMessage("StopPanning");
            }
            stillPlaying = false;
        }
    }
}

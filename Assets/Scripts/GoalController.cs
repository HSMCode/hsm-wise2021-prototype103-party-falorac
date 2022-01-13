using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public float speed = 10.0f;
    private GameObject endPoint;
    private float step;
    void Start()
    {
        step = speed * Time.deltaTime;
        endPoint = GameObject.FindWithTag("EndPoint");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint.transform.position, step);
    }
}

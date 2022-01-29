using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.15f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Target Position
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x,
                                                transform.position.y,
                                                target.position.z + offset.z);

        //Update Position of Camera
        transform.position = Vector3.Lerp(transform.position,
                                            desiredPosition,
                                            smoothSpeed);
    }

    private Transform target;
    private Vector3 offset;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<CameraController>();

                if (instance == null)

                {
                    GameObject go = new GameObject();
                    go.name = "Main Camera";
                    instance = go.AddComponent<CameraController>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    public float smoothSpeed = 0.15f;
    
    //Public Methods
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        offset = transform.position - target.position;
    }


    //Private Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Target Position
        Vector3 desiredPosition = new Vector3(  target.position.x ,
                                                transform.position.y,
                                                target.position.z + offset.z);

        //Update Position of Camera
        transform.position = Vector3.Lerp(transform.position,
                                            desiredPosition,
                                            smoothSpeed);
    }

    private static CameraController instance = null;

    private Transform target;
    private Vector3 offset;
}

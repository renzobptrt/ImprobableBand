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
        offset = transform.position - newTarget.position;
        offset.y = newTarget.gameObject.name.Equals("Murdok") ?
            offsetYMurdok : offSetYHerpo;
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
        startPosition = transform.position;
        offsetYMurdok = transform.position.y - GameObject.Find("/World/Murdok").transform.position.y;
        offSetYHerpo = transform.position.y - GameObject.Find("/World/Herpo").transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Target Position
        Vector3 desiredPosition = new Vector3(  target.position.x ,
                                                target.position.y + offset.y,
                                                target.position.z + offset.z);

        //Update Position of Camera
        transform.position = Vector3.Lerp(transform.position,
                                            desiredPosition,
                                            smoothSpeed);
    }

    private static CameraController instance = null;

    private Vector3 startPosition;
    private Transform target;
    private Vector3 offset;
    private float offsetYMurdok;
    private float offSetYHerpo;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    // Start is called before the first frame update
    public int value = 0;

    private void Start()
    {
        currentValue = value;
        readyToGet = true;
    }
    
    //Public Methods

    public void ResetResource()
    {
        currentValue = value;
        gameObject.SetActive(true);
    }

    public void ResetToGet()
    {
        readyToGet = true;
    }

    //Private Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && readyToGet)
        {
            GUIController.Instance.AddResource(
                other.gameObject.name,
                currentValue
                );
            readyToGet = false;
            currentValue = 0;
            SoundController.Instance.PlaySfxMusic("Resource_2");
            gameObject.SetActive(false);
        }
    }

    private int currentValue = 0;
    private bool readyToGet = false;
}

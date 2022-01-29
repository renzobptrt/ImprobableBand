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
    }
    
    //Public Methods

    public void ResetResource()
    {
        currentValue = value;
        gameObject.SetActive(true);
    }

    //Private Methods

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GUIController.Instance.AddResource(
                other.gameObject.name,
                currentValue
                );
            currentValue = 0;
            gameObject.SetActive(false);
        }
    }

    private int currentValue = 0;
}

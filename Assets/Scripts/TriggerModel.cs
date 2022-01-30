using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerModel : MonoBehaviour
{
    public GameObject model;
    public PlatformMovement platform;

    public enum TypeOfTrigger
    {
        Create,
        Destroy
    }

    public TypeOfTrigger characterToTrigger;

    //Public Methods
    public void ResetModel()
    {
        if (characterToTrigger.Equals(TypeOfTrigger.Create))
            model.SetActive(false);
        else
            model.SetActive(true);

        this.gameObject.SetActive(true);
    }

    //Private Methods
    private void Start()
    {
        if (characterToTrigger.Equals(TypeOfTrigger.Create))
            model.SetActive(false);
        //color azul
        else
            model.SetActive(true);
        //color rojo
    }

    private void OnTriggerEnter(Collider other)
    {
        if (characterToTrigger.Equals(TypeOfTrigger.Create) &&
            other.gameObject.name.Equals("Herpo"))
        {
            bool isAvailable = GUIController.Instance.ConsumeResource(other.gameObject.name);

            if (isAvailable)
            {
                model.SetActive(true);
                this.gameObject.SetActive(false);
                GameController.Instance.IsAllResourcesCheck();
                SoundController.Instance.PlaySfxMusic("Trigger");
            }
        }

        if(characterToTrigger.Equals(TypeOfTrigger.Destroy) &&
            other.gameObject.name.Equals("Murdok"))
        {
            bool isAvailable = GUIController.Instance.ConsumeResource(other.gameObject.name);

            if (isAvailable)
            {
                model.SetActive(false);
                this.gameObject.SetActive(false);
                GameController.Instance.IsAllResourcesCheck();
                SoundController.Instance.PlaySfxMusic("Trigger");

                if (platform != null)
                {
                    platform.SetAbleToMovement();
                }
            }
        }
    }
}

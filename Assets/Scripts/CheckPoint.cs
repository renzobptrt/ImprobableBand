using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int numberCheckPoint = 0;
    public int resourcesToCheck = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && numberCheckPoint != -1)
        {
            GameController.Instance.SetCurrentCurrentBlock(numberCheckPoint);
            GameController.Instance.SetCurrentResourcesToCheck(resourcesToCheck);
            numberCheckPoint = -1;
            this.gameObject.SetActive(false);
        }
    }
}

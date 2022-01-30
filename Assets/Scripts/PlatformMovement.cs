using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformMovement : MonoBehaviour
{

    public float timeToTop = 3;
    public float timeToBot = 3;
    public Vector3 offSetTop = Vector3.zero;
    public Vector3 offSetBot = Vector3.zero;

    public bool isAbleToMovement;

    public bool AbleToMovement
    {
        get { return ableToMovement; }
        set
        {
            ableToMovement = value;
            if (ableToMovement)
            {
                Movement();
            }
        }
    }

    public void SetAbleToMovement()
    {
        AbleToMovement = true;
    }

    private void Start()
    {
        startPosition = this.transform.position;
        topPosition = startPosition + offSetTop;
        botPosition = startPosition - offSetBot;

        if (isAbleToMovement)
        {
            SetAbleToMovement();
        }
    }

    void Movement()
    {
        transform.DOMove(topPosition, timeToTop).OnComplete(() =>
        {
            transform.DOMove(botPosition, timeToBot).OnComplete(() =>
            {
                Movement();
            });
        });
    }

    private Vector3 startPosition;
    private Vector3 topPosition;
    private Vector3 botPosition;
    private bool ableToMovement;
}

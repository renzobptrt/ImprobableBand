using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameController : MonoBehaviour
{
    public int startCheckPointValue = 0;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<GameController>();

                if (instance == null)

                {
                    GameObject go = new GameObject();
                    go.name = "Game Manager";
                    instance = go.AddComponent<GameController>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }


    //Public Methods

    public void SetCurrentCurrentBlock(int checkPoint)
    {
        currentCheckPointValue = checkPoint;

        currentCheckBlock = GameObject.Find("/World/CheckBlock_" + currentCheckPointValue).transform;

        Transform transfromResources = currentCheckBlock.Find("Resources");

        foreach (Transform child in transfromResources)
        {
            currentListResources.Add(child.GetComponent<Resource>());
        }

        Transform transfromTriggers = currentCheckBlock.Find("Triggers");

        foreach(Transform child in transfromTriggers)
        {
            currentListTriggerModel.Add(child.GetComponent<TriggerModel>());
        }

        currentStartPositionMurdok = currentCheckBlock.Find("StartPositionMurdok").transform.position;
        currentStartPositionHerpo = currentCheckBlock.Find("StartPositionHerpo").transform.position;

        currentCheckPointTransform = currentCheckBlock.Find("CheckPoint").transform;

        PlayerController.Instance.SetStartPositions(currentStartPositionMurdok, currentStartPositionHerpo);
    }

    public void SetCurrentResourcesToCheck(int value)
    {
        numberResourcesToCheck = value;
        currentResourcesToCheck = numberResourcesToCheck;
        readyToRestart = true;
    }

    public void IsAllResourcesCheck()
    {
        currentResourcesToCheck--;
        Debug.LogError(currentResourcesToCheck);
        readyToRestart = currentResourcesToCheck <= 0 ? false : true;
    }

    //Private Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetCurrentCurrentBlock(startCheckPointValue);
        SetCurrentResourcesToCheck(numberResourcesToCheck);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && readyToRestart)
        {
            readyToRestart = false;
            CanvasGroup transitionPanel = GUIController.Instance.transitionPanel;
            transitionPanel.interactable = true;
            transitionPanel.DOFade(1, 1).OnComplete(() =>
            {
                ResetCurrentBlock();
                transitionPanel.DOFade(0, 1).OnComplete(() =>
                {
                    PlayerController.Instance.ResetController();
                    readyToRestart = true;
                    transitionPanel.interactable = false;
                });
            });
        }
    }

    void ResetCurrentBlock()
    {
        foreach(Resource r in currentListResources)
        {
            r.ResetResource();
        }

        foreach(TriggerModel t in currentListTriggerModel)
        {
            t.ResetModel();
        }

        PlayerController.Instance.ResetPositions();

        GUIController.Instance.ResetResource();

        ResetCurrentResourcesToCheck();
    }

    void ResetCurrentResourcesToCheck()
    {
        currentResourcesToCheck = numberResourcesToCheck;
    }

    private static GameController instance = null;
    //Complements
    private Transform currentCheckBlock;
    private List<Resource> currentListResources = new List<Resource>();
    private List<TriggerModel> currentListTriggerModel = new List<TriggerModel>();
    private Vector3 currentStartPositionMurdok;
    private Vector3 currentStartPositionHerpo;
    private Transform currentCheckPointTransform;

    //Features
    private int currentCheckPointValue = 0;
    private int currentResourcesToCheck = 0;
    private int numberResourcesToCheck = 3;
    private bool readyToRestart = true;
}

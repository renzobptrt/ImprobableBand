using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIController : MonoBehaviour
{

    [Header("Complements")]
    public CanvasGroup transitionPanel;
    public CanvasGroup finishPanel;
    public Image currentCharacterImage;
    public TextMeshProUGUI currentResourcesMurdokText;
    public TextMeshProUGUI currentResourcesHerpoText;

    [Header("Elements")]
    public Sprite s_Murdok;
    public Sprite s_Herpo;


    public static GUIController Instance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<GUIController>();

                if (instance == null)

                {
                    GameObject go = new GameObject();
                    go.name = "GUIController";
                    instance = go.AddComponent<GUIController>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    //Public Methods
    public void ChangeGuiSpriteCharacter()
    {
        currentCharacterImage.sprite =
            currentCharacterImage.sprite.name.Contains("Murdok") ? s_Herpo : s_Murdok;
    }

    public void AddResource(string name, int value)
    {
        switch(name){
            case "Murdok":  {
                currentMurdokResources += value;
                currentResourcesMurdokText.text = "x" + currentMurdokResources.ToString();
                break;
            }
            case "Herpo": {
                currentHerpoResources += value;
                currentResourcesHerpoText.text = "x" +  currentHerpoResources.ToString();
                break;
            }
        }
    }

    public bool ConsumeResource(string name)
    {
        switch (name)
        {
            case "Murdok":
                {
                    if(currentMurdokResources > 0)
                    {
                        currentMurdokResources--;
                        currentResourcesMurdokText.text = "x" + currentMurdokResources.ToString();
                        return true;
                    }

                    break;
                }
            case "Herpo":
                {
                    if(currentHerpoResources > 0)
                    {
                        currentHerpoResources--;
                        currentResourcesHerpoText.text = "x" + currentHerpoResources.ToString();
                        return true;
                    }
                    break;
                }
        }

        return false;
    }

    public void ResetResource()
    {
        currentHerpoResources = 0;
        currentMurdokResources = 0;

        currentResourcesMurdokText.text = "x" + currentMurdokResources.ToString();
        currentResourcesHerpoText.text = "x" + currentHerpoResources.ToString();
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
        ChangeGuiSpriteCharacter();
    }

    private static GUIController instance = null;
    //Resources
    private int currentMurdokResources = 0;
    private int currentHerpoResources = 0;
}

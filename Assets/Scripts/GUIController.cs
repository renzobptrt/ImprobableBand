using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GUIController : MonoBehaviour
{

    [Header("Complements")]
    public CanvasGroup transitionPanel;
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
            currentCharacterImage.sprite.name.Equals("Murdok") ? s_Herpo : s_Murdok;
    }

    public void AddResource(string name, int value)
    {
        switch(name){
            case "Murdok":  {
                m_currentMurdokResources += value;
                currentResourcesMurdokText.text = "x" + m_currentMurdokResources.ToString();
                break;
            }
            case "Herpo": {
                m_currentHerpoResources += value;
                currentResourcesHerpoText.text = "x" +  m_currentHerpoResources.ToString();
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
                    if(m_currentMurdokResources > 0)
                    {
                        m_currentMurdokResources--;
                        currentResourcesMurdokText.text = "x" + m_currentMurdokResources.ToString();
                        return true;
                    }

                    break;
                }
            case "Herpo":
                {
                    if(m_currentHerpoResources > 0)
                    {
                        m_currentHerpoResources--;
                        currentResourcesHerpoText.text = "x" + m_currentHerpoResources.ToString();
                        return true;
                    }
                    break;
                }
        }

        return false;
    }

    public void ResetResource()
    {
        m_currentMurdokResources = 0;
        m_currentHerpoResources = 0;

        currentResourcesMurdokText.text = "x" + m_currentMurdokResources.ToString();
        currentResourcesHerpoText.text = "x" + m_currentHerpoResources.ToString();
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
    private int m_currentMurdokResources = 0;
    private int m_currentHerpoResources = 0;
}

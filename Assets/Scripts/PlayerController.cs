using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Complements")]
    public LayerMask groundLayer;

    [Header("Settings Player")]
    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -20;

    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {

                instance = FindObjectOfType<PlayerController>();

                if (instance == null)

                {
                    GameObject go = new GameObject();
                    go.name = "PlayerController";
                    instance = go.AddComponent<PlayerController>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    //Public Methods
    public void SetStartPositions(Vector3 newStartPositionMurdok, Vector3 newStartPositionHerpo)
    {
        startPositionMurdok = newStartPositionMurdok;
        startPositionHerpo = newStartPositionHerpo;
    }
    public void ResetPositions()
    {
        controller = null;

        m_Murdok.transform.position = startPositionMurdok;
        m_Herpo.transform.position = startPositionHerpo;

        direction = Vector3.zero;

        //controller.transform.position = startPositionMurdok;
    }

    public void ResetController()
    {
        controller = lastController;
    }

    //Private Methods

    private void Awake()
    {
        currentCharacter = Characters.Murdok;
        m_Murdok = GameObject.Find("Murdok").GetComponent<CharacterController>();
        m_Herpo = GameObject.Find("Herpo").GetComponent<CharacterController>();
        controller = m_Murdok;
        lastController = controller;
        groundCheck = controller.transform.gameObject.transform.Find("GroundCheck").transform;
    }
    void Start()
    {
        //ResetPositions();
        m_Murdok.transform.position = startPositionMurdok;
        m_Herpo.transform.position = startPositionHerpo;

        direction = Vector3.zero;
        CameraController.Instance.SetTarget(controller.transform.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //Velocity X
        float hInput = Input.GetAxis("Horizontal");
        
        direction.x = hInput * speed;

        //Check is Grounded
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);

        if (isGrounded)
        {
            //ChangePlayer
            if (Input.GetKeyDown(KeyCode.C) && controller != null)
            {
                ChangePlayer();
            }

            ableToMakeADoubleJump = true;

            //Jump
            if (Input.GetButtonDown("Jump") && controller.gameObject.name.Equals("Herpo"))
            {
                direction.y = jumpForce;
            }
        }
        else
        {
            //Gravity
            direction.y += gravity * Time.deltaTime;

            if (ableToMakeADoubleJump & Input.GetButtonDown("Jump") && controller.gameObject.name.Equals("Herpo"))
            {
                direction.y = jumpForce;
                ableToMakeADoubleJump = false;
            }
        }

        //Move
        if(controller != null)
        {
            controller.Move(direction * Time.deltaTime);

            if (hInput != 0)
            {
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(0, 0, hInput));
                controller.transform.rotation = newRotation;
            }
        }
    }

    void ChangePlayer()
    {
        switch (currentCharacter)
        {
            case Characters.Murdok:
                {
                    currentCharacter = Characters.Herpo;
                    controller = m_Herpo;
                    break;
                }
            case Characters.Herpo:
                {
                    currentCharacter = Characters.Murdok;
                    controller = m_Murdok;
                    break;
                }
        }
        lastController = controller;
        groundCheck = controller.transform.gameObject.transform.Find("GroundCheck").transform;
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        //Controllers
        CameraController.Instance.SetTarget(controller.transform.gameObject.transform);
        GUIController.Instance.ChangeGuiSpriteCharacter();
    }

    private static PlayerController instance = null;

    private Vector3 direction;
    private bool ableToMakeADoubleJump = true;
    private Characters currentCharacter;

    private CharacterController m_Murdok;
    private CharacterController m_Herpo;

    public CharacterController controller;
    private CharacterController lastController;
    private Transform groundCheck;
    private Vector3 startPositionMurdok;
    private Vector3 startPositionHerpo;

    public float lastUp;

    private enum Characters
    {
        Murdok,
        Herpo
    }
}


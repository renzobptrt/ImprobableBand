using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Complements")]
    public LayerMask groundLayer;

    [Header("Settings Player")]
    public float speed = 8;
    public float jumpForceMurdok = 5;
    public float jumpForceHerpo = 10;
    public float gravity = -20;
    public string nameThemeMurdok = string.Empty;
    public string nameThemeHerpo = string.Empty;

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

    public void FinishController()
    {
        direction = Vector3.zero;
        controller = null;
    }

    //Private Methods
    private void Awake()
    {
        currentCharacter = Characters.Murdok;
        m_Murdok = GameObject.Find("Murdok").GetComponent<CharacterController>();
        m_Herpo = GameObject.Find("Herpo").GetComponent<CharacterController>();
        controller = m_Murdok;
        an_Murdok = m_Murdok.transform.Find("MurdokAnimated").GetComponent<Animator>();
        an_Herpo = m_Herpo.transform.Find("HerpoAnimated").GetComponent<Animator>();
        animator = an_Murdok;
        lastController = controller;
        groundCheck = controller.transform.gameObject.transform.Find("GroundCheck").transform;
    }
    void Start()
    {
        //ResetPositions();
        m_Murdok.transform.position = startPositionMurdok;
        m_Herpo.transform.position = startPositionHerpo;
        jumpForce = jumpForceMurdok;

        direction = Vector3.zero;
        SoundController.Instance.PlayBackgroundMusic("Destruccion_Theme");
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
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
                SoundController.Instance.PlaySfxMusic("Jump");
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
                SoundController.Instance.PlaySfxMusic("Jump");
            }
        }

        //Move
        if(controller != null)
        {
            controller.Move(direction * Time.deltaTime);

            if (hInput != 0)
            {
                animator.SetBool("IsWalking", true);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(0, 0, hInput));
                controller.transform.rotation = newRotation;
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }

    void ChangePlayer()
    {
        string currentBackgroundMusic = string.Empty;
        animator.SetBool("IsWalking", false);
        switch (currentCharacter)
        {
            case Characters.Murdok:
                {
                    currentCharacter = Characters.Herpo;
                    controller = m_Herpo;
                    animator = an_Herpo;
                    jumpForce = jumpForceHerpo;
                    currentBackgroundMusic = nameThemeHerpo;
                    break;
                }
            case Characters.Herpo:
                {
                    currentCharacter = Characters.Murdok;
                    controller = m_Murdok;
                    animator = an_Murdok;
                    jumpForce = jumpForceMurdok;
                    currentBackgroundMusic = nameThemeMurdok;
                    break;
                }
        }
        lastController = controller;
        groundCheck = controller.transform.gameObject.transform.Find("GroundCheck").transform;
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        //Controllers
        CameraController.Instance.SetTarget(controller.transform.gameObject.transform);
        GUIController.Instance.ChangeGuiSpriteCharacter();
        //Audio
        SoundController.Instance.PlaySfxMusic("Change");
        SoundController.Instance.PlayBackgroundMusic(currentBackgroundMusic);
    }

    private static PlayerController instance = null;

    private Vector3 direction;
    private bool ableToMakeADoubleJump = true;
    private Characters currentCharacter;

    private CharacterController m_Murdok;
    private CharacterController m_Herpo;
    private Animator an_Murdok;
    private Animator an_Herpo;

    private CharacterController controller;
    private Animator animator;
    private CharacterController lastController;
    private Transform groundCheck;
    private Vector3 startPositionMurdok;
    private Vector3 startPositionHerpo;

    private float jumpForce = 10;

    private enum Characters
    {
        Murdok,
        Herpo
    }
}


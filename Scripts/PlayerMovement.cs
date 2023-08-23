using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController character;
    float x, z;
    public float walkingSpeed = 8f;
    public float runningSpeed = 15f;
    public float crouchSpeed = 5f;
    float cTotalSpeed;
    public float speed;

    public float gravity = -9.81f;
    Vector3 velocity;
    Vector3 move;
    public Transform gorundCheck;
    public float groundDistance = 0.4f;
    public LayerMask mask;
    bool isGrounded;
    public float jumpHeight = 3f;
    public Animator animator;
    public int health = 100;
    public TMP_Text health_text;
    AtesEtme gun;

    bool isRunnable = true;
    public bool isCrouching = false;
    public bool isWalking = false;
    public bool isRunning = false;
    public bool foundAmmoPack = false;

    public AudioClip ammoPackSound;

    int jumpStates = 0;

    AudioSource audio;

    int time;
    public TMP_Text time_text;





    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        speed = walkingSpeed;
        gun = GameObject.FindWithTag("Gun").GetComponent<AtesEtme>();

        InvokeRepeating("IncreaseTime", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        isRunnable = true;
        isWalking = false;
        isRunning = false;


        CheckRunability();
        CheckMovementStatus();

        CheckHighscore();
        CheckDeath();

        health_text.text = health.ToString();
        time_text.text = time.ToString();

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");


        move = transform.right * x + transform.forward*z;
        character.Move(move*speed*Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(gorundCheck.position, groundDistance, mask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jumpStates = 1;
        }

        if (jumpStates > 0)
        {
            jumpStates++;
        }

        if(jumpStates>30 && isGrounded)
        {
            jumpStates = -1;
        }

        

        if (Input.GetKey(KeyCode.LeftShift) && isRunnable)
        {
            
            speed = runningSpeed;
            gun.mermiDegisebilir = false;
            gun.AtesEdebilir = false;
            if (!animator.GetBool("isRunning"))
            {
                animator.SetBool("isRunning", true);
            }
                
            
           
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) )
        {
            gun.AtesEdebilir = true;
            gun.mermiDegisebilir = true;
            speed = walkingSpeed;
            animator.SetBool("isRunning", false);
        }

        

        if (Input.GetKeyDown(KeyCode.C))
        {

            character.height = 1.8f;
            speed = crouchSpeed;
            isCrouching = true;
            
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            character.height = 3.6f;
            speed = walkingSpeed;
            isCrouching = false;
            //transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);

        }

        PlayMovementSound();
    }

    void CheckRunability()
    {
        //koþmayý engelleyen durumlarý belirt

        if (animator.GetBool("isReloading"))
        {
            isRunnable = false;
        }

        if (z <= 0f)
        {
            isRunnable = false;
        }

        if (isCrouching)
        {
            isRunnable = false;
        }

        if (!isGrounded)
        {
            isRunnable = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ammo Pack"))
        {
            foundAmmoPack = true;
            // AmmoPack ile temas gerçekleþtiðinde yapýlacak iþlemler
            audio.PlayOneShot(ammoPackSound);

            // AmmoPack'i yok etmek için:
            Destroy(hit.gameObject);
        }
    }

    void CheckMovementStatus()
    {
        if(move != new Vector3(0, 0, 0) && isGrounded)
        {
            if (speed == walkingSpeed)
            {
                isWalking = true;

            }
            else if (speed == runningSpeed)
            {
                isRunning = true;

            }
            
        }

    }

    void PlayMovementSound()
    {
        if(isWalking && !audio.isPlaying)
        {
            audio.pitch = 1.1f;
            audio.Play();
        }
        if(isRunning && !audio.isPlaying)
        {
            audio.pitch = 1.5f;
            audio.Play();
        }
        if (isCrouching && !audio.isPlaying)
        {
            audio.pitch = 0.7f;
            audio.Play();
        }
        if(jumpStates==-1 && !audio.isPlaying)
        {
            jumpStates = 0;
            audio.pitch = 0.7f;
            audio.Play();
        }
    }

    void IncreaseTime()
    {
        time++;
    }

    void CheckHighscore()
    {
        int highscore = PlayerPrefs.GetInt("highscore");

        if (highscore < time)
        {
            PlayerPrefs.SetInt("highscore", time);
        }
    }

    void CheckDeath()
    {
        if (health <= 0)
        {
            PlayerPrefs.SetInt("score", time);

            SceneManager.LoadScene("DeathMenu",LoadSceneMode.Single);
        }
    }
}

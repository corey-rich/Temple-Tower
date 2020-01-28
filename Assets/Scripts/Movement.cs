using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Movement : MonoBehaviour
{
    private Animator anim;
    public Animator frontMiles;
    public Animator whipAnim; //this is connected to the whip which has the animation WhipExtend on it to play the whip animation
    private AudioSource audioData;
    public AudioClip[] audioClipArray;
    public cameraShake shake;
    float defaultScale; 
    public float distanceGround;
    public float speed;
    public float rollSpeed;
    public float rollLength;
    public float jumpWaitTime;
    public float fallDelayTime;
    public float jumpForce = 6;
    public string pumaName;
    public bool isJumping = false;
    public bool isDead = false;
    public bool isPaused = false;
    public bool isGrounded;
    public bool isBackTurned = false;
    public bool isRolling = false;
    public bool justJumped = false;
    public bool notMoving = true;
    public bool isWhipping = false;
    public bool isLocked;
    public GameObject bloodSpawn;
    public GameObject[] MilesSprites;
    public SpriteRenderer whip;
    public SpriteRenderer MilesFrontWalk;
    private Rigidbody rb;
    private bool rollStop = false;
    public int score;
    public bool facingFront = false;
    public GameObject scoreText;
    public GameObject pauseMenu;
    public GameObject cameraShake;
    private TextMeshProUGUI text;
    public ParticleSystem fartParticles;
    private int greenValue = 5;
    private int goldValue = 10;
    private int silverValue = 25;
    private int isGreen = 0;
    private int isGold = 0;
    private int isSilver = 0;
    private int counter = 0;
    public bool playedOnce = false;
    public bool firstDropQuake = false;
    public bool fartJumpCooldown = false;
    void Awake()
    {
        audioData = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animator>();  
        rb = GetComponent<Rigidbody>();
        text = scoreText.GetComponent<TextMeshProUGUI>();
        isGrounded = false;
        defaultScale = transform.localScale.x; // assuming this is facing right 
        //distanceGround = GetComponent<Collider> ().bounds.extents.y; 
    }
    void Update()
    {
        if(!isPaused)
        {
            if (isLocked)
            {
                audioData.Stop(); //stops the whip sound from playing when miles is locked on the floor changing mechanism, but hasnt chosen a direction
                whipAnim.Play("MilesWhipPulledBackIdleStop");
                MilesFrontWalk.enabled = false;
                foreach (GameObject sprites in MilesSprites)
                    {
                        sprites.GetComponent<SpriteRenderer>().enabled = false;
                    }
                if (Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
                {
                    audioData.clip=audioClipArray[11]; //plays whip sound after player makes a direction choice
                    audioData.PlayOneShot(audioData.clip);  
                    playedOnce = false;
                    isLocked = false;
                    shake.triggerShakeBig();
                    whipAnim.Play("MilesWhipPulledBackIdle");
                }
            }
            if (isDead)
            {
                SceneManager.LoadScene("DeathScene");
            }
            else if (isWhipping && isGrounded && !isLocked && !justJumped  && !isRolling && !playedOnce)
            {
                foreach (GameObject sprites in MilesSprites)
                {
                    sprites.GetComponent<SpriteRenderer>().enabled = false;
                }
                MilesFrontWalk.enabled = false;
                whipAnim.Play("MilesWhippingFrameByFrame"); //whip estending animation
                if (!isLocked && playedOnce == false)
                {
                    audioData.clip=audioClipArray[11];
                    audioData.PlayOneShot(audioData.clip);  
                    StartCoroutine(WhipAnimationDelay());
                    playedOnce = true;
                }
            }
            else if (!isWhipping && !isLocked)
            {
                // moves character
                if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
                {
                    notMoving = false;
                }
                else
                {
                    notMoving = true;
                }
                if (MilesFrontWalk && !facingFront)
                {
                    TurnOffFrontWalk();
                }    
                transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);

                if (Input.GetMouseButton(0) || Input.GetButtonDown("Fire4"))
                {
                    if(isGrounded && !isRolling && !justJumped)
                    {
                        isWhipping = true;
                        Debug.Log("Whipping");
                    }
                } //press mouse button to whip        

                if (Input.GetAxis("Horizontal") > 0)
                {
                    //player is going right
                    isBackTurned = false;
                    transform.localScale = new Vector3(defaultScale, transform.localScale.y, transform.localScale.z);
                    whip.sortingOrder = 10;
                    if(facingFront  && !notMoving)
                    {
                        RunAnimation();
                    }
                    else if(facingFront && notMoving)
                    {
                        anim.Play("MilesIdle"); 
                    }
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    //player is going left
                    isBackTurned = false;
                    transform.localScale = new Vector3(-defaultScale, transform.localScale.y, transform.localScale.z);
                    whip.sortingOrder = 0;
                    if(facingFront && !notMoving)
                    {
                        RunAnimation();
                    }
                    else if(facingFront && notMoving)
                    {
                        anim.Play("MilesIdle"); 
                    }
                }
                //character moving toward camera
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (Input.GetAxis("Horizontal") == 0)
                    {
                        foreach (GameObject sprites in MilesSprites)
                        {
                            sprites.GetComponent<SpriteRenderer>().enabled = false;
                        }
                        MilesFrontWalk.enabled = true;
                        if (!justJumped && isGrounded)
                        {
                            frontMiles.Play("MilesFrontRunCycle");
                        }
                        isBackTurned = false;
                    }
                }
                //character moving away from camera
                if (Input.GetAxis("Vertical") > 0)
                {
                    if (Input.GetAxis("Horizontal") == 0)
                    {
                        foreach (GameObject sprites in MilesSprites)
                        {
                            sprites.GetComponent<SpriteRenderer>().enabled = false;
                        }
                        MilesFrontWalk.enabled = true;
                        if (!justJumped && isGrounded)
                        {
                            frontMiles.Play("MilesBackRunCycle");
                        }
                        isBackTurned = true;
                    }
                }
                if(Input.GetKeyDown(KeyCode.F))
                {
                    if(isGrounded && notMoving)
                    {
                        anim.Play("MilesFart");
                        fartParticles.Play();
                    }
                    if(!isGrounded && (!notMoving || notMoving))
                    {
                        anim.Play("MilesFart");
                        fartParticles.Play();
                    }
                    if(!isGrounded  && !fartJumpCooldown) 
                    {
                        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                        //fartJumpCooldown = true;
                        fartParticles.Play();
                    }       
                          
                }
                // roll
                if (isGrounded && !notMoving && !isRolling && (Input.GetKeyDown("left shift") || Input.GetButtonDown("Fire3")))
                {
                    if(!facingFront)
                    {
                        speed += rollSpeed;
                        isRolling = true;
                        RollAnimation();
                        StartCoroutine(RollBack());
                    }
                    else if(facingFront && !isRolling) //removing !isRolling causes the player to infinitely speedup
                    {
                        speed += rollSpeed;
                        isRolling = true;
                        frontMiles.speed = 1.8f;
                        StartCoroutine(RollBack());
                    }
                }
                // roll back
                if (isGrounded && rollStop)
                {
                    speed -= rollSpeed;
                    isRolling = false;
                    rollStop = false;
                }
                // applies force vertically if the space key is pressed
                if (isGrounded && (Input.GetKeyDown("space") || Input.GetButton("Fire1")))
                {
                    if (justJumped == false && isJumping == false)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                        justJumped = true;
                        isJumping = true;
                        JumpAnimation();
                        PlayerJumpSound();
                        StartCoroutine(JumpReset());
                    }
                }
        }

        }
        // coins
        if (isGreen > 0)
        {
            if (counter != greenValue)
            {
                score += 1;
                ScoreDisplay();
                text.text += score;
                counter++;

                if (counter == greenValue)
                {
                    counter = 0;
                    isGreen--;
                }
            }
        }
        if (isGold > 0)
        {
            if (counter != goldValue)
            {
                score += 1;
                ScoreDisplay();
                text.text += score;
                counter++;

                if (counter == goldValue)
                {
                    counter = 0;
                    isGold--;
                }
            }
        }
        if (isSilver > 0)
        {
            if (counter != silverValue)
            {
                score += 1;
                ScoreDisplay();
                text.text += score;
                counter++;

                if (counter == silverValue)
                {
                    counter = 0;
                    isSilver--;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "green")
        {
            audioData.clip = audioClipArray[10];
            audioData.PlayOneShot(audioData.clip);
            isGreen++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "gold")
        {
            audioData.clip = audioClipArray[10];
            audioData.PlayOneShot(audioData.clip);
            isGold++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "silver")
        {
            audioData.clip = audioClipArray[10];
            audioData.PlayOneShot(audioData.clip);
            isSilver++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "puma")
        {
            pumaName = collision.gameObject.name;
        }
    }

    /*void FixedUpdate() //version of checking for isGrounded through raycasting
    {
        if (!Physics.Raycast (transform.position, -Vector3.up, distanceGround + 0.2f))
        {
            isGrounded = false;
            Debug.Log("Air");
        }
        else
            {
                isGrounded = true;
                isJumping = false;
                Debug.Log("Ground");
            if (notMoving == true && isGrounded == true && justJumped == false && isRolling == false && !isWhipping)
                IdleAnimation();
            else if (notMoving == false && isGrounded == true && justJumped == false && isRolling == false && !isWhipping)
                RunAnimation();
            }   
    }*/
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true; 
            isJumping = false;  
            fartJumpCooldown = false;         
            if (notMoving == true && isGrounded == true && justJumped == false && isRolling == false && !isWhipping)
                IdleAnimation();
            else if (notMoving == false && isGrounded == true && justJumped == false && isRolling == false && !isWhipping)
                RunAnimation();
            if(firstDropQuake)
            {
                cameraShake.GetComponent<cameraShake>().triggerShakeSmall();    
                firstDropQuake = false;           
            }
        }
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        if (isGrounded == false && justJumped == false && isRolling == false)
        {
            StartCoroutine(FallDelay());            
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Trap" && this.gameObject.tag == "Player")            
        {
            Instantiate (bloodSpawn, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), this.transform.rotation);
        }
    }
    public void ScoreDisplay()
    {
        text.text = "0";
        for (int i = 0; i <= 5 - score.ToString().Length; i++)
        {
            text.text = text.text + '0';
        }
    }
    public void IdleAnimation()
    {
        if(!facingFront)
        {
            anim.Play("MilesIdle"); 
        }
        else if(facingFront && (Input.GetAxis("Vertical") == 0))
        {
            foreach (GameObject sprites in MilesSprites)
                {
                   sprites.GetComponent<SpriteRenderer>().enabled = false; //<---------Change to false to enable Forward Running Only
                }
            MilesFrontWalk.enabled = true;
            frontMiles.Play("MilesFrontIdle");
        }
    } 
    public void RunAnimation()
    {
        if(facingFront && isGrounded && (Input.GetAxis("Horizontal") != 0))
        {
            foreach (GameObject sprites in MilesSprites)
                {
                   sprites.GetComponent<SpriteRenderer>().enabled = false; //<---------Change to false to enable Forward Running Only
                }
            MilesFrontWalk.enabled = true; //<---------Change to true to enable Forward Running Only
            if(!isBackTurned)
            {
                frontMiles.Play("MilesFrontRunCycle");
                anim.Play("RunCycleFrontFacing");
            }
            else if(isBackTurned)
            {
                frontMiles.Play("MilesBackRunCycle");
                anim.Play("RunCycleFrontFacing");
            }
        }       
        if(facingFront && notMoving && (Input.GetAxis("Horizontal") == 0))
            {
                anim.Play("MilesIdle");
                Debug.Log("Facing Forward Idle");
            }
        else if(facingFront && !isGrounded && Input.GetAxis("Horizontal") != 0)
        {
            if(!isBackTurned)
            {
                frontMiles.Play("MilesFrontJump");
            }
            else 
            {
               frontMiles.Play("MilesBackJump");
            }
        }
        else if(!facingFront && !notMoving && Input.GetAxis("Horizontal") != 0)
        {
            anim.Play("RunCycle");
            Debug.Log("Running");
        }
    } 
    public void RollAnimation()
    {
        anim.Play("DodgeRoll"); 
    }  
    public void JumpAnimation()
    {
        anim.Play("MilesJump3");
        frontMiles.speed = 1.0f; //fixes animation desync when rolling and in the front facing camera mode
        if (isBackTurned)
        {
            frontMiles.Play("MilesBackJump");
        }
        else
        {
            frontMiles.Play("MilesFrontJump");
        } 
    }    
    public void FallAnimation()
    {
        anim.Play("MilesFallLoop"); 
    }    
    public void TurnOffFrontWalk()
    {
        foreach (GameObject sprites in MilesSprites)
        {
            sprites.GetComponent<SpriteRenderer>().enabled = true; 
        }
        MilesFrontWalk.enabled = false;
    }     
    public void PlayerHurtSound()
    {
        audioData.clip=audioClipArray[Random.Range(0,2)];
        //audioData.Stop();
        audioData.PlayOneShot(audioData.clip);
    }
    public void PlayerJumpSound()
    {
        audioData.clip=audioClipArray[Random.Range(3,6)];
        //audioData.Stop();
        audioData.PlayOneShot(audioData.clip);
    }
    IEnumerator RollBack()
    {
        yield return new WaitForSeconds(rollLength);
        frontMiles.speed = 1.0f;
        rollStop = true;
    }
    IEnumerator JumpReset()
    {
        yield return new WaitForSeconds(jumpWaitTime);
        justJumped = false;
    }
    IEnumerator FallDelay()
    {
        yield return new WaitForSeconds(fallDelayTime);
        
        if (isGrounded == false && justJumped == false && isRolling ==false)
        {
            FallAnimation();                   
        }
    }
    IEnumerator WhipAnimationDelay()
    {
        yield return new WaitForSeconds(0.73f);
        IdleAnimation();
        if(facingFront)
            {
            foreach (GameObject sprites in MilesSprites)
                {
                    sprites.GetComponent<SpriteRenderer>().enabled = true;
                }    
                //MilesFrontWalk.Play("MilesFrontIdle");
                MilesFrontWalk.enabled = false;     
                anim.Play("MilesIdle");            
            }
        isWhipping = false;
        playedOnce = false;
    }
}

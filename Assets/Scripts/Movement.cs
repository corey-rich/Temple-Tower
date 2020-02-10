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
    public Animator backMiles;
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
    public float timeInAir = 0;
    public float maxAirTime = 0;
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
    public SpriteRenderer MilesBackWalk;
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
    private int counterGreen = 0;
    private int counterGold = 0;
    private int counterSilver = 0;
    public bool playedOnce = false;
    public bool playedOnce2 = false;
    public bool bigDropQuake = false;
    public bool fartJumpCooldown = false;
    void Awake()
    {
        audioData = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();
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
        if (!isPaused)
        {
            if (isLocked)
            {
                audioData.Stop(); //stops the whip sound from playing when miles is locked on the floor changing mechanism, but hasnt chosen a direction
                whipAnim.Play("MilesWhipPulledBackIdleStop");
                MilesFrontWalk.enabled = false;
                MilesBackWalk.enabled = false;

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

            if (notMoving && !isJumping && !isRolling && !justJumped && isGrounded && !isWhipping && !bigDropQuake)//trying to stop sliding
                rb.drag = 2f;
            else
                rb.drag = 0.2f;

            if (isDead)
                SceneManager.LoadScene("DeathScene");
            else if (isWhipping && isGrounded && !isLocked && !justJumped  && !isRolling && !playedOnce)
            {
                foreach (GameObject sprites in MilesSprites)
                {
                    sprites.GetComponent<SpriteRenderer>().enabled = false;
                }

                MilesFrontWalk.enabled = false;
                MilesBackWalk.enabled = false;
                whipAnim.Play("MilesWhippingFrameByFrame"); //whip extending animation

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
                    notMoving = false;
                else
                    notMoving = true;
                if (MilesFrontWalk && !facingFront)
                    TurnOffFrontWalk();
                
                if(!facingFront)
                {
                    transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, -1 * speed * Time.deltaTime);
                }

                if (Input.GetMouseButton(0) || Input.GetButtonDown("Fire4"))
                {
                    if (isGrounded && !isRolling && !justJumped)
                    {
                        isWhipping = true;
                        //Debug.Log("Whipping");
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
                    else if (facingFront && notMoving)
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
                        MilesBackWalk.enabled = false;

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
                        if (facingFront)//these if statments make sure the player cant face away from the camera when in auto run "Boulder" mode
                        {
                            frontMiles.Play("MilesFrontRunCycle");
                        }
                        else if (!facingFront)
                        {
                            MilesBackWalk.enabled = true;
                            MilesFrontWalk.enabled = false;
                        }
                        if (!justJumped && isGrounded)
                        {
                            backMiles.Play("MilesBackRunCycle");
                        }
                        isBackTurned = true;
                    }
                }

                if (!isGrounded)
                {
                    timeInAir += Time.deltaTime;
                    //Debug.Log(timeInAir);
                }

                if (timeInAir > maxAirTime )
                {
                    if(timeInAir > 6f)
                    {
                        bigDropQuake = false;
                    }
                    else if(timeInAir < 6f)
                    {
                        bigDropQuake = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    if(isGrounded && notMoving)
                    {
                        anim.Play("MilesFart");
                        fartParticles.Play();
                    }

                    if(!isGrounded && (!notMoving || notMoving))
                    {
                        anim.Play("MilesFartAirborne");
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
                if (isGrounded && !isRolling && (Input.GetKeyDown("left shift") || Input.GetButtonDown("Fire3")))
                {
                    if(!facingFront && !notMoving)
                    {
                        speed += rollSpeed;
                        isRolling = true;
                        RollAnimation();
                        StartCoroutine(RollBack());
                    }
                    else if (facingFront && !isRolling) //removing !isRolling causes the player to infinitely speedup
                    {
                        speed += rollSpeed;
                        isRolling = true;
                        frontMiles.speed = 1.8f;
                        frontMiles.Play("MilesFrontRunCycle");
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
            if (counterGreen != greenValue)
            {
                score += 1;
                ScoreDisplay();
                text.text += score;
                counterGreen++;

                if (counterGreen == greenValue)
                {
                    counterGreen = 0;
                    isGreen--;
                }
            }
        }
        if (isGold > 0)
        {
            if (counterGold != goldValue)
            {
                score += 1;
                ScoreDisplay();
                text.text += score;
                counterGold++;

                if (counterGold == goldValue)
                {
                    counterGold = 0;
                    isGold--;
                }
            }
        }
        if (isSilver > 0)
        {
            if (counterSilver != silverValue)
            {
                score += 1;
                ScoreDisplay();
                text.text += score;
                counterSilver++;

                if (counterSilver == silverValue)
                {
                    counterSilver = 0;
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
        if (collision.gameObject.tag == "Ground" && bigDropQuake)
        {
            MilesHeroLanding();
        }
        else if (collision.gameObject.tag == "Ground" && !bigDropQuake)
        {
            isGrounded = true; 
            timeInAir = 0;
            isJumping = false;  
            fartJumpCooldown = false;         
            if (notMoving == true && isGrounded == true && justJumped == false && isRolling == false && !isWhipping)
                IdleAnimation();
            else if (notMoving == false && isGrounded == true && justJumped == false && isRolling == false && !isWhipping)
                RunAnimation();
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
            frontMiles.Play("MilesFrontRunCycle"); //change to idle to fix
        }
    } 

    public void RunAnimation()
    {
        if (facingFront && isGrounded && (Input.GetAxis("Horizontal") != 0))
        {
            foreach (GameObject sprites in MilesSprites)
            {
               sprites.GetComponent<SpriteRenderer>().enabled = false; //<---------Change to false to enable Forward Running Only
            }
            MilesFrontWalk.enabled = true; //<---------Change to true to enable Forward Running Only
            if (!isBackTurned)
            {
                frontMiles.Play("MilesFrontRunCycle");
                anim.Play("RunCycleFrontFacing");
            }
            else if (isBackTurned)
            {
                backMiles.Play("MilesBackRunCycle");
                anim.Play("RunCycleFrontFacing");
            }
        }  
        
        if (facingFront && notMoving && (Input.GetAxis("Horizontal") == 0))
        {
            anim.Play("MilesIdle");
            //Debug.Log("Facing Forward Idle");
        }
        else if (facingFront && !isGrounded && Input.GetAxis("Horizontal") != 0)
        {
            if (!isBackTurned)
            {
                MilesBackWalk.enabled = false;
                MilesFrontWalk.enabled = true;
                frontMiles.Play("MilesFrontJump");
            }
            else 
            {
                MilesFrontWalk.enabled = false;
                MilesBackWalk.enabled = true;
                frontMiles.Play("MilesBackJump");
            }
        }
        else if (!facingFront && !notMoving && Input.GetAxis("Horizontal") != 0)
        {
            anim.Play("RunCycle");
            //Debug.Log("Running");
        }
    }
    
    public void RollAnimation()
    {
        anim.Play("DodgeRoll"); 
    }
    
    public void JumpAnimation()
    {
        //MilesBackWalk.enabled = false;
        //MilesFrontWalk.enabled = false;
        anim.Play("MilesJump3");
        backMiles.Play("MilesBackJump");
        frontMiles.Play("MilesFrontJump");
        frontMiles.speed = 1.0f; //fixes animation desync when rolling and in the front facing camera mode

        if (isBackTurned)
        {
            //MilesFrontWalk.enabled = false;
            //MilesBackWalk.enabled = true;

        }
        else
        {
           //MilesBackWalk.enabled = false;
            //MilesFrontWalk.enabled = true;
            //backMiles.Play("MilesBackJump");        
            //frontMiles.Play("MilesFrontJump");
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
        MilesBackWalk.enabled = false;
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

    public void MilesHeroLanding()
    {
        if (!playedOnce2)
        {
            anim.Play("MilesHeroLanding");
            //Debug.Log("worked");
            cameraShake.GetComponent<cameraShake>().triggerShakeSmall();         
            audioData.clip=audioClipArray[12];
            audioData.PlayOneShot(audioData.clip);
            playedOnce2 = true;
            timeInAir = 0;
            rb.velocity = new Vector3(0,0,0);
            StartCoroutine(MilesHeroLandingDelay()); 
        } 
    }

    IEnumerator MilesHeroLandingDelay()
    {
        yield return new WaitForSeconds(0.5f);
        bigDropQuake = false;
        playedOnce2 = false;
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
            MilesBackWalk.enabled = false;     
            anim.Play("MilesIdle");            
        }

        isWhipping = false;
        playedOnce = false;
    }
}

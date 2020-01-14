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
    float defaultScale; 
    public float distanceGround;
    public float speed;
    public float rollSpeed;
    public float rollLength;
    public float jumpWaitTime;
    public float fallDelayTime;
    public float jumpForce = 6;
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

    public int score;
    public GameObject scoreText;
    private TextMeshProUGUI text;
    private int greenValue = 5;
    private int goldValue = 10;
    private int silverValue = 25;
    private int isGreen = 0;
    private int isGold = 0;
    private int isSilver = 0;
    private int counter = 0;
    public bool playedOnce = false;
    public bool playedOnce2 = false;

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
        distanceGround = GetComponent<Collider> ().bounds.extents.y; 
    }

    void Update()
    {
        if (isLocked)
        {
            if (Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0)
            {
                //MilesPullBack();
                WhipPullBack();
                StartCoroutine(WhipDelay());
                playedOnce = false;
                isLocked = false;
            }
        }

        if (isDead)
        {
            SceneManager.LoadScene("DeathScene");
        }
        else if (isWhipping && playedOnce == false)
        {
            //play whipping animation on Miles and the Whip
            //anim.Play("MilesWhipExtend"); //player whipping animation
            foreach (GameObject sprites in MilesSprites)
            {
                sprites.GetComponent<SpriteRenderer>().enabled = false;
            }
            //TurnOffFrontWalk();
            //whipAnim.GetComponent<SpriteRenderer>().enabled = true;
            whipAnim.Play("MilesWhippingFrameByFrame"); //whip estending animation
            if (!isLocked && playedOnce == false)
            {
                StartCoroutine(WhipDelay());
                StartCoroutine(PullBackDelay());
                playedOnce = true;
            }
        }
        else if (!isWhipping && !isLocked)
        {
            // moves character
            if (Input.GetAxis("Horizontal") != 0)
            {
                notMoving = false;
            }
            else
            {
                notMoving = true;
            }
            if (MilesFrontWalk)
            {
                TurnOffFrontWalk();
            }    
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, Input.GetAxis("Vertical") * speed * Time.deltaTime);

            if (Input.GetMouseButton(0) || Input.GetButtonDown("Fire4"))
            {
                isWhipping = true;
            } //press mouse button to whip
        
                


            if (Input.GetAxis("Horizontal") > 0)
            {
                //player is going right
                isBackTurned = false;
                transform.localScale = new Vector3(defaultScale, transform.localScale.y, transform.localScale.z);
                whip.sortingOrder = 10;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                //player is going left
                isBackTurned = false;
                transform.localScale = new Vector3(-defaultScale, transform.localScale.y, transform.localScale.z);
                whip.sortingOrder = 0;
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
            // roll
            if (isGrounded && !notMoving && (Input.GetKeyDown("left shift") || Input.GetButtonDown("Fire3")))
            {
                speed += rollSpeed;
                isRolling = true;
                RollAnimation();
                StartCoroutine(RollBack());
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
   /* void FixedUpdate() //version of checking for isGrounded through raycasting
    {
        if (!Physics.Raycast (transform.position, -Vector3.up, distanceGround + 0.2f))
        {
            isGrounded = false;
            Debug.Log("Air");
        }
        else
            {
                isGrounded = true;
                Debug.Log("Ground");
            }   
    }*/

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

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isJumping = false;
            
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
        if (other.gameObject.tag == "Trap")            
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
        anim.Play("MilesIdle"); 
    } 
    public void RunAnimation()
    {
        anim.Play("RunCycle"); 
    } 
    public void RollAnimation()
    {
        anim.Play("DodgeRoll"); 
    }  
    public void JumpAnimation()
    {
        anim.Play("MilesJump3");
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

    public void MilesPullBack()
    {
        anim.Play("MilesWhipPullback");
    }

    public void WhipPullBack()
    {
        whipAnim.Play("WhipPullback");
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
        speed -= rollSpeed;
        isRolling = false;
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
    
    IEnumerator PullBackDelay()
    {
        yield return new WaitForSeconds(1);
        //WhipPullBack();
        //MilesPullBack();
        playedOnce2 = true;
    }
    
    IEnumerator WhipDelay()
    {
        yield return new WaitForSeconds(0.5146f);
        isWhipping = false;
        playedOnce = false;
    }
}


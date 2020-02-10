using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotteryController : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioData;
    private bool isReady = true;
    public float potteryDelayTime = 0.2f;
    public AudioClip[] audioClipArray;
    public bool isCollided;

    [Header("ShortNarrow = 2, ShortVaseAndLid = 1")]
    [Header("Vase type is: TallVase = 4, Medium = 3")]
    [Header("Coins: green = 0, gold = 1, silver = 2")]
    public int potteryType; 
    public int maxCoins = 5;
    private int coinCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        audioData = GetComponent<AudioSource>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();   
        audioData.clip=audioClipArray[Random.Range(0,audioClipArray.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isReady == true)
        {
            audioData.PlayOneShot(audioData.clip); 
        switch (potteryType)
        {
        case 4:
            anim.Play("TallVaseWobble");
            break;
        case 3:
            anim.Play("MediumVaseWobble");
            break;
        case 2:
            anim.Play("ShortNarrowVaseWobble");
            break;
        case 1:
            anim.Play("ShortVaseAndLidWobble");
            break;
        default:
            print ("No Vase Type Selected");
            break;
        }
            isReady = false;
            if(maxCoins > coinCount)
            {
                isCollided = true;
                coinCount++;
            }
            StartCoroutine(PotterySoundDelay());
        }
    }

    IEnumerator PotterySoundDelay()
    {
        yield return new WaitForSeconds(potteryDelayTime);
        isReady = true;
    }
} 


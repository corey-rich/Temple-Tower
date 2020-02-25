using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveSegment : MonoBehaviour
{

    private int moveNumber = 3;
    public Animator anim;
    private GameObject levelManager;
    private segmentManagerLevelOne scriptManager;
    public GameObject mechanism;
    public CinemachineVirtualCamera vcam;
    public Transform player;
    public GameObject Movement;
    public Movement movementScript;
    public GameObject level;
    public GameObject directionalArrows;
    public GameObject waterfallAssets;

    public ParticleSystem Rockslide;
    public directionalArrows arrowScript;

    public float originalPosition;
    public float zoomInLength;
    public float zoomOutLength;
    public float zoomOutPosition;
    public float zoomInSpeed;
    public bool isLocked = false;
    public float zoomInPosition;
    public float movingSpeed;
    public bool isMoving = false;
    public bool isLeft = false;
    public bool playedOnce = false;

    public Transform leftPoint;
    public Transform rightPoint;
    public float distance;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        anim = mechanism.GetComponent<Animator>();
        Movement = GameObject.FindGameObjectWithTag("Player");
        movementScript = Movement.GetComponent<Movement>();
        arrowScript = directionalArrows.GetComponent<directionalArrows>();
        levelManager = GameObject.Find("SegmentManager");
        scriptManager = levelManager.GetComponent<segmentManagerLevelOne>();
        originalPosition = vcam.m_Lens.FieldOfView;
        zoomInPosition = originalPosition + zoomInLength;
        zoomOutPosition = originalPosition + zoomOutLength;
        DistanceCalculator();
        target.transform.position = new Vector3 (level.transform.position.x + distance, level.transform.position.y, level.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            Zoom();

            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire3"))
            {
                arrowScript.ResetAndDestroy();
                isMoving = true;
                isLocked = false;
                isLeft = true;
                MoveAnim();

                target.position = new Vector3(level.transform.position.x - distance, level.transform.position.y, level.transform.position.z);
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("Fire5"))
            {
                arrowScript.ResetAndDestroy();
                isMoving = true;
                isLocked = false;
                isLeft = false;
                MoveAnim();
            }
        }

        if (isMoving)
        {
            ZoomBack();
            SegmentMover();   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Whip" && !isLocked && !isMoving)
        {
            movementScript.isLocked = true;
            isLocked = true;
        }
    }

    public void Zoom()
    {
        directionalArrows.SetActive(true);  
        if (vcam.m_Lens.FieldOfView <= zoomInPosition)
            vcam.m_Lens.FieldOfView -= (-zoomInSpeed * Time.deltaTime);
           
    }

    public void ZoomBack()
    { 
        if (vcam.m_Lens.FieldOfView >= originalPosition)
            vcam.m_Lens.FieldOfView -= (zoomInSpeed * Time.deltaTime);
        directionalArrows.SetActive(false);
    }
    public void ZoomOut()
    {
        if (vcam.m_Lens.FieldOfView <= zoomOutPosition)
            vcam.m_Lens.FieldOfView -= (-zoomInSpeed * Time.deltaTime);        
    }
    public void ZoomIn()
    {  
            StartCoroutine(ZoomOutRepeater());
    }
    private void DistanceCalculator()
    {
        if (Mathf.Abs(leftPoint.position.x) > Mathf.Abs(rightPoint.position.x))
        {
            distance = (Mathf.Abs(leftPoint.position.x) - Mathf.Abs(rightPoint.position.x)) / 2;
        }
        else
            distance = (Mathf.Abs(rightPoint.position.x) - Mathf.Abs(leftPoint.position.x)) / 2;

        distance *= 2;
    }

    private void SegmentMover()
    {
        if (level.transform.position != target.position && isMoving)
        {
            level.transform.position = Vector3.MoveTowards(level.transform.position, target.position, movingSpeed * Time.deltaTime);

            if (level.transform.position.x == target.position.x)
            {
                target.transform.position = new Vector3(level.transform.position.x + distance, level.transform.position.y, level.transform.position.z);
                isMoving = false;
                IdleAnim();
            }
        }
    }

    public void IdleAnim()
    {
        anim.Play("FloorMechanismIdle");
        Rockslide.Stop();
    }

    public void MoveAnim()
    {
        if(isLeft)
        {
            anim.Play("FloorMechanismTurnLeft");
            moveNumber--;
        }
        if(!isLeft)
        {
            anim.Play("FloorMechanismTurnRight");
            moveNumber++;
        }
        Rockslide.Play();
        if (moveNumber > 4)
        {
            moveNumber = 0;
        }
        if (moveNumber < 0)
        {
            moveNumber = 4;
        }
        switch (moveNumber)
            {
            case 4:
                waterfallAssets.SetActive(false);           
                break;
            case 3:
                scriptManager.disableGears();
                waterfallAssets.SetActive(true);                
                break;
            case 2:
                scriptManager.enableGears();
                waterfallAssets.SetActive(false);
                //turn off waterfall stream
                break;
            case 1:
                scriptManager.disableGears();
                waterfallAssets.SetActive(false);
                break;
            default:
                
                break;
            }
    }
    IEnumerator ZoomOutRepeater()
    {
        if (vcam.m_Lens.FieldOfView > originalPosition)
        {
            vcam.m_Lens.FieldOfView -= (zoomInSpeed * Time.deltaTime);
        }
        if (vcam.m_Lens.FieldOfView <= originalPosition)
        {
            yield break;
        } 
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(ZoomOutRepeater());
    }

}

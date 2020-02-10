using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveSegment : MonoBehaviour
{
    public GameObject mechanism;
    private Animator anim;
    public CinemachineVirtualCamera vcam;
    public Transform player;
    public GameObject Movement;
    public Movement movementScript;
    public GameObject level;

    public ParticleSystem Rockslide;

    public float originalPosition;
    public float zoomInLength;
    public float zoomInSpeed;
    public bool isLocked = false;
    public float zoomInPosition;
    public float movingSpeed;
    public bool isMoving = false;
    public bool isLeft = false;

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
        originalPosition = vcam.m_Lens.FieldOfView;
        zoomInPosition = originalPosition - zoomInLength;
        DistanceCalculator();
        target.transform.position = new Vector3 (level.transform.position.x + distance, level.transform.position.y, level.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            Zoom();

            if (Input.GetKeyDown("a") || Input.GetAxis("Horizontal") < 0)
            {
                isMoving = true;
                isLocked = false;
                isLeft = true;
                MoveAnim();

                target.position = new Vector3(level.transform.position.x - distance, level.transform.position.y, level.transform.position.z);
            }
            else if (Input.GetKeyDown("d") || Input.GetAxis("Horizontal") > 0)
            {
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

    private void Zoom()
    {
        if (vcam.m_Lens.FieldOfView >= zoomInPosition)
            vcam.m_Lens.FieldOfView += (-zoomInSpeed * Time.deltaTime);
           
    }

    private void ZoomBack()
    { 
        if (vcam.m_Lens.FieldOfView <= originalPosition)
            vcam.m_Lens.FieldOfView += (zoomInSpeed * Time.deltaTime);
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
        anim.Play("FloorMechanismImdle");
        Rockslide.Stop();
    }

    public void MoveAnim()
    {
        if(isLeft)
        {
            anim.Play("FloorMechanismTurnLeft");
        }
        if(!isLeft)
        {
            anim.Play("FloorMechanismTurnRight");
        }
        Rockslide.Play();
    }

}

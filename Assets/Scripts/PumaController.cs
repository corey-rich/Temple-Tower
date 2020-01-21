using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PumaController : MonoBehaviour { 

    public float lookRadius = 4f;
    public float speed;
    public Transform[] waypoints;

    public Transform playerTarget;
    public Transform waypointTarget;
    public GameObject dustCloud;
    NavMeshAgent agent;

    private bool playerDetect = false;
    private int current = 0;
    private float Wpradius = 1;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerTarget.position, transform.position);
        speed = 2;
        waypointTarget = waypoints[current];

        if (distance <= lookRadius)
        {
            speed = 0;
            FacePlayer();
            playerDetect = true;
            agent.SetDestination(playerTarget.position);
        }
        else
        {
            speed = 2f;
            playerDetect = false;
            agent.SetDestination(waypoints[current].position);
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
            FaceWaypoint();
        }

        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < Wpradius && playerDetect == false)
        {
            current++;
            SpriteFlip();
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }

    }

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(dustCloud, gameObject.transform.position, gameObject.transform.rotation);
            playerTarget.position = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 750, other.gameObject.transform.position.z);
            gameObject.SetActive(false);
        }
    }


    void FaceWaypoint()
    {
        Vector3 direction = (waypointTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z + 90));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void FacePlayer()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z + 90));
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 30f);
    }

    void SpriteFlip()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if (gameObject.GetComponent<SpriteRenderer>().flipX == false)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }
}


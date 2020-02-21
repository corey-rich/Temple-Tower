using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderMove : MonoBehaviour
{
    public Transform player;
    public Vector3 playerPos;
    public float speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("MilesNewWorking").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(4, 0, 0, Space.Self);
        playerPos = new Vector3(player.position.x, player.position.y + 2, player.position.z );
        //transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }
}

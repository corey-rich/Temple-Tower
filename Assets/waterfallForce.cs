using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterfallForce : MonoBehaviour
{
    public bool isForced = false;
    public Collision player;
    public float rate = 4.5f;

    private void Update()
    {
        if (isForced)
        {
            player.rigidbody.AddForce(-transform.right * rate, ForceMode.VelocityChange);
            //player.rigidbody.MovePosition(-transform.right * 1f * Time.fixedDeltaTime);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isForced = true;
            player = collision;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isForced = false;
            player.rigidbody.velocity = new Vector3(0f, player.rigidbody.velocity.y, 0f);
        }
    }
}

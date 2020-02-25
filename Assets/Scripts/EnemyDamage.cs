using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public Movement movement;
    public float hurtDelayTime = 0.05f;
    public bool iFrames = false;
    public RedHealthBar healthBar;
    public Movement milesScript;

    void Start()
    {
        movement = GameObject.Find("MilesNewWorking").GetComponent<Movement>();
        healthBar = GameObject.Find("RedHealth").GetComponent<RedHealthBar>();
        milesScript = GameObject.Find("MilesNewWorking").GetComponent<Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !movement.isRolling && !iFrames)
        {
            healthBar.AdjustCurrentHealth(damage);
            iFrames = true;
            StartCoroutine(HurtDelay());
            StartCoroutine(DamageDelay());
            //Debug.Log("You took damage");
            //Debug.Log("This is a " + this.gameObject.tag);
        }
    }

    IEnumerator HurtDelay()
    {
        yield return new WaitForSeconds(hurtDelayTime);
        milesScript.PlayerHurtSound();
    }

    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(3);
        iFrames = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float hurtDelayTime = 0.05f;
    public RedHealthBar healthBar;
    public Movement milesScript;

    void Start()
    {
        healthBar = GameObject.Find("RedHealth").GetComponent<RedHealthBar>();
        milesScript = GameObject.Find("MilesNewWorking").GetComponent<Movement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            healthBar.AdjustCurrentHealth(damage);
            StartCoroutine(HurtDelay());
            Debug.Log("You took damage");
            Debug.Log("This is a " + this.gameObject.tag);
        }
    }

    IEnumerator HurtDelay()
    {
    yield return new WaitForSeconds(hurtDelayTime);
    milesScript.PlayerHurtSound();
    }
}

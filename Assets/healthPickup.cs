using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public RedHealthBar healthBar;
    public Movement milesScript;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("RedHealth").GetComponent<RedHealthBar>();
        milesScript = GameObject.Find("MilesNewWorking").GetComponent<Movement>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            healthBar.AdjustCurrentHealth(health);
            milesScript.MilesDrinkHealth();
            Debug.Log("You gained health");
            Debug.Log("This is a " + this.gameObject.tag);
            Destroy(gameObject);
        }
    }
}

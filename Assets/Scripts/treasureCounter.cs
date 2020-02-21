using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using TMPro;

public class treasureCounter : MonoBehaviour
{
    public GameObject[] objects;
    public TextMeshProUGUI treasureTextMesh;
    private Animator anim;
    public int treasureCollectedAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        objects = GameObject.FindGameObjectsWithTag("Treasure");
        treasureTextMesh  = treasureTextMesh.gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(objects.Length);
        treasureTextMesh.text = treasureCollectedAmount.ToString() + "/" + objects.Length.ToString();
    }

    public void collectTreasure()
    {
        anim.Play("TreasureHudSlideIn");
        StartCoroutine(treasureDelay());
    }

    IEnumerator treasureDelay()
    {
        yield return new WaitForSeconds(1.8f);
        treasureCollectedAmount++;
    }
}

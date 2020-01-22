 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 using TMPro;
 
 public class timerScript : MonoBehaviour 
 {
    public float startTime;
    TextMeshProUGUI textMesh;
     
     public static float timer = 0;
     
     void Start()
     {
         startTime = Time.time;
         textMesh  = this.gameObject.GetComponent<TextMeshProUGUI>();
     }
     
     // Update is called once per frame
     void Update () {
         
         float guiTime = timer + (Time.time + startTime);
         if (guiTime > 0)
         {
             int minutes = (int)(guiTime / 60);
             int seconds  = (int)(guiTime % 60);
             int fraction = (int)((guiTime * 100) % 100);
     
             textMesh.text = string.Format ("{00:00}:{1:00}:{2:00}", minutes, seconds, fraction);    
         }
     }
 }
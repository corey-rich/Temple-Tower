 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 using TMPro;
 using UnityEngine.SceneManagement;

 public class timerScript : MonoBehaviour 
 {
     public float startTime;
     TextMeshProUGUI textMesh;
     
     public static float timer = 0;
     public float guiTime; 
     public bool reloaded;
     
     void Start()
     {
        startTime = Time.deltaTime;
        textMesh  = this.gameObject.GetComponent<TextMeshProUGUI>();
        //DontDestroyOnLoad (gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
     }
     
     // Update is called once per frame
     void Update () {
         
         guiTime = timer + (Time.timeSinceLevelLoad + startTime);
         if (guiTime > 0)
         {
             int minutes = (int)(guiTime / 60);
             int seconds  = (int)(guiTime % 60);
             int fraction = (int)((guiTime * 100) % 100);
     
             textMesh.text = string.Format ("{00:00}:{1:00}:{2:00}", minutes, seconds, fraction);    
         }
        //Debug.Log(reloaded);
         /*if (reloaded)
         {
            startTime = Time.deltaTime; // Replace by the value you want
            guiTime = 0;
            timer = 0;
            int minutes = 0;
            int seconds = 0;
            int fraction = 0;
            startTime = Time.timeSinceLevelLoad;
            textMesh.text = string.Format ("{00:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            reloaded = false;
         }*/
     }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Scene Loaded");
        //reloaded = true;
    }
 }


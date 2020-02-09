using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    private AudioSource audioData;
    public AudioClip[] audioClipArray;
    private AudioSource[] allAudioSources;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    // Update is called once per frame
    void Update()
    {
 
    }
   public void levelComplete()
   {        
        foreach( AudioSource audioS in allAudioSources) 
        {
            audioS.Stop();
        }
        audioData.Stop();
        audioData.clip=audioClipArray[0];
        audioData.PlayOneShot(audioData.clip); 
        StartCoroutine(endSongSequence());
   }
   IEnumerator endSongSequence()
   {
        yield return new WaitForSeconds(4);
        audioData.clip=audioClipArray[1];
        audioData.PlayOneShot(audioData.clip); 
        Time.timeScale = 0;
   }
}

using UnityEngine;

public class ScrollMaker : MonoBehaviour
{
    // Scroll main texture based on time

    public float scrollSpeed = 0.5f;
    Renderer rend;
    public bool switchDirection = false;
    public bool goSideways = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;

        if (switchDirection == true && goSideways == false)
        {
            rend.material.mainTextureOffset = new Vector2(0f, -offset);    
        }
        else if (switchDirection == false && goSideways == false)
        {
            rend.material.mainTextureOffset = new Vector2(0f, offset);  
        }
        else if (switchDirection == true && goSideways == true)
        {
            rend.material.mainTextureOffset = new Vector2(-offset, 0f);  
        }
        else if (goSideways == true && switchDirection == false)
        {
            rend.material.mainTextureOffset = new Vector2(offset, 0f);  
        }
    }
}

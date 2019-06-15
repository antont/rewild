using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingScript : MonoBehaviour
{
    bool down = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (down && transform.localPosition.y > 4.90)
            transform.localPosition=new Vector3(transform.localPosition.x, transform.localPosition.y - 0.005f, transform.localPosition.z) ;

        if(!down && transform.localPosition.y < 4.96)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.005f, transform.localPosition.z);
        if (transform.localPosition.y <= 4.90)
            down = false;


    }
}

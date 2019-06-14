using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadingScript : MonoBehaviour
{
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mat.color = new Color(0, 0, 0, a);
        a += increment;
        a = Mathf.Min(a, 1);
        a = Mathf.Max(a, 0);
    }
    float a = 0;
    float increment = 0.016f;

    public void goBlack()
    {
        increment = 0.016f;
    }
    public void goTransparent()
    {
        
        increment = -0.016f;
    }
}

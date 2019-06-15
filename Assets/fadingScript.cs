using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadingScript : MonoBehaviour
{

    public GameObject go;
    public GameObject ImagePlain;
    public Texture[] texes;
    public Material[] mats;
    public Material mat;

    public GameObject cube;
    bool mov = false;
    int movRunning = -1;
    // Start is called before the first frame update
    void Start()
    {
        //mat = this.GetComponent<Renderer>().material;
    }
    private GameObject dis=null;
    // Update is called once per frame
    bool ended = false;
    void Update()
    {
        if (movRunning < 0)
        {
            mat.color = new Color(0, 0, 0, a);
            a += increment;
            if (dis != null && a >= 1)
            {
                dis.SetActive(false);
                dis = null;
                if (mov)
                {
                    startMovie();
                    mov = false;
                }
            }
            if (a <= 0)
                cube.SetActive(false);
            if (a > 0)
                cube.SetActive(true);

            a = Mathf.Min(a, 1);
            a = Mathf.Max(a, 0);
            return;
        }
        movRunning += 1;
        if (movRunning >= 440)
        {
            movRunning = -100;
            ImagePlain.SetActive(false);
            a = 1;
            return;
        }
        int index = Mathf.FloorToInt(movRunning / 40);
        //Debug.Log(movRunning);
        ImagePlain.GetComponent<Renderer>().material= mats[index];



    }
    float a = 1;
    float increment = 0.016f;

    public void goBlack()
    {
        increment = 0.016f;
    }
    public void goBlackDisable(GameObject dis)
    {
        this.dis = dis;
        increment = 0.016f;
    }

    public void goBlackDisable(GameObject dis, bool mov)
    {
        goBlackDisable(dis);
        this.mov = mov;

    }
    public void goTransparent()
    {
        
        increment = -0.016f;
    }

    //private Texture oldTex;
    public void startMovie()
    {
        //oldTex = mat.mainTexture;
        ImagePlain.SetActive(true);
        go.transform.position = new Vector3(1000, 1000, 1000);
        a = 0;
        movRunning = 0;
    }
}

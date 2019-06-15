using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MoveWalking : MonoBehaviour
{

    public GameObject[] Avatars;
    public GameObject[] AvatarWrappers;
    public float[] speedModifiers;
    public float[] turnDistances;

    public int startAvatar;

    public GameObject cameraWrapper;
    public Camera cam;
    public Vector3 startingPosition;

    public fadingScript fader;

    private Vector3 firstCamPos;
    private Vector3 lastCamPos;
    private Vector3 cumulatedPosition=new Vector3(0,0,0);
    private Vector3 lastCumulatedPosition = new Vector3(0, 0, 0);

    public GameObject anchovyNPC;
    public GameObject human;
    public GameObject breadCrumbs;
    public GameObject fullWrapper;

    public GameObject duck;

    private bool PreGame = true;
    public void Start()
    {

    }

    private void Recalibrate()
    {
        GameObject currentAvatarWrapper = AvatarWrappers[startAvatar];
        currentAvatarWrapper.transform.localRotation = Quaternion.identity;
        this.transform.localPosition = startingPosition;
        firstCamPos = cam.transform.localPosition;
        lastCamPos = cam.transform.position;
        cameraWrapper.transform.localPosition = -cam.transform.localPosition;
        lastCumulatedPosition = this.transform.localPosition;
        currentAvatarWrapper.transform.LookAt(this.transform.position + cam.transform.forward);
    }
    bool black = true;

    bool startDuck = false;
    bool eatingDuck = false;
    int duckC = 0;

    bool flying = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            startingPosition = human.transform.position + new Vector3(0,20,0);
            AvatarWrappers[startAvatar].SetActive(false);
            startAvatar = 0;
            AvatarWrappers[startAvatar].SetActive(true);
            this.transform.rotation = Quaternion.identity;
            Recalibrate();

            black = false;
            fader.goTransparent();
            flying = true;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            startingPosition = new Vector3(-64.03f, 4.989f+0.3f, -58.42f);
            AvatarWrappers[startAvatar].SetActive(false);
            startAvatar = 1;
            AvatarWrappers[startAvatar].SetActive(true);
            Recalibrate();
            Vector3 v = human.transform.position;
                v.y = 4.989f + 0.3f;
            this.transform.LookAt(v);

            black = false;
            fader.goTransparent();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PreGame = false;
            startingPosition = new Vector3(-65.27f, 4.8f, -56.9f);
            anchovyNPC.SetActive(false);
            AvatarWrappers[startAvatar].SetActive(false);
            startAvatar = 2;
            AvatarWrappers[startAvatar].SetActive(true);
            Recalibrate();
            this.transform.LookAt(new Vector3(-64.03f, 4.8f, -58.42f));
            //cameraWrapper.transform.localRotation = Quaternion.identity;
            black = false;
            fader.goTransparent();

        }
        GameObject currentAvatarWrapper = AvatarWrappers[startAvatar];
        if (Input.GetKeyDown(KeyCode.R))
        {
            Recalibrate();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            //breadCrumbs.transform.position = this.transform.position + currentAvatarWrapper.transform.forward.normalized * 2;
            //breadCrumbs.transform.position = new Vector3(breadCrumbs.transform.position.x, 4.97f, breadCrumbs.transform.position.z);
            breadCrumbs.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (black)
            {
                black = false;
                fader.goTransparent();

            }
            else
            {
                black = true;
                fader.goBlack();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.transform.position = new Vector3(-65.27f, 4.5f, -56.9f);
            Quaternion q = cam.transform.localRotation;
            Vector3 v = q.eulerAngles;
            v.x = 0;
            v.z = 0;
            q.eulerAngles = v;
            fullWrapper.transform.localRotation= Quaternion.Inverse(q);
            this.transform.LookAt(anchovyNPC.transform.position);

            black = false;
            fader.goTransparent();
            if (PreGame)
            {
                anchovyNPC.SetActive(true);
            }
        }
        if (AnchovyNPC.done)
        {
            black = true;
            fader.goBlackDisable(anchovyNPC);
            AnchovyNPC.done = false;

        }

        if (startDuck)
        {
            Vector3 v = this.transform.position;
            v.y = duck.transform.position.y;
            duck.transform.position = Vector3.MoveTowards(duck.transform.position, v, 0.01f);
            duck.transform.LookAt(v);
            float x = duck.transform.position.x - this.transform.position.x;
            float y = duck.transform.position.z - this.transform.position.z;
            if ((x * x + y * y) < 0.25)
            {
                startDuck = false;
                eatingDuck = true;
            }
        }
        if (eatingDuck)
        {
            duckC += 1;
            if (duckC == 60)
                fader.goBlackDisable(duck, true);
            if (duckC < 70)
                duck.transform.Rotate(new Vector3(1, 0, 0));
            else
            {
                eatingDuck = false;
                black = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            startDuck = true;
        }
        if (PreGame)
        {
            cameraWrapper.transform.localPosition = -cam.transform.localPosition;
            return;
        }

        //Debug.Log(lastCamPos);
        //Debug.Log(cam.transform.localPosition);
        //Debug.Log(cameraWrapper.transform.localPosition);
        //Debug.Log(this.transform.localPosition);
        Vector3 dif = lastCamPos - cam.transform.position;
        //Debug.Log(dif.ToString("F6"));
        cameraWrapper.transform.localPosition = - cam.transform.localPosition;
        if (flying)
            dif.y = dif.y * 2.5f;
        this.transform.localPosition = this.transform.localPosition - speedModifiers[startAvatar] * dif;//InputTracking.GetLocalPosition(XRNode.Head);
        lastCamPos = cam.transform.position;
        /*
        float camX = cam.transform.localRotation.eulerAngles.x+180;
        float camY = cam.transform.localRotation.eulerAngles.y+180;
        float camZ = cam.transform.localRotation.eulerAngles.z+180;

        float avaX = currentAvatarWrapper.transform.rotation.eulerAngles.x+180;
        float avaY = currentAvatarWrapper.transform.rotation.eulerAngles.y+180;
        float avaZ = currentAvatarWrapper.transform.rotation.eulerAngles.z+180;

        float difY = camY - avaY;
        float difX = camX - avaX;
        float difZ = camZ - avaZ;
        bool changeX = false;
        bool changeY = false;
        bool changeZ = false;
        if ((dif = this.transform.localPosition - lastCumulatedPosition).magnitude >= turnDistances[startAvatar])
        {
            //Debug.Log(dif.ToString("F6"));
            //currentAvatarWrapper.transform.LookAt(this.transform.localPosition + dif);
            //adjust model to movement
            //currentAvatarWrapper.transform.localRotation = Quaternion.identity;
            //currentAvatarWrapper.transform.Rotate(new Vector3(0.0f, deg, 0.0f));
            lastCumulatedPosition = this.transform.localPosition;
            changeX = true;
            changeY = true;
            changeZ = true;
        }
        else if ((dif = this.transform.localPosition - lastCumulatedPosition).magnitude >= 0.5*turnDistances[startAvatar])
        {
            if (difY > 50)
            {
                //cam more deg than ava
                difY -= 50;
                changeY = true;
            }
            else if (difY < -50)
            {
                difY += 50;
                changeY = true;
                //cam less deg than ava
            }
            if (difX > 50)
            {
                //cam more deg than ava
                difX -= 50;
                changeX = true;
            }
            else if (difX < -50)
            {
                difX += 50;
                changeX = true;
                //cam less deg than ava
            }
            if (difZ > 50)
            {
                //cam more deg than ava
                difZ -= 50;
                changeZ = true;
            }
            else if (difZ < -50)
            {
                difZ += 50;
                changeZ = true;
                //cam less deg than ava
            }
        }
        else
        {
            if (difY > 100)
            {
                //cam more deg than ava
                difY -= 100;
                changeY = true;
            }
            else if (difY < -100)
            {
                difY += 100;
                changeY = true;
                //cam less deg than ava
            }
        }
        if(changeY)
            avaY += difY -180;
        if(changeX)
            avaX += difX - 180;
        if(changeZ)
            avaZ += difZ - 180;

        Quaternion q2 = currentAvatarWrapper.transform.rotation;
        q2.eulerAngles=new Vector3(avaX, avaY, avaZ);
        //currentAvatarWrapper.transform.rotation = q2;
        */

        
        /*
        float difRot = cam.transform.localRotation.eulerAngles.y - currentAvatarWrapper.transform.rotation.eulerAngles.y;

        if (difRot > 120)
        {
            Quaternion q = currentAvatarWrapper.transform.rotation;
            Vector3 v = q.eulerAngles;
            v.y = v.y + difRot - 120;
            q.eulerAngles = v;
            currentAvatarWrapper.transform.rotation = q;
        }
        if (cam.transform.localRotation.eulerAngles.y < -120)
        {
            Quaternion q = currentAvatarWrapper.transform.rotation;
            Vector3 v = q.eulerAngles;
            v.y = v.y + difRot + 120;
            q.eulerAngles = v;
            currentAvatarWrapper.transform.rotation = q;
        }
        */

        if ((dif= this.transform.localPosition- lastCumulatedPosition).magnitude >= turnDistances[startAvatar])
        {
            //Debug.Log(dif.ToString("F6"));
            //currentAvatarWrapper.transform.LookAt(this.transform.localPosition+ dif);
            currentAvatarWrapper.transform.localRotation = cam.transform.localRotation;
            //adjust model to movement
            //currentAvatarWrapper.transform.localRotation = Quaternion.identity;
            //currentAvatarWrapper.transform.Rotate(new Vector3(0.0f, deg, 0.0f));
            lastCumulatedPosition = this.transform.localPosition;
        }
        

    }
}

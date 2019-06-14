﻿using System.Collections;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            startingPosition = human.transform.position + new Vector3(0,4,0);
            AvatarWrappers[startAvatar].SetActive(false);
            startAvatar = 0;
            AvatarWrappers[startAvatar].SetActive(true);
            this.transform.rotation = Quaternion.identity;
            Recalibrate();
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            startingPosition = new Vector3(-64.03f, 4.989f+0.5f, -58.42f);
            AvatarWrappers[startAvatar].SetActive(false);
            startAvatar = 1;
            AvatarWrappers[startAvatar].SetActive(true);
            Recalibrate();
            Vector3 v = human.transform.position;
                v.y = 4.989f + 0.5f;
            this.transform.LookAt(v);
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
                if (PreGame)
                {
                    anchovyNPC.SetActive(true);
                }
            }
            else
            {
                black = true;
                fader.goBlack();
                if (PreGame)
                {
                    anchovyNPC.SetActive(false);
                }
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
        this.transform.localPosition = this.transform.localPosition - speedModifiers[startAvatar] * dif;//InputTracking.GetLocalPosition(XRNode.Head);
        lastCamPos = cam.transform.position;

        if ((dif= this.transform.localPosition- lastCumulatedPosition).magnitude >= turnDistances[startAvatar])
        {
            Debug.Log(dif.ToString("F6"));
            currentAvatarWrapper.transform.LookAt(this.transform.localPosition+ dif);
            //adjust model to movement
            //currentAvatarWrapper.transform.localRotation = Quaternion.identity;
            //currentAvatarWrapper.transform.Rotate(new Vector3(0.0f, deg, 0.0f));
            lastCumulatedPosition = this.transform.localPosition;
        }

    }
}

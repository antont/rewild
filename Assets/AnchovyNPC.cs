using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchovyNPC : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, 0.005f);
        this.transform.LookAt(target.transform.position);
    }
}

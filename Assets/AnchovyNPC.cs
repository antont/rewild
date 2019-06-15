using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchovyNPC : MonoBehaviour
{
    public static bool done = false;
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
        float x = this.transform.position.x - target.transform.position.x;
        float y = this.transform.position.z - target.transform.position.z;
        if ((x * x + y * y) < 0.1)
        {
            done = true;
        }
    }
}

using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(PostProcessingBehaviour))]
public class EnableWaterEffect : MonoBehaviour
{
    PostProcessingBehaviour postProcess;

    // Start is called before the first frame update
    void Start()
    {
        postProcess = GetComponent<PostProcessingBehaviour>();
        //postProcess.enabled = true;        
    }

    // Update is called once per frame
    void Update()
    {
        float y = gameObject.transform.position.y;
        postProcess.enabled = y < 5;        
    }
}

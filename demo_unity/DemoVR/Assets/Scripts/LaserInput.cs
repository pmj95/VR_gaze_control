using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;

public class LaserInput : MonoBehaviour
{

    public static GameObject currObject;
    private int currID;
    // Start is called before the first frame update
    void Start()
    {
        currObject = null;
        currID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);
        Debug.Log("Pos" + transform.position);
        Debug.Log("For" + transform.forward);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            int id = hit.collider.gameObject.GetInstanceID();

            if (currID != id)
            {
                currID = id;
                currObject = hit.collider.gameObject;
                string name = currObject.name;
                string tag = currObject.tag;
                if (tag == "Button")
                {
                    Debug.Log("Hallo " + name);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PupilLabs;

public class Player : MonoBehaviour
{
    private GazeController gazeController;
    private GazeVisualizer gazeVisualizer;
    public Transform gazeOrigin;
    // Start is called before the first frame update
    void Start()
    {
        gazeController = GameObject.FindObjectOfType<GazeController>();
        gazeController.OnReceive3dGaze += GazeController_OnReceive3dGaze;
        gazeVisualizer = GameObject.FindObjectOfType<GazeVisualizer>();
    }

    private void GazeController_OnReceive3dGaze(GazeData obj)
    {
        Vector3 origin = gazeOrigin.position;
        Vector3 direction = gazeOrigin.TransformDirection(obj.GazeDirection);
        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            GameObject currObject = hit.collider.gameObject;

            GeneralButton bc = currObject.GetComponent<GeneralButton>();
            if (bc != null)
            {
                bc.DoAction();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

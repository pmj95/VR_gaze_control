using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PupilLabs;

public class Player : BaseMono
{
    public GazeController gazeController;
    public Transform gazeOrigin;
    public Light mainLight;
    private Color mainLightColor;

    protected override void DoAwake()
    {
        this.gazeController.OnReceive3dGaze += this.GazeController_OnReceive3dGaze;
    }

    protected override void DoDestroy()
    {
        this.gazeController.OnReceive3dGaze -= this.GazeController_OnReceive3dGaze;
    }

    protected override void OnCalibrationStarted()
    {
        this.gazeController.OnReceive3dGaze -= this.GazeController_OnReceive3dGaze;
        this.mainLightColor = this.mainLight.color;
        this.mainLight.color = Color.white;
    }

    protected override void OnCalibrationRoutineDone()
    {
        this.gazeController.OnReceive3dGaze += this.GazeController_OnReceive3dGaze;
        this.mainLight.color = this.mainLightColor;
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
}

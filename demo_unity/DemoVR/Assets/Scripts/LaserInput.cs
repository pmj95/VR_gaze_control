using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using System;

public class LaserInput : BaseMono
{
    public SteamVR_LaserPointer laserPointer;

    protected override void DoStart()
    {
        // Nothing to do
    }

    protected override void DoAwake()
    {
        this.laserPointer = GameObject.FindObjectOfType<SteamVR_LaserPointer>();
        this.laserPointer.PointerClick += this.SteamVR_LaserPointer_PointerClick;
    }

    protected override void DoDestroy()
    {
        this.laserPointer.PointerClick -= this.SteamVR_LaserPointer_PointerClick;
    }

    protected override void OnCalibrationStarted()
    {
        this.laserPointer.PointerClick -= this.SteamVR_LaserPointer_PointerClick;
    }

    protected override void OnCalibrationRoutineDone()
    {
        this.laserPointer.PointerClick += this.SteamVR_LaserPointer_PointerClick;
    }

    private void SteamVR_LaserPointer_PointerClick(object sender, PointerEventArgs e)
    {
        /*if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject currObject = hit.collider.gameObject;

            GeneralButton bc = currObject.GetComponent<GeneralButton>();
            if (bc != null)
            {
                bc.DoAction();
            }
        }*/
    }
}

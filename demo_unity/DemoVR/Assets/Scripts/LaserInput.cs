using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using System;

public class LaserInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SteamVR_LaserPointer laserPointer = GameObject.FindObjectOfType<SteamVR_LaserPointer>();
        laserPointer.PointerClick += SteamVR_LaserPointer_PointerClick;
    }

    private void SteamVR_LaserPointer_PointerClick(object sender, PointerEventArgs e)
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColor : GeneralButton
{
    public Color color;
    private GameObject[] walls;
    private Light light;
    private List<Renderer> renderers;
    private bool calibrationRunning = false;

    protected override void OnCalibrationStarted()
    {
        this.calibrationRunning = true;
    }

    protected override void OnCalibrationRoutineDone()
    {
        this.calibrationRunning = false;
    }

    protected override void DoAwake()
    {
        walls = GameObject.FindGameObjectsWithTag("Wall");
        renderers = new List<Renderer>();
        foreach (GameObject wall in walls)
        {
            renderers.Add(wall.GetComponent<Renderer>());
        }

        GameObject licht = GameObject.FindGameObjectWithTag("Light");

        light = licht.GetComponent<Light>();
    }

    protected override void DoDestroy()
    {
        // nothing to do
    }

    // Update is called once per frame
    public override void DoAction()
    {
        var colors = this.GetComponent<Button>().colors;
        colors.normalColor = this.color;
        this.GetComponent<Button>().colors = colors;
    }

    void changeWall(Color color)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetColor("_Color", color);
        }
    }

    void changeLight(Color color)
    {
        light.color = color;
    }
}

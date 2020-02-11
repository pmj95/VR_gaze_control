using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonColor : GeneralButton
{
    private GameObject[] walls;
    private Light light;
    private List<Renderer> renderers;

    // Start is called before the first frame update
    void Start()
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

    void Update()
    {

    }

    // Update is called once per frame
    public override void DoAction()
    {
        Color color = this.GetColor();

        this.changeWall(color);
        this.changeLight(color);
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

    public abstract Color GetColor();

}

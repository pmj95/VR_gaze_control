using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private GameObject[] walls;
    private Light light;
    private List<Renderer> renderers;

    private Color[] colors = { Color.red, Color.white, Color.green, Color.yellow };

    private const int fps = 1;
    private int frameCounter = 0;
    private int colorCounter = 0;


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

    // Update is called once per frame
    void Update()
    {
        frameCounter++;
        if (frameCounter == fps)
        {
            frameCounter = 0;

            this.changeWall();
            this.changeLight();
            colorCounter++;
            colorCounter = colorCounter % colors.Length;
        }
    }

    void changeWall()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.SetColor("_Color", colors[colorCounter]);
        }
        
    }

    void changeLight()
    {
        light.color = colors[colorCounter];
    }
}

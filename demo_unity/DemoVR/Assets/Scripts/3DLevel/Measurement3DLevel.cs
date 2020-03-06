using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurement3DLevel : Measurement
{
    public string scaling;
    public List<int> buttonPositionLabel;
    public List<float> buttonPosition;

    public Measurement3DLevel(List<int> buttonPositionLabel, List<float> buttonPosition, Scaling scaling, string level, TriggerState currentState) : base(level, currentState)
    {
        this.buttonPositionLabel = buttonPositionLabel;
        this.buttonPosition = buttonPosition;
        this.scaling = scaling.ToString();
    }
}

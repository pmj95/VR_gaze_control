using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurement3DLevel : Measurement
{
    public List<int> buttonPositionLabel;
    public List<float> buttonPosition;

    public Measurement3DLevel(List<int> buttonPositionLabel, List<float> buttonPosition, Scaling scaling, string level, ControlState currentState) : base(level, currentState, scaling)
    {
        this.buttonPositionLabel = buttonPositionLabel;
        this.buttonPosition = buttonPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for the measurements in the 3DLevel environment
/// </summary>
public class Measurement3DLevel : Measurement
{
    public List<int> buttonPositionLabel;
    public List<float> buttonPosition;

    /// <summary>
    /// constructor for measurement in 3DLevel
    /// </summary>
    /// <param name="buttonPositionLabel">List with all button labes</param>
    /// <param name="buttonPosition">List with the position of the buttons</param>
    /// <param name="scaling">current scaling</param>
    /// <param name="level">name of the current level</param>
    /// <param name="currentState">current control state</param>
    public Measurement3DLevel(List<int> buttonPositionLabel, List<float> buttonPosition, Scaling scaling, string level, ControlState currentState) : base(level, currentState, scaling)
    {
        this.buttonPositionLabel = buttonPositionLabel;
        this.buttonPosition = buttonPosition;
    }
}

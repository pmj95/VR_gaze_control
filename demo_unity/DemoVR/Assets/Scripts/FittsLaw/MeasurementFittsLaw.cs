using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for the measurements in the fitts law environments
/// </summary>
public class MeasurementFittsLaw : Measurement
{
    /// <summary>
    /// constructor for measurement fitts law
    /// </summary>
    /// <param name="level">name of the current level</param>
    /// <param name="currentState">current control state</param>
    /// <param name="scaling">current scaling</param>
    public MeasurementFittsLaw(string level, ControlState currentState, Scaling scaling) : base(level, currentState, scaling)
    {
    }
}

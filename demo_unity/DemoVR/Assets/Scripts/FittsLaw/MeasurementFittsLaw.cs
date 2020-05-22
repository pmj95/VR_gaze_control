using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementFittsLaw : Measurement
{
    public MeasurementFittsLaw(Scaling scaling, string level, ControlState currentState) : base(level, currentState, scaling)
    {
    }
}

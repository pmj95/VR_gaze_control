using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasurementFittsLaw : Measurement
{
    public string scaling;

    public MeasurementFittsLaw(Scaling scaling, string level, TriggerState currentState) : base(level, currentState)
    {
        this.scaling = scaling.ToString();
    }
}

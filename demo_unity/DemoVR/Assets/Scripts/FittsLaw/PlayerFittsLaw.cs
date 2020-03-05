using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFittsLaw : BasePlayer
{
    public void ScaleSliderChanged(float number)
    {
        ScalingProperty.currentScaling = (Scaling)number;
    }
}

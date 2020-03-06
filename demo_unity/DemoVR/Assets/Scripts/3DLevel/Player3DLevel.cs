using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DLevel : BasePlayer
{
    public void ScaleSliderChanged(float number)
    {
        ScalingProperty.currentScaling = (Scaling)number;
    }
}

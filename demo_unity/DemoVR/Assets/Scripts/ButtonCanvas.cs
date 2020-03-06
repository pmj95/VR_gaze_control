using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCanvas : BaseMono
{
    private const float scalingX = 0.005f;
    private const float scalingY = 0.01f;
    private const float scalingZ = 0.02f;

    private void ScalingProperty_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.setScale(scalingX * ScalingProperty.scaleWidth, scalingY * ScalingProperty.scaleHeight, scalingZ);
    }

    private void setScale(float x, float y, float z)
    {
        Vector3 localScale = this.transform.localScale;
        localScale.x = x;
        localScale.y = y;
        localScale.z = z;
        this.transform.localScale = localScale;
    }

    private void resetPosition()
    {
        Vector3 pos = this.transform.localPosition;
        pos.y = 0.00001f;
        this.transform.localPosition = pos;
    }

    protected override void DoStart()
    {
        this.resetPosition();
        this.setScale(scalingX, scalingY, scalingZ);
        ScalingProperty.PropertyChanged += ScalingProperty_PropertyChanged;
    }

    protected override void DoAwake()
    {
        // nothing todo
    }

    protected override void DoDestroy()
    {
        ScalingProperty.PropertyChanged -= ScalingProperty_PropertyChanged;
        this.resetPosition();
    }

    protected override void OnCalibrationStarted()
    {
        // nothing todo
    }

    protected override void OnCalibrationRoutineDone()
    {
        // nothing todo
    }
}

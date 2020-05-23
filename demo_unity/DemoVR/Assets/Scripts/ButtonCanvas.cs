using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for changing the canvas of the button
/// </summary>
public class ButtonCanvas : BaseMono
{
    private const float scalingX = 0.005f;
    private const float scalingY = 0.01f;
    private const float scalingZ = 0.02f;

    /// <summary>
    /// scaling property changed event handler
    /// call setScale for setting the new scale
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ScalingProperty_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.setScale(scalingX * ScalingProperty.scaleWidth, scalingY * ScalingProperty.scaleHeight, scalingZ);
    }

    /// <summary>
    /// sets the scale by the given values
    /// </summary>
    /// <param name="x">scale on x axis</param>
    /// <param name="y">scale on y axis</param>
    /// <param name="z">scale on z axis</param>
    private void setScale(float x, float y, float z)
    {
        Vector3 localScale = this.transform.localScale;
        localScale.x = x;
        localScale.y = y;
        localScale.z = z;
        this.transform.localScale = localScale;
    }

    /// <summary>
    /// resets the position of the button in y axis
    /// </summary>
    private void resetPosition()
    {
        Vector3 pos = this.transform.localPosition;
        pos.y = 0.00001f;
        this.transform.localPosition = pos;
    }

    /// <summary>
    /// Performs while starting following tasks:
    /// 
    /// resets the position
    /// set the scaling to the const values
    /// register scaling property changed event handler
    /// </summary>
    protected override void DoStart()
    {
        this.resetPosition();
        this.setScale(scalingX, scalingY, scalingZ);
        ScalingProperty.PropertyChanged += ScalingProperty_PropertyChanged;
    }

    /// <summary>
    /// nothing to do when object will be awaked
    /// </summary>
    protected override void DoAwake()
    {
        // nothing todo
    }

    /// <summary>
    /// unregister scaling property changed event handler and resets position before destroying the object
    /// </summary>
    protected override void DoDestroy()
    {
        ScalingProperty.PropertyChanged -= ScalingProperty_PropertyChanged;
        this.resetPosition();
    }

    /// <summary>
    /// nothing to do when eyetracking calibration started
    /// </summary>
    protected override void OnCalibrationStarted()
    {
        // nothing todo
    }

    /// <summary>
    /// nothing to do when eyetracking calibration routine is done
    /// </summary>
    protected override void OnCalibrationRoutineDone()
    {
        // nothing todo
    }
}

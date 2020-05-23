using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for a wall
/// </summary>
public class Wall : BaseMono
{
    public BaseGame gameobject;

    /// <summary>
    /// nothing to do when object will be awaked
    /// </summary>
    protected override void DoAwake()
    {

    }

    /// <summary>
    /// nothing to do when object will be destroyed
    /// </summary>
    protected override void DoDestroy()
    {

    }

    /// <summary>
    /// nothing to do when object will be started
    /// </summary>
    protected override void DoStart()
    {

    }

    /// <summary>
    /// nothing to do when eyetracking calibration routine is done
    /// </summary>
    protected override void OnCalibrationRoutineDone()
    {

    }

    /// <summary>
    /// nothing to do when eyetracking calibration routine started
    /// </summary>
    protected override void OnCalibrationStarted()
    {

    }

    /// <summary>
    /// Performs an object clicked action in game object
    /// </summary>
    public void onTigger()
    {
        // do a fake button click
        this.gameobject.objectClicked(-1);
    }
}

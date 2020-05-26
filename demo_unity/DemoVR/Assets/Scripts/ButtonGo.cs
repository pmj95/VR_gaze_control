using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for the button go.
/// </summary>
public class ButtonGo : GeneralButton
{
    public BaseGame game;
    private bool isCalibrationRunning = false;

    /// <summary>
    /// starts the game when button will be clicked and the calibration routine is not running
    /// </summary>
    public override void DoAction()
    {
        if (!this.isCalibrationRunning)
        {
            game.startGame();
        }
    }

    /// <summary>
    /// nothing to do when object will be awaked
    /// </summary>
    protected override void DoAwake()
    {
        // Nothing todo
    }

    /// <summary>
    /// nothing to do when object will be destroyed
    /// </summary>
    protected override void DoDestroy()
    {
        // Nothing todo
    }

    /// <summary>
    /// set flag isCalibrationRunning to false when eyetracking calibration is stopped
    /// </summary>
    protected override void OnCalibrationRoutineDone()
    {
        this.isCalibrationRunning = false;
    }

    /// <summary>
    /// set flag isCalibrationRunning to true while eyetracking calibration is running
    /// </summary>
    protected override void OnCalibrationStarted()
    {
        this.isCalibrationRunning = true;
    }
}

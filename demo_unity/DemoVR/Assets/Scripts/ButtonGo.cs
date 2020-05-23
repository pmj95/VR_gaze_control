using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for the button go.
/// </summary>
public class ButtonGo : GeneralButton
{
    public BaseGame game;

    /// <summary>
    /// starts the game when button will be clicked
    /// </summary>
    public override void DoAction()
    {
        game.startGame();
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
    /// nothing to do when eyetracking calibration is done
    /// </summary>
    protected override void OnCalibrationRoutineDone()
    {
        // Nothing todo
    }

    /// <summary>
    /// nothing to do while eyetracking calibration
    /// </summary>
    protected override void OnCalibrationStarted()
    {
        // Nothing todo
    }
}

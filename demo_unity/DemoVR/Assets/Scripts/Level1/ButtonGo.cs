using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGo : GeneralButton
{
    public GameLevel1 game;

    protected override void DoStart()
    {
        base.DoStart();
    }

    public override void DoAction()
    {
        game.startGame();
    }

    protected override void DoAwake()
    {
        // Nothing todo
    }

    protected override void DoDestroy()
    {
        // Nothing todo
    }

    protected override void OnCalibrationRoutineDone()
    {
        // Nothing todo
    }

    protected override void OnCalibrationStarted()
    {
        // Nothing todo
    }
}

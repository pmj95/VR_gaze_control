using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BaseMono
{
    public BaseGame gameobject;
    
    protected override void DoAwake()
    {

    }

    protected override void DoDestroy()
    {

    }

    protected override void DoStart()
    {

    }

    protected override void OnCalibrationRoutineDone()
    {

    }

    protected override void OnCalibrationStarted()
    {

    }

    public void onTigger()
    {
        // do a fake button click
        this.gameobject.objectClicked(-1);
    }
}

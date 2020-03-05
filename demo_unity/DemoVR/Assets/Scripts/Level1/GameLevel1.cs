using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;

public class GameLevel1 : BaseGame 
{
    private string level = "Level1";

    protected override Measurement createMeasurement(TriggerState currentState)
    {
        return new MeasurementLevel1(1.0, 1.0, this.level, currentState);
    }

    protected override string getLevelDir()
    {
        return this.level + "\\";
    }
}

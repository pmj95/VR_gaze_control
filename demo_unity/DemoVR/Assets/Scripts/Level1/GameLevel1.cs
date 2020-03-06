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

    protected override List<int> getRandomSearchValues()
    {
        List<int> searchValues = new List<int>();

        for (int i = 1; i <= 16; i++)
        {
            searchValues.Add(i);
        }

        searchValues = searchValues.OrderBy(x => UnityEngine.Random.value).ToList<int>();
        searchValues.RemoveRange(4, searchValues.Count - 4);

        return searchValues;
    }
}

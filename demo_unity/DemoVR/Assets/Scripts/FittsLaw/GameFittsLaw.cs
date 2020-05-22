using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameFittsLaw : BaseGame
{
    public string level;

    public GameFittsLaw()
    {
        ScalingProperty.PropertyChanged += ScalingProperty_PropertyChanged;
    }

    protected override Measurement createMeasurement(ControlState currentState)
    {
        return new MeasurementFittsLaw(ScalingProperty.currentScaling, this.level, currentState);
    }

    protected override string getLevelDir()
    {
        return this.level + "\\" + ScalingProperty.currentScaling.ToString() + "\\";
    }

    protected override List<int> getRandomSearchValues()
    {
        List<int[]> searchValues = new List<int[]>();
        int[] testcase1 = { 2, 1, 16, 12, 10 };
        int[] testcase2 = { 3, 4, 13, 9, 11 };
        int[] testcase3 = { 7, 15, 13, 4, 3 };
        int[] testcase4 = { 8, 16, 1, 4, 13 };
        int[] testcase5 = { 5, 6, 14, 8, 13 };
        searchValues.Add(testcase1);
        searchValues.Add(testcase2);
        searchValues.Add(testcase3);
        searchValues.Add(testcase4);
        searchValues.Add(testcase5);

        searchValues = searchValues.OrderBy(x => UnityEngine.Random.value).ToList<int[]>();
        return searchValues.First<int[]>().ToList<int>();
    }

    private void ScalingProperty_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.envChanged();
    }
}

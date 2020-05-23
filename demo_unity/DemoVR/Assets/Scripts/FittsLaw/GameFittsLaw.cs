using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// class for the game in FittsLaw
/// </summary>
public class GameFittsLaw : BaseGame
{
    /// <summary>
    /// creates a new measurement with the needed information
    /// </summary>
    /// <param name="currentState">current control state</param>
    /// <returns>new created Measurement</returns>
    protected override Measurement createMeasurement(ControlState currentState)
    {
        return new MeasurementFittsLaw(this.level, currentState, ScalingProperty.currentScaling);
    }

    /// <summary>
    /// returns a list with search values.
    /// There are existing 5 testcases with predefined order of search values
    /// </summary>
    /// <returns>List with the serach values</returns>
    protected override List<int> getSearchValues()
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
}

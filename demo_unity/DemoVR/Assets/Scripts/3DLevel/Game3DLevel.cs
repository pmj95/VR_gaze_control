using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for the game in 3D-Level
/// </summary>
public class Game3DLevel : BaseGame
{
    private List<int> buttonPositionLabel;
    private List<float> buttonPosition;

    /// <summary>
    /// resets the whole game data
    /// places the button in random position in y axis
    /// </summary>
    protected override void resetGame()
    {
        this.buttonPositionLabel = new List<int>();
        this.buttonPosition = new List<float>();
        List<GameObject> special = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpecialButton"));
        this.placeButton(special.First(), 5.0f);
        this.placeButton(special.Last(), 0.00001f);

        List<GameObject> btns = new List<GameObject>(GameObject.FindGameObjectsWithTag("ButtonCanvas"));
        btns = btns.OrderBy(x => Random.value).ToList<GameObject>();

        foreach (GameObject go in btns)
        {
            this.placeButton(go, Random.Range(0.00001f, 5.0f));
        }

        base.resetGame();
    }

    /// <summary>
    /// creates a new measurement with the needed information
    /// </summary>
    /// <param name="currentState">current control state</param>
    /// <returns>new created Measurement</returns>
    protected override Measurement createMeasurement(ControlState currentState)
    {
        return new Measurement3DLevel(this.buttonPositionLabel, this.buttonPosition, ScalingProperty.currentScaling, this.level, currentState);
    }

    /// <summary>
    /// choose 5 random numbers for the game
    /// </summary>
    /// <returns>List with searched values</returns>
    protected override List<int> getSearchValues()
    {
        List<int> searchValues = new List<int>();

        for (int i = 1; i <= 16; i++)
        {
            searchValues.Add(i);
        }

        searchValues = searchValues.OrderBy(x => UnityEngine.Random.value).ToList<int>();
        searchValues.RemoveRange(5, searchValues.Count - 5);

        return searchValues;
    }

    /// <summary>
    /// set a new position for a button with the given values
    /// </summary>
    /// <param name="go">GameObject of the Button</param>
    /// <param name="yValue">Value of the y axis</param>
    private void placeButton(GameObject go, float yValue)
    {
        Vector3 pos = go.transform.localPosition;
        pos.y = yValue;
        go.transform.localPosition = pos;

        ButtonNumber btnNum = go.GetComponentInChildren<ButtonNumber>();
        this.buttonPositionLabel.Add(btnNum.number);
        this.buttonPosition.Add(pos.y);
    }
}

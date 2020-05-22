using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Game3DLevel : BaseGame
{
    public string level;
    private List<int> buttonPositionLabel;
    private List<float> buttonPosition;

    protected override void resetGame()
    {
        this.buttonPositionLabel = new List<int>();
        this.buttonPosition = new List<float>();
        List<GameObject> special = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpecialButton"));
        this.doTransformation(special.First(), 5.0f);
        this.doTransformation(special.Last(), 0.00001f);

        List<GameObject> btns = new List<GameObject>(GameObject.FindGameObjectsWithTag("ButtonCanvas"));
        btns = btns.OrderBy(x => Random.value).ToList<GameObject>();

        foreach (GameObject go in btns)
        {
            this.doTransformation(go, Random.Range(0.00001f, 5.0f));
        }

        base.resetGame();
    }

    protected override Measurement createMeasurement(ControlState currentState)
    {
        return new Measurement3DLevel(this.buttonPositionLabel, this.buttonPosition, ScalingProperty.currentScaling, this.level, currentState);
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

    private void doTransformation(GameObject go, float yValue)
    {
        Vector3 pos = go.transform.localPosition;
        pos.y = yValue;
        go.transform.localPosition = pos;

        ButtonNumber btnNum = go.GetComponentInChildren<ButtonNumber>();
        this.buttonPositionLabel.Add(btnNum.number);
        this.buttonPosition.Add(pos.y);
    }
}

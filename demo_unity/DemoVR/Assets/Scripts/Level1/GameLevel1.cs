using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;

public class GameLevel1 : BaseMono
{
    private Measurement measurement;
    private List<int> searchValues;
    public Text instructionField;

    private void resetGame()
    {
        this.searchValues.Clear();
        this.measurement = new Measurement();

        for (int i = 1; i <= 16; i++)
        {
            this.searchValues.Add(i);
        }

        this.searchValues = this.searchValues.OrderBy(x => Random.value).ToList<int>();
        this.searchValues.RemoveRange(4, this.searchValues.Count - 4);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Level1Button"))
        {
            ColorBlock colors = go.GetComponent<Button>().colors;
            colors.normalColor = Color.white;
            go.GetComponent<Button>().colors = colors;
        }
        
    }

    private void setInstruction()
    {
        string instruction = string.Empty;
        if (this.searchValues.Count > 0)
        {
            int number = this.searchValues.First();
            instruction = "Please select: " + number.ToString();
        }
        else
        {
            instruction = "Congratulations!";
            this.endGame();
        }

        this.instructionField.text = instruction;
    }

    public void startGame()
    {
        this.resetGame();   
        this.setInstruction();
        this.measurement.start();
    }

    public void endGame()
    {
        this.measurement.stop();
        string jsonstring = JsonUtility.ToJson(this.measurement, true);
        string filename = System.DateTime.Now.ToFileTime().ToString() + ".json";
        File.WriteAllText("Measurement\\" + filename, jsonstring);
    }

    public bool ButtonClicked(int number)
    {
        bool retVal;

        if (this.searchValues.IndexOf(number) == 0)
        {
            this.measurement.addMeasurement(true);
            this.searchValues.Remove(number);
            this.setInstruction();
            retVal = true;
        }
        else
        {
            this.measurement.addMeasurement(false);
            retVal = false;
        }

        return retVal;
    }

    protected override void DoStart()
    {
        this.searchValues = new List<int>();
    }

    protected override void DoAwake()
    {
        // nothing todo
    }

    protected override void DoDestroy()
    {
        // nothing todo
    }

    protected override void OnCalibrationStarted()
    {
        // nothing todo
    }

    protected override void OnCalibrationRoutineDone()
    {
        // nothing todo
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseGame : BaseMono
{
    private Measurement measurement;
    private bool isplaying = false;
    private List<int> searchValues;
    public Text instructionField;

    protected virtual void resetGame()
    {
        this.measurement = this.createMeasurement(StateTrigger.currentState);
        this.searchValues = this.getRandomSearchValues();

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
        this.isplaying = true;
        this.measurement.start();
    }

    public void endGame()
    {
        if (this.isplaying)
        {
            this.isplaying = false;
            this.measurement.stop();
            this.saveMeasurement();
        }
    }

    public bool ButtonClicked(int number)
    {
        bool retVal;

        if (this.searchValues.IndexOf(number) == 0)
        {
            this.measurement.addMeasurement(number, true);
            this.searchValues.Remove(number);
            this.setInstruction();
            retVal = true;
        }
        else
        {
            this.measurement.addMeasurement(number, false);
            retVal = false;
        }

        return retVal;
    }

    protected void envChanged()
    {
        if (isplaying)
        {
            this.resetGame();
            this.instructionField.text = "Canceled Game! Trigger changed";
            this.isplaying = false;
        }
    }

    private void StateTrigger_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.envChanged();
    }

    private void saveMeasurement()
    {
        DateTime date = DateTime.Now;
        string filename = date.ToString("yyyy_MM_dd_HH_mm_ss") + ".json";
        string jsonstring = JsonUtility.ToJson(this.measurement, true);
        string levelFolder = "Measurement\\" + this.getLevelDir();

        if (!Directory.Exists(levelFolder))
        {
            Directory.CreateDirectory(levelFolder);
        }

        File.WriteAllText(levelFolder + filename, jsonstring);
    }

    protected abstract List<int> getRandomSearchValues();

    protected abstract string getLevelDir();

    protected abstract Measurement createMeasurement(TriggerState currentState);

    protected override void DoStart()
    {
        StateTrigger.PropertyChanged += StateTrigger_PropertyChanged;
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

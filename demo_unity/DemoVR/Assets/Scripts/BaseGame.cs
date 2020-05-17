using PupilLabs;
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
    private int logCounter = 0;
    public Text instructionField;
    public GazeController gazeController;
    public Transform gazeOrigin;
    public int gazeLogDistance;

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
            if(this.measurement != null)
            {
                this.measurement.addMeasurement(number, false);
            }
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

    private void subscribeEyetracking()
    {
        if (StateTrigger.currentState == TriggerState.BlinkingEye
            || StateTrigger.currentState == TriggerState.EyeTrigger)
        {
            this.gazeController.OnReceive3dGaze += GazeController_OnReceive3dGaze;
        }
    }

    private void unsubscribeEyetracking()
    {
        try
        {
            this.gazeController.OnReceive3dGaze -= this.GazeController_OnReceive3dGaze;
        }
        catch (Exception ex)
        {
            // nothing to do
        }
    }

    private void GazeController_OnReceive3dGaze(GazeData obj)
    {
        if (this.isplaying == true && this.logCounter == 0)
        {
            Vector3 origin = this.gazeOrigin.position;
            String res = "(" + origin.x.ToString() + ", " + origin.y.ToString() + ", " + origin.z.ToString() + ")";
            this.measurement.addGazePoint(res); 
            Debug.Log(res);
            //Vector3 direction = this.gazeOrigin.TransformDirection(obj.GazeDirection);

            //if (Physics.Raycast(origin, direction, out RaycastHit hit))
            //{
            //    Vector3 point = hit.point;
            //    this.measurement.addGazePoint(point.ToString());
            //    Debug.Log(point.ToString());
            //}
        }
        
        this.logCounter++;
        this.logCounter %= this.gazeLogDistance;
    }

    private void StateTrigger_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.unsubscribeEyetracking();
        this.envChanged();
        this.subscribeEyetracking();
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
        this.unsubscribeEyetracking();
    }

    protected override void OnCalibrationRoutineDone()
    {
        this.subscribeEyetracking();
    }
}

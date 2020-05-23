using PupilLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// base game class
/// </summary>
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
    public string level;

    /// <summary>
    /// constructor add PropertyChanged-Listener for the ScalingProperty
    /// </summary>
    public BaseGame()
    {
        ScalingProperty.PropertyChanged += ScalingProperty_PropertyChanged;
    }

    /// <summary>
    /// set the instructions text of the game
    /// </summary>
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

    /// <summary>
    /// starts the game
    /// For this the game data need a reset, the instruction needs to be set,
    /// the Flag isplaying will be set to true and the measurement starts.
    /// </summary>
    public void startGame()
    {
        this.resetGame();
        this.setInstruction();
        this.isplaying = true;
        this.measurement.start();
    }

    /// <summary>
    /// Ends the game only if the the flag is playing is true
    /// stops and save the measurement
    /// </summary>
    public void endGame()
    {
        if (this.isplaying)
        {
            this.isplaying = false;
            this.measurement.stop();
            this.saveMeasurement();
        }
    }

    /// <summary>
    /// Checks if the clicked object is the searched one.
    /// Adds the result to the measurement.
    /// </summary>
    /// <param name="number">number of the clicked object</param>
    /// <returns>true: correct objetc; false: wrong object</returns>
    public bool objectClicked(int number)
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

    /// <summary>
    /// Resets the whole game data
    /// creates a new Measurement; create new searchValues; Resets the color of the buttons
    /// </summary>
    protected virtual void resetGame()
    {
        this.measurement = this.createMeasurement(ControlStateProperty.currentState);
        this.searchValues = this.getSearchValues();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Level1Button"))
        {
            ColorBlock colors = go.GetComponent<Button>().colors;
            colors.normalColor = Color.white;
            go.GetComponent<Button>().colors = colors;
        }
    }

    /// <summary>
    /// performs a game reset when the game is already started and something of the game environment changed
    /// </summary>
    private void envChanged()
    {
        if (isplaying)
        {
            this.resetGame();
            this.instructionField.text = "Canceled Game! Trigger changed";
            this.isplaying = false;
        }
    }

    /// <summary>
    /// subscribes the eyetracking data. Only when the correct ControlState is set.
    /// </summary>
    private void subscribeEyetracking()
    {
        if (ControlStateProperty.currentState == ControlState.BlinkingEye
            || ControlStateProperty.currentState == ControlState.EyeTrigger)
        {
            this.gazeController.OnReceive3dGaze += GazeController_OnReceive3dGaze;
        }
    }

    /// <summary>
    /// unsubscribes the eyetracking data
    /// </summary>
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

    /// <summary>
    /// Method which receives the GazeData from the eyetracking.
    /// 
    /// Logs the gaze data in the local variable gazeLogDistance defined log distance
    /// </summary>
    /// <param name="obj">gaze data</param>
    private void GazeController_OnReceive3dGaze(GazeData obj)
    {
        if (this.isplaying == true && this.logCounter == 0)
        {
            Vector3 origin = this.gazeOrigin.position;
            this.measurement.addGazePoint(origin.x, origin.y, origin.z); 
        }
        
        this.logCounter++;
        this.logCounter %= this.gazeLogDistance;
    }

    /// <summary>
    /// Will be called when the control state will be changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ControlState_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.unsubscribeEyetracking();
        this.envChanged();
        this.subscribeEyetracking();
    }

    /// <summary>
    /// saves the measurement and stores it with the current timestamp in the level directory
    /// </summary>
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

    /// <summary>
    /// gets all the search values
    /// </summary>
    /// <returns>list with search values</returns>
    protected abstract List<int> getSearchValues();

    /// <summary>
    /// gets the dirname of the level
    /// </summary>
    /// <returns>dirname of the level</returns>
    protected string getLevelDir()
    {
        return this.level + "\\" + ScalingProperty.currentScaling.ToString() + "\\";
    }

    /// <summary>
    /// creates a new measurement with the needed information
    /// </summary>
    /// <param name="currentState">current control state</param>
    /// <returns>new created Measurement</returns>
    protected abstract Measurement createMeasurement(ControlState currentState);

    /// <summary>
    /// Sets the property changed event handler for the control state property 
    /// </summary>
    protected override void DoStart()
    {
        ControlStateProperty.PropertyChanged += ControlState_PropertyChanged;
        this.searchValues = new List<int>();
    }

    /// <summary>
    /// nothing to do when object will be awaked
    /// </summary>
    protected override void DoAwake()
    {
        // nothing todo
    }

    /// <summary>
    /// nothing to do when object will be destroyed
    /// </summary>
    protected override void DoDestroy()
    {
        // nothing todo
    }

    /// <summary>
    /// unsubscribes from eyetracking while calibration
    /// </summary>
    protected override void OnCalibrationStarted()
    {
        this.unsubscribeEyetracking();
    }

    /// <summary>
    /// subscribes to eyetracking after calibration
    /// </summary>
    protected override void OnCalibrationRoutineDone()
    {
        this.subscribeEyetracking();
    }

    /// <summary>
    /// Will be called when the scaling changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ScalingProperty_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.envChanged();
    }
}

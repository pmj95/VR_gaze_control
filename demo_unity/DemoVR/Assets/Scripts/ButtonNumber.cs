using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// class for the buttons with numbers
/// </summary>
public class ButtonNumber: GeneralButton
{
    public BaseGame game;
    public int number;
    private bool isCalibrationRunning = false;
    private Color buttonColor = Color.red;

    /// <summary>
    /// gets the game object after loading
    /// </summary>
    protected override void DoStart()
    {
        base.DoStart();
        GameObject go = GameObject.FindGameObjectWithTag("Game");
        game = go.GetComponent<BaseGame>();
    }

    /// <summary>
    /// set flag isCalibrationRunning to true while eyetracking calibration is running
    /// </summary>
    protected override void OnCalibrationStarted()
    {
        this.isCalibrationRunning = true;
    }

    /// <summary>
    /// set flag isCalibrationRunning to false when eyetracking calibration is stopped
    /// </summary>
    protected override void OnCalibrationRoutineDone()
    {
        this.isCalibrationRunning = false;
    }

    /// <summary>
    /// Sets the text of the Button to the defined number
    /// </summary>
    protected override void DoAwake()
    {
        Text text = this.GetComponentInChildren<Text>();
        text.text = this.number.ToString();
    }

    /// <summary>
    /// nothing to do when object will be destroyed
    /// </summary>
    protected override void DoDestroy()
    {
        // nothing todo
    }

    /// <summary>
    /// Performs the action when button is clicked. 
    /// Asks the game if this is the searched button, when yes than the color of the button will be set to green otherwise to red
    /// </summary>
    public override void DoAction()
    {
        if (!this.isCalibrationRunning)
        {
            // Do relevant stuff
            if (game.objectClicked(this.number))
            {
                buttonColor = Color.green;
            }

            ColorBlock colors = this.GetComponent<Button>().colors;
            colors.normalColor = buttonColor;
            this.GetComponent<Button>().colors = colors;
        }
    }
}

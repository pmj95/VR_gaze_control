using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber: GeneralButton
{
    public int number;
    private bool calibrationRunning = false;
    private Color buttonColor = Color.red;

    protected override void OnCalibrationStarted()
    {
        this.calibrationRunning = true;
    }

    protected override void OnCalibrationRoutineDone()
    {
        this.calibrationRunning = false;
    }

    protected override void DoAwake()
    {
        Text text = this.GetComponentInChildren<Text>();
        text.text = this.number.ToString();
    }

    protected override void DoDestroy()
    {
        // nothing to do
    }

    // Update is called once per frame
    public override void DoAction()
    {
        // Do relevant stuff
        if (GameLevel1.getInstance().ButtonClicked(this.number))
        {
            buttonColor = Color.green;    
        }

        ColorBlock colors = this.GetComponent<Button>().colors;
        colors.normalColor = buttonColor;
        this.GetComponent<Button>().colors = colors;
    }
}

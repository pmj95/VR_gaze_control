using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber: GeneralButton
{
    public GameLevel1 game;
    public int number;
    private bool isCalibrationRunning = false;
    private Color buttonColor = Color.red;

    protected override void DoStart()
    {
        base.DoStart();
        this.game = GameObject.FindObjectOfType<GameLevel1>();
    }

    protected override void OnCalibrationStarted()
    {
        this.isCalibrationRunning = true;
    }

    protected override void OnCalibrationRoutineDone()
    {
        this.isCalibrationRunning = false;
    }

    protected override void DoAwake()
    {
        Text text = this.GetComponentInChildren<Text>();
        text.text = this.number.ToString();
    }

    protected override void DoDestroy()
    {
        base.DoStart();
        // nothing to do
    }

    // Update is called once per frame
    public override void DoAction()
    {
        if (!this.isCalibrationRunning)
        {
            // Do relevant stuff
            if (game.ButtonClicked(this.number))
            {
                buttonColor = Color.green;
            }

            ColorBlock colors = this.GetComponent<Button>().colors;
            colors.normalColor = buttonColor;
            this.GetComponent<Button>().colors = colors;
        }
    }
}

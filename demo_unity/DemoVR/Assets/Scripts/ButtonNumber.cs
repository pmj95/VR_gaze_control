using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber: GeneralButton
{
    public BaseGame game;
    public int number;
    private bool isCalibrationRunning = false;
    private Color buttonColor = Color.red;

    protected override void DoStart()
    {
        base.DoStart();
        GameObject go = GameObject.FindGameObjectWithTag("Game");
        game = go.GetComponent<BaseGame>();
        // GameObject go = new GameObject();
        // CylinderCollider cc = this.gameObject.AddComponent<CylinderCollider>();
        // cc.radius = 65;
        // cc.height = 1;
        // cc.boxCount = 5;
        // cc.widthScale = 1.6f;
        // go.transform.localScale = this.transform.localScale;
        // go.transform.position = this.transform.position;
        // //go.transform.parent = this.transform;
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
        // nothing todo
    }

    // Update is called once per frame
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

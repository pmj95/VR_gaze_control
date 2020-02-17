using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameLevel1
{
    private static GameLevel1 instance;
    private Measurement measurement;
    private List<int> searchValues;
    private Text instructionField;

    private GameLevel1()
    {
        var gameObject = GameObject.FindGameObjectWithTag("Instruction");
        this.instructionField = GameObject.FindGameObjectWithTag("Instruction").GetComponent<Text>();
        this.measurement = new Measurement();
        this.searchValues = new List<int>();
    }

    public static GameLevel1 getInstance()
    {
        if (instance == null)
        {
            instance = new GameLevel1();
        }

        return instance;
    }

    public void startGame()
    {
        this.measurement.start();
        
        for (int i = 1; i <= 16; i++)
        {
            this.searchValues.Add(i);
        }

        this.searchValues = this.searchValues.OrderBy(x => Random.value).ToList<int>();
        this.setInstruction();
    }

    public void endGame()
    {
        this.measurement.stop();
    }

    public bool ButtonClicked(int number)
    {
        bool retVal = false;

        if (this.searchValues.IndexOf(number) == 0)
        {
            this.searchValues.Remove(number);
            this.setInstruction();
            retVal = true;
        }

        return retVal;
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
        }

        this.instructionField.text = instruction;
    }
}

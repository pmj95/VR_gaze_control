using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ControlState { 
    LaserTrigger = 0, 
    EyeTrigger = 1, 
    LaserBlinking = 2, 
    BlinkingEye = 3 
};

public class ControlStateProperty
{
    public static event PropertyChangedEventHandler PropertyChanged;

    private static ControlState cs;

    public static ControlState currentState
    {
        get { return cs; }
        set
        {
            cs = value;

            // Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged();
        }
    }

    private static void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }
}
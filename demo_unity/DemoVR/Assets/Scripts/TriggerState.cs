using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum TriggerState { 
    LaserTrigger = 0, 
    EyeTrigger = 1, 
    LaserBlinking = 2, 
    BlinkingEye = 3 
};

public class StateTrigger
{
    public static event PropertyChangedEventHandler PropertyChanged;

    private static TriggerState cs;

    public static TriggerState currentState
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
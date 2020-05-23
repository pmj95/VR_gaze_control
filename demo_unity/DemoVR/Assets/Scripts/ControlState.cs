using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// enumeration control state
/// available values:
/// LaserTrigger = 0
/// EyeTrigger = 1
/// LaserBlinking = 2
/// BlinkingEye = 3
/// </summary>
public enum ControlState { 
    LaserTrigger = 0, 
    EyeTrigger = 1, 
    LaserBlinking = 2, 
    BlinkingEye = 3 
};

/// <summary>
/// Control state property
/// </summary>
public class ControlStateProperty
{
    public static event PropertyChangedEventHandler PropertyChanged;

    private static ControlState cs;

    /// <summary>
    /// getter and setter for the current control state
    /// </summary>
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

    /// <summary>
    /// invokes a property changed event for controlStateProperty
    /// </summary>
    /// <param name="name"></param>
    private static void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }
}
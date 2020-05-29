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
public enum ControlState
{
    LaserTrigger = 0,
    EyeTrigger = 1,
    LaserBlinking = 2,
    BlinkingEye = 3
};

/// <summary>
/// ControlState property changed event arguments
/// </summary>
public class ControlStatePropertyChangedEventArgs : PropertyChangedEventArgs
{
    public virtual ControlState oldValue { get; }
    public virtual ControlState newValue { get; }

    public ControlStatePropertyChangedEventArgs(string name, ControlState oldValue, ControlState newValue) : base(name)
    {
        this.oldValue = oldValue;
        this.newValue = newValue;
    }
}

/// <summary>
/// EventHandler for ControlStatePropertyChanged
/// </summary>
/// <param name="sender">sender</param>
/// <param name="e">args</param>
public delegate void ControlStatePropertyChangedEventHandler(object sender, ControlStatePropertyChangedEventArgs e);


/// <summary>
/// Control state property
/// </summary>
public class ControlStateProperty
{
    public static event ControlStatePropertyChangedEventHandler PropertyChanged;

    private static ControlState cs;

    /// <summary>
    /// getter and setter for the current control state
    /// </summary>
    public static ControlState currentState
    {
        get { return cs; }
        set
        {
            ControlState oldValue = cs;
            cs = value;

            // Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged(oldValue, cs);
        }
    }

    /// <summary>
    /// invokes a property changed event for controlStateProperty
    /// </summary>
    /// <param name="name"></param>
    private static void OnPropertyChanged(ControlState oldValue, ControlState newValue, [CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(null, new ControlStatePropertyChangedEventArgs(name, oldValue, newValue));
    }
}
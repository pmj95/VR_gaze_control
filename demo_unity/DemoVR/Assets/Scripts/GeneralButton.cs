using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// basis class for buttons
/// </summary>
public abstract class GeneralButton : BaseMono
{
    /// <summary>
    /// will be called when button is clicked.
    /// </summary>
    public abstract void DoAction();

    /// <summary>
    /// add the method DoAction as Listener to button click
    /// </summary>
    protected override void DoStart()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(this.DoAction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GeneralButton : BaseMono
{
    public abstract void DoAction();

    protected override void DoStart()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(this.DoAction);
    }
}

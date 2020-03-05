using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCanvas : MonoBehaviour
{
    private const float scalingX = 0.005f;
    private const float scalingY = 0.01f;
    private const float scalingZ = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        this.setScale(scalingX, scalingY, scalingZ);
        ScalingProperty.PropertyChanged += ScalingProperty_PropertyChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ScalingProperty_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        this.setScale(scalingX * ScalingProperty.scaleWidth, scalingY * ScalingProperty.scaleHeight, scalingZ);
    }

    private void setScale(float x, float y, float z)
    {
        Vector3 localScale = this.transform.localScale;
        localScale.x = x;
        localScale.y = y;
        localScale.z = z;
        this.transform.localScale = localScale;
    }
}

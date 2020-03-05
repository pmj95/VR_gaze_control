using System;

[Serializable]
public class MeasurementLevel1 : Measurement
{
    public double scaleWidth;
    public double scaleHeight;

    public MeasurementLevel1(double scaleWidth, double scaleHeight, string level, TriggerState currentState) : base(level, currentState)
    {
        this.scaleHeight = scaleHeight;
        this.scaleWidth = scaleWidth;
    }
}

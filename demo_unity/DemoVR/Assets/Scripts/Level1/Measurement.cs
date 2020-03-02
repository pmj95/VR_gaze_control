using System;
using System.Collections.Generic;

[Serializable]
public class Measurement
{
    public long startTime;
    public long stopTime;
    public TriggerState currentState;
    public List<long> timestamps;
    public List<bool> successfull;

    public Measurement(TriggerState currentState)
    {
        this.timestamps = new List<long>();
        this.successfull = new List<bool>();
        this.currentState = currentState;
    }

    public void start()
    {
        this.timestamps.Clear();
        this.successfull.Clear();
        this.startTime = this.getTimestamp();
    }

    public void addMeasurement(bool val)
    {
        this.timestamps.Add(this.getTimestamp());
        this.successfull.Add(val);
    }

    public void stop()
    {
        this.stopTime = this.getTimestamp();
    }

    private long getTimestamp()
    {
        DateTime time = DateTime.Now;
        long timestamp = time.Hour * 60;
        timestamp = (timestamp + time.Minute) * 60;
        timestamp = (timestamp + time.Second) * 1000;
        timestamp = timestamp + time.Millisecond;
        return timestamp;
    }
}

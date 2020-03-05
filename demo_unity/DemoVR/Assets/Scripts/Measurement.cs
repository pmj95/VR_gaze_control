using System;
using System.Collections.Generic;

[Serializable]
public abstract class Measurement
{
    public string level;
    public string currentState;
    public long startTime;
    public long stopTime;
    public List<int> buttonsPressed;
    public List<long> timestamps;
    public List<bool> successfull;

    public Measurement(string level, TriggerState currentState)
    {
        this.timestamps = new List<long>();
        this.successfull = new List<bool>();
        this.buttonsPressed = new List<int>();
        this.currentState = currentState.ToString();
        this.level = level;
    }

    public void start()
    {
        this.timestamps.Clear();
        this.successfull.Clear();
        this.startTime = this.getTimestamp();
    }

    public void addMeasurement(int button, bool val)
    {
        this.timestamps.Add(this.getTimestamp());
        this.successfull.Add(val);
        this.buttonsPressed.Add(button);
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

using System;
using System.Collections.Generic;
using System.Numerics;

[Serializable]
public abstract class Measurement
{
    public string level;
    public string currentState;
    public long startTime;
    public long stopTime;
    public List<string> gazePoints;
    public List<string> action;
    public string scaling;

    public Measurement(string level, ControlState currentState, Scaling scaling)
    {
        this.gazePoints = new List<string>();
        this.action = new List<string>();
        this.currentState = currentState.ToString();
        this.scaling = scaling.ToString();
        this.level = level;
    }

    public void start()
    {
        this.gazePoints.Clear();
        this.action.Clear();
        this.startTime = this.getTimestamp();
    }

    public void addMeasurement(int button, bool val)
    {
        string measure = String.Format("{0},{1},{2}", this.getTimestamp(), button, val);
        this.action.Add(measure);
    }

    public void addGazePoint(float x, float y, float z)
    {
        String point = "(" + x.ToString() + ", " + y.ToString() + ", " + z.ToString() + ")";
        this.gazePoints.Add(point);
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

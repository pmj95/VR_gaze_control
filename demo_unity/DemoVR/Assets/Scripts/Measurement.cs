using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// measurement class; is serializable
/// </summary>
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

    /// <summary>
    /// constructor for a measurement; initializes the measurement
    /// </summary>
    /// <param name="level">name of the current level</param>
    /// <param name="currentState">current control state</param>
    /// <param name="scaling">current scaling</param>
    public Measurement(string level, ControlState currentState, Scaling scaling)
    {
        this.gazePoints = new List<string>();
        this.action = new List<string>();
        this.currentState = currentState.ToString();
        this.scaling = scaling.ToString();
        this.level = level;
    }

    /// <summary>
    /// starts the measurement
    /// </summary>
    public void start()
    {
        this.gazePoints.Clear();
        this.action.Clear();
        this.startTime = this.getTimestamp();
    }

    /// <summary>
    /// add a new measurement
    /// </summary>
    /// <param name="button">number of the button</param>
    /// <param name="val">true for correct button; false for the wrong</param>
    public void addMeasurement(int button, bool val)
    {
        string measure = String.Format("{0},{1},{2}", this.getTimestamp(), button, val);
        this.action.Add(measure);
    }

    /// <summary>
    /// add a gaze point
    /// </summary>
    /// <param name="x">position in x axis of the gaze</param>
    /// <param name="y">position in y axis of the gaze</param>
    /// <param name="z">position in z axis of the gaze</param>
    public void addGazePoint(float x, float y, float z)
    {
        string point = String.Format("({0}, {1}, {2})", x.ToString(), y.ToString(), z.ToString());
        this.gazePoints.Add(point);
    }

    /// <summary>
    /// stops the measurement
    /// </summary>
    public void stop()
    {
        this.stopTime = this.getTimestamp();
    }

    /// <summary>
    /// returns the current timestamp in milliseconds
    /// </summary>
    /// <returns>current timestamp</returns>
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

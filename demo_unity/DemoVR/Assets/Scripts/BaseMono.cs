using PupilLabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all unity relevant classes.
/// </summary>
public abstract class BaseMono : MonoBehaviour
{
    private CalibrationController calibrationController;

    /// <summary>
    /// forwards the Start to subclasses with method DoStart
    /// </summary>
    private void Start()
    {
        this.DoStart();
    }

    /// <summary>
    /// Gets the CalibrationController and subscribes eventhandler for OnCalibrationStarted and OnCalibrationRoutineDone
    /// </summary>
    private void Awake()
    {
        this.calibrationController = GameObject.FindObjectOfType<CalibrationController>();
        calibrationController.OnCalibrationStarted += OnCalibrationStarted;
        calibrationController.OnCalibrationRoutineDone += OnCalibrationRoutineDone;
        this.DoAwake();
    }

    /// <summary>
    /// unsubscribes eventhandler for OnCalibrationStarted and OnCalibrationRoutineDone
    /// </summary>
    private void OnDestroy()
    {
        calibrationController.OnCalibrationStarted -= OnCalibrationStarted;
        calibrationController.OnCalibrationRoutineDone -= OnCalibrationRoutineDone;
        this.DoDestroy();
    }

    /// <summary>
    /// Start routine for subclasses
    /// </summary>
    protected abstract void DoStart();

    /// <summary>
    /// awake routine for subclasses
    /// </summary>
    protected abstract void DoAwake();

    /// <summary>
    /// destroy routine for the subclasses
    /// </summary>
    protected abstract void DoDestroy();

    /// <summary>
    /// Eventhandler for OnCalibrationStarted
    /// </summary>
    protected abstract void OnCalibrationStarted();

    /// <summary>
    /// Eventhandler for OnCalibrationRoutineDone
    /// </summary>
    protected abstract void OnCalibrationRoutineDone();
}

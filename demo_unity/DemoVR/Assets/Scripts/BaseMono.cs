using PupilLabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMono : MonoBehaviour
{
    private CalibrationController calibrationController;

    private void Start()
    {
        this.DoStart();
    }

    private void Awake()
    {
        this.calibrationController = GameObject.FindObjectOfType<CalibrationController>();
        calibrationController.OnCalibrationStarted += OnCalibrationStarted;
        calibrationController.OnCalibrationRoutineDone += OnCalibrationRoutineDone;
        this.DoAwake();
    }

    private void OnDestroy()
    {
        calibrationController.OnCalibrationStarted -= OnCalibrationStarted;
        calibrationController.OnCalibrationRoutineDone -= OnCalibrationRoutineDone;
        this.DoDestroy();
    }

    protected abstract void DoStart();

    protected abstract void DoAwake();

    protected abstract void DoDestroy();

    protected abstract void OnCalibrationStarted();

    protected abstract void OnCalibrationRoutineDone();
}

using PupilLabs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMono : MonoBehaviour
{
    private CalibrationController calibrationController;

    void Start()
    {
        Debug.Log("Start");
    }

    void Awake()
    {
        this.calibrationController = GameObject.FindObjectOfType<CalibrationController>();
        Debug.Log("Awake");
        calibrationController.OnCalibrationStarted += OnCalibrationStarted;
        calibrationController.OnCalibrationRoutineDone += OnCalibrationRoutineDone;
        this.DoAwake();
    }

    void OnDestroy()
    {
        calibrationController.OnCalibrationStarted -= OnCalibrationStarted;
        calibrationController.OnCalibrationRoutineDone -= OnCalibrationRoutineDone;
        this.DoDestroy();
    }

    protected abstract void DoAwake();

    protected abstract void DoDestroy();

    protected abstract void OnCalibrationStarted();

    protected abstract void OnCalibrationRoutineDone();
}

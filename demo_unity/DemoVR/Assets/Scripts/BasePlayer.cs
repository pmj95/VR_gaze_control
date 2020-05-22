using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PupilLabs;
using Valve.VR.Extras;
using UnityEngine.UI;
using System;

public abstract class BasePlayer : BaseMono
{
    /**
     * Unity
     */
    public Transform gazeOrigin;
    public Slider laserEyeTrackingSlider;
    public Slider triggerBlinkDetectionSlider;

    /**
     * SteamVR
     */
    public SteamVR_LaserPointer laserPointer;

    /**
     * Pupil Labs
     */
    public SubscriptionsController subscriptionsController;
    public GazeController gazeController;
    public GazeVisualizer gazeVisualizer;
    private RequestController requestCtrl;
    private GazeData lastGaze;

    /**
     * Andere Variablen
     */
    public float blinkDuration = 0.5f;
    private bool blinking = false;
    private bool onBlinking = false;

    #region Override Stuff

    protected override void DoStart()
    {
    }

    protected override void DoAwake()
    {
        this.requestCtrl = this.subscriptionsController.requestCtrl;
        this.setInitialTriggerState();
    }

    protected override void DoDestroy()
    {
        this.unsubscribeAll();
    }

    protected override void OnCalibrationStarted()
    {
        this.unsubscribeAll();
    }

    protected override void OnCalibrationRoutineDone()
    {
        this.subscribeAll();
    }

    #endregion

    #region Subscription

    private void subscribeAll()
    {
        this.subscribeBlink();
        this.subscribeEyetracking();
        this.subscribeLaserPointer();
    }

    private void unsubscribeAll()
    {
        this.unsubscribeBlink();
        this.unsubscribeEyetracking();
        this.unsubscribeLaserPointer();
    }

    private void subscribeLaserPointer()
    {
        if (this.laserPointer != null
            && (ControlStateProperty.currentState == ControlState.LaserTrigger
            || ControlStateProperty.currentState == ControlState.EyeTrigger
            || ControlStateProperty.currentState == ControlState.LaserBlinking))
        {
            this.laserPointer.PointerClick += this.SteamVR_LaserPointer_PointerClick;
        }

        if (ControlStateProperty.currentState == ControlState.EyeTrigger || ControlStateProperty.currentState == ControlState.BlinkingEye)
        {
            this.laserPointer.thickness = 0f;
        }
        else if (ControlStateProperty.currentState == ControlState.LaserTrigger || ControlStateProperty.currentState == ControlState.LaserBlinking)
        {
            this.laserPointer.thickness = 0.002f;
        }
    }

    private void unsubscribeLaserPointer()
    {
        if (this.laserPointer != null
            && (ControlStateProperty.currentState == ControlState.LaserTrigger
            || ControlStateProperty.currentState == ControlState.EyeTrigger
            || ControlStateProperty.currentState == ControlState.LaserBlinking))
        {
            this.laserPointer.PointerClick -= this.SteamVR_LaserPointer_PointerClick;
        }

        this.laserPointer.thickness = 0f;
    }

    private void subscribeEyetracking()
    {
        if (ControlStateProperty.currentState == ControlState.BlinkingEye
            || ControlStateProperty.currentState == ControlState.EyeTrigger)
        {
            this.gazeController.OnReceive3dGaze += this.GazeController_OnReceive3dGaze;
        }

        if (ControlStateProperty.currentState == ControlState.EyeTrigger || ControlStateProperty.currentState == ControlState.BlinkingEye)
        {
            this.gazeVisualizer.gameObject.SetActive(true);
        }
        else if (ControlStateProperty.currentState == ControlState.LaserTrigger || ControlStateProperty.currentState == ControlState.LaserBlinking)
        {
            this.gazeVisualizer.gameObject.SetActive(false);
        }
    }

    private void unsubscribeEyetracking()
    {
        if (ControlStateProperty.currentState == ControlState.BlinkingEye
            || ControlStateProperty.currentState == ControlState.EyeTrigger)
        {
            this.gazeController.OnReceive3dGaze -= this.GazeController_OnReceive3dGaze;
        }
    }

    private void subscribeBlink()
    {
        if (requestCtrl != null 
            && (ControlStateProperty.currentState == ControlState.BlinkingEye
            || ControlStateProperty.currentState == ControlState.LaserBlinking))
        {
            requestCtrl.OnConnected += StartBlinkSubscription;

            if (requestCtrl.IsConnected)
            {
                StartBlinkSubscription();
            }
        }
    }

    private void unsubscribeBlink()
    {
        if (requestCtrl != null
            && (ControlStateProperty.currentState == ControlState.BlinkingEye
            || ControlStateProperty.currentState == ControlState.LaserBlinking))
        {
            requestCtrl.OnConnected -= StartBlinkSubscription;

            if (requestCtrl.IsConnected)
            {
                StopBlinkSubscription();
            }
        }
    }

    #endregion

    #region BlinkDetection

    private void StartBlinkSubscription()
    {
        Debug.Log("StartBlinkSubscription");

        subscriptionsController.SubscribeTo("blinks", ReceiveBlinkData);

        requestCtrl.StartPlugin(
            "Blink_Detection",
            new Dictionary<string, object> {
                    { "history_length", 0.2f },
                    { "onset_confidence_threshold", 0.5f },
                    { "offset_confidence_threshold", 0.5f }
            }
        );
    }

    private void StopBlinkSubscription()
    {
        Debug.Log("StopBlinkSubscription");

        requestCtrl.StopPlugin("Blink_Detection");

        subscriptionsController.UnsubscribeFrom("blinks", ReceiveBlinkData);
    }

    private void ReceiveBlinkData(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame = null)
    {
        if (dictionary.ContainsKey("timestamp") && !this.onBlinking)
        {
            this.StartCoroutine(this.Blink(this.blinkDuration));
        }
    }

    public IEnumerator Blink(float duration)
    {
        if (!this.blinking)
        {
            this.onBlinking = true;
            this.blinking = true;

            switch (ControlStateProperty.currentState)
            {
                case ControlState.LaserTrigger:
                    break;
                case ControlState.EyeTrigger:
                    break;
                case ControlState.LaserBlinking:
                    {
                        this.checkCollision(this.laserPointer.transform.position, this.laserPointer.transform.forward);
                        break;
                    }
                case ControlState.BlinkingEye:
                    {
                        Vector3 origin = gazeOrigin.position;
                        Vector3 direction = gazeOrigin.TransformDirection(this.lastGaze.GazeDirection);
                        this.checkCollision(origin, direction);
                        break;
                    }
                default:
                    break;
            }

            yield return new WaitForSecondsRealtime(duration);
            this.onBlinking = false;
            this.blinking = false;
        }

        yield break;
    }

    #endregion

    #region TriggerEvent

    private void SteamVR_LaserPointer_PointerClick(object sender, PointerEventArgs e)
    {
        switch (ControlStateProperty.currentState)
        {
            case ControlState.LaserTrigger:
                {
                    if (sender is MonoBehaviour)
                    {
                        MonoBehaviour s = (MonoBehaviour)sender;
                        this.checkCollision(s.transform.position, s.transform.forward);
                    }

                    break;
                }
            case ControlState.EyeTrigger:
                {
                    Vector3 origin = gazeOrigin.position;
                    Vector3 direction = gazeOrigin.TransformDirection(this.lastGaze.GazeDirection);
                    this.checkCollision(origin, direction);
                    break;
                }
            case ControlState.LaserBlinking:
                break;
            case ControlState.BlinkingEye:
                break;
            default:
                break;
        }
    }

    #endregion

    #region GazeEvent

    private void GazeController_OnReceive3dGaze(GazeData obj)
    {
        this.lastGaze = obj;
    }

    #endregion

    private void checkCollision(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            GameObject currObject = hit.collider.gameObject;

            if (currObject.TryGetComponent<Slider>(out Slider slider))
            {
                slider.value = slider.value == 0 ? 1 : 0;
            }
            else if (currObject.TryGetComponent<Button>(out Button button))
            {
                button.onClick.Invoke();
            }
            else if (currObject.CompareTag("ButtonCollider") 
                && currObject.transform.parent != null 
                && currObject.transform.parent.parent != null
                && currObject.transform.parent.parent.TryGetComponent<Button>(out Button buttonCollider))
            {
                buttonCollider.onClick.Invoke();
            }
            else if (currObject.TryGetComponent<Wall>(out Wall wall))
            {
                wall.onTigger();
            }
        }
    }

    private void setTriggerState(ControlState state)
    {
        this.unsubscribeAll();
        ControlStateProperty.currentState = state;
        this.subscribeAll();
    }

    private void setInitialTriggerState()
    {
        if (this.laserEyeTrackingSlider.value == 0)
        {
            if (this.triggerBlinkDetectionSlider.value == 0)
            {
                ControlStateProperty.currentState = ControlState.LaserTrigger;
            }
            else if (this.triggerBlinkDetectionSlider.value == 1)
            {
                ControlStateProperty.currentState = ControlState.LaserBlinking;
            }
        }
        else if (this.laserEyeTrackingSlider.value == 1)
        {
            if (this.triggerBlinkDetectionSlider.value == 0)
            {
                ControlStateProperty.currentState = ControlState.EyeTrigger;
            }
            else if (this.triggerBlinkDetectionSlider.value == 1)
            {
                ControlStateProperty.currentState = ControlState.BlinkingEye;
            }
        }

        this.subscribeAll();
    }

    #region Sliderchanged

    public void LaserEyeTrackingSliderChanged()
    {
        int state = (int)ControlStateProperty.currentState;
        state = state ^ 0b01;
        this.setTriggerState((ControlState)state);
    }

    public void TriggerBlinkDetectionSliderChanged()
    {
        int state = (int)ControlStateProperty.currentState;
        state = state ^ 0b10;
        this.setTriggerState((ControlState)state);
    }

    #endregion
}

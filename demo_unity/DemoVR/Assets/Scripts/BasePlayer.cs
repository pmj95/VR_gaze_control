using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PupilLabs;
using Valve.VR.Extras;
using UnityEngine.UI;
using System;

/// <summary>
/// Basis class for the player
/// </summary>
public abstract class BasePlayer : BaseMono
{
    /**
     * Unity
     */
    public Transform gazeOrigin;

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

    #region Overrides

    /// <summary>
    /// nothing to do while starting
    /// </summary>
    protected override void DoStart()
    {
        // nothing to do
    }

    /// <summary>
    /// Gets the request controller for eyetracking and sets the initial control state
    /// </summary>
    protected override void DoAwake()
    {
        this.requestCtrl = this.subscriptionsController.requestCtrl;
        ControlStateProperty.PropertyChanged += ControlStateProperty_PropertyChanged;
        this.subscribeAll(ControlStateProperty.currentState);
    }

    /// <summary>
    /// unsubscribes from all before object will be destroyed
    /// </summary>
    protected override void DoDestroy()
    {
        this.unsubscribeAll(ControlStateProperty.currentState);
    }

    /// <summary>
    /// unsubscribes from all when eyetracking calibration started
    /// </summary>
    protected override void OnCalibrationStarted()
    {
        this.unsubscribeAll(ControlStateProperty.currentState);
    }

    /// <summary>
    /// subscribes to all after eyetracking calibration 
    /// </summary>
    protected override void OnCalibrationRoutineDone()
    {
        this.subscribeAll(ControlStateProperty.currentState);
    }

    #endregion

    #region Subscription

    /// <summary>
    /// subscribes to blink detection, eyetracking and laserpointer
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void subscribeAll(ControlState controlState)
    {
        this.subscribeBlink(controlState);
        this.subscribeEyetracking(controlState);
        this.subscribeLaserPointer(controlState);
    }

    /// <summary>
    /// unsubscribes from blink detection, eyetracking and laserpointer
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void unsubscribeAll(ControlState controlState)
    {
        this.unsubscribeBlink(controlState);
        this.unsubscribeEyetracking(controlState);
        this.unsubscribeLaserPointer(controlState);
    }

    /// <summary>
    /// subscribes to laserpointer.
    /// registers the pointclick event handler
    /// changes thickness of laserpointer 
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void subscribeLaserPointer(ControlState controlState)
    {
        if (this.laserPointer != null
            && (controlState == ControlState.LaserTrigger
            || controlState == ControlState.EyeTrigger
            || controlState == ControlState.LaserBlinking))
        {
            this.laserPointer.PointerClick += this.SteamVR_LaserPointer_PointerClick;
        }

        if (controlState == ControlState.EyeTrigger
            || controlState == ControlState.BlinkingEye)
        {
            this.laserPointer.thickness = 0f;
        }
        else if (controlState == ControlState.LaserTrigger
            || controlState == ControlState.LaserBlinking)
        {
            this.laserPointer.thickness = 0.002f;
        }
    }

    /// <summary>
    /// unsubscribe from laserpointer
    /// unregister point click event handler
    /// set the thickness of the laserpointer to zero
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void unsubscribeLaserPointer(ControlState controlState)
    {
        if (this.laserPointer != null
            && (controlState == ControlState.LaserTrigger
            || controlState == ControlState.EyeTrigger
            || controlState == ControlState.LaserBlinking))
        {
            this.laserPointer.PointerClick -= this.SteamVR_LaserPointer_PointerClick;
        }

        this.laserPointer.thickness = 0f;
    }

    /// <summary>
    /// subscribes to eyetracking
    /// registers on receive 3d gaze event handler
    /// set the visibility of the gazevisualizer in dependence of control state
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void subscribeEyetracking(ControlState controlState)
    {
        if (controlState == ControlState.BlinkingEye
            || controlState == ControlState.EyeTrigger)
        {
            this.gazeController.OnReceive3dGaze += this.GazeController_OnReceive3dGaze;
        }

        if (this.gazeVisualizer != null)
        {
            if (controlState == ControlState.EyeTrigger
                || controlState == ControlState.BlinkingEye)
            {
                this.gazeVisualizer.gameObject.SetActive(true);
            }
            else if (controlState == ControlState.LaserTrigger
                || controlState == ControlState.LaserBlinking)
            {
                this.gazeVisualizer.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// unsubscribes from eyetracking
    /// unregisters on receive 3d gaze event handler
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void unsubscribeEyetracking(ControlState controlState)
    {
        if (controlState == ControlState.BlinkingEye
            || controlState == ControlState.EyeTrigger)
        {
            this.gazeController.OnReceive3dGaze -= this.GazeController_OnReceive3dGaze;
        }
    }

    /// <summary>
    /// subscribes to blink detection
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void subscribeBlink(ControlState controlState)
    {
        if (requestCtrl != null
            && (controlState == ControlState.BlinkingEye
            || controlState == ControlState.LaserBlinking))
        {
            requestCtrl.OnConnected += StartBlinkSubscription;

            if (requestCtrl.IsConnected)
            {
                StartBlinkSubscription();
            }
        }
    }

    /// <summary>
    /// unsubscribes from blink detection
    /// </summary>
    /// <param name="controlState">ControlState, according to which the subscription is to be carried out/param>
    private void unsubscribeBlink(ControlState controlState)
    {
        if (requestCtrl != null
            && (controlState == ControlState.BlinkingEye
            || controlState == ControlState.LaserBlinking))
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

    /// <summary>
    /// starts blink detection subscribtion. 
    /// </summary>
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

    /// <summary>
    /// stops blink detection subscribtion. 
    /// </summary>
    private void StopBlinkSubscription()
    {
        Debug.Log("StopBlinkSubscription");

        requestCtrl.StopPlugin("Blink_Detection");

        subscriptionsController.UnsubscribeFrom("blinks", ReceiveBlinkData);
    }

    /// <summary>
    /// receives blink data
    /// starts co routine only when onBlinking is false
    /// </summary>
    /// <param name="topic">topic of reiceives</param>
    /// <param name="dictionary">data</param>
    /// <param name="thirdFrame"></param>
    private void ReceiveBlinkData(string topic, Dictionary<string, object> dictionary, byte[] thirdFrame = null)
    {
        if (dictionary.ContainsKey("timestamp") && !this.onBlinking)
        {
            this.StartCoroutine(this.Blink(this.blinkDuration));
        }
    }

    /// <summary>
    /// checks collision when blinking is detected
    /// should be started in coroutine; sets blinking to true
    /// </summary>
    /// <param name="duration">duration how long does the co routine wait until finishing</param>
    /// <returns>IEnumerator</returns>
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

    /// <summary>
    /// laserpointer pointer clicked event handler.
    /// checks collision when a click from laser pointer will be performed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// on reive 3d gaze event handler
    /// stores the last gaze in local variable lastGaze
    /// </summary>
    /// <param name="obj">gaze data</param>
    private void GazeController_OnReceive3dGaze(GazeData obj)
    {
        this.lastGaze = obj;
    }

    #endregion

    /// <summary>
    /// checks the collision with the given parameters
    /// </summary>
    /// <param name="origin">position of the object</param>
    /// <param name="direction">direction of object</param>
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

    /// <summary>
    /// Will be called when the controlstate changes. 
    /// unsubscribes all by old control state 
    /// subscribes all by new control state
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ControlStateProperty_PropertyChanged(object sender, ControlStatePropertyChangedEventArgs e)
    {
        this.unsubscribeAll(e.oldValue);
        this.subscribeAll(e.newValue);
    }
}

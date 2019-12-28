using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
//using NetMQ;
//using NetMQ.Sockets;
//using MsgPack.Serialization;

public class ViveGameController : MonoBehaviour
{

    public SteamVR_Action_Boolean myAction;
    public Hand hand;
    public GameObject projectile;

    private void OnEnable()
    {
        if (hand==null)
        {
            hand = this.GetComponent<Hand>();

        }

        if (myAction==null)
        {
            Debug.LogError("dumm");
            return;
        }

        myAction.AddOnChangeListener(OnMyActionChange, hand.handType);
    }

    private void OnDisable()
    {
        if (myAction != null)
        {
            myAction.RemoveOnChangeListener(OnMyActionChange, hand.handType);
        }
    }

    private void OnMyActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSources, bool newValue)
    {
        if (newValue)
        {
            Instantiate(projectile, hand.transform.position, hand.transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //using (var subscriber = new SubscriberSocket())
        //{
        //    subscriber.Connect("tcp://127.0.0.1:500020");
        //    subscriber.Subscribe("SUB_PORT");

        //    while (true)
        //    {
        //        var topic = subscriber.ReceiveFrameString();
        //        var msg = subscriber.ReceiveFrameString();
        //        var serializer = MessagePackSerializer.Get<string>();
        //        //var unpackedObject = serializer.Unpack(msg);
        //        //Console.WriteLine("From Publisher: {0} {1}", topic, msg);
        //    }
        //}

    }
}

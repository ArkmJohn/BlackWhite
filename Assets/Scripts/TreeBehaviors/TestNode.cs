using UnityEngine;
using System.Collections;
using System;

public class TestNode : Node {

    public string Message;

    public TestNode(string myMessage)
    {
        Message = myMessage;
    }

    public override void reset()
    {
        Start();
    }

    public override void act(Enemy me)
    {
        Debug.Log(Message);
        SuccessState();
    }

}

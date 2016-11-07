using UnityEngine;
using System.Collections;
using System;

public class TestNodeA : Node {

    public string Message;

    public TestNodeA(string myMessage)
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

using IntelligentMachine.FSM;
using System;
using UnityEngine;


public class DemoState : IntelliFSMState
{
    public override string Name
    {
        get
        {
            return "Demo";
        }
    }

    public override void Enter()
    {
        Debug.Log("Entering Demo State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting Demo State");

    }

    public override void PostUpdate()
    {
        Debug.Log("Running Demo State PostUpdate");

    }

    public override void PreUpdate()
    {
        Debug.Log("Running DemoState PreUpdate");

    }

    public override void Update()
    {
        Debug.Log("Updating Demo State");

    }
}



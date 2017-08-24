using IntelligentMachine.FSM;
using System;
using UnityEngine;

public class AltDemoState : IntelliFSMState
{
    public override string Name
    {
        get
        {
            return "AltDemo";
        }
    }

    public override void Enter()
    {
        Debug.Log("Entering AltDemo State");
    }

    public override void Exit()
    {
        Debug.Log("Exiting AltDemo State");
    }

    public override void PostUpdate()
    {
        Debug.Log("Running AltDemo State PostUpdate"); ;
    }

    public override void PreUpdate()
    {
        Debug.Log("Running AltDemo State PreUpdate");
    }

    public override void Update()
    {
        Debug.Log("Updating AltDemo State");
    }
}



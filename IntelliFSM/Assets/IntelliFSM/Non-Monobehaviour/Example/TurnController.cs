using IntelligentMachine.FSM;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : IntelliFSM
{
    /*
     * These 4 variables are not important to the function of IntelliFSM.
     * They are just here to autoswitch states to show off a VERY basic usage of the FSM.
     * 
     */

    private List<IntelliFSMState> stateCache;
    private float timer = 0;
    private float changeStateTime = 3f;
    private int stateIndex = 0;

    public TurnController()
    {
        Init();
    }

    private void Init()
    {
        //Here we are just setting up our timer's and everything to automate state changing for a demo.
        stateCache = new List<IntelliFSMState>();

        stateCache.Add(new DemoState());
        stateCache.Add(new AltDemoState());

        for (var i = 0; i < stateCache.Count; i++)
            AddState(stateCache[i]);

        SwitchState(stateCache[stateIndex].Name);
        stateIndex++;
    }

    /*
     * Post and Pre update do not need to be manually called, they are called by the base.Update() method at the proper time.
     */
    protected override void PostUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= changeStateTime)
        {

            SwitchState(stateCache[stateIndex].Name);

            timer = 0;
            stateIndex++;

            if (stateIndex >= stateCache.Count)
                stateIndex = 0;
        }
    }

    //If you override Update to add anything, MAKE SURE to call the base update method
    //otherwise your state will not have any of its pre/post/normal update methods called.
    public override void Update()
    {
        base.Update();
    }

    //Since this is abstract, like PostUpdate, if you do not want to run anything just leave it blank.
    //If it bothers you that this exists and is blank, just change it from abstract to virtual in the base class.
    protected override void PreUpdate()
    {
        
    }
}



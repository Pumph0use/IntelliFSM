using System;
using System.Collections.Generic;
using UnityEngine;

namespace IntelligentMachine.FSM
{
    public abstract class IntelliFSM
    {
        #region Delegates

        public Action EnterState; //Setup the state
        public Action PreUpdateState; //Runs before update
        public Action UpdateState;
        public Action PostUpdateState; //Runs after update
        public Action ExitState; //Handle anything before exiting the state

        #endregion

        #region State Info

        public IntelliFSMState prevState;
        public IntelliFSMState curState;

        #endregion
        
        #region Variables

        public Dictionary<string, IntelliFSMState> states;

        #endregion
        
        #region Update

        protected abstract void PreUpdate();

        public virtual void Update()
        {
            PreUpdate();

            if (PreUpdateState != null)
                PreUpdateState();
            if (UpdateState != null)
                UpdateState();
            if (PostUpdateState != null)
                PostUpdateState();

            PostUpdate();
        }

        protected abstract void PostUpdate();

        #endregion

        #region FSM Control API

        protected void AddState(IntelliFSMState newState)
        {
            if (states == null)
                states = new Dictionary<string, IntelliFSMState>();

            newState.Init();

            states.Add(newState.Name, newState);
        }

        protected void SwitchState(string stateName)
        {
            IntelliFSMState result = null;
            if (states.TryGetValue(stateName, out result))
            {
                if (result != null)
                {
                    if (ExitState != null)
                        ExitState();
                    prevState = curState;
                    curState = result;
                    foreach (var key in result.stateMethods.Keys)
                    {
                        Action a = null;
                        if (result.stateMethods.TryGetValue(key, out a))
                        {
                            if (a != null)
                                AssignDelegates(key, a);
                        }
                    }

                    if (EnterState != null)
                        EnterState();
                }
            }
            else
                Debug.LogError("State: " + stateName + " Doesn't exist in IntelliFSM");
        }
        #endregion
        
        #region Private Methods

        private void AssignDelegates(IntelliFSMPhase key, Action a)
        {
            switch (key)
            {
                case IntelliFSMPhase.Enter:
                    EnterState = a;
                    break;
                case IntelliFSMPhase.PreUpdate:
                    PreUpdateState = a;
                    break;
                case IntelliFSMPhase.Update:
                    UpdateState = a;
                    break;
                case IntelliFSMPhase.PostUpdate:
                    PostUpdateState = a;
                    break;
                case IntelliFSMPhase.Exit:
                    ExitState = a;
                    break;
            }
        } 
        #endregion
    }
}


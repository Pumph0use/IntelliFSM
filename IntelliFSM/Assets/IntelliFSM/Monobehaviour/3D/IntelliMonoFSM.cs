using System;
using System.Collections.Generic;
using UnityEngine;

namespace IntelligentMachine.FSM
{
    public abstract class IntelliMonoFSM : MonoBehaviour
    {
        #region IntelliFSM Delegates

        public Action EnterState; //Setup the state
        public Action PreUpdateState; //Runs before update
        public Action UpdateState;
        public Action PostUpdateState; //Runs after update
        public Action ExitState; //Handle anything before exiting the state 

        #endregion

        #region IntelliMonoFSM Delegates

        public Action<Collision> OnCollisionEnterState;
        public Action<Collision> OnCollisionStayState;
        public Action<Collision> OnCollisionExitState;

        public Action<Collider> OnTriggerEnterState;
        public Action<Collider> OnTriggerStayState;
        public Action<Collider> OnTriggerExitState;

        #endregion
        
        #region State Info

        public IntelliMonoFSMState prevState;
        public IntelliMonoFSMState curState;

        #endregion
        
        #region Variables

        public Dictionary<string, IntelliMonoFSMState> states;

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
        
        #region Monobehaviour Overridable Methods

        public virtual void OnCollisionEnter(Collision col)
        {
            if (OnCollisionEnterState != null)
                OnCollisionEnterState(col);
        }

        public virtual void OnCollisionStay(Collision col)
        {
            if (OnCollisionStayState != null)
                OnCollisionStayState(col);
        }

        public virtual void OnCollisionExit(Collision col)
        {
            if (OnCollisionExitState != null)
                OnCollisionExitState(col);
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (OnTriggerEnterState != null)
                OnTriggerEnterState(other);
        }

        public virtual void OnTriggerStay(Collider other)
        {
            if (OnTriggerStayState != null)
                OnTriggerStayState(other);
        }

        public virtual void OnTriggerExit(Collider other)
        {
            if (OnTriggerExitState != null)
                OnTriggerExitState(other);
        }

        #endregion

        #region FSM Control API

        protected void AddState(IntelliMonoFSMState newState)
        {
            if (states == null)
                states = new Dictionary<string, IntelliMonoFSMState>();

            newState.Init();

            states.Add(newState.Name, newState);
        }

        protected void SwitchState(string stateName)
        {
            IntelliMonoFSMState result = null;
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

                    foreach (var key in result.collisionMethods.Keys)
                    {
                        Action<Collision> a = null;
                        if (result.collisionMethods.TryGetValue(key, out a))
                        {
                            if (a != null)
                                AssignDelegates(key, a);
                        }
                    }

                    foreach (var key in result.triggerMethods.Keys)
                    {
                        Action<Collider> a = null;
                        if (result.triggerMethods.TryGetValue(key, out a))
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
                Debug.LogError("State: " + stateName + " Doesn't exist in IntelliMonoFSM");
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

        private void AssignDelegates(IntelliFSMCollisionPhase key, Action<Collision> a)
        {
            switch (key)
            {
                case IntelliFSMCollisionPhase.OnColliderEnter:
                    OnCollisionEnterState = a;
                    break;
                case IntelliFSMCollisionPhase.OnColliderStay:
                    OnCollisionStayState = a;
                    break;
                case IntelliFSMCollisionPhase.OnColliderExit:
                    OnCollisionExitState = a;
                    break;
            }
        }

        private void AssignDelegates(IntelliFSMTriggerPhase key, Action<Collider> a)
        {
            switch (key)
            {
                case IntelliFSMTriggerPhase.OnTriggerEnter:
                    OnTriggerEnterState = a;
                    break;
                case IntelliFSMTriggerPhase.OnTriggerStay:
                    OnTriggerStayState = a;
                    break;
                case IntelliFSMTriggerPhase.OnTriggerExit:
                    OnTriggerExitState = a;
                    break;
            }
        }

        #endregion

    }
}
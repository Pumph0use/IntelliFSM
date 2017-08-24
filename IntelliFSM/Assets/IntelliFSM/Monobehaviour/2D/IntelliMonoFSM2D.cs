using System;
using System.Collections.Generic;
using UnityEngine;

namespace IntelligentMachine.FSM
{
    public abstract class IntelliMonoFSM2D : MonoBehaviour
    {
        #region IntelliFSM Delegates

        public Action EnterState; //Setup the state
        public Action PreUpdateState; //Runs before update
        public Action UpdateState;
        public Action PostUpdateState; //Runs after update
        public Action ExitState; //Handle anything before exiting the state 

        #endregion

        #region IntelliMonoFSM Delegates

        public Action<Collision2D> OnCollisionEnterState2D;
        public Action<Collision2D> OnCollisionStayState2D;
        public Action<Collision2D> OnCollisionExitState2D;

        public Action<Collider2D> OnTriggerEnterState2D;
        public Action<Collider2D> OnTriggerStayState2D;
        public Action<Collider2D> OnTriggerExitState2D;

        #endregion

        #region State Info

        public IntelliMonoFSMState2D prevState;
        public IntelliMonoFSMState2D curState;

        #endregion

        #region Variables

        public Dictionary<string, IntelliMonoFSMState2D> states;

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

        public virtual void OnCollisionEnter2D(Collision2D col)
        {
            if (OnCollisionEnterState2D != null)
                OnCollisionEnterState2D(col);
        }

        public virtual void OnCollisionStay2D(Collision2D col)
        {
            if (OnCollisionStayState2D != null)
                OnCollisionStayState2D(col);
        }

        public virtual void OnCollisionExit2D(Collision2D col)
        {
            if (OnCollisionExitState2D != null)
                OnCollisionExitState2D(col);
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (OnTriggerEnterState2D != null)
                OnTriggerEnterState2D(other);
        }

        public virtual void OnTriggerStay2D(Collider2D other)
        {
            if (OnTriggerStayState2D != null)
                OnTriggerStayState2D(other);
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (OnTriggerExitState2D != null)
                OnTriggerExitState2D(other);
        }

        #endregion

        #region FSM Control API

        protected void AddState(IntelliMonoFSMState2D newState)
        {
            if (states == null)
                states = new Dictionary<string, IntelliMonoFSMState2D>();

            newState.Init();

            states.Add(newState.Name, newState);
        }

        protected void SwitchState(string stateName)
        {
            IntelliMonoFSMState2D result = null;
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
                        Action<Collision2D> a = null;
                        if (result.collisionMethods.TryGetValue(key, out a))
                        {
                            if (a != null)
                                AssignDelegates(key, a);
                        }
                    }

                    foreach (var key in result.triggerMethods.Keys)
                    {
                        Action<Collider2D> a = null;
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

        private void AssignDelegates(IntelliFSMCollisionPhase2D key, Action<Collision2D> a)
        {
            switch (key)
            {
                case IntelliFSMCollisionPhase2D.OnColliderEnter2D:
                    OnCollisionEnterState2D = a;
                    break;
                case IntelliFSMCollisionPhase2D.OnColliderStay2D:
                    OnCollisionStayState2D = a;
                    break;
                case IntelliFSMCollisionPhase2D.OnColliderExit2D:
                    OnCollisionExitState2D = a;
                    break;
            }
        }

        private void AssignDelegates(IntelliFSMTriggerPhase2D key, Action<Collider2D> a)
        {
            switch (key)
            {
                case IntelliFSMTriggerPhase2D.OnTriggerEnter2D:
                    OnTriggerEnterState2D = a;
                    break;
                case IntelliFSMTriggerPhase2D.OnTriggerStay2D:
                    OnTriggerStayState2D = a;
                    break;
                case IntelliFSMTriggerPhase2D.OnTriggerExit2D:
                    OnTriggerExitState2D = a;
                    break;
            }
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace IntelligentMachine.FSM
{
    public abstract class IntelliMonoFSMState : IntelliFSMState
    {
        #region Variables

        public Dictionary<IntelliFSMCollisionPhase, Action<Collision>> collisionMethods;
        public Dictionary<IntelliFSMTriggerPhase, Action<Collider>> triggerMethods;

        #endregion
        
        #region Initialization

        public override void Init()
        {
            base.Init();

            if (collisionMethods == null)
                collisionMethods = new Dictionary<IntelliFSMCollisionPhase, Action<Collision>>();
            var colStates = Utilities.GetEnumValues<IntelliFSMCollisionPhase>();
            foreach (var state in colStates)
            {
                collisionMethods.Add(state, Utilities.CreateDelegate<Action<Collision>>(this, state.ToString()));
            }

            if (triggerMethods == null)
                triggerMethods = new Dictionary<IntelliFSMTriggerPhase, Action<Collider>>();
            var trigStates = Utilities.GetEnumValues<IntelliFSMTriggerPhase>();
            foreach (var state in trigStates)
            {
                triggerMethods.Add(state, Utilities.CreateDelegate<Action<Collider>>(this, state.ToString()));
            }
        }

        #endregion

        #region Abstract Methods

        public abstract void OnCollisionEnter(Collision col);
        public abstract void OnCollisionStay(Collision col);
        public abstract void OnCollisionExit(Collision col);

        public abstract void OnTriggerEnter(Collider col);
        public abstract void OnTriggerStay(Collider col);
        public abstract void OnTriggerExit(Collider col);

        #endregion
    }
}


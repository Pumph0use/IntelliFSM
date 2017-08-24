using System;
using System.Collections.Generic;
using UnityEngine;

namespace IntelligentMachine.FSM
{
    public abstract class IntelliMonoFSMState2D : IntelliFSMState
    {
        #region Variables

        public Dictionary<IntelliFSMCollisionPhase2D, Action<Collision2D>> collisionMethods;
        public Dictionary<IntelliFSMTriggerPhase2D, Action<Collider2D>> triggerMethods;

        #endregion

        #region Initialization

        public override void Init()
        {
            base.Init();

            if (collisionMethods == null)
                collisionMethods = new Dictionary<IntelliFSMCollisionPhase2D, Action<Collision2D>>();
            var colStates = Utilities.GetEnumValues<IntelliFSMCollisionPhase2D>();
            foreach (var state in colStates)
            {
                collisionMethods.Add(state, Utilities.CreateDelegate<Action<Collision2D>>(this, state.ToString()));
            }

            if (triggerMethods == null)
                triggerMethods = new Dictionary<IntelliFSMTriggerPhase2D, Action<Collider2D>>();
            var trigStates = Utilities.GetEnumValues<IntelliFSMTriggerPhase2D>();
            foreach (var state in trigStates)
            {
                triggerMethods.Add(state, Utilities.CreateDelegate<Action<Collider2D>>(this, state.ToString()));
            }
        }

        #endregion

        #region Abstract Methods

        public abstract void OnCollisionEnter2D(Collision2D col);
        public abstract void OnCollisionStay2D(Collision2D col);
        public abstract void OnCollisionExit2D(Collision2D col);

        public abstract void OnTriggerEnter2D(Collider2D col);
        public abstract void OnTriggerStay2D(Collider2D col);
        public abstract void OnTriggerExit2D(Collider2D col);

        #endregion
    }
}


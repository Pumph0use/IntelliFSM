using System;
using System.Collections.Generic;

namespace IntelligentMachine.FSM
{
    public abstract class IntelliFSMState
    {
        #region Variables

        public Dictionary<IntelliFSMPhase, Action> stateMethods;

        public abstract string Name
        {
            get;
        }

        #endregion

        #region Initialization

        public virtual void Init()
        {
            if (stateMethods == null)
                stateMethods = new Dictionary<IntelliFSMPhase, Action>();
            var states = Utilities.GetEnumValues<IntelliFSMPhase>();
            foreach (var state in states)
            {
                stateMethods.Add(state, Utilities.CreateDelegate<Action>(this, state.ToString()));
            }
        }

        #endregion

        #region Abstract Methods

        public abstract void Enter();
        public abstract void PreUpdate();
        public abstract void Update();
        public abstract void PostUpdate();
        public abstract void Exit();

        #endregion
    }
}


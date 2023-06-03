using System;
using UnityEngine;

namespace AVG_Command
{
    [Serializable]
    public abstract class CommandBase
    {
        [HideInInspector]
        [SerializeField]
        protected string class_name = "command name";

        public CommandBase()
        {
            class_name = this.GetType().Name;
        }

        public abstract void Execute();

        public abstract void OnUpdate();

        public abstract bool IsFinished();
    }

}
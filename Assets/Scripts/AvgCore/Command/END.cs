using System;
using AVG_module;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AVG_Command
{
    [Serializable]
    [LabelText("剧情结束")]
    public class END : CommandBase
    {
        public override void Execute()
        {
        }

        public override void OnUpdate()
        {
        }

        public override bool IsFinished()
        {
            AVGModule.Instance.plotEnd.Invoke();
            return true;
        }
    }
}
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AVG_Command
{
    [Serializable]
    [LabelText("延迟设置")]
    public class Delay : CommandBase
    {
        [field: SerializeField]
        public float time;

        private float startTime;

        public override void Execute()
        {
            startTime = Time.time;
        }

        public override void OnUpdate()
        {

        }

        public override bool IsFinished()
        {
            return Time.time > startTime + time;
        }
    }
}
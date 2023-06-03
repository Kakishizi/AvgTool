using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace AVG_Command
{
    [Serializable]
    [LabelText("背景设置")]
    public class Background : CommandBase
    {
        [SerializeField]
        [LabelText("背景图片")]
        private Sprite image;

        public override void Execute()
        {
            AvgUISettings.Instance.backgroundImage.sprite = image;
        }

        public override void OnUpdate()
        {

        }

        public override bool IsFinished()
        {
            return true;
        }
    }
}
// **********************************************************************
//
// Script Name :		CharacterAction
// Author Name :		Kaki
// Create Time :		2023年06月03日 星期六 12:34
// Description :
// **********************************************************************

using System;
using System.Collections;
using AVG_module;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AVG_Command
{
    [LabelText("角色行为")]
    [Serializable]
    public class CharacterAction : CommandBase
    {
        [LabelText("角色名")] public string character;
        [LabelText("行为类型")] public CharacterActionType actionType;

        [LabelText("目标位置")] [ShowIf("HasMove")]
        public Vector3 targetPosition;

        [LabelText("移动时间")] [ShowIf("HasMove")]
        public float moveTime;

        [LabelText("目标透明度")] [ShowIf("HasShowOrHide")]
        public float targetAlpha;

        [LabelText("透明度渐变时间")] [ShowIf("HasShowOrHide")]
        public float alphaTime;


        private bool isFinish;

        public override void Execute()
        {
            Run();
        }

        public async UniTask Run()
        {
            isFinish = false;
            if (HasMove())
            {
                var character = AVGModule.Instance.characters[this.character];
                character.MoveTo(targetPosition, moveTime);
            }

            if (HasShowOrHide())
            {
                var character = AVGModule.Instance.characters[this.character];
                character.FadeTo(targetAlpha, alphaTime);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(Mathf.Max(moveTime, alphaTime)))
                .ContinueWith(() => isFinish = true);
        }

        public override void OnUpdate()
        {
        }

        public override bool IsFinished()
        {
            return isFinish;
        }

        private bool HasMove()
        {
            return (actionType & CharacterActionType.Move) != 0;
        }

        private bool HasShowOrHide()
        {
            return (actionType & CharacterActionType.Show) != 0 || (actionType & CharacterActionType.Hide) != 0;
        }

        [Flags]
        public enum CharacterActionType
        {
            [LabelText("出现")] Show = 1 << 0,
            [LabelText("消失")] Hide = 1 << 1,
            [LabelText("移动")] Move = 1 << 2,
        }
    }
}
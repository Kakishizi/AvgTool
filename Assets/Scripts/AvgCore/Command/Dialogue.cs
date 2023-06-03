using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AVG_Command
{
    [Serializable]
    [LabelText("对话设置")]
    public class Dialogue : CommandBase
    {
        [BoxGroup("剧情内容")][LabelText("角色名")] public string talker_name;
        [BoxGroup("剧情内容")] [LabelText("对话内容")][TextArea] public string talker_text;

        private float startTime;
        private int textLength;
        private bool isFinished = false;

        public override void Execute()
        {
            isFinished = false;

            AvgUISettings.Instance.nameText.text = talker_name;
            // AvgUISettings.Instance.dialogueText.text = talker_text;
            AvgUISettings.Instance.dialogueText.TypeText(talker_text, AvgSettingConfig.typingEffectTimeDevision);
            startTime = Time.time;
            textLength = talker_text.Length;
            DialogueContent dialogueContent;
            dialogueContent.talker_name = talker_name;
            dialogueContent.talker_text = talker_text;
            AvgUISettings.Instance.dialogueContents.Add(dialogueContent);
        }

        public override void OnUpdate()
        {
            if (Time.time < startTime + textLength * AvgSettingConfig.typingEffectTimeDevision)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AvgUISettings.Instance.dialogueText.SkipTypeText(); //跳过打字效果
                    startTime = Time.time - textLength * AvgSettingConfig.typingEffectTimeDevision; //立即结束
                }
            }
            else
            {
                isFinished = AvgUISettings.Instance.autoPlay || Input.GetMouseButtonDown(0);
            }
            // else if (Input.GetMouseButtonDown(0) || AvgUISettings.Instance.autoPlay)
            // {
            //     startTime = Time.time - textLength * AvgSettingConfig.typingEffectTimeDevision;
            //     isFinished = true;
            //     Debug.Log("Dialogue Done!");
            // }
        }

        public override bool IsFinished()
        {
            return isFinished;
        }
    }
}
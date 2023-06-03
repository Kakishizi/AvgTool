using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace AVG_Command
{
    [Serializable]
    [LabelText("剧情选项")]
    public class Selection : CommandBase
    {
        [field: SerializeField] public List<string> list;

        private bool buttonClicked = false;

        public override void Execute()
        {
            buttonClicked = false;

            if (list == null) return;

            for (int i = 0; i < list.Count; i++)
            {
                var button = AvgUISettings.Instance.selectButtons[i];
                button.GetComponentInChildren<Text>().text = list[i];
                button.gameObject.SetActive(true);
                AddLister(button, i);
            }
        }

        private void AddLister(Button btn, int index)
        {
            btn.onClick.AddListener(() =>
            {
                //命令队列从队列中取出命令直到取出的命令为对应的Predicate或者为End
                while (CommandSender.Instance.GetCommandsCount() != 0)
                {
                    CommandBase command = CommandSender.Instance.DequeueCommand();
                    Predicate p = command as Predicate;
                    if (p != null && (p.value == (ValueRange)Enum.ToObject(typeof(ValueRange), index) ||
                                      p.value == ValueRange.end)) break;
                    AvgUISettings.Instance.playerDecisions.Add(index);
                    Destory(null);
                }
            });
        }


        public override void OnUpdate()
        {
        }

        private void Destory(object state)
        {
            foreach (var VARIABLE in AvgUISettings.Instance.selectButtons)
            {
                VARIABLE.gameObject.SetActive(false);
            }

            buttonClicked = true;
        }

        public override bool IsFinished()
        {
            return buttonClicked;
        }
    }
}
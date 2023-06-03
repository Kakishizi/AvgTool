using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AVG_Command
{
    [Serializable]
    [LabelText("标题设置")]
    public class HEADER : CommandBase
    {
        [LabelText("标题")] public string title;
        [LabelText("时间")] public float time;

        private float startTime;
        private GameObject headerInstance;

        public override void Execute()
        {
            var header = Resources.Load<GameObject>("TitlePanel");
            headerInstance = GameObject.Instantiate(header, AvgUISettings.Instance.transform);
            header.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = title;
            startTime = Time.time;
        }

        public override void OnUpdate()
        {
        }

        public override bool IsFinished()
        {
            if (Time.time > startTime + time)
            {
                Object.Destroy(headerInstance);
                return true;
            }

            return false;
        }
    }
}
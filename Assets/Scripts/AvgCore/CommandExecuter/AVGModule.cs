using AVG_Command_Creator;
using System;
using System.Collections;
using System.Collections.Generic;
using AVG_Command;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace AVG_module
{
    public class AVGModule : Singleton<AVGModule>
    {
        // 供外部调用进入剧情
        public UnityEvent plotBegin = new UnityEvent();

        // 每次剧情结束，补间动画放完就自动调这个，外部可以对它注册一些回调
        public UnityEvent plotEnd = new UnityEvent();

        // 当前已经展示的角色字典
        public Dictionary<string, AvgPortraitCom> characters = new Dictionary<string, AvgPortraitCom>();

        // 初始化从这里开始
        public void Init()
        {
            CommandSender.Instance.Init();
        }

        // 在剧情开始前，设置好你的剧情配置文件
        public void SetPlotConfig(CommandConfig plotConfig)
        {
            CommandSender.Instance.commandConfig = plotConfig;
        }

        // 在剧情开始前，设置好你的剧情配置文件，路径在Resources文件夹下
        public void SetPlotConfig(string plotConfigPath)
        {
            CommandConfig commandConfig = Resources.Load<CommandConfig>(plotConfigPath);
            if (commandConfig == null)
            {
                Debug.LogError("SetPlotConfig : path invalid.");
                return;
            }
            SetPlotConfig(commandConfig);
        }


        /// 设置屏幕分辨率大小
        public void SetPixel(Vector2 screenPixel)
        {
            AvgSettingConfig.pixelSize = screenPixel;
        }

        // 打字效果的时间间隔，推荐范围为 0.01f - 0.1f
        public void SetTypingTimeDevision(float t)
        {
            AvgSettingConfig.typingEffectTimeDevision = t;
        }

        public List<int> GetPlayerDecisions()
        {
            return AvgUISettings.Instance.playerDecisions;
        }
    }
}
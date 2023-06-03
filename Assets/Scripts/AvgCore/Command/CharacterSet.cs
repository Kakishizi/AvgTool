using System;
using System.Collections.Generic;
using AVG_module;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AVG_Command
{
    [LabelText("设置角色")]
    public class CharacterSet : CommandBase
    {
        [LabelText("角色列表")] public List<AVGPortrait> characters = new List<AVGPortrait>();

        public override void Execute()
        {
            for (int i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                if (!AVGModule.Instance.characters.ContainsKey(character.name))
                {
                    //如果不存在该角色，实例化
                    var prefab = Resources.Load<AvgPortraitCom>("Character");
                    var go = GameObject.Instantiate(prefab, AvgUISettings.Instance.portraitPositions);
                    go.SetPosition(character.position);
                    go.SetImage(character.sprite);
                    go.FadeTo(character.alpha);
                    AVGModule.Instance.characters.Add(character.name, go);
                }
                else
                {
                    Debug.Log("角色已存在");
                }
            }
        }

        public override void OnUpdate()
        {
        }

        public override bool IsFinished()
        {
            return true;
        }
    }

    [Serializable]
    public class AVGPortrait
    {
        [LabelText("角色名")] public string name;
        [LabelText("角色图片")] public Sprite sprite;
        [LabelText("角色位置")] public Vector3 position;
        [LabelText("初始透明度")] public float alpha;
    }
}
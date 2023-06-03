
using System;
using AVG_module;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AVG_Command
{
    [Serializable]
    [LabelText("聚焦角色")]
    public class FocusCharacter : CommandBase
    {
        [LabelText("角色名")] public string characterName;

        public override void Execute()
        {
            if (!AVGModule.Instance.characters.ContainsKey(characterName))
            {
                throw new Exception($"角色名不存在{characterName}");
            }

            foreach (var character in AVGModule.Instance.characters)
            {
                character.Value.Focus(character.Key == characterName);
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
}
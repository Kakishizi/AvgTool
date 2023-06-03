using System;
using AvgCore.Utils;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AVG_Command
{
    [LabelText("场景特效")]
    public class SceneEffect : CommandBase
    {
        [LabelText("特效类型")] public EffectType effectType;

        [ShowIf("effectType", EffectType.Shake)] [LabelText("震频（衰退率）")] [Range(0, 1)]
        public float DecayStart = 0.05f;

        [ShowIf("effectType", EffectType.Shake)] [LabelText("震动范围")]
        public Vector3 Range = new Vector3(0.2f, 0.2f, -0.1f);

        [LabelText("设置特效时间")] public float time = 1f;


        private bool isFinish;

        public override void Execute()
        {
            if (effectType == EffectType.Shake)
            {
                Run();
            }
        }

        private async UniTask Run()
        {
            switch (effectType)
            {
                case EffectType.Shake:
                    await ShakeHelper.Shake(AvgUISettings.Instance.backgroundImage.transform, time, Range, DecayStart);
                    isFinish = true;
                    break;
                case EffectType.FadeIn:
                    AvgUISettings.Instance.FadeIn(time);
                    await UniTask.Delay(TimeSpan.FromSeconds(time)).ContinueWith(() => isFinish = true);
                    break;
                case EffectType.FadeOut:
                    AvgUISettings.Instance.FadeOut(time);
                    await UniTask.Delay(TimeSpan.FromSeconds(time)).ContinueWith(() => isFinish = true);
                    break;
            }
        }

        public override void OnUpdate()
        {
        }

        public override bool IsFinished()
        {
            return isFinish;
        }

        public enum EffectType
        {
            [LabelText("淡入")] FadeIn,
            [LabelText("淡出")] FadeOut,
            [LabelText("震动")] Shake
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrayolapedeModinreallife.AbilityEffects
{
    public class StatusEffect_ApplyRandom_Effect : EffectSO
    {
        [Header("Data")]
        public bool _ChoosePositiveStatus = false;

        public bool _ApplyToFirstUnit;

        public bool _JustOneRandomTarget;

        public bool _RandomBetweenPrevious;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            StatusEffect_SO StatusEffect = GetRandomStatusEffect();
            if (StatusEffect == null)
            {
                return false;
            }

            if (_ApplyToFirstUnit || _JustOneRandomTarget)
            {
                List<TargetSlotInfo> list = new List<TargetSlotInfo>(targets);
                foreach (TargetSlotInfo targetSlotInfo in targets)
                {
                    if (targetSlotInfo.HasUnit)
                    {
                        list.Add(targetSlotInfo);
                        if (_ApplyToFirstUnit)
                        {
                            break;
                        }
                    }
                }

                if (list.Count > 0)
                {
                    int index = Random.Range(0, list.Count);
                    exitAmount += ApplyStatusEffect(list[index].Unit, entryVariable, StatusEffect);
                }
            }
            else
            {
                for (int j = 0; j < targets.Length; j++)
                {
                    if (targets[j].HasUnit)
                    {
                        exitAmount += ApplyStatusEffect(targets[j].Unit, entryVariable, StatusEffect);
                    }
                }
            }

            return exitAmount > 0;
        }

        public StatusEffect_SO GetRandomStatusEffect()
        {
            List<StatusEffect_SO> ChosenStatusEffects = new List<StatusEffect_SO>();
            foreach (StatusEffect_SO status in LoadedDBsHandler.StatusFieldDB._StatusEffects.Values)
            {
                if (status.IsPositive != _ChoosePositiveStatus) continue;
                ChosenStatusEffects.Add(status);
            }
            if (ChosenStatusEffects.Count == 0) return null;
            return ChosenStatusEffects[Random.Range(0, ChosenStatusEffects.Count)];
        }

        public int ApplyStatusEffect(IUnit unit, int entryVariable, StatusEffect_SO Status)
        {
            int num = (_RandomBetweenPrevious ? Random.Range(base.PreviousExitValue, entryVariable + 1) : entryVariable);
            if (num < Status.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!unit.ApplyStatusEffect(Status, num))
            {
                return 0;
            }

            return num;
        }

    }
}

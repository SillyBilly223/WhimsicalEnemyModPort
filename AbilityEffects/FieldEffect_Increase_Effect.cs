using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrayolapedeModinreallife.AbilityEffects
{
    public class FieldEffect_Increase_Effect : EffectSO
    {
        [Header("Field")]
        public FieldEffect_SO _Field;

        [Header("Previous Random Option Data")]
        public bool _UseRandomBetweenPrevious;

        [Header("Previous Multiplier Option Data")]
        public bool _UsePreviousExitValueAsMultiplier;

        public int _PreviousExtraAddition;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (_Field == null)
            {
                return false;
            }

            if (_UsePreviousExitValueAsMultiplier)
            {
                entryVariable = _PreviousExtraAddition + entryVariable * base.PreviousExitValue;
            }

            for (int i = 0; i < targets.Length; i++)
            {
                if (!stats.combatSlots.UnitInSlotContainsFieldEffect(targets[i].SlotID, targets[i].IsTargetCharacterSlot, _Field.FieldID)) continue;
                exitAmount += ApplyFieldEffect(stats, targets[i], entryVariable);
            }

            return exitAmount > 0;
        }

        public int ApplyFieldEffect(CombatStats stats, TargetSlotInfo target, int entryVariable)
        {
            int num = (_UseRandomBetweenPrevious ? Random.Range(base.PreviousExitValue, entryVariable + 1) : entryVariable);
            if (num < _Field.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!stats.combatSlots.ApplyFieldEffect(target.SlotID, target.IsTargetCharacterSlot, _Field, num))
            {
                return 0;
            }

            return num;
        }
    }
}

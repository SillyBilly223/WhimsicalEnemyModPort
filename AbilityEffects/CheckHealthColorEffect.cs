using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.AbilityEffects
{
    internal class CheckHealthColorEffect : EffectSO
    {
        [SerializeField]
        public ManaColorSO _color1;

        [SerializeField]
        public ManaColorSO _color2;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    if (targetSlotInfo.Unit.HealthColor == _color1 || targetSlotInfo.Unit.HealthColor == _color2)
                    {
                        exitAmount++;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

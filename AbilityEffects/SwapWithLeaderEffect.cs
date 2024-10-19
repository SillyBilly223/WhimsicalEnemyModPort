using BrutalAPI;
using CrayolapedeModinreallife.Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.AbilityEffects
{
    public class SwapWithLeaderEffect: EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            IUnit unit = null;
            bool GotTarget = false;
            int CasterOriginalSlot = caster.SlotID + (caster.Size - 1);
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    if (targetSlotInfo.Unit.ContainsPassiveAbility("Leader"))
                    {
                        unit = targetSlotInfo.Unit;
                        GotTarget = true;
                    }
                }
            }
            if (!GotTarget) return false;
            int TargetOriginalSlot = (CasterOriginalSlot < unit.SlotID) ? unit.SlotID + (unit.Size - 1) : unit.SlotID;
            int casterMove = (TargetOriginalSlot > CasterOriginalSlot) ? caster.Size : -1;
            int moveAmount = Math.Abs((CasterOriginalSlot - TargetOriginalSlot) - (unit.Size - 1));
            for (int i = 0; i < moveAmount; i++)
            {
                if (stats.combatSlots.CanEnemiesSwap(caster.SlotID, caster.SlotID + casterMove, out var firstSlotSwap, out var secondSlotSwap) && stats.combatSlots.SwapEnemies(caster.SlotID, firstSlotSwap, caster.SlotID + casterMove, secondSlotSwap))
                {
                    exitAmount++;
                }
            }
            TargetOriginalSlot = (CasterOriginalSlot < unit.SlotID) ? unit.SlotID + (unit.Size - 1) : unit.SlotID;
            int casterMove2 = (CasterOriginalSlot > TargetOriginalSlot) ? unit.Size : -1;
            int moveAmount2 = Math.Abs((TargetOriginalSlot - CasterOriginalSlot) - (caster.Size - 1));
            for (int i = 0; i < moveAmount2; i++)
            {
                if (stats.combatSlots.CanEnemiesSwap(unit.SlotID, unit.SlotID + casterMove2, out var firstSlotSwap, out var secondSlotSwap) && stats.combatSlots.SwapEnemies(unit.SlotID, firstSlotSwap, unit.SlotID + casterMove2, secondSlotSwap))
                {
                    exitAmount++;
                }
            }
            return exitAmount > 0;
        }
    }
}

using BrutalAPI;
using CrayolapedeModinreallife.Enemies;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.AbilityEffects
{
    public class SwapToSlotEffect : EffectSO
    {
        [SerializeField]
        public int _outputIncrease = 2;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (!caster.CanSwap) return false;
            for (int i = 0; i < targets.Length; i++)
            {
                int CastersSlot = (targets[i].SlotID > caster.SlotID) ? caster.SlotID + (caster.Size - 1) : caster.SlotID;
                int CasterMove = (targets[i].SlotID > caster.SlotID) ? caster.Size : (-1);
                if (caster.IsUnitCharacter)
                {
                    CastersSlot = caster.SlotID;
                    CasterMove = (targets[i].SlotID > caster.SlotID) ? 1 : (-1);
                }
                else
                {
                    CastersSlot = (targets[i].SlotID > caster.SlotID) ? caster.SlotID + (caster.Size - 1) : caster.SlotID;
                    CasterMove = (targets[i].SlotID > caster.SlotID) ? caster.Size : (-1);
                }
                int moveAmount = Math.Abs(CastersSlot - targets[i].SlotID);
                for (int j = 0; j < moveAmount; j++)
                {
                    if (caster.IsUnitCharacter)
                    {
                        if (caster.SlotID + CasterMove >= 0 && caster.SlotID + CasterMove < stats.combatSlots.CharacterSlots.Length && stats.combatSlots.SwapCharacters(caster.SlotID, caster.SlotID + CasterMove, true))
                        {
                            exitAmount++;
                        }
                    }
                    else
                    {
                        if (stats.combatSlots.CanEnemiesSwap(caster.SlotID, caster.SlotID + CasterMove, out var firstSlotSwap, out var secondSlotSwap) && stats.combatSlots.SwapEnemies(caster.SlotID, firstSlotSwap, caster.SlotID + CasterMove, secondSlotSwap))
                        {
                            exitAmount++;
                        }
                    }
                }
                exitAmount = exitAmount * _outputIncrease;
            }
            
            return exitAmount > 0;
        }
    }
}

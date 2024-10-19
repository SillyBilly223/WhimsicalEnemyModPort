using BrutalAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.Actions
{
    public class PerformIronQuillEffectAction : IImmediateAction
    {
        public PerformIronQuillEffectAction(int direction)
        {
            _moveDirection = direction;
        }

        public void Execute(CombatStats stats)
        {
            foreach (CharacterCombat characterCombat in stats.CharactersOnField.Values)
            {
                if (characterCombat.ContainsStatusEffect("Pierced_ID", 0) && !characterCombat.ContainsPassiveAbility("IronQuill_ID"))
                {
                    if (characterCombat.SlotID + _moveDirection >= 0 && characterCombat.SlotID + _moveDirection < stats.combatSlots.CharacterSlots.Length)
                    {
                        stats.combatSlots.SwapCharacters(characterCombat.SlotID, characterCombat.SlotID + _moveDirection, true);
                        continue;
                    }

                    _moveDirection *= -1;
                    if (characterCombat.SlotID + _moveDirection >= 0 && characterCombat.SlotID + _moveDirection < stats.combatSlots.CharacterSlots.Length)
                    {
                        stats.combatSlots.SwapCharacters(characterCombat.SlotID, characterCombat.SlotID + _moveDirection, true);
                    }
                }
            }
            foreach (EnemyCombat enemyCombat in stats.EnemiesOnField.Values)
            {
                
                if (enemyCombat.ContainsStatusEffect("Pierced_ID", 0) && !enemyCombat.ContainsPassiveAbility("IronQuill_ID"))
                {

                    int direction = _moveDirection > 0? enemyCombat.Size : -1;
                    if (stats.combatSlots.CanEnemiesSwap(enemyCombat.SlotID, enemyCombat.SlotID + direction, out var firstSlotSwap, out var secondSlotSwap))
                    {
                        stats.combatSlots.SwapEnemies(enemyCombat.SlotID, firstSlotSwap, enemyCombat.SlotID + direction, secondSlotSwap, true);
                        continue;
                    }

                    _moveDirection = _moveDirection < 0? enemyCombat.Size : -1;
                    if (stats.combatSlots.CanEnemiesSwap(enemyCombat.SlotID, enemyCombat.SlotID + direction, out firstSlotSwap, out secondSlotSwap))
                    {
                        stats.combatSlots.SwapEnemies(enemyCombat.SlotID, firstSlotSwap, enemyCombat.SlotID + direction, secondSlotSwap, true);
                    }
                }
            }
        }

        public int _moveDirection;
    }
}
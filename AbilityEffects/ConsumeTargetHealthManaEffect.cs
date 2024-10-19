using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.AbilityEffects
{
    public class ConsumeTargetHealthManaEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].HasUnit)
                {
                    JumpAnimationInformation jumpInfo = stats.GenerateUnitJumpInformation(targets[i].Unit.ID, targets[i].Unit.IsUnitCharacter);
                    string manaConsumedSound = stats.audioController.manaConsumedSound;
                    exitAmount += stats.MainManaBar.ConsumeAllManaColor(targets[i].Unit.HealthColor, jumpInfo, manaConsumedSound);
                }                            
            }
            return exitAmount > 0;
        }
    }
}

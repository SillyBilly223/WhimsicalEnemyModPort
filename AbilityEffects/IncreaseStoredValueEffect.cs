using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.AbilityEffects
{
    public class IncreaseStoredValueEffect : EffectSO
    {
        public string PassiveID = "Leader";

        public string StoredValueID = "Leader_Bonuesdmg";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            for (int i = 0; i < targets.Length; i++) 
            {
                if (targets[i].HasUnit && targets[i].Unit.ContainsPassiveAbility(PassiveID))
                {
                    int Amount = targets[i].Unit.SimpleGetStoredValue(StoredValueID);
                    targets[i].Unit.SimpleSetStoredValue(StoredValueID, Amount + entryVariable);
                    exitAmount++;
                }
            }
            return exitAmount > 0;
        }
    }
}

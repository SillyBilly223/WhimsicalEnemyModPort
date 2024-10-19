using CrayolapedeModinreallife.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.CustomeTargetting
{
    public class Targetting_Leader : BaseCombatTargettingSO
    {
        public bool GetAllies = true;
        public override bool AreTargetAllies => GetAllies;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            List<TargetSlotInfo> targetSlots = new List<TargetSlotInfo>();
            foreach (EnemyCombat enemyCombat in CombatManager._instance._stats.EnemiesOnField.Values)
            {
                if (enemyCombat.ContainsPassiveAbility("Leader"))
                {
                    TargetSlotInfo allySlotTarget = slots.GetGenericAllySlotTarget(enemyCombat.SlotID, isCasterCharacter);
                    if (allySlotTarget != null && allySlotTarget.HasUnit)
                    {
                        targetSlots.Add(allySlotTarget);
                    }
                }
            }
            return (targetSlots.Count > 0)? targetSlots.ToArray() : new TargetSlotInfo[0];
        }
    }
}

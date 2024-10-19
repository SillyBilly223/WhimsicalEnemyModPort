using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FiendishFools.Targetting
{
    public class Targetting_Orgin_Conditional : BaseCombatTargettingSO
    {
        public EffectConditionSO EffectCondition;

        public BaseCombatTargettingSO BaseTargetting;

        public override bool AreTargetAllies => BaseTargetting.AreTargetAllies;

        public override bool AreTargetSlots => BaseTargetting.AreTargetSlots;

        public bool CanGetTargets(SlotsCombat slots, int casterSlotID)
        {
            TargetSlotInfo Caster = slots.GetCharacterTargetSlot(casterSlotID, 0);
            if (Caster == null || !Caster.HasUnit) return false;
            if (!EffectCondition.MeetCondition(Caster.Unit, null, 0)) return false;
            return true;
        }

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (!CanGetTargets(slots, casterSlotID)) return new TargetSlotInfo[0];

            return BaseTargetting.GetTargets(slots, casterSlotID, isCasterCharacter);
        }

        public static Targetting_Orgin_Conditional GenTargetting(BaseCombatTargettingSO baseTargetting, EffectConditionSO condtion)
        {
            Targetting_Orgin_Conditional Targetting = ScriptableObject.CreateInstance<Targetting_Orgin_Conditional>();
            Targetting.BaseTargetting = baseTargetting;
            Targetting.EffectCondition = condtion;
            return Targetting;
        }
    }
}

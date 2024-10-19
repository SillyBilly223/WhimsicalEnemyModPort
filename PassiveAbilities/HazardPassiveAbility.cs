using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.PassiveAbilities
{
    public class HazardPassiveAbility : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => true;

        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            DeathReference DeathReference = args as DeathReference;
            if (DeathReference.HasKiller && DeathReference.killer.IsUnitCharacter == (sender as IUnit).IsUnitCharacter)
            {
                EffectInfo[] effect = new EffectInfo[] { new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 3, targets = Targeting.Slot_Front } };
                CombatManager.Instance.AddSubAction(new EffectAction(effect, sender as IUnit));
            }
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            CombatManager.Instance.AddObserver(TryTriggerPassive, "OnDeath", unit);
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            CombatManager.Instance.RemoveObserver(TryTriggerPassive, "OnDeath", unit);
        }
    }
}

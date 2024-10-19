using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.PassiveAbilities
{
    public class TwoFacedPassiveAbility : BasePassiveAbilitySO
    {
        public ManaColorSO Color1 = Pigments.Red;

        public ManaColorSO Color2 = Pigments.Blue;

        public override bool IsPassiveImmediate => false;
        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            IUnit unit = sender as IUnit;
            unit.ChangeHealthColor((unit.HealthColor == Color1) ? Color2 : Color1);
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            CombatManager.Instance.AddObserver(TryTriggerPassive, "OnDirectDamaged", unit);
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            CombatManager.Instance.RemoveObserver(TryTriggerPassive, "OnDirectDamaged", unit);
        }
    }
}

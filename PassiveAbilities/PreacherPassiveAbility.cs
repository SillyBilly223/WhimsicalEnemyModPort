using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CrayolapedeModinreallife.Actions;
using CrayolapedeModinreallife.Enemies;
using static UnityEngine.UI.CanvasScaler;

namespace CrayolapedeModinreallife.PassiveAbilities
{
    public class PreacherPassiveAbility : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => true;

        public override bool DoesPassiveTrigger => true;

        public bool Applier = true;

        public override void TriggerPassive(object sender, object args) 
        {
            IUnit unit = sender as IUnit;
            DamageDealtValueChangeException ex = args as DamageDealtValueChangeException;
            ex.AddModifier(new ScarsValueModifier(unit.SimpleGetStoredValue("Leader_Bonuesdmg")));
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            if (IsFollower)
            {
                AddFollower();
                return;
            }
            CombatManager.Instance.AddObserver(TriggerPassive, "OnWillApplyDamage", unit);
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            if (IsFollower)
            {
                RemoveFollower();
                return;
            }
            CombatManager.Instance.RemoveObserver(TriggerPassive, "OnWillApplyDamage", unit);
        }

        public void AddFollower()
        {
            ExtraUtils.EvenglistsInCombat++;
            ExtraUtils.StartEvenglistSearch();
        }

        public void RemoveFollower()
        {
            ExtraUtils.EvenglistsInCombat--;
            if (ExtraUtils.EvenglistsInCombat > 0) return;
            ExtraUtils.StopEvenglistSearch();
        }

        public bool IsFollower;
    }
}

using BrutalAPI;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.TouchScreenKeyboard;

namespace CrayolapedeModinreallife.PassiveAbilities
{
    public class LymphropodPassiveAbility : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => true;
        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            EnemyCombat enemy = sender as EnemyCombat;
            CombatManager._instance._stats.timeline.TryAddNewExtraEnemyTurns(enemy, 1);
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            CombatManager.Instance.AddObserver(TryTriggerPassive, "TerritorialTrigger", unit);
            ExtraUtils.LymphropodInCombat++;
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            CombatManager.Instance.RemoveObserver(TryTriggerPassive, "TerritorialTrigger", unit);
            ExtraUtils.LymphropodInCombat--;
        }
    }
}

using BrutalAPI;
using CrayolapedeModinreallife.Actions;
using CrayolapedeModinreallife.Enemies;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrayolapedeModinreallife.PassiveAbilities
{
    public class HiveMindPassiveAbility : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => false;
        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            bool HasCrayolaPede = false;
            foreach (EnemyCombat Enemies in CombatManager._instance._stats.EnemiesOnField.Values)
            {
                if (Enemies.IsAlive)
                    if (Enemies.Enemy.name == "Spligis_EN")
                        HasCrayolaPede = true;
            }
            if (!HasCrayolaPede) return;

            EnemyCombat unit = sender as EnemyCombat;
            List<EnemySO> SpawnEnemies = new List<EnemySO>();
            for (int i = 0; i < BossSpluglings.BossSplugs.Length; i++)
            {
                if (BossSpluglings.BossSplugs[i].name != unit.Enemy.name)
                {
                    SpawnEnemies.Add(BossSpluglings.BossSplugs[i]);
                }
            }
            CombatManager.Instance.AddSubAction(new SpawnEnemyAction(BossSpluglings.BossSplugs[Random.Range(0, SpawnEnemies.Count)], -1, false, false, ""));
        }

        public void HiveMindDeathTrigger(object sender, object args)
        {
            CombatManager._instance.AddPriorityRootAction(new EnemyHiveMindAction());
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            CombatManager.Instance.AddObserver(TryTriggerPassive, TriggerCalls.OnDeath.ToString(), unit);
            CombatManager.Instance.AddObserver(HiveMindDeathTrigger, TriggerCalls.OnAnyAbilityUsed.ToString(), unit);
            CombatManager.Instance.AddObserver(HiveMindDeathTrigger, TriggerCalls.OnDamaged.ToString(), unit);
            CombatManager.Instance.AddObserver(HiveMindDeathTrigger, TriggerCalls.OnTurnStart.ToString(), unit);
            CombatManager.Instance.AddObserver(HiveMindDeathTrigger, TriggerCalls.OnTurnFinished.ToString(), unit);
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            CombatManager.Instance.RemoveObserver(TryTriggerPassive, TriggerCalls.OnDeath.ToString(), unit);
            CombatManager.Instance.RemoveObserver(HiveMindDeathTrigger, TriggerCalls.OnAnyAbilityUsed.ToString(), unit);
            CombatManager.Instance.RemoveObserver(HiveMindDeathTrigger, TriggerCalls.OnDamaged.ToString(), unit);
            CombatManager.Instance.RemoveObserver(HiveMindDeathTrigger, TriggerCalls.OnTurnStart.ToString(), unit);
            CombatManager.Instance.RemoveObserver(HiveMindDeathTrigger, TriggerCalls.OnTurnFinished.ToString(), unit);
        }
    }
}

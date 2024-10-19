using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.Actions
{
    public class EnemyHiveMindAction : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            List<EnemyCombat> HiveMindEnemies = new List<EnemyCombat>();
            List<int> IDs = new List<int>();
            List<bool> IsCharacter = new List<bool>();

            List<string> PassiveName = new List<string>();
            List<Sprite> PassiveSprite = new List<Sprite>();
            foreach (EnemyCombat Enemies in stats.EnemiesOnField.Values)
            {
                if (Enemies.IsAlive)
                {
                    if (Enemies.Enemy.unitTypes.Contains("HiveMindMaster")) yield break;
                    if (!Enemies.TryGetPassiveAbility("Hivemind_ID", out BasePassiveAbilitySO passive)) continue;

                    HiveMindEnemies.Add(Enemies);
                    IDs.Add(Enemies.ID);
                    IsCharacter.Add(Enemies.IsUnitCharacter);

                    PassiveName.Add(passive.GetPassiveLocData().text);
                    PassiveSprite.Add(passive.passiveIcon);
                }
            }

            CombatManager.Instance.AddUIAction(new ShowMultiplePassiveInformationUIAction(IDs.ToArray(), IsCharacter.ToArray(), PassiveName.ToArray(), PassiveSprite.ToArray()));
            DeathReference deathReference = new DeathReference(null, witheringDeath: true);
            for (int i = 0; i < HiveMindEnemies.Count; i++)
            {
                HiveMindEnemies[i].EnemyDeath(deathReference, DeathType_GameIDs.Withering.ToString());
                CombatManager.Instance.AddUIAction(new EnemyDeathUIAction(HiveMindEnemies[i].ID, playDeathSound: true));
                stats.RemoveEnemy(HiveMindEnemies[i].ID);
            }
        }
    }
}

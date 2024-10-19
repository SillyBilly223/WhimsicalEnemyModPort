using BrutalAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Actions
{
    public class RemoveLeaderAction : CombatAction
    {
        public override IEnumerator Execute(CombatStats stats)
        {
            BasePassiveAbilitySO LeaderPassive = Passives.GetCustomPassive("Leader");

            foreach (EnemyCombat Enemy in stats.EnemiesOnField.Values)
            {
                if (Enemy.IsAlive && Enemy.ContainsPassiveAbility("Leader"))
                {
                    Enemy.TryRemovePassiveAbility("Leader");
                    CombatManager._instance.AddUIAction(new ShowPassiveInformationUIAction(Enemy.ID, Enemy.IsUnitCharacter, "Leader removed", LeaderPassive.passiveIcon));
                }
            }
            yield break;
        }
    }
}

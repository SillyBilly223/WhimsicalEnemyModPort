using BrutalAPI;
using CrayolapedeModinreallife.Enemies;
using CrayolapedeModinreallife.PassiveAbilities;
using MUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrayolapedeModinreallife.Actions
{
    public class FindLeaderAction : CombatAction
    {
        public FindLeaderAction(EnemyCombat ChosenLeader, EnemyCombat LastLeader)
        {
            ChosenEnemy = ChosenLeader;
            CurrentLeader = LastLeader;
        }

        public override IEnumerator Execute(CombatStats stats)
        {
            ExtraUtils.IsProcessingLeaderSearch = true;

            BasePassiveAbilitySO LeaderPassive = Passives.GetCustomPassive("Leader");
            int LeaderDamageBoostAmount = 0;

            if (CurrentLeader != null && CurrentLeader.IsAlive)
            {
                LeaderDamageBoostAmount = CurrentLeader.SimpleGetStoredValue("Leader_Bonuesdmg");
                CurrentLeader.TryRemovePassiveAbility("Leader");
                CombatManager._instance.AddUIAction(new ShowPassiveInformationUIAction(CurrentLeader.ID, CurrentLeader.IsUnitCharacter, "Leader removed", LeaderPassive.passiveIcon));
            }
            if (ChosenEnemy != null && ChosenEnemy.IsAlive)
            {
                ChosenEnemy.AddPassiveAbility(LeaderPassive);
                ChosenEnemy.SimpleSetStoredValue("Leader_Bonuesdmg", LeaderDamageBoostAmount);
                CombatManager._instance.AddUIAction(new ShowPassiveInformationUIAction(ChosenEnemy.ID, ChosenEnemy.IsUnitCharacter, "New Leader found!", LeaderPassive.passiveIcon));
            }
            ExtraUtils.IsProcessingLeaderSearch = false;
            yield break;
        }

        public EnemyCombat ChosenEnemy;

        public EnemyCombat CurrentLeader;
    }
}

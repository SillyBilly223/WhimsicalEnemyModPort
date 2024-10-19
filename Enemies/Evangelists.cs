using BrutalAPI;
using CrayolapedeModinreallife.AbilityEffects;
using CrayolapedeModinreallife.CustomeTargetting;
using CrayolapedeModinreallife.PassiveAbilities;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;
using UnityEngine;

namespace CrayolapedeModinreallife.Enemies
{
    public class Evangelists
    {
        public static void Add()
        {
            #region Passives

            PreacherPassiveAbility Follower = ScriptableObject.CreateInstance<PreacherPassiveAbility>();
            Follower._passiveName = "Follower";
            Follower.passiveIcon = ResourceLoader.LoadSprite("FollowerPassive");
            Follower.m_PassiveID = "Follower";
            Follower._enemyDescription = "This creature only follows the strongest.";
            Follower._characterDescription = "what!";
            Follower.doesPassiveTriggerInformationPanel = false;
            Follower.IsFollower = true;

            UnitStoreData.CreateAndAdd_IntTooltip_UnitStoreDataToPool("Leader_Bonuesdmg", "Bonus Damage {0}", Color.red);

            PreacherPassiveAbility Leader = ScriptableObject.CreateInstance<PreacherPassiveAbility>();
            Leader._passiveName = "Leader";
            Leader.passiveIcon = ResourceLoader.LoadSprite("LeaderPassive");
            Leader.m_PassiveID = "Leader";
            Leader._enemyDescription = "This enemy has proven to be the strongest amongst all. Bonus damage maintains between Leaders.";
            Leader._characterDescription = "g.";
            Leader.specialStoredData = UnitStoreData.GetCustom_UnitStoreData("Leader_Bonuesdmg");
            Leader.doesPassiveTriggerInformationPanel = false;
            Leader.IsFollower = false;

            LoadedDBsHandler.PassiveDB.AddNewPassive("Follower", Follower);
            LoadedDBsHandler.PassiveDB.AddNewPassive("Leader", Leader);

            Glossary.CreateAndAddCustom_PassiveToGlossary("Follower", "This enemy/party member only follows the strongest(Leader).", ResourceLoader.LoadSprite("FollowerPassive"));
            Glossary.CreateAndAddCustom_PassiveToGlossary("Leader", "This enemy/party member has proven to be the strongest amongst all. Bonus damage maintains between Leaders.", ResourceLoader.LoadSprite("LeaderPassive"));

            #endregion Passives

            #region ScriptableObjects

            Targetting_Leader LeaderTargetting = ScriptableObject.CreateInstance<Targetting_Leader>();

            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Shield_ID", out FieldEffect_SO Shield);

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = Shield;

            #endregion ScriptableObjects

            Enemy enemy = EXOP.EnemyInfoSetter("Evangelists", 35, Pigments.Blue, EXOP._vaboola);
            enemy.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Missionary/Missionary.prefab", MainClass.assetBundle);
            enemy.CombatSprite = ResourceLoader.LoadSprite("MissionaryIcon");
            enemy.OverworldAliveSprite = ResourceLoader.LoadSprite("MissionaryIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.OverworldDeadSprite = ResourceLoader.LoadSprite("MissionaryDeadIcon", new Vector2?(new Vector2(0.5f, 0f)));
            enemy.AddPassives(new BasePassiveAbilitySO[] { Passives.Withering, Follower });

            Ability ability = new Ability("Follow the Leader", "FollowtheLeader_AB");
            ability.Description = "Switches places with the leader, the leader performs a random ability.";
            ability.ability.priority = Priority.VerySlow;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapWithLeaderEffect>(), entryVariable = 0, targets = LeaderTargetting },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<AddTurnTargetToTimelineEffect>(), entryVariable = 1, targets = LeaderTargetting },
            };
            ability.Visuals = EXOP._agon.rankedData[0].rankAbilities[1].ability.visuals;
            ability.AnimationTarget = LeaderTargetting;
            ability.AddIntentsToTarget(LeaderTargetting, new string[] { "Other_Refresh" });
            ability.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides" });

            GetAmountHealthEffect getAmountHealthEffect = ScriptableObject.CreateInstance<GetAmountHealthEffect>();
            getAmountHealthEffect._DecreaseByPercentage = true;

            Ability ability2 = new Ability("Marching stance", "Marchingstance_AB");
            ability2.Description = "Deals damage to the Opposing party member equal to half the Leaders current health, increases the Leaders damage by 2.";
            ability2.ability.priority = Priority.VeryFast;
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = getAmountHealthEffect, entryVariable = 0, targets = LeaderTargetting },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamagePlusePreviousEffect>(), entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<IncreaseStoredValueEffect>(), entryVariable = 2, targets = LeaderTargetting },
            };
            ability2.Visuals = EXOP._jumbleGutsFlummoxing.abilities[0].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_Front;
            ability2.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_1_2" });
            ability2.AddIntentsToTarget(LeaderTargetting, new string[] { "Misc_Additional" });

            Ability ability3 = new Ability("Raging Bags", "RagingBags_AB");
            ability3.Description = "Applsy 6 Shield to the Leader position, increases the Leaders damage by 2.";
            ability3.ability.priority = Priority.VeryFast;
            ability3.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ApplyShield, entryVariable = 6, targets = LeaderTargetting },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<IncreaseStoredValueEffect>(), entryVariable = 2, targets = LeaderTargetting },
            };
            ability3.Visuals = EXOP._spoggleResonant.abilities[2].ability.visuals;
            ability3.AnimationTarget = LeaderTargetting;
            ability3.AddIntentsToTarget(LeaderTargetting, new string[] { "Field_Shield", "Misc_Additional" });

            enemy.Abilities = new Ability[] { ability, ability2, ability3 };
            ExtraUtils.AddBaseEnemyABSprite(enemy.enemy.abilities);
            enemy.AddEnemy();
        }

    }
}

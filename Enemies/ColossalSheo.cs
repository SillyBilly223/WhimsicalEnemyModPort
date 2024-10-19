using BrutalAPI;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CrayolapedeModinreallife.Enemies
{
    public class ColossalSheo
    {
        public static void Add()
        {
            #region Passive

            ScavangerPassive scavangerPassive = ScriptableObject.CreateInstance<ScavangerPassive>();
            scavangerPassive._passiveName = "Scavanger";
            scavangerPassive.passiveIcon = ResourceLoader.LoadSprite("Scavanger");
            scavangerPassive.m_PassiveID = "Scavanger_ID";
            scavangerPassive._enemyDescription = "Upon a party member or enemy dying heal this enemy 5 health.";
            scavangerPassive._characterDescription = "Upon a party member or enemy dying heal this enemy 5 health.";

            LoadedDBsHandler.PassiveDB.AddNewPassive("Scavanger_ID", scavangerPassive);

            #endregion Passive

            #region ScriptableObjects

            AbilitySelector_ColossalSheo abilitySelector_Colossal = ScriptableObject.CreateInstance<AbilitySelector_ColossalSheo>();

            SpawnRandomEnemyAnywhereEffect spawnRandomEnemyAnywhereEffect = ScriptableObject.CreateInstance<SpawnRandomEnemyAnywhereEffect>();
            spawnRandomEnemyAnywhereEffect._enemies = new List<EnemySO>
            {
                EXOP._mungEN,
                EXOP._flaMinGoa,
                EXOP._mungie,
                EXOP._mudLung
            };

            #endregion ScriptableObjects

            Enemy enemy = EXOP.EnemyInfoSetter("Colossal Sheo", 30, Pigments.Red, LoadedAssetsHandler.GetEnemy("SkinningHomunculus_EN"));
            enemy.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/ColossalSheo/ColossalSheo.prefab", MainClass.assetBundle);
            enemy.CombatSprite = ResourceLoader.LoadSprite("BoneBirdIcon");
            enemy.OverworldAliveSprite = ResourceLoader.LoadSprite("BoneBirdIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.OverworldDeadSprite = ResourceLoader.LoadSprite("BoneBirdIconDead", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.AbilitySelector = abilitySelector_Colossal;
            enemy.AddPassive(scavangerPassive);

            Ability ability = new Ability("Voice Imitation", "VoiceImitation_ID");
            ability.Description = "Moves Left or Right, spawns a random small Farshore enemy.";
            ability.Rarity.rarityValue = 25;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 1, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = spawnRandomEnemyAnywhereEffect, entryVariable = 1, targets = Targeting.Slot_SelfSlot },
            };
            ability.Visuals = EXOP._agon.rankedData[0].rankAbilities[1].ability.visuals;
            ability.AnimationTarget = Targeting.Slot_SelfSlot;
            ability.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides", "Other_Spawn" });
            abilitySelector_Colossal._spawnAbility = ability.ability._abilityName;

            Ability ability2 = new Ability("Peck", "Peck_ID");
            ability2.Description = "Deals a painful amount of damage to the Right and Left party members.";
            ability2.Rarity.rarityValue = 50;
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 5, targets = Targeting.Slot_OpponentSides },
            };
            ability2.Visuals = EXOP._agon.rankedData[0].rankAbilities[1].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_OpponentSides;
            ability2.AddIntentsToTarget(Targeting.Slot_OpponentSides, new string[] { "Damage_3_6" });

            Ability ability3 = new Ability("Bash", "Bash_ID");
            ability3.Description = "Deals an agonizing amount of damage to the opposing party member. Moves Left or Right.";
            ability3.Rarity.rarityValue = 50;
            ability3.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 8, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 1, targets = Targeting.Slot_SelfSlot },
            };
            ability3.Visuals = EXOP._pearl.rankedData[0].rankAbilities[1].ability.visuals;
            ability3.AnimationTarget = Targeting.Slot_Front;
            ability3.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_7_10" });

            enemy.AddEnemyAbilities(new Ability[]
            {
                ability,
                ability2,
                ability3
            });

            ExtraUtils.AddBaseEnemyABSprite(enemy.enemy.abilities);
            enemy.AddEnemy();
        }
    }
}

using BrutalAPI;
using CrayolapedeModinreallife.AbilityEffects;
using CrayolapedeModinreallife.AbilitySelectors;
using CrayolapedeModinreallife.Conditions;
using CrayolapedeModinreallife.PassiveAbilities;
using FiendishFools.Targetting;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace CrayolapedeModinreallife.Enemies
{
    public static class Convict
    {
        public static void Add()
        {
            #region Passives

            HazardPassiveAbility hazardPassive = ScriptableObject.CreateInstance<HazardPassiveAbility>();
            hazardPassive._passiveName = "Hazard";
            hazardPassive.passiveIcon = ResourceLoader.LoadSprite("HazardIcon");
            hazardPassive.m_PassiveID = "Hazard";
            hazardPassive._enemyDescription = "Upon death if this enemy was killed by an enemy, deal a Painful amount of damage to the Opposing party member.";
            hazardPassive._characterDescription = "Upon death if this party member was killed by a party member, deal a Painful amount of damage to the Opposing party member.";
            hazardPassive._triggerOn = new TriggerCalls[0];

            #endregion Passives

            #region  ScriptableObjects

            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Shield_ID", out FieldEffect_SO Shield);

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = Shield;

            DamageEffect damageEffect = ScriptableObject.CreateInstance<DamageEffect>();
            damageEffect._indirect = true;

            #endregion ScriptableObjects

            #region StalactiteEN

            Enemy Stalactite = EXOP.EnemyInfoSetter("Stalactite", 12, Pigments.Grey, EXOP._gospel);
            Stalactite.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Convict/SharpStone.prefab", MainClass.assetBundle);
            Stalactite.CombatSprite = ResourceLoader.LoadSprite("StoneIcon");
            Stalactite.OverworldAliveSprite = ResourceLoader.LoadSprite("StoneIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            Stalactite.OverworldDeadSprite = ResourceLoader.LoadSprite("StoneIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            Stalactite.AddUnitType("ConvStalacite");
            Stalactite.AddPassives(new BasePassiveAbilitySO[] { hazardPassive, Passives.Inanimate, Passives.Withering });

            Ability Stalactite_AB = new Ability("Sway", "Sway_AB");
            Stalactite_AB.Description = "Deals a Little amount of damage to the Opposing party member, moves the Opposing party member Left or Right.";
            Stalactite_AB.Rarity.rarityValue = 3;
            Stalactite_AB.ability.priority.priorityValue = 2;
            Stalactite_AB.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 2, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 0, targets = Targeting.Slot_Front },
            };
            Stalactite_AB.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_1_2", "Swap_Sides" });

            Ability Stalactite_AB2 = new Ability("Debris", "Debris_AB");
            Stalactite_AB2.Description = "Applies 4 shield to this enemy position.";
            Stalactite_AB2.Rarity.rarityValue = 3;
            Stalactite_AB2.ability.priority.priorityValue = 1;
            Stalactite_AB2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ApplyShield, entryVariable = 6, targets = Targeting.Slot_SelfSlot },
            };
            Stalactite_AB2.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Field_Shield" });

            Ability Stalactite_AB3 = new Ability("Crack", "Crack_AB");
            Stalactite_AB3.Description = "Deals a Painful amount of indirect damage to this enemy.";
            Stalactite_AB3.Rarity.rarityValue = 3;
            Stalactite_AB3.ability.priority.priorityValue = 0;
            Stalactite_AB3.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = damageEffect, entryVariable = 6, targets = Targeting.Slot_SelfSlot },
            };
            Stalactite_AB3.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Damage_3_6" });

            Stalactite.AddEnemyAbilities(new Ability[]
            {
                Stalactite_AB,
                Stalactite_AB2,
                Stalactite_AB3
            });

            ExtraUtils.AddBaseEnemyABSprite(Stalactite.enemy.abilities);
            Stalactite.AddEnemy();

            #endregion StalactiteEN

            SpawnEnemyAnywhereEffect SpawnStalactiteEffect = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            SpawnStalactiteEffect.enemy = Stalactite.enemy;
            SpawnStalactiteEffect.givesExperience = false;

            AbilitySelector_Convict abilitySelector_Convict = ScriptableObject.CreateInstance<AbilitySelector_Convict>();
            Enemy Convict = EXOP.EnemyInfoSetter("Convict", 65, Pigments.Red, EXOP._Roids);
            Convict.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Convict/Convict.prefab", MainClass.assetBundle);
            Convict.enemy.enemyTemplate.m_Data.m_Gibs = MainClass.SaltGibs.LoadAsset<GameObject>("Assets/The/BigGuy_Gibs.prefab").GetComponent<ParticleSystem>();
            Convict.CombatSprite = ResourceLoader.LoadSprite("ConvictIcon");
            Convict.OverworldAliveSprite = ResourceLoader.LoadSprite("ConvictIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            Convict.OverworldDeadSprite = ResourceLoader.LoadSprite("ConvictIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            Convict.AbilitySelector = abilitySelector_Convict;

            Ability ability = new Ability("Head Bash", "HeadBash_AB");
            ability.Description = "Deals a painful amount of damage to the Opposing party member, moves Left or Right.\nDangles down 2 Stalactites.";
            ability.Rarity.rarityValue = 5;
            ability.ability.priority.priorityValue = 2;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 5, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 0, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = SpawnStalactiteEffect, entryVariable = 2, targets = Targeting.Slot_SelfSlot},
            };
            ability.Visuals = EXOP._OsmanSinnoks.abilities[0].ability.visuals;
            ability.AnimationTarget = Targeting.Slot_Front;
            ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6" });
            ability.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides", "Other_Spawn" });

            Ability ability2 = new Ability("Shattering Wail", "ShatteringWail_AB");
            ability2.Description = "Deals a Painful amount of damage to the Opposing party member.\nBreaks all Stalactites, Dangles down 3 Stalactites. Moves Left or Right 2 times.";
            ability2.Rarity.rarityValue = 5;
            ability2.ability.priority.priorityValue = -1;
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 5, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DirectDeathEffect>(), entryVariable = 1, targets = Targeting.GenerateSlotTarget(new int[] { -4, -3, -2, -1, 1, 2, 3, 4}, true) },
                new EffectInfo() { effect = SpawnStalactiteEffect, entryVariable = 3, targets = Targeting.Slot_SelfSlot},
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 0, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 0, targets = Targeting.Slot_SelfSlot },
            };
            ability2.Visuals = EXOP._agon.rankedData[0].rankAbilities[1].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_SelfSlot;
            ability2.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6" });
            ability2.AddIntentsToTarget(Targeting.GenerateSlotTarget(new int[] { -4, -3, -2, -1, 1, 2, 3, 4 }, true), new string[] { "Damage_Death" });
            ability2.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Other_Spawn", "Swap_Sides", "Swap_Sides" });

            Ability RightRush = new Ability("Right Rush", "RightRush_AB");
            RightRush.Description = "Moves to the Right as far as possible. Deals a Painful amount of damage to the Opposing enemy, damage is increased by 2 for the amount of times moved.\nDeal painful damage to all Stalacites.";
            RightRush.Rarity.rarityValue = 10;
            RightRush.ability.priority.priorityValue = -1;
            RightRush.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSlotEffect>(), entryVariable = 0, targets = Targeting.GenerateGenericTarget(new int[] { 4 }) },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<AnimationVisualReturnPreviousEffect>(), entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamagePlusePreviousEffect>(), entryVariable = 4, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 3, targets = Targeting.GenerateSlotTarget(new int[] { -4, -3, -2, -1, 1, 2, 3, 4}, true) },
            };
            RightRush.AnimationTarget = Targeting.Slot_SelfSlot;
            RightRush.Visuals = EXOP._clive.rankedData[0].rankAbilities[2].ability.visuals;
            RightRush.AddIntentsToTarget(Targeting.GenerateSlotTarget(new int[] { -4, -3, -2, -1, 1, 2, 3, 4 }, true), new string[] { "Damage_3_6" });
            RightRush.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides" });
            RightRush.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6" });

            Ability LeftRush = new Ability("Left Rush", "LeftRush_AB");
            LeftRush.Description = "Moves to the Left as far as possible. Deals a Painful amount of damage to the Opposing enemy, damage is increased by 2 for the amount of times moved.\nDeal painful damage to all Stalacites.";
            LeftRush.Rarity.rarityValue = 10;
            LeftRush.ability.priority.priorityValue = -1;
            LeftRush.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSlotEffect>(), entryVariable = 0, targets = Targeting.GenerateGenericTarget(new int[] { 0 }) },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<AnimationVisualReturnPreviousEffect>(), entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamagePlusePreviousEffect>(), entryVariable = 4, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 3, targets = Targeting.GenerateSlotTarget(new int[] { -4, -3, -2, -1, 1, 2, 3, 4}, true) },
            };
            LeftRush.AnimationTarget = Targeting.Slot_SelfSlot;
            LeftRush.Visuals = EXOP._clive.rankedData[0].rankAbilities[2].ability.visuals;
            LeftRush.AddIntentsToTarget(Targeting.GenerateSlotTarget(new int[] { -4, -3, -2, -1, 1, 2, 3, 4 }, true), new string[] { "Damage_3_6" });
            LeftRush.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides" });
            LeftRush.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6" });

            abilitySelector_Convict._leftAbility = LeftRush.ability.name;
            abilitySelector_Convict._rightAbility = RightRush.ability.name;

            Convict.AddEnemyAbilities(new Ability[]
            {
                ability,
                ability2,
                RightRush,
                LeftRush
            });

            ExtraUtils.AddBaseEnemyABSprite(Convict.enemy.abilities);
            Convict.AddEnemy();
        }
    }
}
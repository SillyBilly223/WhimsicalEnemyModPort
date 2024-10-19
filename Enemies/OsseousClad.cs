using BrutalAPI;
using CrayolapedeModinreallife.AbilityEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.Enemies
{
    public static class OsseousClad
    {
        public static void Add()
        {
            #region Passive

            IronQuillPassiveAbility ironQuillPassive = ScriptableObject.CreateInstance<IronQuillPassiveAbility>();
            ironQuillPassive._passiveName = "IronQuill";
            ironQuillPassive.passiveIcon = ResourceLoader.LoadSprite("IronQuillIcon");
            ironQuillPassive.m_PassiveID = "IronQuill_ID";
            ironQuillPassive._enemyDescription = "Upon this enemy moving, all enemies and party member's inflicted with Pierced will also move in the same direction.\nThis dose not apply to entities with IronQuill.";
            ironQuillPassive._characterDescription = "Upon this party member moving, all enemies and party members inflicted with Pierced will also move in the same direction.\nThis dose not apply to entities with IronQuill.";
            ironQuillPassive.doesPassiveTriggerInformationPanel = false;
            ironQuillPassive._triggerOn = new TriggerCalls[0];

            LoadedDBsHandler.PassiveDB.AddNewPassive("IronQuill_ID", ironQuillPassive);

            #endregion Passive

            #region ScriptableObjects

            LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Pierced_ID", out StatusEffect_SO Pierced);
            LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Ruptured_ID", out StatusEffect_SO Ruptured);
            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Shield_ID", out FieldEffect_SO Shield);

            StatusEffect_Apply_Effect ApplyPierced = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyPierced._Status = Pierced;

            StatusEffect_Apply_Effect ApplyRuptured = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyRuptured._Status = Ruptured;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = Shield;

            #endregion ScriptableObjects

            Enemy enemy = EXOP.EnemyInfoSetter("OsseousClad", 22, Pigments.Red, EXOP._flarb);
            MainClass.assetBundle.LoadAsset<GameObject>("Assets/Enemies/OsseousClad/Osseous.prefab");
            enemy.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/OsseousClad/Osseous.prefab", MainClass.assetBundle);
            enemy.CombatSprite = ResourceLoader.LoadSprite("SpikeGuyIcon");
            enemy.OverworldAliveSprite = ResourceLoader.LoadSprite("SpikeGuyIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.OverworldDeadSprite = ResourceLoader.LoadSprite("SpikeGuyIconDead", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_ByRarity>();
            enemy.AddPassive(ironQuillPassive);

            Ability ability = new Ability("Puncture", "Puncture_ID");
            ability.Description = "Deals a Painful amount of damage to the Left and Right party member's.\nInflicts 3 pierced to the Left and Right party member's.";
            ability.AbilitySprite = EXOP._mungEN.abilities[0].ability.abilitySprite;
            ability.Rarity.rarityValue = 4;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 4, targets = Targeting.GenerateSlotTarget(new int[] { -1, 1 }) },
                new EffectInfo() { effect = ApplyPierced, entryVariable = 3, targets = Targeting.GenerateSlotTarget(new int[] { -1, 1 }) },
            };
            ability.Visuals = EXOP._fennec.rankedData[0].rankAbilities[0].ability.visuals;
            ability.AnimationTarget = Targeting.GenerateSlotTarget(new int[] { -1, 1 });
            ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6", "ApplyPierced" });

            Ability ability2 = new Ability("Tactical Roll", "TacticalRoll_ID");
            ability2.Description = "Moves left or right 1 time.\nApplies 4 Shield to this enemy position, inflicts 2 Ruptured to the Opposing party member.";
            ability2.AbilitySprite = EXOP._mungEN.abilities[0].ability.abilitySprite;
            ability2.Rarity.rarityValue = 25;
            ability2.ability.priority.priorityValue = -1;
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 1, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ApplyShield, entryVariable = 4, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ApplyRuptured, entryVariable = 2, targets = Targeting.Slot_Front },
            };
            ability2.Visuals = EXOP._fennec.rankedData[0].rankAbilities[0].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_SelfSlot;
            ability2.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides", "Field_Shield" });
            ability2.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Status_Ruptured" });

            Ability ability3 = new Ability("Tumble", "Tumble_ID");
            ability3.Description = "Moves Left or Right 3 times.\nInflicts 3 Pierced to the Opposing party member.";
            ability3.AbilitySprite = EXOP._mungEN.abilities[0].ability.abilitySprite;
            ability2.Rarity.rarityValue = 25;
            ability3.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 1, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 1, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 1, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ApplyPierced, entryVariable = 3, targets = Targeting.Slot_Front },
            };
            ability3.Visuals = EXOP._fennec.rankedData[0].rankAbilities[0].ability.visuals;
            ability3.AnimationTarget = Targeting.Slot_SelfSlot;
            ability3.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides", "Swap_Sides", "Swap_Sides" });
            ability3.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "ApplyPierced" });

            enemy.AddEnemyAbilities(new Ability[]
            {
                ability,
                ability2,
                ability3
            });

            enemy.AddEnemy();
        }
    }
}
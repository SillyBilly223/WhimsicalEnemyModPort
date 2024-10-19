using BrutalAPI;
using CrayolapedeModinreallife.AbilityEffects;
using CrayolapedeModinreallife.Conditions;
using CrayolapedeModinreallife.PassiveAbilities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CrayolapedeModinreallife.Enemies
{
    public class Spligis
    {
        public static void Add()
        {

            #region passives

            TwoFacedPassiveAbility twoFacedPassiveAbility = ScriptableObject.CreateInstance<TwoFacedPassiveAbility>();
            twoFacedPassiveAbility._passiveName = "Two Faced";
            twoFacedPassiveAbility.passiveIcon = ResourceLoader.LoadSprite("RedBlueTwoFace");
            twoFacedPassiveAbility.m_PassiveID = "TwoFacedCrayolapede_ID";
            twoFacedPassiveAbility._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from Red to Blue or vice versa.";
            twoFacedPassiveAbility._characterDescription = "Upon receiving direct damage this enemy will change its health colour from red to blue or vice versa.";
            twoFacedPassiveAbility.Color1 = Pigments.Blue;
            twoFacedPassiveAbility.Color2 = Pigments.Red;
            twoFacedPassiveAbility._triggerOn = new TriggerCalls[0];

            LoadedDBsHandler.PassiveDB.AddNewPassive("TwoFaced_ID", twoFacedPassiveAbility);

            ExtraAttackPassiveAbility BonusAttackPassive = Object.Instantiate(LoadedAssetsHandler.GetEnemy("Xiphactinus_EN").passiveAbilities[1]) as ExtraAttackPassiveAbility;
            BonusAttackPassive._passiveName = "Bonus attack (Abominable Amalgam)";
            BonusAttackPassive._enemyDescription = "Spligis performs \"Abominable Amalgam\" as an additonal attack.";
            BonusAttackPassive.m_PassiveID = "BonusAttack_Amalgam_ID";

            #endregion passives

            #region ScriptableObjects

            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Shield_ID", out FieldEffect_SO Shield);
            LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Scars_ID", out StatusEffect_SO Scars);

            StatusEffect_Apply_Effect ApplyScarsEffect = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyScarsEffect._Status = Scars;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = Shield;

            IndexEffectConditon IfSecondEffect = ScriptableObject.CreateInstance<IndexEffectConditon>();
            IfSecondEffect.EffectIndex = 1;

            IndexEffectConditon IfFirstEffectFalse = ScriptableObject.CreateInstance<IndexEffectConditon>();
            IfFirstEffectFalse.EffectIndex = 0;
            IfFirstEffectFalse.wasSuccessful = false;

            IndexEffectConditon IfFirstEffectTrue = ScriptableObject.CreateInstance<IndexEffectConditon>();
            IfFirstEffectTrue.EffectIndex = 0;

            CheckHealthColorEffect CheckIf_BlueOrPurple_Effect = ScriptableObject.CreateInstance<CheckHealthColorEffect>();
            CheckIf_BlueOrPurple_Effect._color1 = Pigments.Blue;
            CheckIf_BlueOrPurple_Effect._color2 = Pigments.Purple;

            CheckHealthColorEffect CheckIf_YellowOrPurple_Effect = ScriptableObject.CreateInstance<CheckHealthColorEffect>();
            CheckIf_YellowOrPurple_Effect._color1 = Pigments.Purple;
            CheckIf_YellowOrPurple_Effect._color2 = Pigments.Yellow;

            CheckHealthColorEffect CheckIf_RedOrBlue_Effect = ScriptableObject.CreateInstance<CheckHealthColorEffect>();
            CheckIf_RedOrBlue_Effect._color1 = Pigments.Red;
            CheckIf_RedOrBlue_Effect._color2 = Pigments.Blue;

            #endregion ScriptableObjects

            #region BonusAttack

            Ability ability = new Ability("Abominable Amalgam", "AbominableAmalgam_ID");
            ability.AbilitySprite = EXOP._mungEN.abilities[0].ability.abilitySprite;
            ability.Description = "Change the Opposing party members health color to match this enemy health color.\nApply 2 shield to this enemy position.";
            ability.Rarity.rarityValue = 35;
            ability.ability.priority.priorityValue = -3;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<ChangeHealthColorByCasterColorEffect>(), entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ApplyShield, entryVariable = 2, targets = Targeting.Slot_SelfSlot },
            };
            ability.Visuals = EXOP._wrigglingSacrifice.abilities[0].ability.visuals;
            ability.AnimationTarget = Targeting.Slot_SelfSlot;
            ability.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Field_Shield" });
            ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Mana_Generate" });

            BonusAttackPassive._extraAbility.ability = ability.GenerateEnemyAbility().ability;
            BonusAttackPassive._extraAbility.rarity = ability.Rarity;

            #endregion BonusAttack 

            Enemy enemy = EXOP.EnemyInfoSetter("Spligis", 90, Pigments.Blue, EXOP._splig.damageSound, EXOP._splig.deathSound);
            enemy.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/CrayolaPede/RedBlueCrayola.prefab", MainClass.assetBundle);
            enemy.CombatSprite = ResourceLoader.LoadSprite("RedBlueCrayolaIcon");
            enemy.OverworldAliveSprite = ResourceLoader.LoadSprite("RedBlueCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.OverworldDeadSprite = ResourceLoader.LoadSprite("RedBlueCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.AddUnitType("HiveMindMaster");
            enemy.AddPassives(new BasePassiveAbilitySO[] { twoFacedPassiveAbility, BonusAttackPassive });

            Ability ability2 = new Ability("Two Minded", "TwoMinded_ID");
            ability2.Description = "Deal a Painful amount of damage to the Left and Right party member's.\nIf the Left or Right party member has Blue or Purple health, move Left or Right and deal a Painful amount of damage to the Left and Right party member's.";
            ability2.Rarity.rarityValue = 40;
            ability2.ability.priority.priorityValue = 2;
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 4, targets = Targeting.Slot_OpponentSides },
                new EffectInfo() { effect = CheckIf_BlueOrPurple_Effect, entryVariable = 0, targets = Targeting.Slot_OpponentSides },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable = 0, targets = Targeting.Slot_SelfSlot, condition = IfSecondEffect },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 4, targets = Targeting.Slot_OpponentSides, condition = IfSecondEffect },
            };
            ability2.Visuals = EXOP._Charcarrion.abilities[0].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_OpponentSides;
            ability2.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Swap_Sides" });
            ability2.AddIntentsToTarget(Targeting.Slot_OpponentSides, new string[] { "Damage_3_6", "Damage_3_6" });

            Ability ability3 = new Ability("Clear Stagnation", "ClearStagnation_ID");
            ability3.Description = "If the Opposing party member health is purple or yellow, inflicts 2 scar and slightly heals the Opposing enemy.\nIf else or there is no Opposing party member, then deal an a little amount of damage to All party member's.";
            ability3.Rarity.rarityValue = 40;
            ability3.ability.priority.priorityValue = 2;
            ability3.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = CheckIf_YellowOrPurple_Effect, entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ApplyScarsEffect, entryVariable = 2, targets = Targeting.Slot_Front, condition = IfFirstEffectTrue },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<HealEffect>(), entryVariable = 2, targets = Targeting.Slot_Front, condition = IfFirstEffectTrue },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 2, targets = Targeting.Unit_AllOpponents, condition = IfFirstEffectFalse },
            };
            ability3.Visuals = EXOP._hans.rankedData[0].rankAbilities[0].ability.visuals;
            ability3.AnimationTarget = Targeting.Slot_Front;
            ability3.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Status_Scars", "Heal_1_4" });
            ability3.AddIntentsToTarget(Targeting.Unit_AllOpponents, new string[] { "Damage_1_2" });

            Ability ability4 = new Ability("Cleanse the vile", "Cleansethevile_ID");
            ability4.Description = "If the Opposing party member has red or blue health deal a Mortal amount of damage to that party member.\nIf else deal a Agonizing amount of damage to that party member.";
            ability4.Rarity.rarityValue = 40;
            ability4.ability.priority.priorityValue = 2;
            ability4.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = CheckIf_RedOrBlue_Effect, entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 21, targets = Targeting.Slot_Front, condition = IfFirstEffectTrue },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 8, targets = Targeting.Slot_Front, condition = IfFirstEffectFalse },

            };
            ability4.Visuals = EXOP._jumbleGutsWaning.abilities[0].ability.visuals;
            ability4.AnimationTarget = Targeting.Slot_Front;
            ability4.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_7_10", "Damage_21" });

            enemy.AddEnemyAbilities(new Ability[]
            {
                ability2,
                ability3,
                ability4
            });

            ExtraUtils.AddBaseEnemyABSprite(enemy.enemy.abilities);
            enemy.AddEnemy();
        }
    }
}
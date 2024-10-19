using BrutalAPI;
using CrayolapedeModinreallife.PassiveAbilities;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.Enemies
{
    public class Lymphropod
    {
        public static void Add()
        {
            #region Passive

            LymphropodPassiveAbility lymphropodPassive = ScriptableObject.CreateInstance<LymphropodPassiveAbility>();
            lymphropodPassive._passiveName = "Territorial";
            lymphropodPassive.passiveIcon = ResourceLoader.LoadSprite("BugPassive");
            lymphropodPassive.m_PassiveID = "Territorial_ID";
            lymphropodPassive._enemyDescription = "If a party member moves infront of this enemy position, this enemy will perform an additional attack.";
            lymphropodPassive._characterDescription = "Lol im brutal.";
            lymphropodPassive._triggerOn = new TriggerCalls[0];

            LoadedDBsHandler.PassiveDB.AddNewPassive("Territorial_ID", lymphropodPassive);

            #endregion Passive

            #region ScriptableObjects

            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Shield_ID", out FieldEffect_SO Shield);

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = Shield;

            #endregion ScriptableObjects

            Enemy enemy = EXOP.EnemyInfoSetter("Lymphropod", 12, Pigments.Red, LoadedAssetsHandler.GetEnemy("FlaMinGoa_EN"));
            enemy.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Lymphropod/Lymp.prefab", MainClass.assetBundle);
            enemy.CombatSprite = ResourceLoader.LoadSprite("BugIcon");
            enemy.OverworldAliveSprite = ResourceLoader.LoadSprite("BugIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.OverworldDeadSprite = ResourceLoader.LoadSprite("BugIconDead", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_ByRarity>();
            enemy.AddPassive(lymphropodPassive);

            Ability ability = new Ability("Kick", "Kick_ID");
            ability.Description = "Deals a Painful amount of damage to the Opposing party member, move the Opposing party member Left or Right.";
            ability.Rarity.rarityValue = 4;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 3, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<SwapToSidesEffect>(), entryVariable =1, targets = Targeting.Slot_Front },
            };
            ability.Visuals = EXOP._keko.abilities[0].ability.visuals;
            ability.AnimationTarget = Targeting.Slot_Front;
            ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6", "Swap_Sides" });

            Ability ability2 = new Ability("Jaw Crush", "JawCrush_ID");
            ability2.Description = "Deals a Painful amount of damage to the Opposing party member.";
            ability2.Rarity.rarityValue = 6;
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 4, targets = Targeting.Slot_Front },
            };
            ability2.Visuals = EXOP._keko.abilities[0].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_Front;
            ability2.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6" });

            Ability ability3 = new Ability("Trap Hole", "TrapHole_ID");
            ability3.Description = "Applys 5 Shield to this enemy current position.";
            ability3.Rarity.rarityValue = 5;
            ability3.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ApplyShield, entryVariable = 5, targets = Targeting.Slot_SelfAll },
            };
            ability3.Visuals = EXOP._boyle.rankedData[0].rankAbilities[2].ability.visuals;
            ability3.AnimationTarget = Targeting.Slot_SelfAll;
            ability3.AddIntentsToTarget(Targeting.Slot_SelfAll, new string[] { "Field_Shield" });

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
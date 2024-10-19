using BepInEx;
using BrutalAPI;
using CrayolapedeModinreallife.AbilityEffects;
using CrayolapedeModinreallife.Conditions;
using CrayolapedeModinreallife.PassiveAbilities;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace CrayolapedeModinreallife.Enemies
{
    public static class BossSpluglings
    {
        public static EnemySO[] BossSplugs;

        public static void Add()
        {

            #region HiveMindPassive

            HiveMindPassiveAbility HivemindPassive = ScriptableObject.CreateInstance<HiveMindPassiveAbility>();
            HivemindPassive._passiveName = "Hivemind";
            HivemindPassive.passiveIcon = ResourceLoader.LoadSprite("HivemindIcon");
            HivemindPassive.m_PassiveID = "Hivemind_ID";
            HivemindPassive._enemyDescription = "This enemies existances soley relys in Crayolapede. If the Crayolapede is still alive, upon death this enemy will transform into another enemy.\nIf else this enemy will die.";
            HivemindPassive._characterDescription = "f";
            HivemindPassive._triggerOn = new TriggerCalls[0];

            LoadedDBsHandler.PassiveDB.AddNewPassive("Hivemind", HivemindPassive);

            #endregion HiveMindPassive

            #region TwoFacedPassives 

            TwoFacedPassiveAbility RedPurpleTwoFaced = ScriptableObject.CreateInstance<TwoFacedPassiveAbility>();
            RedPurpleTwoFaced._passiveName = "Two Faced";
            RedPurpleTwoFaced.passiveIcon = ResourceLoader.LoadSprite("PurpleRedTwoFace");
            RedPurpleTwoFaced.m_PassiveID = "TwoFaced_RedPurple";
            RedPurpleTwoFaced._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from Red to Purple or vice versa.";
            RedPurpleTwoFaced._characterDescription = "Upon receiving direct damage this enemy will change its health colour from red to blue or vice versa.";
            RedPurpleTwoFaced.Color1 = Pigments.Red;
            RedPurpleTwoFaced.Color2 = Pigments.Purple;
            RedPurpleTwoFaced._triggerOn = new TriggerCalls[0];

            TwoFacedPassiveAbility BluePurpleTwoFaced = ScriptableObject.CreateInstance<TwoFacedPassiveAbility>();
            BluePurpleTwoFaced._passiveName = "Two Faced";
            BluePurpleTwoFaced.passiveIcon = ResourceLoader.LoadSprite("BluePurpleTwoFace");
            BluePurpleTwoFaced.m_PassiveID = "Two Faced_BluePurple";
            BluePurpleTwoFaced._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from Red to Purple or vice versa.";
            BluePurpleTwoFaced._characterDescription = "Upon receiving direct damage this enemy will change its health colour from red to blue or vice versa.";
            BluePurpleTwoFaced.Color1 = Pigments.Blue;
            BluePurpleTwoFaced.Color2 = Pigments.Purple;
            BluePurpleTwoFaced._triggerOn = new TriggerCalls[0];

            TwoFacedPassiveAbility BlueYellowTwoFaced = ScriptableObject.CreateInstance<TwoFacedPassiveAbility>();
            BlueYellowTwoFaced._passiveName = "Two Faced";
            BlueYellowTwoFaced.passiveIcon = ResourceLoader.LoadSprite("YellowBlueTwoFace");
            BlueYellowTwoFaced.m_PassiveID = "Two Faced_BlueYellow";
            BlueYellowTwoFaced._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from Red to Purple or vice versa.";
            BlueYellowTwoFaced._characterDescription = "Upon receiving direct damage this enemy will change its health colour from red to blue or vice versa.";
            BlueYellowTwoFaced.Color1 = Pigments.Blue;
            BlueYellowTwoFaced.Color2 = Pigments.Yellow;
            BlueYellowTwoFaced._triggerOn = new TriggerCalls[0];

            TwoFacedPassiveAbility RedYellowTwoFaced = ScriptableObject.CreateInstance<TwoFacedPassiveAbility>();
            RedYellowTwoFaced._passiveName = "Two Faced";
            RedYellowTwoFaced.passiveIcon = ResourceLoader.LoadSprite("RedYellowTwoFace");
            RedYellowTwoFaced.m_PassiveID = "Two Faced_RedYellow";
            RedYellowTwoFaced._enemyDescription = "Upon receiving direct damage this enemy will change its health colour from Red to Purple or vice versa.";
            RedYellowTwoFaced._characterDescription = "Upon receiving direct damage this enemy will change its health colour from red to blue or vice versa.";
            RedYellowTwoFaced.Color1 = Pigments.Red;
            RedYellowTwoFaced.Color2 = Pigments.Yellow;
            RedYellowTwoFaced._triggerOn = new TriggerCalls[0];

            #endregion TwoFacedPassives

            #region ScriptableObjects

            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Shield_ID", out FieldEffect_SO Shield);
            LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Frail_ID", out StatusEffect_SO Frail);

            StatusEffect_Apply_Effect ApplyFrailEffect = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ApplyFrailEffect._Status = Frail;

            FieldEffect_Apply_Effect ApplyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShield._Field = Shield;

            FieldEffect_Apply_Effect ApplyShieldUsingExitValue = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyShieldUsingExitValue._Field = Shield;
            ApplyShieldUsingExitValue._UsePreviousExitValueAsMultiplier = true;

            CheckHealthColorEffect CheckIf_BlueOrPurple_Effect = ScriptableObject.CreateInstance<CheckHealthColorEffect>();
            CheckIf_BlueOrPurple_Effect._color1 = Pigments.Blue;
            CheckIf_BlueOrPurple_Effect._color2 = Pigments.Purple;

            CheckHealthColorEffect CheckIf_RedOrPurple_Effect = ScriptableObject.CreateInstance<CheckHealthColorEffect>();
            CheckIf_RedOrPurple_Effect._color1 = Pigments.Purple;
            CheckIf_RedOrPurple_Effect._color2 = Pigments.Red;

            CheckHealthColorEffect CheckIf_RedOrYelow_Effect = ScriptableObject.CreateInstance<CheckHealthColorEffect>();
            CheckIf_RedOrYelow_Effect._color1 = Pigments.Yellow;
            CheckIf_RedOrYelow_Effect._color2 = Pigments.Red;

            CheckHealthColorEffect CheckIf_BlueOrYelow_Effect = ScriptableObject.CreateInstance<CheckHealthColorEffect>();
            CheckIf_BlueOrYelow_Effect._color1 = Pigments.Blue;
            CheckIf_BlueOrYelow_Effect._color2 = Pigments.Yellow;

            IndexEffectConditon IfFirstEffectTrue = ScriptableObject.CreateInstance<IndexEffectConditon>();
            IfFirstEffectTrue.EffectIndex = 0;

            HealEffect HealUsingExitValueEffect = ScriptableObject.CreateInstance<HealEffect>();
            HealUsingExitValueEffect.usePreviousExitValue = true;

            PreviousEffectCondition previousEffectCondition = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            previousEffectCondition.wasSuccessful = true;

            PreviousEffectCondition previousEffectConditionFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            previousEffectConditionFalse.wasSuccessful = false;

            #endregion ScriptableObjects

            #region Abilities

            Ability Defualt_AB = new Ability("Crayola Catastrophe", "Crayola Catastrophe_AB");
            Defualt_AB.Description = "Changes the left enemy health color to this enemy health color. Applies 2 shield to this enemy position.";
            Defualt_AB.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<ChangeHealthColorByCasterColorEffect>(), entryVariable = 1, targets = Targeting.Slot_AllyLeft },
                new EffectInfo() { effect = ApplyShield, entryVariable = 1, targets = Targeting.Slot_SelfSlot },
            };
            Defualt_AB.Visuals = EXOP._jumbleGutsWaning.abilities[1].ability.visuals;
            Defualt_AB.AnimationTarget = Targeting.Slot_SelfSlot;
            Defualt_AB.Rarity.rarityValue = 40;
            Defualt_AB.ability.priority.priorityValue = 1;
            Defualt_AB.AddIntentsToTarget(Targeting.Slot_AllyLeft, new string[] { "Mana_Generate" });
            Defualt_AB.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Field_Shield" });

            //////////

            Ability DefualtTwo_AB = new Ability("Abominable Amalgam", "Abominable Amalgam_AB");
            DefualtTwo_AB.Description = "Changes the Opposing party member health color to this enemy health color.\nDeals a Painful amount of damage to the Opposing party member.";
            DefualtTwo_AB.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<ChangeHealthColorByCasterColorEffect>(), entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamageEffect>(), entryVariable = 4, targets = Targeting.Slot_Front },
            };
            DefualtTwo_AB.Visuals = EXOP._wrigglingSacrifice.abilities[0].ability.visuals;
            DefualtTwo_AB.AnimationTarget = Targeting.Slot_Front;
            DefualtTwo_AB.Rarity.rarityValue = 40;
            DefualtTwo_AB.ability.priority.priorityValue = 2;
            DefualtTwo_AB.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Mana_Generate", "Damage_3_6" });

            //////////

            Ability BluePurpleSplug_Ability = new Ability("Botched Stitch Job", "Botched Stitch Job_AB");
            BluePurpleSplug_Ability.Description = "Inflicts 3 Frail to the Opposing party member if their health color is Blue or Purple.";
            BluePurpleSplug_Ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = CheckIf_BlueOrPurple_Effect, entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ApplyFrailEffect, entryVariable = 3, targets = Targeting.Slot_Front, condition = previousEffectCondition },
            };
            BluePurpleSplug_Ability.Visuals = EXOP._hans.rankedData[0].rankAbilities[1].ability.visuals;
            BluePurpleSplug_Ability.AnimationTarget = Targeting.Slot_Front;
            BluePurpleSplug_Ability.Rarity.rarityValue = 40;
            BluePurpleSplug_Ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Status_Frail" });

            //////////

            Ability RedPurpleSplug_Ability = new Ability("Pigment Congestion", "Pigment Congestion_AB");
            RedPurpleSplug_Ability.Description = "If the Opposing character has Red or Purple health, consumes all pigments of that party member health color, heal all enemies for the amount of pigments consumed.";
            RedPurpleSplug_Ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = CheckIf_RedOrPurple_Effect, entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<ConsumeTargetHealthManaEffect>(), entryVariable = 0, targets = Targeting.Slot_Front, condition = IfFirstEffectTrue },
                new EffectInfo() { effect = HealUsingExitValueEffect, entryVariable = 1, targets = Targeting.Unit_AllAllies, condition = IfFirstEffectTrue },
            };
            RedPurpleSplug_Ability.Visuals = EXOP._jumbleGutsWaning.abilities[1].ability.visuals;
            RedPurpleSplug_Ability.AnimationTarget = Targeting.Slot_SelfSlot;
            RedPurpleSplug_Ability.Rarity.rarityValue = 40;
            RedPurpleSplug_Ability.ability.priority.priorityValue = 1;
            RedPurpleSplug_Ability.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Mana_Consume" });
            RedPurpleSplug_Ability.AddIntentsToTarget(Targeting.Unit_AllAllies, new string[] { "Heal_1_4" });

            //////////

            Ability RedYellowSplug_Ability = new Ability("Dripping Marrow", "Dripping Marrow_AB");
            RedYellowSplug_Ability.Description = "If the Opposing enemy dosent have Red or Yellow health, Deals a Painful amount of wet damage to Opposing party member.";
            RedYellowSplug_Ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = CheckIf_RedOrYelow_Effect, entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<WetDamageEffect>(), entryVariable = 4, targets = Targeting.Slot_Front, condition = previousEffectConditionFalse },
            };
            RedYellowSplug_Ability.Visuals = EXOP._jumbleGutsWaning.abilities[0].ability.visuals;
            RedYellowSplug_Ability.AnimationTarget = Targeting.Slot_Front;
            RedYellowSplug_Ability.Rarity.rarityValue = 40;
            RedYellowSplug_Ability.ability.priority.priorityValue = 1;
            RedYellowSplug_Ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6" });

            //////////

            Ability BlueYellowSplug_Ability = new Ability("Duel Conscious", "Duel Conscious_AB");
            BlueYellowSplug_Ability.Description = "Applies Shield to all enemies equal to the amount of party members with blue or yellow health.";
            BlueYellowSplug_Ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = CheckIf_BlueOrYelow_Effect, entryVariable = 0, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ApplyShieldUsingExitValue, entryVariable = 1, targets = Targeting.Unit_AllAllies },
            };
            BlueYellowSplug_Ability.Visuals = EXOP._jumbleGutsFlummoxing.abilities[0].ability.visuals;
            BlueYellowSplug_Ability.AnimationTarget = Targeting.Slot_Front;
            BlueYellowSplug_Ability.Rarity.rarityValue = 40;
            BlueYellowSplug_Ability.ability.priority.priorityValue = 1;
            BlueYellowSplug_Ability.AddIntentsToTarget(Targeting.Unit_AllAllies, new string[] { "Field_Shield" });

            #endregion Abilities

            Enemy RedYellowSplug = EXOP.EnemyInfoSetter("Wailing Splugling", 15, Pigments.Yellow, EXOP._splig.damageSound, EXOP._splig.deathSound);
            RedYellowSplug.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Splug_RY/RedYellowCrayola.prefab", MainClass.assetBundle);
            RedYellowSplug.CombatSprite = ResourceLoader.LoadSprite("RedYellowCrayolaIcon");
            RedYellowSplug.OverworldAliveSprite = ResourceLoader.LoadSprite("RedYellowCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            RedYellowSplug.OverworldDeadSprite = ResourceLoader.LoadSprite("RedYellowCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            RedYellowSplug.AddUnitType("Splugling");
            RedYellowSplug.AddPassives(new BasePassiveAbilitySO[] { RedYellowTwoFaced, HivemindPassive, Passives.Forgetful });
            RedYellowSplug.AddEnemyAbilities(new Ability[]
            {
                Defualt_AB,
                DefualtTwo_AB,
                RedYellowSplug_Ability
            });
            RedYellowSplug.AddEnemy();

            Enemy RedPurpleSplug = EXOP.EnemyInfoSetter("Bellowing Splugling", 15, Pigments.Red, EXOP._splig.damageSound, EXOP._splig.deathSound);
            RedPurpleSplug.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Splug_RP/RedPurpleCrayola.prefab", MainClass.assetBundle);
            RedPurpleSplug.CombatSprite = ResourceLoader.LoadSprite("RedPurpleCrayolaIcon");
            RedPurpleSplug.OverworldAliveSprite = ResourceLoader.LoadSprite("RedPurpleCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            RedPurpleSplug.OverworldDeadSprite = ResourceLoader.LoadSprite("RedPurpleCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            RedPurpleSplug.AddUnitType("Splugling");
            RedPurpleSplug.AddPassives(new BasePassiveAbilitySO[] { RedPurpleTwoFaced, HivemindPassive, Passives.Forgetful });
            RedPurpleSplug.AddEnemyAbilities(new Ability[]
            {
                Defualt_AB,
                DefualtTwo_AB,
                RedPurpleSplug_Ability
            });
            RedPurpleSplug.AddEnemy();

            Enemy BlueYellowSplug = EXOP.EnemyInfoSetter("Festering Splugling", 15, Pigments.Blue, EXOP._splig.damageSound, EXOP._splig.deathSound);
            BlueYellowSplug.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Splug_BY/YellowBlueCrayola.prefab", MainClass.assetBundle);
            BlueYellowSplug.CombatSprite = ResourceLoader.LoadSprite("BlueYellowCrayolaIcon");
            BlueYellowSplug.OverworldAliveSprite = ResourceLoader.LoadSprite("BlueYellowCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            BlueYellowSplug.OverworldDeadSprite = ResourceLoader.LoadSprite("BlueYellowCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            BlueYellowSplug.AddUnitType("Splugling");
            BlueYellowSplug.AddPassives(new BasePassiveAbilitySO[] { BlueYellowTwoFaced, HivemindPassive, Passives.Forgetful });
            BlueYellowSplug.AddEnemyAbilities(new Ability[]
            {
                Defualt_AB,
                DefualtTwo_AB,
                BlueYellowSplug_Ability
            });
            BlueYellowSplug.AddEnemy();

            Enemy BluePurpleSplug = EXOP.EnemyInfoSetter("Weeping Splugling", 15, Pigments.Purple, EXOP._splig.damageSound, EXOP._splig.deathSound);
            BluePurpleSplug.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/Splug_PB/PurpleBlueCrayola.prefab", MainClass.assetBundle);
            BluePurpleSplug.CombatSprite = ResourceLoader.LoadSprite("BluePurpleCrayolaIcon");
            BluePurpleSplug.OverworldAliveSprite = ResourceLoader.LoadSprite("BluePurpleCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            BluePurpleSplug.OverworldDeadSprite = ResourceLoader.LoadSprite("BluePurpleCrayolaIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            BluePurpleSplug.AddUnitType("Splugling");
            BluePurpleSplug.AddPassives(new BasePassiveAbilitySO[] { BluePurpleTwoFaced, HivemindPassive, Passives.Forgetful });
            BluePurpleSplug.AddEnemyAbilities(new Ability[]
            {
                Defualt_AB,
                DefualtTwo_AB,
                BluePurpleSplug_Ability
            });
            BluePurpleSplug.AddEnemy();

            BossSplugs = new EnemySO[] { RedYellowSplug.enemy, RedPurpleSplug.enemy, BlueYellowSplug.enemy, BluePurpleSplug.enemy };

            for (int i = 0; i < BossSplugs.Length; i++)
                ExtraUtils.AddBaseEnemyABSprite(BossSplugs[i].abilities);
        }
    }
}

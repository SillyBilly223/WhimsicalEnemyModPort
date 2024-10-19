using BrutalAPI;
using CrayolapedeModinreallife.AbilityEffects;
using CrayolapedeModinreallife.AbilitySelectors;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.Enemies
{
    public static class FumeFactory
    {
        public static void Add()
        {
            #region ScriptableObject

            AbilitySelector_FumeFactory abilitySelector_FumeStacks = ScriptableObject.CreateInstance<AbilitySelector_FumeFactory>();

            DamageEffect damageEffect = ScriptableObject.CreateInstance<DamageEffect>();
            damageEffect._indirect = true;

            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect("Fumes_ID", out FieldEffect_SO Fumes);

            FieldEffect_Apply_Effect ApplyFumes = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            ApplyFumes._Field = Fumes;

            FieldEffect_Increase_Effect IncreaseFumes = ScriptableObject.CreateInstance<FieldEffect_Increase_Effect>();
            IncreaseFumes._Field = Fumes;

            #endregion ScriptableObject

            Enemy enemy = EXOP.EnemyInfoSetter("Fume Factory", 30, Pigments.Purple, EXOP._smokeStacks);
            enemy.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/InfectedSmokeStacks/InfectedSmokesStacks.prefab", MainClass.assetBundle);
            enemy.enemy.enemyTemplate.m_Data.m_Gibs = MainClass.SaltGibs.LoadAsset<GameObject>("Assets/The/EvilSmoke_Gibs.prefab").GetComponent<ParticleSystem>();
            enemy.CombatSprite = ResourceLoader.LoadSprite("InfectedSmokeStacksIcon");
            enemy.OverworldAliveSprite = ResourceLoader.LoadSprite("InfectedSmokeStacksIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.OverworldDeadSprite = ResourceLoader.LoadSprite("InfectedSmokeStacksIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.AbilitySelector = abilitySelector_FumeStacks;
            enemy.AddPassives(new BasePassiveAbilitySO[] { Passives.Skittish, Passives.Forgetful });

            Ability ability = new Ability("Hot Fever", "HotFever_ID");
            ability.Description = "Inflicts 4 fumes to the Opposing party members location.";
            ability.Rarity.rarityValue = 4;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ApplyFumes, entryVariable = 4, targets = Targeting.Slot_Front },
            };
            ability.Visuals = EXOP._thype.rankedData[0].rankAbilities[1].ability.visuals;
            ability.AnimationTarget = Targeting.Slot_Front;
            ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "ApplyFumes" });

            Ability ability2 = new Ability("Coughing Fit", "CoughingFit_AB");
            ability2.Rarity.rarityValue = 3;
            ability2.ability.priority.priorityValue = 1;
            ability2.Description = "Inflicts 2 random negative status effects to the Opposing party member.";
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<StatusEffect_ApplyRandom_Effect>(), entryVariable = 2, targets = Targeting.Slot_Front },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<StatusEffect_ApplyRandom_Effect>(), entryVariable = 2, targets = Targeting.Slot_Front },
            };
            ability2.Visuals = EXOP._flaMinGoa.abilities[2].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_Front;
            ability2.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Mana_Consume" });

            Ability ability3 = new Ability("Burning Growth", "BurningGrowth_AB");
            ability3.Rarity.rarityValue = 4;
            ability3.ability.priority.priorityValue = 0;
            ability3.Description = "Increases all Fumes on character side by 2.\nInflicts 2 Fumes to the Opposing party member.";
            ability3.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = IncreaseFumes, entryVariable = 2, targets = Targeting.Slot_OpponentAllSlots },
                new EffectInfo() { effect = ApplyFumes, entryVariable = 2, targets = Targeting.Slot_Front },
            };
            ability3.Visuals = EXOP._rags.rankedData[0].rankAbilities[1].ability.visuals;
            ability3.AnimationTarget = Targeting.Slot_Front;
            ability3.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, new string[] { "ApplyFumes" });
            ability3.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "ApplyFumes" });

            abilitySelector_FumeStacks._SelectedAbility = ability3.ability.name;

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

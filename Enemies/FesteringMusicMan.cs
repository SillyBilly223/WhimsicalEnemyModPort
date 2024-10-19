using BrutalAPI;
using CrayolapedeModinreallife.AbilityEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.Enemies
{
    public static class FesteringMusicMan
    {
        public static void Add()
        {

            #region ScriptableObjects

            GetMissingHealthEffect getMissingHealthEffect = ScriptableObject.CreateInstance<GetMissingHealthEffect>();
            getMissingHealthEffect._DecreaseByPercentage = true;

            #endregion ScriptableObjects

            //FesteringMusicMan
            Enemy enemy = EXOP.EnemyInfoSetter("Festering Music Man", 25, Pigments.Red, EXOP._musicMan.damageSound, EXOP._musicMan.deathSound);
            enemy.PrepareEnemyPrefab("Assets/WhimsicalEnemyMod/Enemies/BrokenMusicMan/BrokenMusicMan.prefab", MainClass.assetBundle);
            enemy.enemy.enemyTemplate.m_Data.m_Gibs = MainClass.SaltGibs.LoadAsset<GameObject>("Assets/The/MusicMaggot_Gibs.prefab").GetComponent<ParticleSystem>();
            enemy.CombatSprite = ResourceLoader.LoadSprite("BrokenMusicManIcon");
            enemy.OverworldAliveSprite = ResourceLoader.LoadSprite("BrokenMusicManIcon", new Vector2?(new Vector2(0.5f, 0.05f)));
            enemy.OverworldDeadSprite = EXOP._conductor.enemyOWCorpseSprite;
            enemy.AbilitySelector = ScriptableObject.CreateInstance<AbilitySelector_ByRarity>();
            enemy.AddPassive(Passives.Skittish);

            Ability ability = new Ability("Off Tune", "OffTune_AB");
            ability.Description = "Deals a painful amount of damage to the Opposing party member.\nIncrease damage equivalent to this enemy missing health.";
            ability.Rarity.rarityValue = 1;
            ability.ability.priority.priorityValue = 2;
            ability.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = ScriptableObject.CreateInstance<GetMissingHealthEffect>(), entryVariable = 0, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamagePlusePreviousEffect>(), entryVariable = 3, targets = Targeting.Slot_Front },
            };
            ability.Visuals = EXOP._conductor.abilities[2].ability.visuals;
            ability.AnimationTarget = Targeting.Slot_Front;
            ability.AddIntentsToTarget(Targeting.Slot_Front, new string[] { "Damage_3_6" });
            ability.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Other_MaxHealth_Alt" });

            Ability ability2 = new Ability("Disrupt The Rhythm", "DisruptTheRhythm_AB");
            ability2.Description = "deals a painful amount of damage to the Left and Right party member.\nIncrease damage based on half of this enemy missing health";
            ability2.Rarity.rarityValue = 1;
            ability2.ability.priority.priorityValue = 1;
            ability2.Effects = new EffectInfo[]
            {
                new EffectInfo() { effect = getMissingHealthEffect, entryVariable = 0, targets = Targeting.Slot_SelfSlot },
                new EffectInfo() { effect = ScriptableObject.CreateInstance<DamagePlusePreviousEffect>(), entryVariable = 3, targets = Targeting.Slot_OpponentSides },
            };
            ability2.Visuals = EXOP._conductor.abilities[2].ability.visuals;
            ability2.AnimationTarget = Targeting.Slot_OpponentSides;
            ability2.AddIntentsToTarget(Targeting.Slot_OpponentSides, new string[] { "Damage_3_6" });
            ability2.AddIntentsToTarget(Targeting.Slot_SelfSlot, new string[] { "Other_MaxHealth_Alt" });

            enemy.AddEnemyAbilities(new Ability[]
            {
                ability,
                ability2,
            });

            ExtraUtils.AddBaseEnemyABSprite(enemy.enemy.abilities);
            enemy.AddEnemy();
        }
    }
}

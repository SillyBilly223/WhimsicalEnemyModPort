using BrutalAPI;
using CrayolapedeModinreallife.Actions;
using CrayolapedeModinreallife.PassiveAbilities;
using MonoMod.RuntimeDetour;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.CanvasScaler;

namespace CrayolapedeModinreallife
{
    public static class ExtraUtils
    {
        #region hooks
        public static void EnemyDeath(Action<EnemyCombat, DeathReference, string> orig, EnemyCombat self, DeathReference deathReference, string deathTypeID)
        {
            if (ExtraUtils.SheoInCombat > 0)
            {
                foreach (EnemyCombat enemyCombat in CombatManager.Instance._stats.EnemiesOnField.Values)
                {
                    if (enemyCombat.ContainsPassiveAbility("Scavanger_ID") && enemyCombat.IsAlive && enemyCombat.CurrentHealth > 0)
                    {
                        CombatManager.Instance.PostNotification("ScavangerTrigger", enemyCombat, self);
                    }
                }
            } 
            orig(self, deathReference, deathTypeID);
        }


        public delegate void OutFourth<T1, T2, T3, T4>(T1 a, T2 b, T3 c, out T4 d);
        public static void CharacterDeath(OutFourth<CharacterCombat, DeathReference, string, bool> orig, CharacterCombat self, DeathReference deathReference, string deathTypeID, out bool canBeRemoved)
        {
            if (ExtraUtils.SheoInCombat > 0)
            {
                foreach (EnemyCombat enemyCombat in CombatManager.Instance._stats.EnemiesOnField.Values)
                {
                    if (enemyCombat.ContainsPassiveAbility("Scavanger_ID") && enemyCombat.IsAlive && enemyCombat.CurrentHealth > 0)
                    {
                        CombatManager.Instance.PostNotification("ScavangerTrigger", enemyCombat, self);
                    }
                }
            }
            orig(self, deathReference, deathTypeID, out canBeRemoved);
        }

        public static void SwapTo(Action<CharacterCombat, int> orig, CharacterCombat self, int slotID)
        {
            orig(self, slotID);
            if (ExtraUtils.LymphropodInCombat > 0)
            {
                foreach (EnemyCombat enemyCombat in CombatManager.Instance._stats.EnemiesOnField.Values)
                {
                    if (enemyCombat.ContainsPassiveAbility("Territorial_ID") && enemyCombat.IsAlive && enemyCombat.SlotID == slotID)
                    {
                        CombatManager.Instance.PostNotification("TerritorialTrigger", enemyCombat, self);
                    }
                }
            }
        }

        public static void SwappedTo(Action<CharacterCombat, int> orig, CharacterCombat self, int slotID)
        {
            orig(self, slotID);
            if (ExtraUtils.LymphropodInCombat > 0)
            {
                foreach (EnemyCombat enemyCombat in CombatManager.Instance._stats.EnemiesOnField.Values)
                {
                    if (enemyCombat.ContainsPassiveAbility("Territorial_ID") && enemyCombat.IsAlive && enemyCombat.SlotID == slotID)
                    {
                        CombatManager.Instance.PostNotification("TerritorialTrigger", enemyCombat, self);
                    }
                }
            }
        }
        #endregion hooks

        #region EvenglistSearch

        static private Coroutine EvenglistSearch;

        static public bool IsProcessingLeaderSearch = false;

        public static void StartEvenglistSearch()
        {
            if (EvenglistSearch != null) return;
            EvenglistSearch = CombatManager._instance.StartCoroutine(EvenglistCombatWatched());
        }

        public static void StopEvenglistSearch()
        {
            if (EvenglistSearch == null) return;
            EvenglistsInCombat = 0;
        }

        public static IEnumerator EvenglistCombatWatched()
        {
            CombatStats Stats = CombatManager._instance._stats;
            yield return GetHighestHealthEnemy(Stats);
            while (EvenglistsInCombat > 0)
            {
                yield return new WaitForSeconds(1.5f);
                if (!IsProcessingLeaderSearch )
                    yield return GetHighestHealthEnemy(Stats);
                yield return null;
            }
            EvenglistSearch = null;
            CombatManager._instance.AddPriorityRootAction(new RemoveLeaderAction());          
        }

        public static IEnumerator GetHighestHealthEnemy(CombatStats Stats)
        {
            if (IsProcessingLeaderSearch) yield break;
            List<EnemyCombat> Enemies = new List<EnemyCombat>();
            List<int> Health = new List<int>();
            bool ContiansFollower = false;

            foreach (EnemyCombat value in Stats.EnemiesOnField.Values)
            {
                if (value.ContainsPassiveAbility("Follower"))
                {
                    ContiansFollower = true;
                    continue;
                }
                if (value.IsAlive)
                {
                    Enemies.Add(value);
                    Health.Add(value.CurrentHealth);
                }
            }

            if (Enemies.Count == 0 || !ContiansFollower) yield break;

            EnemyCombat ChosenEnemy = null;
            EnemyCombat CurrentLeader = null;
            int LargestHealth = Health.Max();

            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].CurrentHealth == LargestHealth)
                {
                    ChosenEnemy = Enemies[i];
                }
                if (Enemies[i].ContainsPassiveAbility("Leader"))
                {
                    CurrentLeader = Enemies[i];
                }
            }

            yield return null;

            if (ChosenEnemy == null || CurrentLeader != null && ChosenEnemy == CurrentLeader) yield break;

            IsProcessingLeaderSearch = true;
            CombatManager._instance.AddPriorityRootAction(new FindLeaderAction(ChosenEnemy, CurrentLeader));
        }

        #endregion EvenglistSearch

        #region misc
        public static EffectSO AutoSetFieldEffectEffects(EffectSO effect, string fieldEffectID)
        {
            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect(fieldEffectID, out FieldEffect_SO fieldEffect);
            foreach (FieldInfo field in effect.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == typeof(FieldEffect_SO))
                    field.SetValue(effect, fieldEffect);
            return effect;
        }

        public static EffectSO AutoSetStatusEffectEffects(EffectSO effect, string statusEffectID)
        {
            LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(statusEffectID, out StatusEffect_SO statusEffect);
            foreach (FieldInfo field in effect.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == typeof(StatusEffect_SO))
                    field.SetValue(effect, statusEffect);
            return effect;
        }

        public static EffectSO AutoSetEffects(EffectSO effect, Type type, object obj)
        {
            foreach (FieldInfo field in effect.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == type)
                    field.SetValue(effect, obj);
            return effect;
        }

        public static T AutoSetObject<T>(T instobj, Type type, object obj)
        {
            foreach (FieldInfo field in instobj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
                if (field.FieldType == type)
                    field.SetValue(instobj, obj);
            return instobj;
        }

        public static T AutoSetScriptableObject<T>(Type type, object obj) where T : ScriptableObject
        {
            return AutoSetObject(ScriptableObject.CreateInstance<T>(), type, obj);
        }

        #endregion misc

        public static void AddBaseEnemyABSprite(List<EnemyAbilityInfo> Abilities)
        {
            Sprite sprite = EXOP._mungEN.abilities[0].ability.abilitySprite;
            for (int i = 0; i < Abilities.Count; i++)
                Abilities[i].ability.abilitySprite = sprite;
        }

        public static void SetUp()
        {
            IDetour detour1 = new Hook(typeof(EnemyCombat).GetMethod("EnemyDeath", (BindingFlags)(-1)), typeof(ExtraUtils).GetMethod("EnemyDeath", (BindingFlags)(-1)));
            IDetour detour2 = new Hook(typeof(CharacterCombat).GetMethod("CharacterDeath", (BindingFlags)(-1)), typeof(ExtraUtils).GetMethod("CharacterDeath", (BindingFlags)(-1)));
            IDetour detour3 = new Hook(typeof(CharacterCombat).GetMethod("SwapTo", (BindingFlags)(-1)), typeof(ExtraUtils).GetMethod("SwapTo", (BindingFlags)(-1)));
            IDetour detour4 = new Hook(typeof(CharacterCombat).GetMethod("SwappedTo", (BindingFlags)(-1)), typeof(ExtraUtils).GetMethod("SwappedTo", (BindingFlags)(-1)));                  
        }

        public static int SheoInCombat;
        public static int LymphropodInCombat;
        public static int EvenglistsInCombat;
    }
}
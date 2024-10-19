using BrutalAPI;
using BrutalAPI.Items;
using CrayolapedeModinreallife.Hooks;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace CrayolapedeModinreallife
{
    public class FumesFE_SO : FieldEffect_SO
    {
        
        public static void SetUpFieldEffect()
        {
            FumesFE_SO fumesFE_SO = ScriptableObject.CreateInstance<FumesFE_SO>();
            fumesFE_SO._FieldID = "Fumes_ID";

            LoadedDBsHandler.StatusFieldDB.TryGetFieldEffect(TempFieldEffectID.OnFire_ID.ToString(), out FieldEffect_SO Fire);

            SlotStatusEffectInfoSO slotStatusEffectInfoSO = ScriptableObject.CreateInstance<SlotStatusEffectInfoSO>();
            slotStatusEffectInfoSO._fieldName = "Fumes";
            slotStatusEffectInfoSO.icon = ResourceLoader.LoadSprite("FumesIcon");
            slotStatusEffectInfoSO._description = "Upon Performing an ability, deal damage equal to the amount of Fumes to this enemy or party member.\nAt the end of each turn, the amount of Fumes is halfed.";
            slotStatusEffectInfoSO._applied_SE_Event = "event:/FumesApplyEvent";
            slotStatusEffectInfoSO._updated_SE_Event = Fire.EffectInfo._updated_SE_Event;
            slotStatusEffectInfoSO._removed_SE_Event = Fire.EffectInfo._removed_SE_Event;
            slotStatusEffectInfoSO.m_CharacterLayoutTemplate = MainClass.assetBundle.LoadAsset<GameObject>("Assets/WhimsicalEnemyMod/Enemies/InfectedSmokeStacks/Fumes_Char_FEL.prefab").GetComponent<CharacterFieldEffectLayout>();
            slotStatusEffectInfoSO.m_EnemyLayoutTemplate = MainClass.assetBundle.LoadAsset<GameObject>("Assets/WhimsicalEnemyMod/Enemies/InfectedSmokeStacks/Fumes_Enem_FEL.prefab").GetComponent<EnemyFieldEffectLayout>();

            fumesFE_SO._EffectInfo = slotStatusEffectInfoSO;

            IntentInfoBasic ApplyFumes = new IntentInfoBasic();
            ApplyFumes.id = "ApplyFumes";
            ApplyFumes._sprite = ResourceLoader.LoadSprite("FumesIcon");

            IntentInfoBasic RemoveFumes = new IntentInfoBasic();
            RemoveFumes.id = "RemoveFumes";
            RemoveFumes._sprite = ResourceLoader.LoadSprite("FumesIcon");
            RemoveFumes._color = Color.black;

            Color32 c = new Color32(128, 255, 128, byte.MaxValue);
            Color32 c2 = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
            TMP_ColorGradient tmp_ColorGradient = ScriptableObject.CreateInstance<TMP_ColorGradient>();
            tmp_ColorGradient.bottomLeft = c;
            tmp_ColorGradient.bottomRight = c;
            tmp_ColorGradient.topLeft = c2;
            tmp_ColorGradient.topRight = c2;

            LoadedDBsHandler.StatusFieldDB.AddNewFieldEffect(fumesFE_SO);

            LoadedDBsHandler.CombatDB.AddNewTextColor("Dmg_Fumes", tmp_ColorGradient);
            LoadedDBsHandler.CombatDB.AddNewSound("Dmg_Fumes", "event:/FumesDamageEvent");

            LoadedDBsHandler.IntentDB.AddNewBasicIntent("ApplyFumes", ApplyFumes);
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("RemoveFumes", RemoveFumes);
        }

        public override void ReduceDuration(FieldEffect_Holder holder)
        {
            int contentMain = holder.m_ContentMain;
            holder.m_ContentMain = contentMain / 2;
            if (!TryRemoveFieldEffect(holder))
            {
                holder.Effector.FieldEffectValuesChanged(_FieldID, false, holder.m_ContentMain - contentMain);
            }
        }

        public override void OnSlotEffectorTriggerAttached(FieldEffect_Holder holder)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), holder.Effector);
        }

        public override void OnSlotEffectorTriggerDettached(FieldEffect_Holder holder)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), holder.Effector);
        }

        public override void OnTriggerAttached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);          
        }

        public override void OnTriggerDettached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);          
        }

        public override void OnEventCall_01(FieldEffect_Holder holder, object sender, object args)
        {
            IUnit unit = sender as IUnit;
            unit.Damage(holder.m_ContentMain, null, "Basic", 0, false, false, true, "Dmg_Fumes");
            holder.Effector.RemoveFieldEffect(holder.FieldID);      
        }

        public override void OnEventCall_02(FieldEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder);
        }
    }

}

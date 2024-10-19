using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife
{
    public class PiercedSE_SO : StatusEffect_SO
    {
        
        public static void SetUpStatusEffect()
        {
            PiercedSE_SO piercedSE_SO = ScriptableObject.CreateInstance<PiercedSE_SO>();
            piercedSE_SO._StatusID = "Pierced_ID";

            LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(TempStatusEffectID.Scars_ID.ToString(), out StatusEffect_SO Scars);


            StatusEffectInfoSO statusEffectInfoSO = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
            statusEffectInfoSO._statusName = "Pierced";
            statusEffectInfoSO.icon = ResourceLoader.LoadSprite("PiercedIcon");
            statusEffectInfoSO._description = "Upon a party member or enemy with IronQuill moving, this enemy or party member will move in that direction too.\n1 Pierced is lost at the end of each turn";
            statusEffectInfoSO._applied_SE_Event = Scars._EffectInfo.AppliedSoundEvent;
            statusEffectInfoSO._removed_SE_Event = Scars._EffectInfo.RemovedSoundEvent;
            statusEffectInfoSO._updated_SE_Event = Scars._EffectInfo.UpdatedSoundEvent;

            piercedSE_SO._EffectInfo = statusEffectInfoSO;

            LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(piercedSE_SO);

            IntentInfoBasic ApplyPiercedIntent = new IntentInfoBasic();
            ApplyPiercedIntent.id = "ApplyPierced";
            ApplyPiercedIntent._sprite = ResourceLoader.LoadSprite("PiercedIcon");

            IntentInfoBasic RemovePiercedIntent = new IntentInfoBasic();
            RemovePiercedIntent.id = "RemovePierced";
            RemovePiercedIntent._sprite = ResourceLoader.LoadSprite("PiercedIcon");
            RemovePiercedIntent._color = Color.black;

            LoadedDBsHandler.IntentDB.AddNewBasicIntent("ApplyPierced", ApplyPiercedIntent);
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("RemovePierced", RemovePiercedIntent);
        }

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            holder.m_ObjectData = caller;
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_01), "OnTurnStart", caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_01), "OnTurnStart", caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder, sender as IStatusEffector);
        }
    }
}

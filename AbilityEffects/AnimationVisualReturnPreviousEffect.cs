using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife.AbilityEffects
{
    public class AnimationVisualReturnPreviousEffect : EffectSO
    {
        [SerializeField]
        public AttackVisualsSO _visuals = EXOP._OsmanSinnoks.abilities[0].ability.visuals;

        [SerializeField]
        public BaseCombatTargettingSO _animationTarget = Targeting.Slot_Front;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            CombatManager.Instance.AddUIAction(new PlayAbilityAnimationAction(_visuals, _animationTarget, caster));
            exitAmount = PreviousExitValue;
            return exitAmount > 0;
        }
    }
}

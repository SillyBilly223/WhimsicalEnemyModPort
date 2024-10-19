using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CrayolapedeModinreallife
{
    public class GetMissingHealthEffect : EffectSO
    {
        [SerializeField]
        public bool _DecreaseByPercentage;

        [SerializeField]
        public float _PercentageAmount = 50f;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                int Diffrence = 0;
                if (targetSlotInfo.HasUnit)
                {
                    Diffrence = Math.Abs(targetSlotInfo.Unit.CurrentHealth - targetSlotInfo.Unit.MaximumHealth);
                    Debug.Log(Diffrence);
                    if (_DecreaseByPercentage)
                    {
                        float f = _PercentageAmount * (float)Diffrence / 100f;
                        Diffrence = Mathf.Max(1, Mathf.FloorToInt(f));
                        Debug.Log(Diffrence + "half");
                    }
                }
                exitAmount += Diffrence;
            }
            return exitAmount > 0;
        }
    }
}

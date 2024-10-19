using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.Conditions
{
    public class CanMoveCondition : EffectConditionSO
    {
        public bool SuccessIfCanMove = true;

        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return caster.CanSwap == SuccessIfCanMove;
        }
    }
}

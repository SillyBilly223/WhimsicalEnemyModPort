using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife
{
    public class ScavangerPassive : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => true;
        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            IUnit unit = sender as IUnit;
            if (unit.IsAlive)
            {
                unit.Heal(5, unit, false);
            }
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            CombatManager.Instance.AddObserver(TriggerPassive, "ScavangerTrigger", unit);
            ExtraUtils.SheoInCombat++;
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            CombatManager.Instance.RemoveObserver(TriggerPassive, "ScavangerTrigger", unit);
            ExtraUtils.SheoInCombat--;
        }
    }
}

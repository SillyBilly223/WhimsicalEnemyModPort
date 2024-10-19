using System;
using System.Collections.Generic;
using System.Text;
using CrayolapedeModinreallife.Actions;
using static UnityEngine.UI.CanvasScaler;

namespace CrayolapedeModinreallife
{
    public class IronQuillPassiveAbility : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => true;
        public override bool DoesPassiveTrigger => true;
        public override void TriggerPassive(object sender, object args)
        {         
            IntegerReference integerReference = args as IntegerReference;
            IUnit unit = sender as IUnit;
            int direction = unit.SlotID < integerReference.value ? -1 : 1;
            CombatManager.Instance.ProcessImmediateAction(new PerformIronQuillEffectAction(direction));        
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            CombatManager.Instance.AddObserver(TryTriggerPassive, "OnMoved", unit);
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            CombatManager.Instance.RemoveObserver(TryTriggerPassive, "OnMoved", unit);
        }
    }
}

using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;
using Random = UnityEngine.Random;

namespace CrayolapedeModinreallife.AbilitySelectors
{
    public class AbilitySelector_Convict : BaseAbilitySelectorSO
    {

        public string _leftAbility = "";
        public string _rightAbility = "";

        public override bool UsesRarity => true;

        public override int GetNextAbilitySlotUsage(List<CombatAbility> abilities, IUnit unit)
        {

            int num = 0;
            int num2 = 0;
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < abilities.Count; i++)
            {
                if (ShouldBeIgnored(abilities[i], unit))
                {
                    num2 += abilities[i].rarity.rarityValue;
                    list2.Add(i);
                }
                else
                {
                    num += abilities[i].rarity.rarityValue;
                    list.Add(i);
                }
            }

            int num3 = Random.Range(0, num);
            num = 0;
            foreach (int item in list)
            {
                num += abilities[item].rarity.rarityValue;
                if (num3 < num)
                {
                    return item;
                }
            }

            num3 = Random.Range(0, num2);
            num2 = 0;
            foreach (int item2 in list2)
            {
                num2 += abilities[item2].rarity.rarityValue;
                if (num3 < num2)
                {
                    return item2;
                }
            }

            return -1;
        }

        public bool ShouldBeIgnored(CombatAbility ability, IUnit Unit)
        {
            if (ability.ability.name != _leftAbility && ability.ability.name != _rightAbility)
                return false;
            Debug.Log("sex?");
            if (CombatManager._instance._stats.EnemiesOnField.Count == 1)
                return true;
            Debug.Log($"sex.. {Unit.SlotID}");
            if (Unit.SlotID == 2) return false;
            if (ability.ability.name == _rightAbility && Unit.SlotID <= 1 || ability.ability.name == _leftAbility && Unit.SlotID >= 3) return false;
            Debug.Log($"loss?");
            return true;
        }
    }
}

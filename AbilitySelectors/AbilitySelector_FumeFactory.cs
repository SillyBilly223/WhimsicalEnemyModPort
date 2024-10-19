using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CrayolapedeModinreallife.AbilitySelectors
{
    public class AbilitySelector_FumeFactory : BaseAbilitySelectorSO
    {
        [SerializeField]
        public string _SelectedAbility = "";

        public override bool UsesRarity => true;

        public override int GetNextAbilitySlotUsage(List<CombatAbility> abilities, IUnit unit)
        {
            int num = 0;
            int num2 = 0;
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < abilities.Count; i++)
            {
                if (ShouldBeIgnored(abilities[i]))
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

        public bool ShouldBeIgnored(CombatAbility Ability)
        {
            if (Ability.ability.name != _SelectedAbility) return false;

            CombatSlot[] CharacterSlots = CombatManager._instance._stats.combatSlots.CharacterSlots;
            for (int i = 0; i < CharacterSlots.Length;i++) 
            {
                if (CharacterSlots[i].ContainsFieldEffect("Fumes_ID")) return false;
            }
            return true;
        }
    }
}

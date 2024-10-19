using System;
using System.Collections.Generic;
using System.Text;

namespace CrayolapedeModinreallife.ValueModifiers
{
    public class DeflectValueModifier : IntValueModifier
    {
        public DeflectValueModifier(int toNumb) : base(70)
        {
            this.toNumb = toNumb;
        }

        public override int Modify(int value)
        {
            bool flag = value > 0;
            int result;
            if (flag)
            {
                result = value - toNumb;
            }
            else
            {
                result = value;
            }
            return result;
        }

        public readonly int toNumb;
    }
}

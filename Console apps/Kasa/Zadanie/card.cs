using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public class Card : Payment

    {
        public string Name;
        public string SureName;
        public long CardNumber;
        public Card(string name, string sureName, long cardNumber, int sum) : base(sum)

        {
            Name = name;
            SureName = sureName;
            CardNumber = cardNumber;

        }




        public override int Pay()
        {
            Console.WriteLine(Name);
            Console.WriteLine(SureName);
            Console.WriteLine(CardNumber);
            Console.Write("pln: ");
            return Sum;
        }
    }
}

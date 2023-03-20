using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment
{
    public class Cash : Payment

    {
        public int Bill { get; set; }

        public Cash(int bill, int sum) : base(sum)
        {
            Bill = bill;
        }
        public override int Pay()
        {
            Console.Write("Do zapłaty: ");
            Console.WriteLine(Sum);
            Console.WriteLine("Dajesz banknot: " + Bill);
            Console.Write("Reszta pln: ");
            return Bill - Sum;
        }
    }
}

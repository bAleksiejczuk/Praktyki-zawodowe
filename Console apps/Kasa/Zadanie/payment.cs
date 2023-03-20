using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Payment
{
    public abstract class Payment
    {
        public int Sum { get; set; }
        public Payment(int sum)
        {
           Sum = sum;
        }
     

        public abstract int Pay();



    }
   

}



using Payment;
Console.WriteLine("Ile do zaplaty?");
int b = Convert.ToInt32(Console.ReadLine());
var karta = new Card("Jan","Kowalski",1234567812345678,b);
Console.WriteLine(karta.Pay());

Console.WriteLine("Jakim Banknotem?");
int a = Convert.ToInt32(Console.ReadLine());
var bill = new Cash(a,b);
Console.WriteLine(bill.Pay());




using CarList;


var carList = new List<Car>();

carList.Add(new Car("black", "skoda", "felicia", 63));
carList.Add(new Car("black", "audi", "a4", 143));
carList.Add(new Car("silver", "bmw", "e36", 120));
carList.Add(new Car("silver", "mercedes", "c", 180));
carList.Add(new Car("silver", "toyota", "celica", 123));
carList.Add(new Car("white", "aston martin", "szybki", 213));
carList.Add(new Car("white", "aston martin", "szybki", 90));
carList.Add(new Car("white", "toyota", "celica", 123));



var carsToPrint = carList.Where(x => x.Color.Equals("black"));
Console.WriteLine("Auta Czarne:");
foreach (var item in carsToPrint)
{
    Console.WriteLine(item.ToString());
}
carsToPrint = null;

carsToPrint = carList.Where(x => x.Brand.Equals("aston martin"));
Console.WriteLine("Auta Marki aston martin:");
foreach (var item in carsToPrint)
{
    Console.WriteLine(item.ToString());
}
carsToPrint = null;

carsToPrint = carList.Where(x => x.Color.Equals("white") && x.Power > 100 );
Console.WriteLine("Auta Białe ktore mają wiecej niz 100 km:");
foreach (var item in carsToPrint)
{
    Console.WriteLine(item.ToString());
}
carsToPrint = null;

carsToPrint = carList.Where(x => x.Brand !=("toyota"));
Console.WriteLine("Auta Marki innej niz Toyota:");
foreach (var item in carsToPrint)
{
    Console.WriteLine(item.ToString());
}
carsToPrint = null;

carsToPrint = carList.Where(x => x.Color.Equals("white") || x.Brand.Equals("toyota"));
Console.WriteLine("Auta Białe lub Marki Toyota:");
foreach (var item in carsToPrint)
{
    Console.WriteLine(item.ToString());
}
carsToPrint = null;







using System;
namespace Klasy
{
    class Program
    {
        static void Main(string[] args)
        {
            Auto Auto1 = new Auto(); // definicja obiektu1 typu Pudelko
            Auto Auto2 = new Auto(); // definicja obiektu2 typu Pudelko
            Auto Auto3 = new Auto(); // definicja obiektu2 typu Pudelko
                                     // spefyfikacja 1
            Auto1.Kolor = "Czarny";
            Auto1.Moc = 400;
            Auto1.Marka = "Audi";
            Auto1.Model = "A4";
            // spefyfikaja 2
            Auto2.Kolor = "Srebrny";
            Auto2.Moc = 120;
            Auto2.Marka = "BMW";
            Auto2.Model = "E36";
            // spefyfikaja 3
            Auto3.Kolor = "Czerwony";
            Auto3.Moc = 370;
            Auto3.Marka = "Astron Martin";
            Auto3.Model = "Vantage S Sportshift";

            var car = new List<Auto>();
            var carTo = new List<Auto>();
            car.Add(Auto1);
            car.Add(Auto2);
            car.Add(Auto3);
       
            car[0].wypisz();
            car[1].wypisz();
           

            string czarny = "Czarny";

            for (int i = 0; i < car.Count; i++)
                if (Auto1.Kolor.Contains(czarny))
            {
                
                carTo.Add(car[i]);
                



                }
            //Do stuff

        }
    }
    class Auto
    {
        public string Kolor;
        public int Moc;
        public string Marka;
        public string Model;
        public string carTo;
/*        public string samochodyCzarne;
        public string samochodyAstronMartin;
        public string bialeStoKoni;
        public string nieToyota;
        public string bialeAlboToyota;*/

        public void wypisz()
        {
            
            Console.WriteLine("Kolor: "+Kolor);
            Console.WriteLine("Km: "+Moc);    
            Console.WriteLine("Marka: "+Marka); 
            Console.WriteLine("Model: "+Model);
            Console.WriteLine("-------------");
        }
        public void znajdz()
        {
            Console.WriteLine(carTo);
           
        }

       
    }
}
using Shop;









var prodList = new List<Produkt>();









var koszyk = new Koszyk();







string Continue = "yes";
string usuwanie = "no";









prodList.Add(new Produkt("Żubr", 3, 1));
prodList.Add(new Produkt("Tatra", 2));
prodList.Add(new Produkt("Warka", 1));
prodList.Add(new Produkt("Żywiec", 2));
prodList.Add(new Produkt("Somersby", 5));
prodList.Add(new Produkt("Lech", 3));
prodList.Add(new Produkt("Haineken", 2));
prodList.Add(new Produkt("Desperados", 6));










foreach (var item in prodList)
{
    Console.WriteLine(item.Wypisz()); //Wyswietla co mamy w sklepie.
}
while (Continue != "")
{





    Console.WriteLine("Co chcesz zrobic(dodac,usunac,kasa)");
    Continue = Console.ReadLine();
    if (Continue == "dodac")




    {
        Console.WriteLine("Jaki Browar Warjacie:");
        string piwo = Console.ReadLine();
        var a = prodList.Where(z => z.NazwaProduktu.Equals(piwo)).FirstOrDefault();
        if (a != null)
        {
            var t = prodList.Where(x => x.NazwaProduktu.Equals(piwo)).FirstOrDefault();
            var index = prodList.IndexOf(t);
            koszyk.DodajProdukt(prodList[index]);

        }


        Console.WriteLine("Stan koszyka:");
        foreach (var item in koszyk.listaProduktow)
        {
            Console.WriteLine(item.NazwaProduktu + " ilość: " + item.Ilosc);
        }
    }




    if (Continue == "usunac")
    {





        Console.WriteLine("Jaki Browar usunac?:");
        string piwoUsun = Console.ReadLine();
        var x = koszyk.listaProduktow.Where(y => y.NazwaProduktu.Equals(piwoUsun)).FirstOrDefault();
        if (x != null)
        {
            var t2 = prodList.Where(x => x.NazwaProduktu.Equals(piwoUsun)).FirstOrDefault();
            var index2 = prodList.IndexOf(t2);
            koszyk.UsunProdukt(prodList[index2]);



        }






        foreach (var item in koszyk.listaProduktow)
        {
            Console.WriteLine(item.NazwaProduktu + " ilość: " + item.Ilosc);
        }





    }
    if (Continue == "kasa")
    {


        koszyk.SumujCene();
        break;
    }







}
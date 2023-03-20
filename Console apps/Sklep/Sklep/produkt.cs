using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Shop
{
    public class Koszyk
    {




        public List<Produkt> listaProduktow { get; set; }
        public Koszyk()
        {

            listaProduktow = new List<Produkt>();
        }

        public void DodajProdukt(Produkt produkt )
        {
            if (listaProduktow.Where(x => x.NazwaProduktu.Equals(produkt.NazwaProduktu)).Any())
            {
                var t = listaProduktow.Where(x => x.NazwaProduktu.Equals(produkt.NazwaProduktu)).FirstOrDefault();
                var index = listaProduktow.IndexOf(t);
                listaProduktow[index].Ilosc++;
         
            }
            else
            {
                produkt.Ilosc = 1;
                listaProduktow.Add(produkt);



            }
        
           
        }
        public void UsunProdukt(Produkt produkt)
        {
            var t = listaProduktow.Where(x => x.NazwaProduktu.Equals(produkt.NazwaProduktu)).FirstOrDefault();
            var index = listaProduktow.IndexOf(t);
            listaProduktow[index].Ilosc--;
            if (produkt.Ilosc == 0)
            {
                listaProduktow.Remove(listaProduktow[index]);

            }
            
        }
        public void SumujCene()
        {
           
           var rachunek = listaProduktow.Sum(x => x.Cena * x.Ilosc);
            Console.WriteLine(rachunek+"zł");
            
        }





    }






    public class Produkt
    {
        public string NazwaProduktu { get; set; }
        public int Cena { get; set; }



        public int Ilosc { get; set; }





        public Produkt(string nazwa_produktu, int cena, int ilosc = 0)
        {
            NazwaProduktu = nazwa_produktu;
            Cena = cena;
            Ilosc = ilosc;
        }



        public string Wypisz()
        {
            return $"Nazwa: {NazwaProduktu} Cena: {Cena}zł";
        }



    }







}
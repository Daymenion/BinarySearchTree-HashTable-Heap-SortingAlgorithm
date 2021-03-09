using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace proje3
{
    internal class Duraksinifi
    {
        public string Durakadi;
        public int BP;
        public int TB;
        public int NB;
        public ArrayList musteriler = new ArrayList();

        public Duraksinifi(string durakadi, int BP, int TB, int NB, ArrayList musteriler)
        {
            Durakadi = durakadi;
            this.BP = BP;
            this.TB = TB;
            this.NB = NB;
            this.musteriler = musteriler;
        }
    }

    internal class Program
    {
        public static string[] Duzenle(string[] dizi, int temp)
        {
            //rastgele veri secildikten sonra dizilerin guncellenmesi;
            var dizi1 = new string[dizi.Length - 1];
            for (var i = 0; i < dizi.Length - 1; i++)
                if (i >= temp)
                    dizi1[i] = dizi[i + 1];
                else
                    dizi1[i] = dizi[i];
            return dizi1;
        }

        private static void Main()
        {
            //Console.BackgroundColor = ConsoleColor.White; //Ben siyah cmd kullaniyorum bu yuzden yorum satiri olarak biraktim.
            //Console.ForegroundColor = ConsoleColor.Black;
            string[] duraklar =
            {
                "Bornova Metro", "8", "0", "4", "Üç Kuyular", "19", "1", "10", "Dogal Yasam P.", "17", "1", "6",
                "Bostanlı İskele", "7", "0", "5", "Sahilevleri", "8", "1", "11", "İnciraltı", "28", "2", "10",
                "Alsancak Gar", "10", "0", "5", "Konak Metro", "10", "1", "10", "Göztepe Köprü", "16", "3", "14"
            };
            var arrayliste = new ArrayList();
            var sayac = 0;
            Duraksinifi duraksinifi;
            List<Duraksinifi> genericListe;
            var musteriler = new ArrayList();
            var durakHashtable = new Hashtable();
            var a = duraklar.Length; //az sonra bircok for dongusunde kullanacagiz
            while (sayac < a)
            {
                var yeniDurak = new ArrayList(); //hashtable icin
                yeniDurak.Add(duraklar[0]);
                yeniDurak.Add(int.Parse(duraklar[1]));
                yeniDurak.Add(int.Parse(duraklar[2]));
                yeniDurak.Add(int.Parse(duraklar[3]));
                durakHashtable.Add(duraklar[0].ToCharArray(0, 1), yeniDurak);

                genericListe = new List<Duraksinifi>();
                duraksinifi = new Duraksinifi(duraklar[0], Convert.ToInt32(duraklar[1]), Convert.ToInt32(duraklar[2]), Convert.ToInt32(duraklar[3]), musteriler);
                genericListe.Add(duraksinifi); //musteri arraylisti bostur
                arrayliste.Add(genericListe); //ve genericlisteye ekliyoruz sonrada duzenle methoduyla stringleri duzenliyoruz
                for (var i = 0; i < 4; i++)
                {
                    duraklar = Duzenle(duraklar, 0);
                    sayac++; //sayaci arttirarak whileden cikana kadar donmesini saglayabiliriz.
                }
            }

            var heap = new Heap<int, Duraksinifi>();
            var agac = new Tree();
            foreach (List<Duraksinifi> item in arrayliste)
            {
                foreach (var item1 in item)
                {
                    heap.Enqueue((item1.NB), item1);
                    agac.insert(item1);
                }
            }

            agac.Kontrol(agac.getRoot());
            var devam = "d";
            while (devam != "q")
            {
                Console.WriteLine("(1)Ağaç yapısı soruları \n(2)Hash Table yapısı soruları \n(3)Heap yapısı soruları \n(4)Sorting soruları");
                var secim = Console.ReadLine();
                if (secim == "1")
                {
                    Console.WriteLine("\n\t(1)Agacin bilgileri \n\t(2)Musteri ID arama\n\t(3)Kiralama islemi");
                    secim = Console.ReadLine();
                    if (secim == "1")
                    {
                        Console.WriteLine("\n\t\tAğaç çıktısı hangi yapıda olsun? \n\t\t(1)Preorder:\n\t\t(2)Postorder:\n\t\t(3)İnorder:");
                        var k = Console.ReadLine();
                        if (k == "1")
                        {
                            Console.Write("\nAgacın PreOrder Dolasılması : ");
                            agac.preOrder(agac.getRoot());
                            agac.FindAndWriteTreeInfo(agac.getRoot(), a / 4);
                        }
                        else if (k == "2")
                        {
                            Console.Write("\nAgacın PostOrder Dolasılması : ");
                            agac.postOrder(agac.getRoot());
                            agac.FindAndWriteTreeInfo(agac.getRoot(), a / 4);
                        }
                        else if (k == "3")
                        {
                            Console.Write("\nAgacın İnOrder Dolasılması : ");
                            agac.inOrder(agac.getRoot());
                            agac.FindAndWriteTreeInfo(agac.getRoot(), a / 4);
                        }
                    }
                    else if (secim == "2")
                    {
                        Console.WriteLine("\n\t\tBulmak istediğiniz id numarasını girin:");
                        var k = Console.ReadLine();
                        agac.MusteriSor(agac.getRoot(), k);
                    }
                    else if (secim == "3")
                    {
                        Console.WriteLine("\n\t\tkiralama yapmak istediginiz durak hangisi: ");
                        var durak = Console.ReadLine();
                        Console.WriteLine("\n\t\tID numaranizi girin lutfen: ");
                        var id = Console.ReadLine();
                        if (!agac.Kiralama(agac.getRoot(), durak, id))
                            Console.WriteLine("\n\n\t\t***ISLEM BASARISIZ*** ");
                    }
                }

                else if (secim == "2")
                {
                    // hashtable yaptim sayilir yazi fontunu biraz duzelticem//
                    Console.WriteLine("\t\t\tGuncellenmis Halleriyle Duraklar ve Bilgileri\n\t\t\t---------------------------------------------");
                    Console.WriteLine("Durak adi:\t\tBos Park:\t\tTandem Bisiklet:\t\tNormal Bisiklet:");
                    Console.WriteLine("==========\t\t=========\t\t=================\t\t===============");
                    foreach (ArrayList item in durakHashtable.Values)
                    {
                        //5'den az ise bos park sayisinin azaltilip normal bisiklet sayisinin arttirilmasi
                        if ((int) item[1] >= 5)
                        {
                            item[1] = (int) item[1] - 5;
                            item[3] = (int) item[3] + 5;
                            //itemler guncellendikten sonra, consola basilmasi.
                        }

                        Console.WriteLine("{0}\t\t{1}\t\t\t{2}\t\t\t\t{3}", item[0], item[1], item[2], item[3]);
                    }
                }

                else if (secim == "3")
                {
                    Console.WriteLine("\nNormal Bisiklet sayisinin fazla olmasina göre ilk 3 durak:");
                    for (var i = 0; i < 3; i++)
                    {
                        Duraksinifi item; //Soru 3 un outputu
                        item = heap.Dequeue().Value;
                        Console.WriteLine("{0}. Siradaki Durak\t {1} , Normal Bisiklet sayisi: {2}  ", i + 1, item.Durakadi, item.NB);
                    }
                }

                else if (secim == "4")
                {
                    int arrayBoyutu;
                    var watch = new Stopwatch();
                    double gecenZaman; // mili saniye olarak verilen sureyi saniyeye cevirecegiz
                    Console.WriteLine("\tIstediginiz array boyutunu giriniz: ");
                    arrayBoyutu = Convert.ToInt32(Console.ReadLine());
                    var array = new int[arrayBoyutu];
                    var array1 = new int[arrayBoyutu];
                    sorting.ArrayDoldurma(array); //random sayilarla araylerin doldurulmasi
                    array.CopyTo((Memory<int>) array1);

                    watch.Reset();
                    watch.Start();
                    sorting.QuickSort(array, 0, arrayBoyutu - 1); //Quicksort a yolluyoruz ve sureyi bastiriyoruz
                    watch.Stop();
                    gecenZaman = watch.ElapsedMilliseconds / 1000.00; //milisaniyeyi saniyeye cevirme
                    Console.WriteLine("Quick Sort ile siralamada gecen sure: {0:F5}", gecenZaman);

                    watch.Reset();
                    watch.Start();
                    sorting.SelectionSort(array); //Selectionsort a yolluyoruz ve sureyi bastiriyoruz
                    watch.Stop();
                    gecenZaman = watch.ElapsedMilliseconds / 1000.00;
                    Console.WriteLine("Selection Sort siralamada gecen sure: {0:F5}", gecenZaman);
                    //foreach (var k in array) Console.WriteLine(k); //Siralanmis arrayi yazdirmak icin kullanilabilir.
                }

                else if (secim == "q")
                {
                    break;
                }

                Console.WriteLine("\nDevam etmek istiyor musunuz? Devam icin herhangibir tuşa bas çıkmak için q'a bas");
                devam = Console.ReadLine();
            }

            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;

namespace proje3
{
    public class Heap<TBisiklet, TDurak> // TBisiklet oncelik derecesini TDurak ise durak bilgilerini temsil eder
    {
        private List<KeyValuePair<TBisiklet, TDurak>> _heap; // Heap veri yapısına uygun olacak şekilde içeriğimizi tutacağımız koleksiyon
        private IComparer<TBisiklet> _kiyasla; // Max-Heap' e göre bir uyarlamaya hizmet verebilmek için kullanılacak arayüz referansı
        private const string Uyari = "Koleksiyonda hiç eleman yok"; //Hata mesajimiz

        #region Constructors 

        // TBisiklet ile gelen tipin varsayılan karşılaştırma kriterine göre bir yol izlenir
        public Heap()
            : this(Comparer<TBisiklet>.Default)
        {
        }

        // TBisiklet tipinin karşılaştırma işlevselliğini üstlenen bir IComparer implementasyonu ile bir Construct işlemi
        public Heap(IComparer<TBisiklet> karsilastirici)
        {
            if (karsilastirici == null)
                throw new ArgumentNullException();

            _heap = new List<KeyValuePair<TBisiklet, TDurak>>();
            _kiyasla = karsilastirici;
        }

        #endregion

        #region Temel Fonksiyonlar
        //heape ekleme islemi
        public void Enqueue(TBisiklet oncelik, TDurak deger)
        {
            KeyValuePair<TBisiklet, TDurak> veri = new KeyValuePair<TBisiklet, TDurak>(oncelik, deger);
            _heap.Add(veri);
            // Sondan basa dogru yeniden bir siralama yaptirilir.
            LastToFirst(_heap.Count - 1);
        }
        public KeyValuePair<TBisiklet, TDurak> Dequeue() //hepaden cikarma
        {
            if (!Bosmu) //bos degilse gir
            {
                FirstToLast(0); //siralama fonka yolla
                KeyValuePair<TBisiklet, TDurak> sonuc = _heap[0]; //heapin en basindakini sonuca ata
                if (_heap.Count <= 1)
                {
                    _heap.Clear(); //birden azsa heapi sil
                }
                else
                {
                    _heap[0] = _heap[_heap.Count - 1]; //fazla ise son veriyi ilk veriye ata ve son veriyi sil 
                    _heap.RemoveAt(_heap.Count - 1);
                }
                return sonuc;
            }
            else
                throw new InvalidOperationException(Uyari);
        }


        //Peek ile varsayilan ilk elemani geriye dondurebiliriz. Elaman heapden silinmez 
        public KeyValuePair<TBisiklet, TDurak> Peek()
        {
            if (!Bosmu)
                return _heap[0];
            else
                throw new InvalidOperationException(Uyari);
        }

        //Koleksiyonda eleman olup olmadiginin kontrolu icindir
        public bool Bosmu
        {
            get { return _heap.Count == 0; }
        }

        #endregion

        #region Sıralama Fonksiyonları
        private void LastToFirst(int pozisyon) //assagidan yukariya dogru heapi tarar ve en yukari en buyuk olani getirmeye calisir.
        {
            if (pozisyon >= _heap.Count)
                return;

            int YukariPos;

            while (pozisyon > 0)
            {
                YukariPos = (pozisyon - 1) / 2;
                if (_kiyasla.Compare(_heap[YukariPos].Key, _heap[pozisyon].Key) < 0) //kiyasla ve buyukse yer degistir
                {
                    YerleriDegis(YukariPos, pozisyon);
                    pozisyon = YukariPos;
                }
                else break;
            }
        }

        private void FirstToLast(int pozisyon) //Yukaridan asagi heapi tarar ve en yukari en buyuk olani getirmeye calisir.
        {
            if (pozisyon >= _heap.Count)
                return;
            while (true)
            {
                int buyukPozisyon = pozisyon;
                int solPozisyon = 2 * pozisyon + 1;
                int sagPozisyon = 2 * pozisyon + 2;
                if (solPozisyon < _heap.Count &&
                    _kiyasla.Compare(_heap[buyukPozisyon].Key, _heap[solPozisyon].Key) < 0) //sol ve sag pozisyonu karsilastir
                    buyukPozisyon = solPozisyon;
                if (sagPozisyon < _heap.Count &&
                    _kiyasla.Compare(_heap[buyukPozisyon].Key, _heap[sagPozisyon].Key) < 0) //hangisi buyukse onunla yer degistir
                    buyukPozisyon = sagPozisyon;

                if (buyukPozisyon != pozisyon)
                {
                    YerleriDegis(buyukPozisyon, pozisyon);
                    pozisyon = buyukPozisyon;

                }
                else break;
            }
        }

        private void YerleriDegis(int pozisyon1, int pozisyon2) //heap icerisinde gonderilen iki indexin yerini degistirir.
        {
            KeyValuePair<TBisiklet, TDurak> val = _heap[pozisyon1];
            _heap[pozisyon1] = _heap[pozisyon2];
            _heap[pozisyon2] = val;
        }

        #endregion
    }
}

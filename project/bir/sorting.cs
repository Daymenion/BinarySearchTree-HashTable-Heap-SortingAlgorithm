using System;

namespace proje3
{
    public class sorting
    {
        public static void YerDegis(int[] array, int index1, int index2)
        {//yer degistirme fonksiyonu
            int temporary;

            temporary = array[index1];
            array[index1] = array[index2];
            array[index2] = temporary;
        }

        public static int ArrayMin(int[] array, int start)
        {//minimum degerli pozisyonu bulma ve donme
            var minPos = start;
            for (var pos = start + 1; pos < array.Length; pos++)
                if (array[pos] < array[minPos])
                    minPos = pos;
            return minPos;
        }

        public static void SelectionSort(int[] array)
        {
            int i;
            var N = array.Length;

            for (i = 0; i < N - 1; i++) //tum arrayi tariyoruz
            {
                var k = ArrayMin(array, i); //arrayde suankı yerımızden arrayın en sonuna kadar tararız ve en kucuk degerin yerini degistiriyoruz
                if (i != k)
                    YerDegis(array, i, k);
            }
        }

        public static void QuickSort(int[] array, int left, int right)
        {
            int i, j, pivot;

            i = left;
            j = right;

            pivot = array[(left + right) / 2]; //ortadaki degerimizi buluyoruz
            while (true)
            {
                while (array[i] < pivot) //i indexindeki deger ortadakinden kucukse sadece i yi arttirabiliriz, buyukse dongu kirilacak
                    i++;                //ayni seyin tersi j indexleri icin tekrarlanacak ve bu dongu kiran indexler kendi aralarinda degisilecek
                while (pivot < array[j]) 
                    j--;               //ortadaki degerin solundakilerin (indexi daha kucuk olanlar) ortadaki degerden kucuk
                if (i <= j)            //sagindakilerin (indexi daha buyuk olanlar) ortadaki degerden buyuk olmasini istiyoruz
                {
                    YerDegis(array, i, j);
                    i++;
                    j--;
                } //yer degistikten sonra i ve j yi arttirip devam edicez, i j'den kucukse kaldigimiz yerden devam edicez
                // buyukse ana while dongusunden cikilacak sonra bu islem her iki taraf icin ikiye bolunerek gidecek
                if (i > j)
                    break;
            } //recursive olarak takip edersek dongu her zaman sol tarafta recursive edilecek sol recursiveler bitip j=0 oldugunda
            //artik sag recursiveler baslayacaktir. i =  arrayboyutu -1 oldugunda da siralama bitecektir.

            if (left < j)
                QuickSort(array, left, j);
            if (i < right)
                QuickSort(array, i, right);
        }

        public static void ArrayDoldurma(int[] array)
        {
            var r = new Random(Guid.NewGuid().GetHashCode()); //butun cagirmalar ya da tekrar baslatmalarda seedimiz degisecek
            for (var i = 0; i < array.Length; i++)  //bu da bize devasa bir olasilik denizi sunuyor.
                array[i] = r.Next();
        }
    }
}

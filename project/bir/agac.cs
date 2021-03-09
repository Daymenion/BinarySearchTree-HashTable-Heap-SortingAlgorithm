using System;
using System.Collections;
using System.Collections.Generic;

namespace proje3
{
// Düğüm Sınıfı
internal class TreeNode
{
    public Duraksinifi data;
    public TreeNode leftChild;
    public TreeNode rightChild;

    public void DisplayNode()
    {
        Console.Write("\nDurak Adi:" + data.Durakadi  + "\t\tBos Park: " + data.BP + "\t\tTandem Bisiklet " + data.TB + "\t\tNormal Bisiklet " + data.NB + " " + "\nMusteri sayisi {0} \n\t\tMusteriler:\n\t\t===========\n",data.musteriler.Count);
        foreach (List<string> v in data.musteriler)
        {
            foreach (var a in v)
            {
                Console.Write(" \t  " + a);
            }
            Console.WriteLine();
        }
    }
}

// Agaç Sınıfı
    internal class Tree
    {
        private TreeNode root;

        //variables for traverse statistics
        public int totalDepth;
        public int falsenumber;
        public int maxDepth;
        public int leavesDepthTotal;
        public int leavesCount;
        public int[] elementCountForEachDepth;
        public int[] sumElementValuesForEachDepth;
        private int[] iddizi = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
        private static Random random = new Random(Guid.NewGuid().GetHashCode());

        public Tree()
        {
            root = null;
        }

        public TreeNode getRoot()
        {
            return root;
        }

        // Agacın preOrder Dolasılması
        public void preOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                localRoot.DisplayNode();
                preOrder(localRoot.leftChild);
                preOrder(localRoot.rightChild);
            }
        }

        // Agacın inOrder Dolasılması
        public void inOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.DisplayNode();
                inOrder(localRoot.rightChild);
            }
        }

        // Agacın postOrder Dolasılması
        public void postOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                postOrder(localRoot.leftChild);
                postOrder(localRoot.rightChild);
                localRoot.DisplayNode();
            }
        }

        // Agaca bir dügüm eklemeyi saglayan metot
        public void insert(Duraksinifi newdata)
        {
            var newNode = new TreeNode();
            l:
            var n = random.Next(0, iddizi.Length < 10 ? iddizi.Length + 1 : 11);
            if (n > newdata.NB || n > iddizi.Length) goto l;
            newdata = new Duraksinifi(newdata.Durakadi, newdata.BP + n, newdata.TB, newdata.NB - n, MusteriEkle(n));
            newNode.data = newdata;
            if (root == null)
            {
                root = newNode;
            }
            else
            {
                var current = root;
                TreeNode parent;
                while (true)
                {
                    parent = current;
                    if (string.Compare(newdata.Durakadi, current.data.Durakadi, StringComparison.OrdinalIgnoreCase) <= 0)
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                } 
            } 
        } 

        public ArrayList MusteriEkle(int n)
        {
            var start = TimeSpan.FromHours(0);
            var end = TimeSpan.FromHours(24);
            var maxMinutes = (int) (end - start).TotalMinutes;
            ArrayList musteriArrayList = new ArrayList();
            List<string> musteri;
            for (var i = 0; i < n; ++i)
            {
                musteri = new List<string>();
                var k = random.Next(iddizi.Length);
                var id = "Musteri id: " + iddizi[k];
                iddizi = Duzenle(iddizi, k);
                ;
                var minutes = random.Next(maxMinutes);
                var t = "Kiralama Zamani: " + start.Add(TimeSpan.FromMinutes(minutes));
                musteri.Add(id);
                musteri.Add(t);
                musteriArrayList.Add(musteri);
            }

            return musteriArrayList;
        }

        public static int[] Duzenle(int[] dizi, int temp)
        {
            //rastgele sectigimiz veriden sonra stringin yeniden duzenlenmesi
            var dizi1 = new int[dizi.Length - 1];
            for (var i = 0; i < dizi.Length - 1; i++)
                if (i >= temp)
                    dizi1[i] = dizi[i + 1];
                else
                    dizi1[i] = dizi[i];
            return dizi1;
        }

        public void Kontrol(TreeNode node)
        {
            //20 musteriden istasyona rastgele olarak yerlesmede bosta kalinma olursa otomatik olarak ilk istasyona o musterileri yollayacak
            if (iddizi.Length != 0)
            {
                if (node != null)
                {
                    if (node.data.NB >= 1)
                    {
                        ArrayList list;
                        int a;
                        if ((10 - node.data.musteriler.Count >= (a = iddizi.Length)) && node.data.NB >= iddizi.Length)
                        {
                            list = MusteriEkle(iddizi.Length);
                            node.data.NB -= a;
                            node.data.BP += a;
                        }
                        else
                        {
                            list = MusteriEkle(10 - node.data.musteriler.Count);
                            node.data.NB -= 10 - node.data.musteriler.Count;
                            node.data.BP += 10 - node.data.musteriler.Count;
                        }

                        node.data.musteriler.AddRange(list);
                    }

                    Kontrol(node.rightChild);
                    Kontrol(node.leftChild);
                }
            }
        }

        public void MusteriSor(TreeNode node, string n)
        {
            int w = 0;
            string b = "Musteri id: " + n;
            if (node != null)
            {
                foreach (List<string> v in node.data.musteriler)
                {
                    foreach (var a in v)
                    {
                        if (string.Equals(a, b) == true)
                        {
                            w = 1;
                            Console.WriteLine("\n\tDurak bilgileri ---- musteri bilgileri");
                            Console.Write("\t" + node.data.Durakadi + " " + node.data.BP + " " + node.data.TB + " " + node.data.NB + " ---- " + a + " ");
                        }
                        else if (w == 1)
                        {
                            Console.Write(a);
                            w = 0;
                            break;
                        }
                    }
                }

                MusteriSor(node.rightChild, n);
                MusteriSor(node.leftChild, n);
            }

        }

        public bool Kiralama(TreeNode node, string root, string id)
        {
            var start = TimeSpan.FromHours(0);
            var end = TimeSpan.FromHours(24);
            var maxMinutes = (int) (end - start).TotalMinutes;
            List<string> musteri;
            var musteriArrayList = new ArrayList();

            if (node != null)
            {

                var areEqual = node.data.Durakadi.Contains(root, StringComparison.OrdinalIgnoreCase);
                if (areEqual)
                {
                    if (node.data.NB >= 1)
                    {
                        musteri = new List<string>();
                        var minutes = random.Next(maxMinutes);
                        var t = "Kiralama Zamani: " + start.Add(TimeSpan.FromMinutes(minutes));
                        Console.WriteLine("Istasyondan normal bir bisiklet kiralaniyor:  {0}", t);
                        var Musid = "Musteri id: " + id;
                        musteri.Add(Musid);
                        musteri.Add(t);
                        musteriArrayList.Add(musteri);
                        node.data.musteriler.AddRange(musteriArrayList);
                        node.data.NB -= 1;
                        node.data.BP += 1;
                        return true;
                    }
                    else if (node.data.TB >= 1)
                    {
                        musteri = new List<string>();
                        var minutes = random.Next(maxMinutes);
                        var t = "Kiralama Zamani: " + start.Add(TimeSpan.FromMinutes(minutes));
                        Console.WriteLine("Istasyonda bosta normal bisiklet kalmamistir, Tadem bisiklet veriliyor.");
                        Console.WriteLine("Istasyondan Tadem bir bisiklet kiralaniyor:  {0}", t);
                        var Musid = "Musteri id: " + id;
                        musteri.Add(Musid);
                        musteri.Add(t);
                        musteriArrayList.Add(musteri);
                        node.data.musteriler.AddRange(musteriArrayList);
                        node.data.TB -= 1;
                        node.data.BP += 1;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\tDurakta hic bisiklet kalmamistir baska duraklarimiza bakabilirsiniz, iyi gunler dileriz");
                        return false;
                    }
                }
                else
                {
                    falsenumber++;
                    Kiralama(node.rightChild, root, id);
                    Kiralama(node.leftChild, root, id);
                }
            }

            if (falsenumber == 9 ||
                falsenumber == 18 ||
                falsenumber == 27 ||
                falsenumber == 36 ||
                falsenumber == 45 ||
                falsenumber == 54 ||
                falsenumber == 63 ||
                falsenumber == 72 ||
                falsenumber == 81 ||
                falsenumber == 90)
            {
                return false;
            }
            //program recursive oldugu icin return false u kosul icerisinde yollamaliyim,
            //Aksi halde herbir recursive un donusunde bana islem basarisiz yazisini basacaktir. 9 un kati demek
            //if (areEqual) false olmasi yani node ile bizim ismimizin eslesmemesi ve nihayetinde falsenumberin artmasi
            //9 falsenumber olmussa hicbir node da bulamamis demektir.
            return true;
        }

        //traverse preorder to extract information about depth, element count, and value of nodes
    private void TraverseTreeForInfo(TreeNode node, int depth)
    {
        if (node != null)
        {
            depth++;
            elementCountForEachDepth[depth]++;
            sumElementValuesForEachDepth[depth] += node.data.NB;

            if (depth > maxDepth)
                maxDepth = depth; //update max depth

            totalDepth += depth;

            //for calculating the average leaves depth
            if (node.leftChild == null && node.rightChild == null)
            {
                leavesCount++;
                leavesDepthTotal += depth;
            }

            TraverseTreeForInfo(node.leftChild, depth); //traverse left sub-tree
            TraverseTreeForInfo(node.rightChild, depth); //traverse right sub-tree

        }
    }

    public void FindAndWriteTreeInfo(TreeNode rootNode, int treeSize)
    {

        totalDepth = 0;
        maxDepth = 0;

        elementCountForEachDepth = new int[treeSize];
        sumElementValuesForEachDepth = new int[treeSize];

        //For average leaves depth
        leavesDepthTotal = 0;
        leavesCount = 0;

        TraverseTreeForInfo(rootNode, -1);

        Console.WriteLine("\nAgacin derinligi: " + maxDepth);
        Console.WriteLine("Herbir derinlik basina eleman sayisi ve Normal bisiklet sayilarinin toplami");
        for (var i = 0; i <= maxDepth; i++) Console.WriteLine("\tDerinlik {0}: Eleman sayisi: {1}  Normal bisiklet sayisi toplami {2}", i, elementCountForEachDepth[i], sumElementValuesForEachDepth[i]);
        Console.WriteLine("Ortalama yaprak derinliği: " + (double)leavesDepthTotal / leavesCount);

    }

}
}

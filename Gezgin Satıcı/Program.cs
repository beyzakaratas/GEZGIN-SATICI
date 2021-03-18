using System;

namespace Gezgin_Satıcı
{
    class Program
    {
        //rota uzunluğunu hesaplayan metot
        static double RotaHesapla(int[] Çözüm, double[,] Mesafe)
        {
            double sonuç = 0;
            for (int i = 0; i < Çözüm.Length - 1; i++)
                sonuç += Mesafe[Çözüm[i], Çözüm[i + 1]];
            sonuç += Mesafe[Çözüm[Çözüm.Length - 1], Çözüm[0]];

            return sonuç;
        }

        static void Main(string[] args)
        {
            // müşteri sayısının konsoldan alınması
            Console.Write("Müşteri Sayısını Giriniz: ");
            int MS = Convert.ToInt32(Console.ReadLine());

            // iterasyon sayısının konsoldan alınması
            Console.Write("İterasyon Sayısını Giriniz: ");
            int İterasyonSayısı = Convert.ToInt32(Console.ReadLine());

            // Tanımlamalar başlangıcı
            int[] Xkoor = new int[MS];
            int[] Ykoor = new int[MS];
            Random RassalÜret = new Random(16);
            double[,] Mesafe = new double[MS, MS];
            int[] Çözüm = new int[MS];
            int[] EnİyiÇözüm = new int[MS];
            double RotaUzunluğu, EnKısaRotaUzunluğu;
            // Tanımlamalar bitişi

            Console.WriteLine();
            Console.WriteLine("MüşNo\tXKoor\tYKoor");
            Console.WriteLine("-----\t-----\t-----");
            //müşteri koordinatlarının üretilmesi
            for (int i = 0; i < MS; i++)
            {
                Xkoor[i] = RassalÜret.Next(10, 100);
                Ykoor[i] = RassalÜret.Next(10, 100);
                Console.WriteLine(i+"\t" + Xkoor[i] + "\t" + Ykoor[i]);
            }
            Console.WriteLine();
            //müşteri çiftleri arası mesafelerin hesaplanması
            for (int i = 0; i < MS; i++)
                for (int j = 0; j < MS; j++)
                    Mesafe[i, j] = Math.Round(Math.Sqrt(Math.Pow(Xkoor[i] - Xkoor[j], 2) + Math.Pow(Ykoor[i] - Ykoor[j], 2)), 1);

            //problem için bir başlangıç çözümü oluşturulması
            Console.WriteLine();
            Console.Write("Başlangıç Çözümü: ");
            for (int i = 0; i < MS; i++)
            {
                Çözüm[i] = i;
                Console.Write(Çözüm[i] + "-");
            }

            //çözümün rota uzunluğunun hesaplanması
            RotaUzunluğu = RotaHesapla(Çözüm, Mesafe);


            // en iyi çözümün hafızaya alınması
            EnKısaRotaUzunluğu = RotaUzunluğu;
            Array.Copy(Çözüm, EnİyiÇözüm, MS);

            Console.WriteLine();
            Console.WriteLine("Rota Uzunluğu = " + RotaUzunluğu);

            // iterasyon sayısı kadar komşu çözüp üretip içlerinden en iyisinin seçilmesi
            for (int Deneme = 0; Deneme < İterasyonSayısı; Deneme++)
            {
                // swap operatörüyle komşu çözüm üretilmesi
                // rota üzerinde rassal iki pozisyon üret
                int Poz1 = RassalÜret.Next(0, MS);
                int Poz2 = RassalÜret.Next(0, MS);

                // rassal üretilen iki pozisyondaki müşterileri yer değiştir
                int Geçici = Çözüm[Poz1];
                Çözüm[Poz1] = Çözüm[Poz2];
                Çözüm[Poz2] = Geçici;

                //yeni çözümün rota uzunluğunun hesaplanması
                RotaUzunluğu = RotaHesapla(Çözüm, Mesafe);

                // yeni çözümün hafızadaki en iyi çözümle karşılaştırılması
                if (RotaUzunluğu < EnKısaRotaUzunluğu)
                {
                    // en iyi çözümün hafızaya alınması
                    EnKısaRotaUzunluğu = RotaUzunluğu;
                    Array.Copy(Çözüm, EnİyiÇözüm, MS);
                }
            }

            // en iyi çözümün ekrana yazdırılması
            Console.WriteLine();
            Console.Write("En İyi Rota: ");
            for (int i = 0; i < MS; i++)
                Console.Write(EnİyiÇözüm[i] + "-");

            Console.WriteLine();
            Console.Write("En Kısa Rota Uzunluğu: " + EnKısaRotaUzunluğu);

            Console.ReadLine();
        
        }
    }
}

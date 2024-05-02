namespace GunlukKayit_V1_V2
{
    internal class Program
    {
        class Kayit
        {
            public string Metin { get; set; }
            public DateTime KayitTarihi { get; set; }
            public Kayit(string metin)
            {
                Metin = metin;
                KayitTarihi = DateTime.Now;
            }
        }

        static List<Kayit> kayitlar = new List<Kayit>();

        static void MenuyeDon()
        {
            Console.WriteLine("\nMenüye dönmek için bir tuşa basın");
            Console.ReadKey(true);
            MenuGoster();
        }
        static void KayitlariListele()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTüm Kayıtlar");
            Console.ResetColor();
            if (kayitlar.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Listelenecek Kayıt bulunamadı.");
                Console.ResetColor();
            }
            for (int i = 0; i < kayitlar.Count; i++)
            {
                kayitlar[i].KayitTarihi.ToString("ddMMyyyy");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(kayitlar[i].KayitTarihi);
                Console.WriteLine("==========================================");
                Console.ResetColor();
                Console.WriteLine($"{i + 1} - {kayitlar[i].Metin}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("==========================================");
                Console.ResetColor();
                Console.WriteLine("(S)onraki Kayıt || (A)na Menü ");
                string inputSecim = Console.ReadLine();
                inputSecim = inputSecim.ToLower();
                if (inputSecim == "a")
                {
                    MenuyeDon();
                    break;
                }
                else if (inputSecim != "s")
                {
                    Console.Clear();
                    Console.WriteLine("Yanlış seçim!");
                    i--;
                    continue;
                }
            }
            Console.Clear();
            Console.WriteLine("Başka kayıt bulunamadı!");
            MenuyeDon();
        }
        static void KayitEkle()
        {
            Console.Clear();
            DateTime bugun = DateTime.Today;
            bool gunlukteKayitVarmi = kayitlar.Any(p => p.KayitTarihi.Date == bugun);
            if (gunlukteKayitVarmi)
            {
                Console.WriteLine("Bugün kayıt eklediniz. Yarın görüşmek üzere.");
                MenuyeDon();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Günlüğe kayıt edilecekler: ");
                Console.ResetColor();
                string inputKayit = Console.ReadLine();
                kayitlar.Add(new Kayit(inputKayit));
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Günlük Kayıt aldına alındı.");
                Console.ResetColor();
                TxtKaydet();
                MenuyeDon();
            }
        }
        static void KayitSil()
        {
            Console.Clear();
            Console.WriteLine("\nKayıtları silmek istediğine emin misin (E/H)");
            string inputDelete = Console.ReadLine();
            inputDelete = inputDelete.ToUpper();
            if (inputDelete == "E")
            {
                kayitlar.Clear();
                Console.Clear();
                Console.WriteLine("Tüm Kayıtlar Silindi");
                TxtKaydet();
                MenuyeDon();
            }
            else
            {
                Console.WriteLine("Ana menü için bir tuşa basınız.");
                MenuyeDon();
            }
        }
        static void TxtKaydet()
        {
            using StreamWriter writer = new StreamWriter("gunluk.txt");
            foreach (var kayit in kayitlar)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                writer.WriteLine(kayit.KayitTarihi);
                Console.ResetColor();
                writer.WriteLine(kayit.Metin);
            }
        }
        static void KayitlariYukle()
        {
            using StreamReader reader = new StreamReader("gunluk.txt");

            string satir;
            while ((satir = reader.ReadLine()) != null)
            {
                string guncelKayit = satir;
                string metin = reader.ReadLine();
                DateTime kayitTarihi;
                if (DateTime.TryParse(guncelKayit, out kayitTarihi))
                {
                    kayitlar.Add(new Kayit(metin) { KayitTarihi = kayitTarihi });
                }
            }
        }
        static void MenuGoster(bool ilkAcilisMi = false)
        {
            Console.Clear();
            if (ilkAcilisMi)
            {
                Console.WriteLine("HOŞ GELDİNİZ");
            }
            Console.WriteLine("Günlük Uygulaması");
            Console.WriteLine("=========================");
            Console.WriteLine("1 - Kayıtları Listele");
            Console.WriteLine("2 - Yeni Kayıt Ekle");
            Console.WriteLine("3 - Tüm Kayıtları Sil");
            Console.WriteLine("4 - Çıkış");
            Console.Write("\nSeçiminiz: ");
            char inputSecim = Console.ReadKey().KeyChar;
            switch (inputSecim)
            {
                case '1':
                    KayitlariListele();
                    break;
                case '2':
                    KayitEkle();
                    break;
                case '3':
                    KayitSil();
                    break;
                case '4':
                    break;
                default:
                    Console.WriteLine("\nYanlış Tuşlama");
                    MenuyeDon();
                    break;

            }
        }
        static void Main(string[] args)
        {
            KayitlariYukle();
            MenuGoster(true);
        }
    }
}

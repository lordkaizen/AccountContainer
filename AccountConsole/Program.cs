using System;
using AccountConsole;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;




internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Yapmak istediğiniz işlemi seçiniz :");
        Console.WriteLine("1- Hesap Ekleme");
        Console.WriteLine("2- Hesap Bilgilerini Değiştirme");
        Console.WriteLine("3- Hesap Silme");
        Console.WriteLine("4- Hesap Bilgilerini Göster");
        Console.WriteLine("5- Hacklendim!");
        int Operation = int.Parse(Console.ReadLine());

        if (Operation == 1)
        {//Entity Kullanılarak Verilen Bilgiler DB ye Kaydedilecek
            Console.Write("Platform adını giriniz(2. Hesaplarınızın sayısını belirtmeyi unutmayın!) :");
            string Platform = Console.ReadLine();
            Console.Write("Platformda kullandığınız e-posta adresini giriniz :");
            string Eposta = Console.ReadLine();
            Console.Write("Platformda kulandığınız şifrenizi giriniz :");
            string Password = Console.ReadLine();
            Console.Write("Hesap kurtarma kodunuz varsa giriniz :");
            string RecoveryCode = Console.ReadLine();

            Add(Platform, Eposta, Password, RecoveryCode);
            Main();
        }
        else if (Operation == 2)
        {// İstenilen hesap çağırılarak bilgileri değiştirilecek
            Console.Write("Bilgilerini güncellemek istediğiniz hesabın Id'sini yazınız. Id'yi bilmiyorsanız tüm hesap bilgilerinizi öğrenebileceğiniz 4 numaralı işleme gidebilirsiniz ");
            int ID = int.Parse(Console.ReadLine());
            Console.Write("Platformda kullandığınız e-posta adresini giriniz :");
            string Eposta = Console.ReadLine();
            Console.Write("Platformda kulandığınız şifrenizi giriniz :");
            string Password = Console.ReadLine();
            Console.Write("Hesap kurtarma kodunuz varsa giriniz :");
            string RecoveryCode = Console.ReadLine();
            Update(ID, Eposta, Password, RecoveryCode);
            Main();
        }
        else if (Operation == 3)
        {// İstenilen Hesap Bilgileri Silinicek
            Console.Write("Silmek istediğiniz hesabın Id'sini yazınız. Id'yi bilmiyorsanız tüm hesap bilgilerinizi öğrenebileceğiniz 4 numaralı işleme gidebilirsiniz ");
            int ID = int.Parse(Console.ReadLine());
            Delete(ID);
            Main();
        }
        else if (Operation == 4)
        {// Tüm Hesap Bilgileri Yazdırılıcak
            List();
            Main();


        }
        else if (Operation == 5)
        {// Tüm Hesap Bilgileri Silinerek Ele Geçirilmesi Önlenicek
            Console.Write("Tüm bilgileriniz silinicek emin misiniz ?");
            if (Console.ReadLine() == "evet" || Console.ReadLine() == "Evet" || Console.ReadLine() == "EVET")
                Hacked();
            else
                Console.WriteLine("");
            Main();
        }
        else
        {// Yukarıdaki işlemlerden birinin seçilmesi istenicek 
            Console.WriteLine("yukarıdaki işlemlerden birini seçiniz...");
            Main();
        }


        static async void Add(string Platform, string Email, string Password, string? RecoveryCode)
        {
            PasswordDbContext context = new();
            Account account = new()
            {
                Platform = Platform,
                Email = Email,
                Password = Password,
                RecoveryCode = RecoveryCode
            };
            await context.AddAsync(account);
            await context.SaveChangesAsync();
            Console.WriteLine("Hesabınız Eklendi " + DateTime.Now);
        }

        static async void Update(int ID, string Email, string Password, string? RecoveryCode)
        {
            PasswordDbContext context = new();
            Account account = await context.Accounts.FirstOrDefaultAsync(a => a.Id == ID);
            account.Email = Email;
            account.Password = Password;
            account.RecoveryCode = RecoveryCode;
            await context.SaveChangesAsync();
            Console.WriteLine(ID + "'li Hesap Bilgileriniz Güncellendi " + DateTime.Now);

        }

        static async void Delete(int ID)
        {
            PasswordDbContext context = new();
            Account account = await context.Accounts.FirstOrDefaultAsync(a => a.Id == ID);
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            Console.WriteLine("Hesap Bilgileriniz Silindi" + DateTime.Now);
        }

        static async void List()
        {
            PasswordDbContext context = new();
            var accounts = await context.Accounts.ToListAsync();
            accounts.ForEach(accounts => {
                Console.WriteLine("Id :"+accounts.Id);
                Console.WriteLine("Platform :"+accounts.Platform);
                Console.WriteLine("E-posta :"+accounts.Email);
                Console.WriteLine("Şifre :"+accounts.Password);
                Console.WriteLine("Kurtarma Kodu"+accounts.RecoveryCode);    
            });     
            

        }
        static async void Hacked() 
        {
            PasswordDbContext context = new();
            var account = await context.Accounts.ToListAsync();
            context.Accounts.RemoveRange(account);
            await context.SaveChangesAsync();
            Console.WriteLine("Tüm Bilgileriniz Silindi"+DateTime.Now);

        }
    }
}
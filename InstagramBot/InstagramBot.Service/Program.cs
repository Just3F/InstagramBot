using System;

namespace InstagramBot.Service
{
    class Program
    {
        static void Main()
        {
            DbSetup dbSetup = new DbSetup();
            dbSetup.Run().GetAwaiter();

            var instaService = new InstaService();
            instaService.Run();

            Console.ReadLine();
        }
    }
}

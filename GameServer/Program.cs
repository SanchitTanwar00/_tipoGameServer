namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            Server.Start(29, 6969);
            Console.ReadKey();
        }
    }
}
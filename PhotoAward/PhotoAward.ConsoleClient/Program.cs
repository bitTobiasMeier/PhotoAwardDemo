using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using PhotoAward.ConsoleClient.Commands;

namespace PhotoAward.ConsoleClient
{
    class Program
    {
        private static readonly Dictionary<string, Command> Commands  = new Dictionary<string, Command>()
        {
            {"ping",new PingCommand() },
            {"getMember",new GetMemberCommand() },
            {"addMember",new AddMemberCommand() },
            { "addPhoto",new AddPhotoCommand() },
            {"getPhotosOfMember",new GetPhotosOfMemberCommand() }

        };

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Commands: ....");
                Console.WriteLine("Press Enter to quit!");
                Console.ReadLine();

                return;
            }

            HandleCommmand(args);

            while (true)
            {
                Thread.Sleep(500);
            }            
        }

        private  static async void HandleCommmand(string[] args)
        {
            //Console.WriteLine("Press ENTER key to start");
            //Console.ReadLine();
            var cmdName = args[0];
            if (!Commands.ContainsKey(cmdName))
            {
                Console.WriteLine("Invalid command");
                Console.WriteLine("Press Enter to quit!");
                Console.ReadLine();
                return;
            }
            var cmd = Commands[cmdName];
            var parameters = args.ToList();
            parameters.RemoveAt(0);

            await cmd.ExecuteAsync(parameters.ToArray());

            Console.WriteLine("Bitte drücken Sie die ENTER-Taste!");
            Console.ReadLine();
            Environment.Exit(0);            
        }
    }
}

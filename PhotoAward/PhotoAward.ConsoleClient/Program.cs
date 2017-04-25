using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
            {"changePassword", new ChangePasswordCommand() },
            {"addMember",new AddMemberCommand() },
            { "addPhoto",new AddPhotoCommand() },
            {
                "getPhotosOfMember",new GetPhotosOfMemberCommand() 
            },
            { "addPhotoComment",new AddPhotoCommentCommand() },
            { "getComments",new GetPhotoCommentsCommand() },
            { "getInfos",new GetInfosAboutAllPhotos() },
            //{ "backup", new BackupPhotosCommand() },
            //{ "restore", new RestorePhotosCommand() },
            {"deletePhoto", new DeletePhotoCommand() }

        };

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("PhotoAward Client");
                foreach (var entry in Commands)
                {
                    var cmd = entry.Value;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine (entry.Key+" " + cmd.GetArguments());
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(cmd.GetDescription());
                    Console.WriteLine();
                }
                Environment.Exit(0);

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
                Environment.Exit(0);
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

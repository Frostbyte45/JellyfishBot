/** Program
 * Author(s): Ryan Rieger, Nia Specht
 * Date: 11/20/2017
 * Description: A bot that has some cool features, and also happens to be a meme between a group of friends.
 * Usage: Please credit Ryan Rieger and Nia Specht for the code if you use it.
 * Version: 1.0
 * Completion Date: N/A
 */

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Jellyfish
{
    class Program
    {
        public static List<string> pokemon = new List<string>();
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "MzgwNDIxNzM4ODQxNzY3OTM2.DO4XCg.LJmlTlBWiYhF2GZDSe96R2KuQ18";

            // event subscriptions
            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();
            await LoadObjects();
            await Task.Delay(-1);
            
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("&", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);

                if (result.IsSuccess)
                {
                    await Task.Delay(1500);
                    await arg.DeleteAsync();
                }
            }

        }

        private async Task LoadObjects()
        {
            // Load objects
            System.IO.StreamReader file = new System.IO.StreamReader("pokemonList.txt");
            int counter = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                line = line.ToLower();
                if(line == "generation I")
                {
                    // Add code later
                }
                else if (line == "generation II")
                {
                    // Add code later
                }
                else if (line == "generation III")
                {
                    // Add code later
                }
                else if (line == "generation IV")
                {
                    // Add code later
                }
                else if (line == "generation V")
                {
                    // Add code later
                }
                else if (line == "generation VI")
                {
                    // Add code later
                }
                else if (line == "generation VII")
                {
                    // Add code later
                }
                else if (line == "generation VIII")
                {
                    // Add code later
                }
                if (!pokemon.Contains(line))
                {
                    if (line.Length >= 10 && line.Substring(0, 10) == "generation")
                    {
                        // FIEND
                    }
                    else
                    {
                        pokemon.Add(line);
                    }
                }
                counter++;
            }
            file.Close();
        }
    }
}

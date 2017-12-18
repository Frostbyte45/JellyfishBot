/** Program
 * Author(s): Ryan Rieger, Nia Specht
 * Date: 11/20/2017
 * Description: A bot that has some cool features, and also happens to be a meme between a group of friends.
 * Usage: Please credit Ryan Rieger and Nia Specht for the code if you use it.
 * Version: 1.0
 * Completion Date: N/A
 */

using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Drawing;
// using System.Linq; // Uncomment this if needed
// using System.Text; // Uncomment this if needed

namespace Jellyfish
{
    class Program
    {
        public static List<string> pokemon = new List<string>();
        public static List<Bitmap> playingCards = new List<Bitmap>();
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
                .AddSingleton<InteractiveService>()
                .BuildServiceProvider();

            string botToken = "MzgwNDIxNzM4ODQxNzY3OTM2.DO4XCg.LJmlTlBWiYhF2GZDSe96R2KuQ18";

            // event subscriptions
            _client.Log += Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();
            await LoadObjects();

            _client.UserJoined += async (e) => {
                // Current version is guild-specific
                if (e.Guild.Id == 322611150246117376)
                {
                    // Adds guild-specific role
                    await e.AddRoleAsync(e.Guild.GetRole(389164716485771264));
                }
            };

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
            //if (message.Content.Contains("11"))
            //{
            //    await arg.DeleteAsync();
            //}
            /*else*/ if (message.HasStringPrefix("&", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
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

        #region Additional Startup Tasks

        
    private async Task LoadObjects()
        {
            // Load objects
            await _client.SetGameAsync("&help to transcend!");

            // Load pokemon
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

            // Load cards
            foreach (Card.Suit suit in Card.valuesS)
            {
                for (int x = 0; x < 13; x++)
                {
                    if (x > 7) // Face cards
                    {
                        switch (x)
                        {
                            case 8:
                                playingCards.Add(new Bitmap("PNG-cards-1.3/" + "10" + "_of_" + suit.ToString().ToLower() + "s.png"));
                                break;
                            case 9:
                                playingCards.Add(new Bitmap("PNG-cards-1.3/" + "jack" + "_of_" + suit.ToString().ToLower() + "s2.png"));
                                break;
                            case 10:
                                playingCards.Add(new Bitmap("PNG-cards-1.3/" + "queen" + "_of_" + suit.ToString().ToLower() + "s2.png"));
                                break;
                            case 10 + 1:
                                playingCards.Add(new Bitmap("PNG-cards-1.3/" + "king" + "_of_" + suit.ToString().ToLower() + "s2.png"));
                                break;
                            case 12:
                                playingCards.Add(new Bitmap("PNG-cards-1.3/" + "ace" + "_of_" + suit.ToString().ToLower() + "s.png"));
                                break;
                            default:
                                Console.WriteLine("Error.");
                                break;
                        }
                    }
                    else // Normal cards
                    {
                        playingCards.Add(new Bitmap("PNG-cards-1.3/" + (x+2) + "_of_" + suit.ToString().ToLower() + "s.png"));
                    }
                }
            }
        }
        
        #endregion
    }
}

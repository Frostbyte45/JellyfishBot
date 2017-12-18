/** Jellyfish Commands
 * Author(s): Nia Specht, Ryan Rieger
 * Date: 11/20/2017
 * Description: Commands for the "Jellyfish" bot.
 * Usage: DO NOT USE CODE WITHOUT CREDIT TO AUTHORS. The Textify/ToBraille commands took a lot of time to write and are Nia's side-projects; credit is due.
 * Version: 1.0
 * Completion date: N/A
 */

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Timers;
using System.Linq;
using Discord.Addons.Interactive;

namespace Jellyfish.Modules
{
    public class JellyfishCommands : InteractiveBase // ModuleBase<SocketCommandContext> If problems arise with Interactive Addons
    {
        #region TODO
        /* TODO
         * TODO REWRITE ALL LOOP METHODS IN ASYNC MODE
         * Add logs for each command's usage, and also count the following variables:
            -"Who's That Pokemon" wins
            -"Help" calls (lol)
            -"89" calls (of course)
            -Times rolled an 89 with "roll"
        * Also, add formatting to help section! (@Ryan)
        * Add Blackjack! And maybe other card games
        * Add Card Game BGM option that allows users to listen to this:https://www.youtube.com/watch?v=6sjP1rSqf1w&list=PLR_Wo4dgp0sjwLUBfuOdXDDE9SYGHRJsl&index=67
            -While playing Blackjack or other card games, in the music channel
        * Add remind command that uses some sort of event handler and the datetime object (maybe Timer?)
        * Create remote server computer for running bot while we aren't testing things
        * DefineEmote - Define lots of different emotes
        * Card Games - Add await ReplyAsync("Who wants to play! (Say \"I do!\" or \"Me!\")"); before games and add multiple player support
                       If not enough players for the game decide to play, cancel the game
        * Card Games - Cards Against Humanity
        * Card Games - Rock, Paper, Scissors
        * Card Games - Poker
        * Card Games - Bull
        * Card Games - Uno
        * Additional Game - FIEnd or FrIEnd
        * TODO Use Graphics class to merge images; it will probably be a lot quicker
         */
        #endregion
        
        public String pokemonName;

        #region Generic Bot Commands

        #region Help
        [Command("help")]
        public async Task HelpAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            IDMChannel c = await Context.User.GetOrCreateDMChannelAsync();
            string msg = "Helpu has arrived.\n```";

            // Command "help"
            msg += "help <cmd> - get command list, or get specific command usage\n";

            // Command "rules"
            msg += "rules <game> - get the rules on a card game\n";

            // Command "invite"
            msg += "invite - get a link to add the bot to your server\n";

            // Command "defaultRole"
            msg += "defaultRole <role> - set a role to be applied to new members on join\n";

            // Command "updateRoles"
            msg += "updateRoles <role> - change members with no roles to the specified role\n";

            // Command "goodnight"
            msg += "goodnight <@user> - say goodnight properly to someone\n";

            // Command "89"
            msg += "89 - 89 made of 89's. What could be better?\n";

            // Command "blackjack"
            msg += "blackjack - start a game of Blackjack!\n";

            // Command "gofish"
            msg += "gofish - start a game of Go Fish!\n";

            // Command "bull"
            msg += "bull - start a game of Bull (Also known as \"I Doubt It\")!\n";

            // Command "uno"
            msg += "uno - start a game of Uno\n";

            // Command "humanity"
            msg += "humanity - start a game of Cards Against Humanity!\n";

            // Command "poker"
            msg += "poker - start a game of Poker!\n";

            // Command "cardlist"
            msg += "cardlist - get an image of the cardset (if you don\'t think it\'s right)\n";

            // Command "ping"
            msg += "ping - get pong'd\n";

            // Command "echo"
            msg += "echo <text> - repeats after you\n";

            // Command "roll"
            msg += "roll <range> - rolls the dice (default is 1-6)\n";

            // Command "pingu"
            msg += "pingu - noot noot\n";

            // Command "avatar"
            msg += "avatar <@user> - get a link to the user's avatar\n";

            //Command "textify"
            msg += "textify <@user OR link> - converts mentioned user's profile picture or an online picture's direct link into pasteable unicode text\n";

            msg += "\n```";
            await c.SendMessageAsync(msg);
        }
        [Command("help")]
        public async Task HelpAsync([Remainder] string args)
        {
            IDMChannel c = await Context.User.GetOrCreateDMChannelAsync();
            string msg2 = "```";
            switch (args) // Lists usage of requested command
            {
                // Help usage
                case "help":
                    msg2 += "You must really need help...\n";
                    msg2 += "Help Usage:\n\"&help <command>\" to get usage on a specific command, ";
                    msg2 += "or \"help\" to get a list of commands.\n";
                    break;

                // Rules usage
                case "rules":
                    msg2 += "Rules Usage:\n\"&rules <card game>\" to get the rules on a card game";
                    break;

                // Invite usage
                case "invite":
                    msg2 += "Invite Usage:\n\"&invite\" to get a link for adding the bot to a server. ";
                    msg2 += "Note: you must have permission to manage bots on the server!";
                    break;

                // Goodnight usage
                case "goodnight":
                    msg2 += "Goodnight Usage:\n\"&goodnight <@user>\" to say goodnight to a specific user, ";
                    msg2 += "or \"&goodnight\" to wish yourself goodnight.\n";
                    break;

                // 89 usage
                case "89":
                    msg2 += "89 Usage:\n\"&89\" to get 89 made of 89's.";
                    break;

                // Cardset usage
                case "cardset":
                    msg2 += "Cardset Usage:\n\"&cardset\" to get an image of all the cards used in ";
                    msg2 += "the bot\'s card games";
                    break;
                
                // Blackjack usage
                case "blackjack":
                    msg2 += "Blackjack Usage:\n\"&blackjack\" to start a game of Blackjack, ";
                    msg2 += "use \"&rules blackjack\" to get the rules";
                    break;

                // Go Fish usage
                case "gofish":
                    msg2 += "Go Fish Usage:\n\"&gofish\" to start a game of Go Fish, ";
                    msg2 += "use \"&rules gofish\" to get the rules";
                    break;

                // Bull usage
                case "bull":
                    msg2 += "Bull Usage:\n\"&bull\" to start a game of Bull (also known as \"I Doubt It\"), ";
                    msg2 += "use \"&rules bull\" to get the rules";
                    break;

                // Uno usage
                case "uno":
                    msg2 += "Uno Usage:\n\"&uno\" to start a game of Uno, ";
                    msg2 += "use \"&rules uno\" to get the rules";
                    break;

                // Cards Against Humanity usage
                case "humanity":
                    msg2 += "Cards Against Humanity Usage:\n\"&humanity\" to start a game of Cards Against Humanity, ";
                    msg2 += "use \"&rules humanity\" to get the rules";
                    break;

                // Poker usage
                case "poker":
                    msg2 += "Poker Usage:\n\"&poker\" to start a game of Poker, ";
                    msg2 += "use \"&rules poker\" to get the rules";
                    break;

                // Echo usage
                case "echo":
                    msg2 += "Echo Usage:\n\"&echo <text>\" to repeat text.";
                    break;
                
                // Roll usage
                case "roll":
                    msg2 += "Roll Usage:\n\"&roll <range>\" to return a random number from 1 to range.";
                    break;

                // Pingu usage
                case "pingu":
                    msg2 += "Pingu Usage:\n\"&pingu\" for noots.";
                    break;
                
                // Avatar usage
                case "avatar":
                    msg2 += "Avatar Usage:\n\"&avatar <@user>\" to get that user's avatar.";
                    break;

                // Textify usage
                case "textify":
                    msg2 += "textify Usage:\n\"&textify <@user OR link>\" to get that user's avatar or link's picture as text (use mentions), ";
                    msg2 += "no parameters will use your profile picture.";
                    break;
                
                // Incorrect spelling section
                default:
                    msg2 += "Please enter a valid command to get help on.";
                    break;
            }
            msg2 += "```";
            await c.SendMessageAsync(msg2);
        }
        #endregion

        #region Invite
        [Command ("invite")]
        public async Task InviteAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            string link = "https://discordapp.com/oauth2/authorize?&client_id=380421738841767936&scope=bot&permissions=0";
            await ReplyAsync("Let\'s convert another server to our religion!: " + link);
        }
        #endregion

        #region Echo
        [Command("echo")]
        public async Task EchoAsync([Remainder] string stuffToEcho)
        {
            await Context.Channel.TriggerTypingAsync();
            stuffToEcho = "*" + stuffToEcho + "*";

            await ReplyAsync(stuffToEcho/* + "\n" + stuffToEcho + "\n" + stuffToEcho*/);
        }
        #endregion

        #region Ping
        [Command("ping")]
        public async Task PingAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            EmbedBuilder builder = new EmbedBuilder();
            Discord.Color myRgbColor = new Discord.Color(255, 102, 179);

            builder.AddField("Ping!", "*bloop* ~ Pong!")
                .WithColor(myRgbColor);

            await ReplyAsync("", false, builder.Build());
        }
        #endregion

        #region Roll
        [Command("roll")]
        public async Task RollAsync(int num)
        {
            await Context.Channel.TriggerTypingAsync();
            if (num == 11)
            {
                await ReplyAsync("...No.");
            }
            else if (num < 1)
            {
                await ReplyAsync("Number has to be greater than one!");
            }
            else
            {
                Random gen = new Random();
                int temp = gen.Next(num) + 1;
                if (temp == 11)
                {
                    await ReplyAsync("Fiend, you rolled an " + temp + "... \nThe 89 Jellyfish mourn with you.");
                }
                else
                {
                    await ReplyAsync("You rolled a " + temp);
                }
            }
        }
        [Command("roll")]
        public async Task RollAsync()
        {
            Random gen = new Random();
            int temp = gen.Next(6) + 1;
            if (temp == 11)
            {
                await ReplyAsync("Fiend, you rolled an " + temp + "... \nThe 89 Jellyfish mourn with you.");
            }
            else
            {
                await ReplyAsync("You rolled a " + temp);
            }
        }
        #endregion
        
        #region Role Updater

        [Command("updateRoles")]

        public async Task RoleUpdateAsync([Remainder] IRole role)
        {
            if (Context.Guild.CurrentUser.GetPermissions(Context.Guild.GetChannel(Context.Channel.Id)).ManagePermissions == true)
            {
                bool change = false;
                try
                {
                    for (int x = 0; x < Context.Guild.MemberCount; x++)
                    {
                        IUser user = Context.Guild.Users.ElementAt(x);
                        if (Context.Guild.GetUser(user.Id).Roles.Count == 1 && !user.IsBot)
                        {
                            change = true;
                            await Context.Guild.GetUser(user.Id).AddRoleAsync(role);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await ReplyAsync("There was an error:```" + ex.ToString() + "```");
                }
                if (change)
                    await ReplyAsync("Successfully changed users with no roles to the specified role.");
                else await ReplyAsync("There were no users to change!");
            }
            else
            {
                await ReplyAsync("You don\'t have permission to use this command!");
            }
        }

        [Command("updateRoles")]

        public async Task RoleUpdateAsync([Remainder] string notRole)
        {
            await ReplyAsync("The specified role does not exist! Check spelling");
        }

        #endregion

        #region Default Role

        [Command("defaultRole")]

        public async Task DefaultRoleAsync([Remainder] IRole role)
        {
            try
            {
                if (Context.Guild.CurrentUser.GetPermissions(Context.Guild.GetChannel(Context.Channel.Id)).ManagePermissions == true)
                {
                    Context.Client.UserJoined -= async (e) => { await e.AddRoleAsync(role); };
                    Context.Client.UserJoined += async (e) => { await e.AddRoleAsync(role); };
                    await ReplyAsync("The requested role will be applied to new members from now on!");
                }
                else
                {
                    await ReplyAsync("You don\'t have permission to set the default new user role!");
                }
            }
            catch (Exception ex)
            {
                await ReplyAsync("An error occurred while applying the role, try again later:\n" + ex);
            }
        }

        // Slower code that allows for error messaging
        /*[Command("defaultRole")]

        public async Task DefaultRoleAsync([Remainder] string role)
        {
            bool change = false;
            if (Context.Guild.CurrentUser.GetPermissions(Context.Guild.GetChannel(Context.Channel.Id)).ManagePermissions == true)
            {
                for (int x = 0; x < Context.Guild.Roles.Count; x++)
                {
                    if (Context.Guild.Roles.ElementAt(x).ToString().ToLower() == role.ToLower())
                    {
                        Context.Client.UserJoined -= async (e) => { await e.AddRoleAsync(Context.Guild.Roles.ElementAt(x)); };
                        Context.Client.UserJoined += async (e) => { await e.AddRoleAsync(Context.Guild.Roles.ElementAt(x)); };
                        await ReplyAsync("The requested role will be applied to new members from now on!");
                        change = true;
                        break;
                    }
                }
                if (!change)
                {
                    await ReplyAsync("Could not find the requested role, check the spelling!");
                }
            }
            else
            {
                await ReplyAsync("You don\'t have permission to set the default new user role!");
            }
        }*/

        #endregion

        #endregion

        #region Card Games

        #region Rules

        [Command("rules")]
        public async Task RulesAsync([Remainder] string game)
        {
            game = game.ToLower().Trim();
            string rules = "";
            bool error = false;
            switch (game)
            {
                // Blackjack
                case "blackjack":
                    rules += "Blackjack starts by showing you the dealer\'s first card (hiding their other card) and your hand.\n";
                    rules += "Then, you \"hit\" to get another card, or \"stand\" to keep your current hand.\n";
                    rules += "You want to get as close to a total count of 21, but you don\'t want to go over it!\n";
                    rules += "In this game, face cards are all worth 10, and aces can be 1 or one more than 10. Good luck!\n";
                    break;

                // Go Fish
                case "gofish":
                    rules += "Go Fish isn\'t supported yet, sorry!";
                    break;

                // Bull
                case "bull":
                    rules += "Bull (Also called \"I Doubt It\") isn\'t supported yet, sorry!";
                    break;

                // Uno
                case "uno":
                    rules += "Uno isn\'t supported yet, sorry!";
                    break;

                // Cards Against Humanity
                case "humanity":
                    rules += "Cards Against Humanity isn\'t supported yet, sorry!";
                    break;
                
                // Poker
                case "poker":
                    rules += "Poker isn\'t supported yet, sorry!";
                    break;

                // Other
                default:
                    await ReplyAsync("The game " + game + " is not supported or is mispelled...\nUse \"&help\" to check supported games!");
                    error = true;
                    break;
            }
            if(!error)
                await ReplyAsync("Here are the rules on " + game + ":\n" + rules);
        }

        [Command("rules")]
        // Overloader for incorrect parameters
        public async Task RulesAsync()
        {
            await ReplyAsync("Please choose a card game to get rules on with \"&rules <game>\" (without \"<>\")!");
        }

        #endregion

        #region Card List

        [Command("cardlist")]
        public async Task CardListAsync()
        {
            await Context.Channel.SendFileAsync("cardlist.png", "Here are all the cards in a deck. I hope they\'re right...");
        }

        #endregion

        #region Blackjack

        // Players: 1 - 3
        [Command("blackjack",RunMode = RunMode.Async)]
        public async Task BlackjackAsync()
        {
            IMessageChannel chan = Context.Channel;
            await chan.TriggerTypingAsync();

            // Generate game
            Deck deck = new Deck();
            deck.Shuffle();
            List<Card> hand = new List<Card>();
            List<Card> dHand = new List<Card>();
            List<Bitmap> playerCards = new List<Bitmap>();
            List<Bitmap> dealerCards = new List<Bitmap>();
            Bitmap playCards;
            Bitmap dealCards;

            // Add dealer cards
            dHand.Add(deck.NextCard());
            dHand.Add(deck.NextCard());
            dealerCards.Add(dHand[0].GetBitmap());
            dealerCards.Add(dHand[1].GetBitmap());

            // Add player cards
            hand.Add(deck.NextCard());
            hand.Add(deck.NextCard());
            playerCards.Add(hand[0].GetBitmap());
            playerCards.Add(hand[1].GetBitmap());

            // Merge hands
            playCards = await Task.Run(() => MergeAsync(playerCards));
            dealCards = await Task.Run(() => MergeAsync(dealerCards));

            // Show dealer's first card (hide the "hole" card!)
            await Task.Delay(1500);
            // await ReplyAsync("Here\'s the dealer\'s first card!");
            MemoryStream stream3 = new MemoryStream();
            dHand[0].GetBitmap().Save(stream3, System.Drawing.Imaging.ImageFormat.Png);
            stream3.Seek(0, SeekOrigin.Begin);
            await chan.SendFileAsync(stream3, "cards.png", "Here\'s the dealer\'s first card!");

            // Booleans to keep game running correctly
            bool stop = true;
            bool gameOver = false;

            // Request user's response until they stand
            int index = 2;
            do
            {
                // Send hand
                await chan.TriggerTypingAsync();
                await Task.Delay(1500);
                playCards = await Task.Run(() => MergeAsync(playerCards));
                MemoryStream stream = new MemoryStream();
                playCards.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                await chan.SendFileAsync(stream, "cards.png", "Here\'s your hand " + Context.User.Mention + "!");

                // Booleans to keep game running correctly
                bool contin = false;
                bool hit = false;

                IMessage Request;
                SocketMessage Response;
                // Make sure the user's input is correct
                do
                {
                    await chan.TriggerTypingAsync();
                    await Task.Delay(1500);
                    Request = await ReplyAsync("Hit or stand, " + Context.User.Mention + "?");

                    // Await next message, then check contents
                    Response = await NextMessageAsync(true, true, new TimeSpan(0, 0, 45));

                    if (Response.Content.ToLower().Contains("hit") && (Response.Content.ToLower().Contains("stay") || Response.Content.ToLower().Contains("stand")))
                    {
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Please say only \"hit\", or only \"stay\" in your response.");
                    }
                    else if (Response.Content.ToLower().Contains("hit"))
                    {
                        hit = true;
                        contin = true;
                    }
                    else if (Response.Content.ToLower().Contains("stay") || Response.Content.ToLower().Contains("stand"))
                    {
                        hit = false;
                        contin = true;
                    }
                    else
                    {
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Please say \"hit\" or \"stay\" in your response.");
                    }
                } while (!contin);

                // Hit or stand
                if (hit)
                {
                    hand.Add(deck.NextCard());
                    playerCards.Add(hand[index].GetBitmap());
                    stop = false;
                    index++;
                }
                else
                {
                    stop = true;
                }
                if (deck.Count(hand) > 21)
                {
                    stop = true;
                    gameOver = true;
                }
            } while (!stop);
            if (gameOver)
            {
                //Console.WriteLine("P: " + deck.Count(hand) + " D: " + deck.Count(dHand));
                await chan.TriggerTypingAsync();
                await Task.Delay(1500);

                // Player's final hand
                // TODO @Ryan Add embed builder here for images? idrk
                // Merge hands
                playCards = await Task.Run(() => MergeAsync(playerCards));
                dealCards = await Task.Run(() => MergeAsync(dealerCards));

                // Send hand
                // await ReplyAsync("You\'ve lost " + Context.User.Mention + "...!\nYour card count went over 21.\nHere\'s your hand:");
                playCards = await Task.Run(() => MergeAsync(playerCards));
                string customName = Convert.ToString(Context.User.Id);
                MemoryStream stream = new MemoryStream();
                playCards.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                await chan.SendFileAsync(stream, "cards.png", "You\'ve lost " + Context.User.Mention + "...!\nYour card count went over 21.\nHere\'s your hand:");

            }
            else // Dealer's turn
            {
                await chan.TriggerTypingAsync();
                await Task.Delay(1500);
                int index2 = 2;
                do
                {
                    // Dealer abuse
                    dHand.Add(deck.NextCard());
                    dealerCards.Add(dHand[index2].GetBitmap());
                    index2++;

                    // Check to see if the dealer lost already!
                    if (deck.Count(dHand) > 21)
                    {
                        // Console.WriteLine("P: " + deck.Count(hand) + " D: " + deck.Count(dHand));
                        // await ReplyAsync("You\'ve won " + Context.User.Mention + "!\nThe dealer\'s card count went over 21.\nHere\'s your hand:");

                        // Player's final hand
                        playCards = await Task.Run(() => MergeAsync(playerCards));
                        MemoryStream stream = new MemoryStream();
                        playCards.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream,"cards.png", "You\'ve won " + Context.User.Mention + "!\nThe dealer\'s card count went over 21.\nHere\'s your hand:");

                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        // await ReplyAsync("Here\'s the dealer\'s hand:");
                        // Dealer's final hand
                        dealCards = await Task.Run(() => MergeAsync(dealerCards));
                        MemoryStream stream2 = new MemoryStream();
                        dealCards.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);
                        stream2.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream2, "cards.png", "Here\'s the dealer\'s hand:");
                        // Exit game
                        gameOver = true;
                        break;
                    }
                } while (deck.Count(dHand) < 17);

                // See who wins if nobody lost yet
                if (!gameOver)
                {
                    if (deck.Count(hand) > deck.Count(dHand)) // Player wins
                    {
                        // Console.WriteLine("P: " + deck.Count(hand) + " D: " + deck.Count(dHand));
                        // await ReplyAsync("You\'ve won " + Context.User.Mention + "!\nGood game! Your hand was better!\nHere\'s your hand:");

                        // Player's final hand
                        playCards = await Task.Run(() => MergeAsync(playerCards));
                        MemoryStream stream = new MemoryStream();
                        playCards.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream, "cards.png", "You\'ve won " + Context.User.Mention + "!\nGood game! Your hand was better!\nHere\'s your hand:");

                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        // await ReplyAsync("Here\'s the dealer\'s hand:");
                        // Dealer's final hand
                        dealCards = await Task.Run(() => MergeAsync(dealerCards));
                        MemoryStream stream2 = new MemoryStream();
                        dealCards.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);
                        stream2.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream2, "cards.png", "Here\'s the dealer\'s hand:");



                        // Exit game
                    }
                    else if (deck.Count(dHand) > deck.Count(hand)) // Dealer wins
                    {
                        // Console.WriteLine("P: " + deck.Count(hand) + " D: " + deck.Count(dHand));
                        // await ReplyAsync("You\'ve lost " + Context.User.Mention + "...!\nGood game! The dealer\'s hand was better.\nHere\'s your hand:");

                        // Player's final hand
                        playCards = await Task.Run(() => MergeAsync(playerCards));
                        MemoryStream stream = new MemoryStream();
                        playCards.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream, "cards.png", "You\'ve lost " + Context.User.Mention + "...!\nGood game! The dealer\'s hand was better.\nHere\'s your hand:");

                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        // await ReplyAsync("Here\'s the dealer\'s hand:");
                        // Dealer's final hand
                        dealCards = await Task.Run(() => MergeAsync(dealerCards));
                        MemoryStream stream2 = new MemoryStream();
                        dealCards.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);
                        stream2.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream2, "cards.png", "Here\'s the dealer\'s hand:");



                        // Exit game
                    }
                    else // Player and Dealer tie
                    {
                        // Console.WriteLine("P: " + deck.Count(hand) + " D: " + deck.Count(dHand));
                        // await ReplyAsync("You\'ve tied with the dealer " + Context.User.Mention + ".\nGood game! The dealer\'s hand and yours are equal.\nHere\'s your hand:");

                        // Player's final hand
                        playCards = await Task.Run(() => MergeAsync(playerCards));
                        MemoryStream stream = new MemoryStream();
                        dealCards.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream, "cards.png", "You\'ve tied with the dealer " + Context.User.Mention + ".\nGood game! The dealer\'s hand and yours are equal.\nHere\'s your hand:");

                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        // await ReplyAsync("Here\'s the dealer\'s hand:");
                        // Dealer's final hand
                        dealCards = await Task.Run(() => MergeAsync(dealerCards));
                        MemoryStream stream2 = new MemoryStream();
                        dealCards.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);
                        stream2.Seek(0, SeekOrigin.Begin);
                        await chan.SendFileAsync(stream2, "cards.png", "Here\'s the dealer\'s hand:");

                        // Exit game
                        // return;
                    }
                }
            }
        }
        #endregion

        #region Go Fish

        // Players: 2 - 5 (up to 10 works)
        [Command("gofish",RunMode = RunMode.Async)]
        public async Task GoFishAsync()
        {
            // If 5 players or more, deal 5 cards to each player
            // Else deal 7 cards to each player
            // Suit doesn't matter, must have one of rank before asking
            // If person has rank, they give all of that rank away, else asker must "Go Fish"
            // Manage game with no commands, only keywords, ie. "Do you have any (keyword)3's/threes?", "(key phrase)Go fish!"
            // The game would then hit the asker with a card, or transfer all cards of that rank if the responder has that rank
            await ReplyAsync("Who wants to play Go Fish? Say \"done\" after everyone has decided. (Up to 10 players!)");
            List<IMessage> responses = new List<IMessage>();
            bool done = false;
            int playerNum = 0;
            int runs = 0;
            int timeOuts = 0;

            // Add players
            List<IUser> players = new List<IUser>();
            players.Add(Context.User);
            playerNum++;
            do
            {
                responses.Add(await NextMessageAsync(false, true, new TimeSpan(0,0,30)));
                if(responses.ElementAt(runs) == null)
                {
                    timeOuts++;
                }
                else if(runs > 0 && responses.ElementAt(runs) != null && responses.ElementAt(runs).Content.ToLower().Contains("done"))
                {
                    done = true;
                }
                else if(responses.ElementAt(runs) != null && !responses.ElementAt(runs).Author.IsBot && !players.Contains(responses.ElementAt(runs).Author) && !responses.ElementAt(runs).Content.ToLower().Contains("i don") && (responses.ElementAt(runs).Content.ToLower().Contains("i do") || responses.ElementAt(runs).Content.ToLower().Contains("me") || responses.ElementAt(runs).Content.ToLower().Contains("i wan")))
                {
                    // Get user and add them to a list
                    players.Add(responses.ElementAt(runs).Author);
                    playerNum++;
                    await ReplyAsync(players.ElementAt(playerNum - 1).Username + ", you\'re in!");
                }
                else
                {
                    // Ignore
                }
                runs++;
                if((players.Count < 2 && timeOuts > 1) || (players.Count < 2 && runs > 9) || (players.Count < 2 && done))
                {
                    await ReplyAsync("The game can\'t be started with only " + players.Count + " player(s)... Try again when you have more people!");
                    return;
                }
            } while (!done || runs > 9 || timeOuts > 1);

            // Deal out cards
            await ReplyAsync("You\'ll get your hand in a bit... They\'ll be in your DM\'s!");
            Deck deck = new Deck();
            deck.Shuffle();
            List<List<Card>> hands = new List<List<Card>>();
            List<List<Bitmap>> cards = new List<List<Bitmap>>();
            List<List<Card.Rank>> books = new List<List<Card.Rank>>();
            int cardsDealt = 7;
            if(players.Count >= 5)
            {
                cardsDealt = 5;
            }
            for(int x = 0; x < players.Count; x++)
            {
                if(players[x] != null)
                {
                    hands.Add(new List<Card>());
                    cards.Add(new List<Bitmap>());
                    books.Add(new List<Card.Rank>());
                    for (int y = 0; y < cardsDealt; y++)
                    {
                        hands[x].Add(deck.NextCard());
                        cards[x].Add(hands[x].ElementAt(y).GetBitmap());
                    }

                    // Send player their hand
                    IDMChannel c = await players[x].GetOrCreateDMChannelAsync();
                    Bitmap temp = await Task.Run(() => MergeAsync(cards[x]));
                    MemoryStream stream = new MemoryStream();
                    temp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);
                    await c.SendFileAsync(stream, "cards.png", "Here\'s your hand!");
                }
                else
                {
                    // Console.WriteLine("Players at " + x + "is null.");
                }
            }

            // Start the game, choosing a player to go first randomly
            // Console.WriteLine("Game started.");
            Random gen = new Random();

            // Note: Incremental class was made in CardTools.cs, and is extremely helpful for turns, since you can start at any value!
            Incremental whosTurn = new Incremental(0, players.Count - 1, gen.Next(players.Count));

            // Output turn order
            EmbedBuilder builder = new EmbedBuilder();
            string playerOrder = players[whosTurn.Current()].Username;
            
            while(!whosTurn.RoundEnd()) // Adds players in order until the end
            {
                playerOrder += "\n" + players[whosTurn.Next()].Username;
            }
            builder.AddField("Turn Order:", playerOrder)
                .WithColor(new Discord.Color(255, 102, 179));
            await ReplyAsync("Here\'s the turn order, start when you\'re ready " + players[whosTurn.Reset()].Mention + ":", false,builder.Build());

            // Start game loop, searching messages for key words!
            // Use KeyWords.DecodeRank(string rank) to check user inputs after a split
            ISocketMessageChannel chan = Context.Channel;
            SocketMessage Response;
            bool gameOver = false;
            bool roundOver = false;
            int timeSpans = 0;
            int waitTime = 60;
            bool askTurn = true;
            Card.Rank reqRank = Card.Rank.Two;

            // Game loop
            while (!gameOver)
            {
                do // Message loop
                {
                    // Check each user's hand for 4 matching ranks
                    for (int y = 0; y < players.Count; y++)
                    {
                        // While the player has books, remove the cards from their hand and add to their book list
                        while (deck.RankMatch(hands[y]))
                        {

                        }
                    }

                    // Await next message, then check contents
                    Response = await NextMessageAsync(false, true, new TimeSpan(0, 0, waitTime));

                    // For empty/null message, time out after a while so it doesn't hold up the gateway
                    if (Response == null)
                    {
                        switch (timeSpans)
                        {
                            case 3:
                                await ReplyAsync("Ending game.");
                                break;
                            case 2:
                                await ReplyAsync("The game will end in a minute if you don\'t say anything!\n Say \"pause <seconds>\" without the <> to wait that many seconds until you\'re back.");
                                break;
                            case 1:
                                await ReplyAsync("You really shouldn\'t be taking two minutes for this, " + players[whosTurn.Current()].Username + "!");
                                break;
                            default:
                                await ReplyAsync("You\'ve taken a minute, just so you know, " + players[whosTurn.Current()].Username + "-"); // Comment out later
                                break;
                        }
                        waitTime = 60;
                        timeSpans++;
                        continue;
                    }

                    // Wait for people that have to brb/afk
                    if (Response.Content.ToLower().Contains("pause"))
                    {
                        string[] resp = Response.Content.ToLower().Split(' ');
                        for (int x = 0; x < resp.Length; x++)
                        {
                            if (resp[x].Equals("pause"))
                            {
                                try
                                {
                                    waitTime = Convert.ToInt32(resp[x + 1]);
                                    if (waitTime > 600) waitTime = 600;
                                    await ReplyAsync("Let\'s wait " + waitTime + " seconds for the next turn.");
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Error in number formatting for pause time: " + e);
                                    break;
                                }
                            }
                        }
                        continue;
                    }

                    // Check message for key words from current user
                    if (askTurn)
                    {
                        // Ignore messages sent by other people!
                        if (Response.Author.Id != players[whosTurn.Current()].Id)
                            continue;

                        // Check if the message is actually a request
                        if(Response.Content.ToLower().Contains("have any"))
                        {
                            string[] resp = Response.Content.ToLower().Split(' ');
                            bool fail = true;
                            Card.Rank rank = Card.Rank.Two;
                            for (int x = 0; x < resp.Length; x++)
                            {
                                if (resp[x].Equals("any"))
                                {
                                    try
                                    {
                                        rank = KeyWords.DecodeRank(resp[x + 1]);
                                        fail = false;
                                        break;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Not a rank: " + e);
                                        fail = true;
                                        break;
                                    }
                                }
                            }
                            
                            // Just ignore if people don't say an actual rank after "any"
                            if (fail) continue;

                            // See if the current user has the rank in their hand, if not remind them of the rules with :eyes: for effect
                            if (!deck.HasRank(rank, hands[whosTurn.Current()]))
                            {
                                await ReplyAsync("You have to have one of the ranks of the card you ask for, " + players[whosTurn.Current()].Username + "... :eyes:");
                                continue;
                            }

                            // Move to next turn
                            askTurn = false;
                            whosTurn.Next();
                            reqRank = rank;
                        }
                    }

                    // Response must have "go fish" or "I do" or the like
                    else
                    {
                        // Ignore messages sent by other people!
                        if (Response.Author.Id != players[whosTurn.Current()].Id)
                            continue;

                        // Check if the message is actually a response
                        if (Response.Content.ToLower().Contains("go fish"))
                        {
                            // See if the current user has the rank in their hand, if so remind them of the rules with :eyes: for effect
                            if (deck.HasRank(reqRank, hands[whosTurn.Current()]))
                            {
                                await ReplyAsync("You can only tell them to fish if you _don\'t_ have that card rank, " + players[whosTurn.Current()].Username + "... :eyes:");
                                continue;
                            }

                            // Add a random card to the last player's hand, so they "go fish"
                            hands[whosTurn.Last()].Add(deck.NextCard());
                            IDMChannel c = await players[whosTurn.Last()].GetOrCreateDMChannelAsync();
                            Bitmap temp = await Task.Run(() => MergeAsync(cards[whosTurn.Last()]));
                            MemoryStream stream = new MemoryStream();
                            temp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            stream.Seek(0, SeekOrigin.Begin);
                            await c.SendFileAsync(stream, "cards.png", "Here\'s your new hand, enjoy your fishing.");
                            askTurn = true;
                            continue;
                        }

                        // Check if the message is actually a response
                        string cont = Response.Content.ToLower();
                        if ((cont.Contains("i do") || cont.Contains("yes") || cont.Contains("indeed") || cont.Contains("ya")))
                        {
                            // See if the current user has the rank in their hand, if so remind them of the rules with :eyes: for effect
                            if (!deck.HasRank(reqRank, hands[whosTurn.Current()]))
                            {
                                await ReplyAsync("You can only say that you do if you actually do, y\'know, " + players[whosTurn.Current()].Username + "... :eyes:");
                                continue;
                            }

                            // Transfer all cards of that rank to last player
                            for(int x = 0; x < hands[whosTurn.Current()].Count; x++)
                            {
                                if(hands[whosTurn.Current()].ElementAt(x).GetRank() == reqRank)
                                {
                                    hands[whosTurn.Last()].Add(hands[whosTurn.Current()].ElementAt(x));
                                    hands[whosTurn.Current()].RemoveAt(x);
                                }
                            }

                            // Send players their updated hands
                            // Requesting player
                            IDMChannel c = await players[whosTurn.Last()].GetOrCreateDMChannelAsync();
                            Bitmap temp = await Task.Run(() => MergeAsync(cards[whosTurn.Last()]));
                            MemoryStream stream = new MemoryStream();
                            temp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            stream.Seek(0, SeekOrigin.Begin);
                            await c.SendFileAsync(stream, "cards.png", "Here\'s your new hand, you\'ve got some new ones!");

                            // Responding player
                            IDMChannel ch = await players[whosTurn.Current()].GetOrCreateDMChannelAsync();
                            Bitmap temp2 = await Task.Run(() => MergeAsync(cards[whosTurn.Current()]));
                            MemoryStream stream2 = new MemoryStream();
                            temp.Save(stream2, System.Drawing.Imaging.ImageFormat.Png);
                            stream2.Seek(0, SeekOrigin.Begin);
                            await ch.SendFileAsync(stream2, "cards.png", "Here\'s your new hand, you\'ve lost a few.");

                            askTurn = true;
                        }
                    }

                    timeSpans = 0;
                } while (!roundOver); // End message loop
            } // End game loop
        }

        #endregion

        #region Bull

        // Players: 3 - 6
        [Command("bull",RunMode = RunMode.Async)]
        public async Task BullAsync()
        {
            await ReplyAsync("Not yet supported.");
        }

        #endregion

        #region Cards Against Humanity

        // Players: 3 - 10 (up to 20 works)
        [Command("humanity",RunMode = RunMode.Async)]
        public async Task HumanityAsync()
        {
            await ReplyAsync("Not yet supported.");
        }

        #endregion

        #region Poker

        // Players: 2 - 9
        [Command("poker",RunMode = RunMode.Async)]
        public async Task PokerAsync()
        {
            await ReplyAsync("Not yet supported.");
        }

        #endregion

        #endregion

        #region Memes

        #region goodnight
        [Command("goodnight")]
        public async Task GoodnightAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync(Context.User.Mention + ", May the 89 jellyfish gods bless your dreams with their presence.");
        }

        [Command("goodnight")]
        public async Task GoodnightAsync(SocketUser user)
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync(user.Mention + ", May the 89 jellyfish gods bless your dreams with their presence.");
        }
        #endregion

        #region 89
        [Command("89")]
        public async Task EightyNineAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync("e  i  g  h  t  y  n  i  n  e          e  i  g  h  t  y  n  i  n  e\n" +
                             "i                                   i           i                                  i\n" +
                             "g                                 g          g                                 g\n" +
                             "h                                 h          h                                 h\n" +
                             "t                                  t           t                                  t\n" +
                             "y                                 y          y                                 y\n" +
                             "n                                 n         n                                 n\n" +
                             "i                                   i          i                                   i\n" +
                             "n                                 n         n                                 n\n" +
                             "e  i  g  h  t  y  n  i  n  e         e  i  g  h  t  y  n  i  n  e\n" +
                             "i                                   i                                             i\n" +
                             "g                                 g                                            g\n" +
                             "h                                 h                                            h\n" +
                             "t                                  t                                             t\n" +
                             "y                                 y                                            y\n" +
                             "n                                 n                                            n\n" +
                             "i                                   i                                             i\n" +
                             "n                                 n                                            n\n" +
                             "e  i  g  h  t  y  n  i  n  e                                             e");
        }
        #endregion

        #region pingu
        [Command("pingu")]
        public async Task PinguAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync("https://www.youtube.com/watch?v=Fs3BHRIyF2E");
        }
        #endregion

        #endregion

        #region Who's That Pokemon
        [Command("whosthat")]
        public async Task WhoAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            pokemonName = "";
            Random generator = new Random();
            pokemonName = Program.pokemon[generator.Next(Program.pokemon.Count - 1) + 1];
            Uri picUri = new Uri("https://img.pokemondb.net/artwork/" + pokemonName + ".jpg");
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCallback);
            Object obj = new object();
            webClient.DownloadDataAsync(picUri,obj);
            await ReplyAsync("Who's that pokemon..?");
        }
        #endregion

        #region Avatar
        public async Task AvatarAsync([Remainder] SocketUser mention)
        {
            await Context.Channel.TriggerTypingAsync();
            var picUrl = mention.GetAvatarUrl(Discord.ImageFormat.Jpeg);
            await ReplyAsync("Here's the avatar url, " + Context.User.Mention + "!: " + picUrl);
        }
        #endregion

        #region Textify
        [Command("textify")]
        public async Task TextifyAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync("(Beta version, pictures might not look quite right!)");
            // Get user's profile picture
            var picUrl = Context.User.GetAvatarUrl(Discord.ImageFormat.Jpeg);
            Uri picUri = new Uri(picUrl);
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCallback);
            webClient.DownloadDataAsync(picUri);
            // The rest of this command is handled in the event handler
        }

        [Command("textify")]
        public async Task TextifyAsync([Remainder] SocketUser mention) // Overloader method for <@mentions>
        {
            await ReplyAsync("(Beta version, pictures might not look quite right!)");
            // Get user's profile picture
            //IUser mentionedUser = await Context.Channel.GetUserAsync((ulong)Convert.ToInt64(mention.Replace('<',' ').Replace('!',' ').Replace('@',' ').Replace('>',' ').Trim()));
            var picUrl = mention.GetAvatarUrl(Discord.ImageFormat.Jpeg);
            Uri picUri = new Uri(picUrl);
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCallback);
            webClient.DownloadDataAsync(picUri);
            // The rest of this command is handled in the event handler
        }

        [Command("textify")]
        public async Task TextifyAsync([Remainder] string url) // Overloader method for <@mentions>
        {
            Uri uri = new Uri(url);
            await ReplyAsync("(Beta version, pictures might not look quite right!)");
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCallback);
            webClient.DownloadDataAsync(uri);
            // The rest of this command is handled in the event handler
        }
        #endregion

        #region Downloader
        // Event handler for download of User avatar (called by Overloader as well)
        public async void DownloadDataCallback(Object sender,DownloadDataCompletedEventArgs e) //Overloader for the download
        {
            try
            {
                // If the request was not canceled and did not throw
                // an exception, start conversion.
                if (e.Cancelled)
                {
                    Console.WriteLine("Cancelled: " + e.Error);
                }
                if (!e.Cancelled && e.Error == null)
                {
                    using (Context.Channel.EnterTypingState())
                    {
                        // Get the raw result bytes and declare Bitmap object
                        byte[] data = (byte[])e.Result;
                        Bitmap bmp;

                        // Read image data into a memory stream and use the stream to initialize Bitmap object
                        using (MemoryStream ms = new MemoryStream(data, 0, data.Length))
                        {
                            ms.Write(data, 0, data.Length);
                            bmp = new Bitmap(ms);
                        }

                        if (bmp.Width != 128 || bmp.Height != 128)
                        {
                            bmp = ResizeImage(bmp, new Size(128, 128));
                        }

                        // Get Pixels
                        int[,] pixel = new int[bmp.Width, bmp.Height]; // Grayscale
                                                                       // System.Drawing.Color[,] pixels = new System.Drawing.Color[bmp.Width,bmp.Height];
                        for (int cntr = 0; cntr < bmp.Width; cntr++)
                        {
                            for (int cntr2 = 0; cntr2 < bmp.Height; cntr2++)
                            {
                                // Console.WriteLine("Counter: " + cntr + "Counter2: " + cntr2 + "     Pixel: (R): " + bmp.GetPixel(cntr,cntr2).R + " (G): " + bmp.GetPixel(cntr,cntr2).G + " (B): " + bmp.GetPixel(cntr,cntr2).B);
                                // pixels[cntr, cntr2] = bmp.GetPixel(cntr,cntr2);
                                pixel[cntr, cntr2] = (bmp.GetPixel(cntr, cntr2).R + bmp.GetPixel(cntr, cntr2).G + bmp.GetPixel(cntr, cntr2).B) / 3;
                            }
                        }


                        // Get background color by checking sides? Really bad implementation, fix later
                        int background = 255;
                        if (pixel[0, pixel.GetLength(1) / 2] == pixel[pixel.GetLength(0) - 1, pixel.GetLength(1) / 2])
                        {
                            background = pixel[0, pixel.GetLength(1) / 2];
                        }

                        // Get significant gradience?
                        // int maxGradient = 0;

                        // TODO: CHANGE PROGRAM TO USE SHADING TECHNIQUES
                        // Begin conversion; check pixels for significance by ignoring duplicate colors
                        int lastPixel = background;
                        Boolean[,] sigPix = new Boolean[pixel.GetLength(0), pixel.GetLength(1)];
                        for (int cntr2 = 0; cntr2 < pixel.GetLength(1); cntr2++) // Vertical significance checker
                        {
                            for (int cntr = 0; cntr < pixel.GetLength(0); cntr++)
                            {
                                // Use random
                                sigPix[cntr, cntr2] = false; // Default significance value
                                if (lastPixel < pixel[cntr, cntr2] + 10 || pixel[cntr, cntr2] > 175/*(pixel[cntr, cntr2] - 10 < background && background < pixel[cntr, cntr2] + 10)*/)
                                //if (pixel[cntr, cntr2] < (255 / 2) || (pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25) || (pixel[cntr, cntr2] - 5 < background && background < pixel[cntr, cntr2] + 5))
                                //if(pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25)
                                {
                                    // Ignore pixels that are duplicates, since they won't be edges in the image
                                }
                                else
                                {
                                    sigPix[cntr, cntr2] = true;
                                }
                                lastPixel = pixel[cntr, cntr2];
                            }
                        }
                        lastPixel = background;
                        for (int cntr = 0; cntr < pixel.GetLength(0); cntr++) // Horizontal significance checker
                        {
                            for (int cntr2 = 0; cntr2 < pixel.GetLength(1); cntr2++)
                            {
                                // Use random
                                //sigPix[cntr, cntr2] = false; // Default significance value
                                if (lastPixel < pixel[cntr, cntr2] + 10 || pixel[cntr, cntr2] > 175/*(pixel[cntr, cntr2] - 10 < background && background < pixel[cntr, cntr2] + 10)*/)
                                //if (pixel[cntr, cntr2] < (255 / 2) || (pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25) || (pixel[cntr, cntr2] - 5 < background && background < pixel[cntr, cntr2] + 5))
                                //if(pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25)
                                {
                                    // Ignore pixels that are duplicates, since they won't be edges in the image
                                }
                                else
                                {
                                    sigPix[cntr, cntr2] = true;
                                }
                                lastPixel = pixel[cntr, cntr2];
                            }
                        }
                        lastPixel = background;
                        for (int cntr2 = pixel.GetLength(1) - 1; cntr2 >= 0; cntr2--) // Vertical Inverse significance checker
                        {
                            for (int cntr = pixel.GetLength(0) - 1; cntr >= 0; cntr--)
                            {
                                // Use random
                                //sigPix[cntr, cntr2] = false; // Default significance value
                                if (lastPixel < pixel[cntr, cntr2] + 10 || pixel[cntr, cntr2] > 175/*(pixel[cntr, cntr2] - 10 < background && background < pixel[cntr, cntr2] + 10)*/)
                                //if (pixel[cntr, cntr2] < (255 / 2) || (pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25) || (pixel[cntr, cntr2] - 5 < background && background < pixel[cntr, cntr2] + 5))
                                //if(pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25)
                                {
                                    // Ignore pixels that are duplicates, since they won't be edges in the image
                                }
                                else
                                {
                                    sigPix[cntr, cntr2] = true;
                                }
                                lastPixel = pixel[cntr, cntr2];
                            }
                        }
                        lastPixel = background;
                        for (int cntr = pixel.GetLength(0) - 1; cntr >= 0; cntr--) // Horizontal Inverse significance checker
                        {
                            for (int cntr2 = pixel.GetLength(1) - 1; cntr2 >= 0; cntr2--)
                            {
                                // Use random
                                //sigPix[cntr, cntr2] = false; // Default significance value
                                if (lastPixel < pixel[cntr, cntr2] + 10 || pixel[cntr, cntr2] > 175/*(pixel[cntr, cntr2] - 10 < background && background < pixel[cntr, cntr2] + 10)*/)
                                //if (pixel[cntr, cntr2] < (255 / 2) || (pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25) || (pixel[cntr, cntr2] - 5 < background && background < pixel[cntr, cntr2] + 5))
                                //if(pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25)
                                {
                                    // Ignore pixels that are duplicates, since they won't be edges in the image
                                }
                                else
                                {
                                    sigPix[cntr, cntr2] = true;
                                }
                                lastPixel = pixel[cntr, cntr2];
                            }
                        }

                        /*// Braille set
                        char[] braille = new char[256];
                        for (int cntr = 10240; cntr < 10496; cntr++)
                        {
                            braille[cntr - 10240] = (char)cntr;
                        }*/

                        // With sixPix as the material and unicode braille as the medium, construct a textual representation of the image
                        string output = ToBraille(sigPix);

                        // Final output
                        if (output.Length > 2000)
                        {
                            Console.WriteLine("Image too large; size: " + output.Length);
                            output = output.Substring(0, 1999);
                        }
                        if (e.UserState == null)
                        {
                            await ReplyAsync("Conversion complete, your image is on the way " + Context.User.Mention + "!");

                            // Should we DM them the image text since it would be spam-like otherwise?
                            IDMChannel ch = await Context.User.GetOrCreateDMChannelAsync();
                            await ch.SendMessageAsync(output);
                        }
                        else
                        {
                            await Task.Delay(1500);
                            await ReplyAsync(output);
                            await CountdownAsync(15);
                        }
                    }
                }
            }catch(Exception err)
            {
                await ReplyAsync("There was a problem converting that image, sorry!");
                Console.WriteLine(err);
            }
        }
        #endregion

        #region Timer
        // Countdown Timer
        private async Task CountdownAsync(int time)
        {
            Timer timer = new Timer(5000);

            // The message that needs to be edited
            await Context.Channel.TriggerTypingAsync();
            await Task.Delay(1500);
            var Message = await Context.Channel.SendMessageAsync("Time remaining: " + time + " seconds");

            timer.Start();
            timer.Elapsed += (sender, e) => ElapsedEventHandler(sender, e, Message);
        }

        // Event handler for elapsed time (Who's That Pokemon?)
        // Can be modified to call whichever command needs a timer via Message parameter if needed
        private async void ElapsedEventHandler(object sender, ElapsedEventArgs e, RestUserMessage Message)
        {
            Timer timer = (Timer)sender;
            string time = Message.Content.Substring(16, 2);
            if (time == "10" || time == "15" || time == "20")
            {
                //timer.Interval = 5000;
                await Message.ModifyAsync(msg2 => msg2.Content = "Time remaining: " + (Convert.ToInt32(Message.Content.Substring(16, 2)) - 5) + " seconds");
            }
            else if (Message.Content.Substring(16, 1) == "5")
            {
                timer.Stop();
                await Message.ModifyAsync(msg2 => msg2.Content = "Time\'s up!");
                await ReplyAsync("It\'s " + pokemonName.Substring(0, 1).ToUpper() + pokemonName.Substring(1, pokemonName.Length - 1) + "!");
                ISocketMessageChannel chan = Context.Channel;
                IAsyncEnumerable<IReadOnlyCollection<IMessage>> guessesEnum = chan.GetMessagesAsync(50);
                IReadOnlyCollection<IMessage> messages = await guessesEnum.ElementAt(1);
                Boolean originReached = false;
                Boolean guessedIt = false;
                for (int x = messages.Count - 1; x > 0; x--)
                {
                    if (messages.ElementAt(x) == null)
                    {
                        continue;
                    }
                    if (messages.ElementAt(x).Id == Message.Id)
                    {
                        originReached = true;
                    }
                    else if (originReached && !messages.ElementAt(x).Author.IsBot && messages.ElementAt(x).Content.ToLower().Contains(pokemonName))
                    {
                        await Context.Channel.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Congratulations, you got it right first " + messages.ElementAt(x).Author.Mention + "!");
                        guessedIt = true;
                        break;
                    }
                }
                if (!guessedIt)
                {
                    await Context.Channel.TriggerTypingAsync();
                    await Task.Delay(1500);
                    await ReplyAsync("Nobody guessed it! Better luck next time ~");
                }
            }
        }
        #endregion

        #region ResizeImage
        /* ResizeImage
         * Code is not original for this helper method
         */
        private Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }
        #endregion

        #region Merge

        public async Task<Bitmap> MergeAsync(List<Bitmap> cards)
        {
            int tempWidth = cards[0].Width * cards.Count;
            Bitmap bmp2 = new Bitmap(tempWidth, cards[0].Height);
            
            for(int x = 0; x < cards.Count; x++)
            {
                // Fills the new width space with the next card
                for (int z = cards[0].Width * x; z < cards[0].Width * (x + 1); z++)
                {
                    for(int y = 0; y < cards[0].Height; y++)
                    {
                        bmp2.SetPixel(z, y, cards[x].GetPixel(z - (cards[0].Width * x), y));
                    }
                }
            }
            return bmp2;
        }

        #endregion

        #region ToBraille
        /* ToBraille
         * Credit to Nia for this huge function!
         * There might be an easier way to handle this using binary numbers.
         */
        private string ToBraille(Boolean[,] sigPix)
        {
            string output = "";
            int boundsX = 128;
            int boundsY = 128;
            // These bounds will ensure that the last pixels on the image will be rendered properly by the braille
            while (boundsX % 8 != 0) { boundsX++; }
            while (boundsY % 8 != 0) { boundsY++; }
            for (int cntr2 = 4; cntr2 < boundsY; cntr2 += 4) // X Value, increments by two
            {
                for (int cntr = 2; cntr < boundsX; cntr += 2) // Y Value, increments by 8
                {
                    // ⠀⠁⠂⠃⠄⠅⠆⠇⠈⠉⠊⠋⠌⠍⠎⠏⠐⠑⠒⠓⠔⠕⠖⠗⠘⠙⠚⠛⠜⠝⠞⠟⠠⠡⠢⠣⠤⠥⠦⠧⠨⠩⠪⠫⠬⠭⠮⠯⠰⠱⠲⠳⠴⠵⠶⠷⠸⠹⠺⠻⠼⠽⠾⠿⡀⡁⡂⡃⡄⡅⡆⡇⡈⡉⡊⡋⡌⡍⡎⡏⡐⡑⡒⡓⡔⡕⡖⡗⡘⡙⡚⡛⡜⡝⡞⡟⡠⡡⡢⡣⡤⡥⡦⡧⡨⡩⡪⡫⡬⡭⡮⡯⡰⡱⡲⡳⡴⡵⡶⡷⡸⡹⡺⡻⡼⡽⡾⡿⢀⢁⢂⢃⢄⢅⢆⢇⢈⢉⢊⢋⢌⢍⢎⢏⢐⢑⢒⢓⢔⢕⢖⢗⢘⢙⢚⢛⢜⢝⢞⢟⢠⢡⢢⢣⢤⢥⢦⢧⢨⢩⢪⢫⢬⢭⢮⢯⢰⢱⢲⢳⢴⢵⢶⢷⢸⢹⢺⢻⢼⢽⢾⢿⣀⣁⣂⣃⣄⣅⣆⣇⣈⣉⣊⣋⣌⣍⣎⣏⣐⣑⣒⣓⣔⣕⣖⣗⣘⣙⣚⣛⣜⣝⣞⣟⣠⣡⣢⣣⣤⣥⣦⣧⣨⣩⣪⣫⣬⣭⣮⣯⣰⣱⣲⣳⣴⣵⣶⣷⣸⣹⣺⣻⣼⣽⣾⣿
                    if (sigPix[cntr, cntr2]) // ⢀⢁⢂⢃⢄⢅⢆⢇⢈⢉⢊⢋⢌⢍⢎⢏⢐⢑⢒⢓⢔⢕⢖⢗⢘⢙⢚⢛⢜⢝⢞⢟⢠⢡⢢⢣⢤⢥⢦⢧⢨⢩⢪⢫⢬⢭⢮⢯⢰⢱⢲⢳⢴⢵⢶⢷⢸⢹⢺⢻⢼⢽⢾⢿⣀⣁⣂⣃⣄⣅⣆⣇⣈⣉⣊⣋⣌⣍⣎⣏⣐⣑⣒⣓⣔⣕⣖⣗⣘⣙⣚⣛⣜⣝⣞⣟⣠⣡⣢⣣⣤⣥⣦⣧⣨⣩⣪⣫⣬⣭⣮⣯⣰⣱⣲⣳⣴⣵⣶⣷⣸⣹⣺⣻⣼⣽⣾⣿
                    {
                        if (sigPix[cntr - 1, cntr2]) // ⣀⣁⣂⣃⣄⣅⣆⣇⣈⣉⣊⣋⣌⣍⣎⣏⣐⣑⣒⣓⣔⣕⣖⣗⣘⣙⣚⣛⣜⣝⣞⣟⣠⣡⣢⣣⣤⣥⣦⣧⣨⣩⣪⣫⣬⣭⣮⣯⣰⣱⣲⣳⣴⣵⣶⣷⣸⣹⣺⣻⣼⣽⣾⣿
                        {
                            if (sigPix[cntr, cntr2 - 1]) // ⣠⣡⣢⣣⣤⣥⣦⣧⣨⣩⣪⣫⣬⣭⣮⣯⣰⣱⣲⣳⣴⣵⣶⣷⣸⣹⣺⣻⣼⣽⣾⣿
                            {
                                if (sigPix[cntr - 1, cntr2 - 1]) // ⣤⣥⣦⣧⣬⣭⣮⣯⣴⣵⣶⣷⣼⣽⣾⣿
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⣴⣵⣶⣷⣼⣽⣾⣿
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣶⣷⣾⣿
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣾⣿
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣿
                                                {
                                                    output += '⣿';
                                                }
                                                else // ⣾
                                                {
                                                    output += '⣾';
                                                }
                                            }
                                            else // ⣶⣷
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣷
                                                {
                                                    output += '⣷';
                                                }
                                                else // ⣶
                                                {
                                                    output += '⣶';
                                                }
                                            }
                                        }
                                        else // ⣴⣵⣼⣽
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣼⣽
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣽
                                                {
                                                    output += '⣽';
                                                }
                                                else // ⣼
                                                {
                                                    output += '⣼';
                                                }
                                            }
                                            else // ⣴⣵
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣵
                                                {
                                                    output += '⣵';
                                                }
                                                else // ⣴
                                                {
                                                    output += '⣴';
                                                }
                                            }
                                        }
                                    }
                                    else // ⣤⣥⣦⣧⣬⣭⣮⣯
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣦⣧⣮⣯
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣮⣯
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣯
                                                {
                                                    output += '⣯';
                                                }
                                                else // ⣮
                                                {
                                                    output += '⣮';
                                                }
                                            }
                                            else // ⣦⣧
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣧
                                                {
                                                    output += '⣧';
                                                }
                                                else // ⣦
                                                {
                                                    output += '⣦';
                                                }
                                            }
                                        }
                                        else // ⣤⣥⣬⣭
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣬⣭
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣭
                                                {
                                                    output += '⣭';
                                                }
                                                else // ⣬
                                                {
                                                    output += '⣬';
                                                }
                                            }
                                            else // ⣤⣥
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣥
                                                {
                                                    output += '⣥';
                                                }
                                                else // ⣤
                                                {
                                                    output += '⣤';
                                                }
                                            }
                                        }
                                    }
                                }
                                else // ⣠⣡⣢⣣⣨⣩⣪⣫⣰⣱⣲⣳⣸⣹⣺⣻
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⣰⣱⣲⣳⣸⣹⣺⣻
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣲⣳⣺⣻
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣺⣻
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣻
                                                {
                                                    output += '⣻';
                                                }
                                                else // ⣺
                                                {
                                                    output += '⣺';
                                                }
                                            }
                                            else // ⣲⣳
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣳
                                                {
                                                    output += '⣳';
                                                }
                                                else // ⣲
                                                {
                                                    output += '⣲';
                                                }
                                            }
                                        }
                                        else // ⣰⣱⣸⣹
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣸⣹
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣹
                                                {
                                                    output += '⣹';
                                                }
                                                else // ⣸
                                                {
                                                    output += '⣸';
                                                }
                                            }
                                            else // ⣰⣱
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣱
                                                {
                                                    output += '⣱';
                                                }
                                                else // ⣰
                                                {
                                                    output += '⣰';
                                                }
                                            }
                                        }
                                    }
                                    else // ⣠⣡⣢⣣⣨⣩⣪⣫
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣢⣣⣪⣫
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣪⣫
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣫
                                                {
                                                    output += '⣫';
                                                }
                                                else // ⣪
                                                {
                                                    output += '⣪';
                                                }
                                            }
                                            else // ⣢⣣
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣣
                                                {
                                                    output += '⣣';
                                                }
                                                else // ⣢
                                                {
                                                    output += '⣢';
                                                }
                                            }
                                        }
                                        else // ⣠⣡⣨⣩
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣨⣩
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣩
                                                {
                                                    output += '⣩';
                                                }
                                                else // ⣨
                                                {
                                                    output += '⣨';
                                                }
                                            }
                                            else // ⣠⣡
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣡
                                                {
                                                    output += '⣡';
                                                }
                                                else // ⣠
                                                {
                                                    output += '⣠';
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else // ⣀⣁⣂⣃⣄⣅⣆⣇⣈⣉⣊⣋⣌⣍⣎⣏⣐⣑⣒⣓⣔⣕⣖⣗⣘⣙⣚⣛⣜⣝⣞⣟
                            {
                                if (sigPix[cntr - 1, cntr2 - 1]) // ⣄⣅⣆⣇⣌⣍⣎⣏⣔⣕⣖⣗⣜⣝⣞⣟
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⣔⣕⣖⣗⣜⣝⣞⣟
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣖⣗⣞⣟
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣞⣟
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣟
                                                {
                                                    output += '⣟';
                                                }
                                                else // ⣞
                                                {
                                                    output += '⣞';
                                                }
                                            }
                                            else // ⣖⣗
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣗
                                                {
                                                    output += '⣗';
                                                }
                                                else // ⣖
                                                {
                                                    output += '⣖';
                                                }
                                            }
                                        }
                                        else // ⣔⣕⣜⣝
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣜⣝
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣝
                                                {
                                                    output += '⣝';
                                                }
                                                else // ⣜
                                                {
                                                    output += '⣜';
                                                }
                                            }
                                            else // ⣔⣕
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣕
                                                {
                                                    output += '⣕';
                                                }
                                                else // ⣔
                                                {
                                                    output += '⣔';
                                                }
                                            }
                                        }
                                    }
                                    else // ⣄⣅⣆⣇⣌⣍⣎⣏
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣆⣇⣎⣏
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣎⣏
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣏
                                                {
                                                    output += '⣏';
                                                }
                                                else // ⣎
                                                {
                                                    output += '⣎';
                                                }
                                            }
                                            else // ⣆⣇
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣇
                                                {
                                                    output += '⣇';
                                                }
                                                else // ⣆
                                                {
                                                    output += '⣆';
                                                }
                                            }
                                        }
                                        else // ⣄⣅⣌⣍
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣌⣍
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣍
                                                {
                                                    output += '⣍';
                                                }
                                                else // ⣌
                                                {
                                                    output += '⣌';
                                                }
                                            }
                                            else // ⣄⣅
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣅
                                                {
                                                    output += '⣅';
                                                }
                                                else // ⣄
                                                {
                                                    output += '⣄';
                                                }
                                            }
                                        }
                                    }
                                }
                                else // ⣀⣁⣂⣃⣈⣉⣊⣋⣐⣑⣒⣓⣘⣙⣚⣛
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⣐⣑⣒⣓⣘⣙⣚⣛
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣒⣓⣚⣛
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣚⣛
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣛
                                                {
                                                    output += '⣛';
                                                }
                                                else // ⣚
                                                {
                                                    output += '⣚';
                                                }
                                            }
                                            else // ⣒⣓
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣓
                                                {
                                                    output += '⣓';
                                                }
                                                else // ⣒
                                                {
                                                    output += '⣒';
                                                }
                                            }
                                        }
                                        else // ⣐⣑⣘⣙
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣘⣙
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣙
                                                {
                                                    output += '⣙';
                                                }
                                                else // ⣘
                                                {
                                                    output += '⣘';
                                                }
                                            }
                                            else // ⣐⣑
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣑
                                                {
                                                    output += '⣑';
                                                }
                                                else // ⣐
                                                {
                                                    output += '⣐';
                                                }
                                            }
                                        }
                                    }
                                    else // ⣀⣁⣂⣃⣈⣉⣊⣋
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⣂⣃⣊⣋
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣊⣋
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣋
                                                {
                                                    output += '⣋';
                                                }
                                                else // ⣊
                                                {
                                                    output += '⣊';
                                                }
                                            }
                                            else // ⣂⣃
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣃
                                                {
                                                    output += '⣃';
                                                }
                                                else // ⣂
                                                {
                                                    output += '⣂';
                                                }
                                            }
                                        }
                                        else // ⣀⣁⣈⣉
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⣈⣉
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⣉
                                                {
                                                    output += '⣉';
                                                }
                                                else // ⣈
                                                {
                                                    output += '⣈';
                                                }
                                            }
                                            else // ⣀⣁
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) //⣁ 
                                                {
                                                    output += '⣁';
                                                }
                                                else // ⣀
                                                {
                                                    output += '⣀';
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else // ⢀⢁⢂⢃⢄⢅⢆⢇⢈⢉⢊⢋⢌⢍⢎⢏⢐⢑⢒⢓⢔⢕⢖⢗⢘⢙⢚⢛⢜⢝⢞⢟⢠⢡⢢⢣⢤⢥⢦⢧⢨⢩⢪⢫⢬⢭⢮⢯⢰⢱⢲⢳⢴⢵⢶⢷⢸⢹⢺⢻⢼⢽⢾⢿
                        {
                            if (sigPix[cntr, cntr2 - 1]) // ⢠⢡⢢⢣⢤⢥⢦⢧⢨⢩⢪⢫⢬⢭⢮⢯⢰⢱⢲⢳⢴⢵⢶⢷⢸⢹⢺⢻⢼⢽⢾⢿
                            {
                                if (sigPix[cntr - 1, cntr2 - 1]) // ⢤⢥⢦⢧⢬⢭⢮⢯⢴⢵⢶⢷⢼⢽⢾⢿
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⢴⢵⢶⢷⢼⢽⢾⢿
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢶⢷⢾⢿
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢾⢿
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢿
                                                {
                                                    output += '⢿';
                                                }
                                                else // ⢾
                                                {
                                                    output += '⢾';
                                                }
                                            }
                                            else // ⢶⢷
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢷
                                                {
                                                    output += '⢷';
                                                }
                                                else // ⢶
                                                {
                                                    output += '⢶';
                                                }
                                            }
                                        }
                                        else // ⢴⢵⢼⢽
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢼⢽
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢽
                                                {
                                                    output += '⢽';
                                                }
                                                else // ⢼
                                                {
                                                    output += '⢼';
                                                }
                                            }
                                            else // ⢴⢵
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢵
                                                {
                                                    output += '⢵';
                                                }
                                                else // ⢴
                                                {
                                                    output += '⢴';
                                                }
                                            }
                                        }
                                    }
                                    else // ⢤⢥⢦⢧⢬⢭⢮⢯
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢦⢧⢮⢯
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢮⢯
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢯
                                                {
                                                    output += '⢯';
                                                }
                                                else // ⢮
                                                {
                                                    output += '⢮';
                                                }
                                            }
                                            else // ⢦⢧
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢧
                                                {
                                                    output += '⢧';
                                                }
                                                else // ⢦
                                                {
                                                    output += '⢦';
                                                }
                                            }
                                        }
                                        else // ⢤⢥⢬⢭
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢬⢭
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢭
                                                {
                                                    output += '⢭';
                                                }
                                                else // ⢬
                                                {
                                                    output += '⢬';
                                                }
                                            }
                                            else // ⢤⢥
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢥
                                                {
                                                    output += '⢥';
                                                }
                                                else // ⢤
                                                {
                                                    output += '⢤';
                                                }
                                            }
                                        }
                                    }
                                }
                                else // ⢠⢡⢢⢣⢨⢩⢪⢫⢰⢱⢲⢳⢸⢹⢺⢻
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⢰⢱⢲⢳⢸⢹⢺⢻
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢲⢳⢺⢻
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢺⢻
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢻
                                                {
                                                    output += '⢻';
                                                }
                                                else
                                                {
                                                    output += '⢺';
                                                }
                                            }
                                            else // ⢲⢳
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢳
                                                {
                                                    output += '⢳';
                                                }
                                                else // ⢲
                                                {
                                                    output += '⢲';
                                                }
                                            }
                                        }
                                        else // ⢰⢱⢸⢹
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢸⢹
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢹
                                                {
                                                    output += '⢹';
                                                }
                                                else // ⢸
                                                {
                                                    output += '⢸';
                                                }
                                            }
                                            else // ⢰⢱
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢱
                                                {
                                                    output += '⢱';
                                                }
                                                else // ⢰
                                                {
                                                    output += '⢰';
                                                }
                                            }
                                        }
                                    }
                                    else // ⢠⢡⢢⢣⢨⢩⢪⢫
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢢⢣⢪⢫
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢪⢫
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢫
                                                {
                                                    output += '⢫';
                                                }
                                                else // ⢪
                                                {
                                                    output += '⢪';
                                                }
                                            }
                                            else // ⢢⢣
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢣
                                                {
                                                    output += '⢣';
                                                }
                                                else // ⢢
                                                {
                                                    output += '⢢';
                                                }
                                            }
                                        }
                                        else // ⢠⢡⢨⢩
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢨⢩
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢩
                                                {
                                                    output += '⢩';
                                                }
                                                else // ⢨
                                                {
                                                    output += '⢨';
                                                }
                                            }
                                            else // ⢠⢡
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢡
                                                {
                                                    output += '⢡';
                                                }
                                                else // ⢠
                                                {
                                                    output += '⢠';
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else // ⢀⢁⢂⢃⢄⢅⢆⢇⢈⢉⢊⢋⢌⢍⢎⢏⢐⢑⢒⢓⢔⢕⢖⢗⢘⢙⢚⢛⢜⢝⢞⢟
                            {
                                if (sigPix[cntr - 1, cntr2 - 1]) // ⢄⢅⢆⢇⢌⢍⢎⢏⢔⢕⢖⢗⢜⢝⢞⢟
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⢔⢕⢖⢗⢜⢝⢞⢟
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢖⢗⢞⢟
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢞⢟
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢟
                                                {
                                                    output += '⢟';
                                                }
                                                else // ⢞
                                                {
                                                    output += '⢞';
                                                }
                                            }
                                            else // ⢖⢗
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢗
                                                {
                                                    output += '⢗';
                                                }
                                                else // ⢖
                                                {
                                                    output += '⢖';
                                                }
                                            }
                                        }
                                        else // ⢔⢕⢜⢝
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢜⢝
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢝
                                                {
                                                    output += '⢝';
                                                }
                                                else // ⢜
                                                {
                                                    output += '⢜';
                                                }
                                            }
                                            else // ⢔⢕
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢕
                                                {
                                                    output += '⢕';
                                                }
                                                else // ⢔
                                                {
                                                    output += '⢔';
                                                }
                                            }
                                        }
                                    }
                                    else // ⢄⢅⢆⢇⢌⢍⢎⢏
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢆⢇⢎⢏
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢎⢏
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢏
                                                {
                                                    output += '⢏';
                                                }
                                                else // ⢎
                                                {
                                                    output += '⢎';
                                                }
                                            }
                                            else // ⢆⢇
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢇
                                                {
                                                    output += '⢇';
                                                }
                                                else // ⢆
                                                {
                                                    output += '⢆';
                                                }
                                            }
                                        }
                                        else // ⢄⢅⢌⢍
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢌⢍
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢍
                                                {
                                                    output += '⢍';
                                                }
                                                else // ⢌
                                                {
                                                    output += '⢌';
                                                }
                                            }
                                            else // ⢄⢅
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢅
                                                {
                                                    output += '⢅';
                                                }
                                                else // ⢄
                                                {
                                                    output += '⢄';
                                                }
                                            }
                                        }
                                    }
                                }
                                else // ⢀⢁⢂⢃⢈⢉⢊⢋⢐⢑⢒⢓⢘⢙⢚⢛
                                {
                                    if (sigPix[cntr, cntr2 - 2]) // ⢐⢑⢒⢓⢘⢙⢚⢛
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢒⢓⢚⢛
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢚⢛
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢛
                                                {
                                                    output += '⢛';
                                                }
                                                else // ⢚
                                                {
                                                    output += '⢚';
                                                }
                                            }
                                            else // ⢒⢓
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢓
                                                {
                                                    output += '⢓';
                                                }
                                                else // ⢒
                                                {
                                                    output += '⢒';
                                                }
                                            }
                                        }
                                        else // ⢐⢑⢘⢙
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢘⢙
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢙
                                                {
                                                    output += '⢙';
                                                }
                                                else // ⢘
                                                {
                                                    output += '⢘';
                                                }
                                            }
                                            else // ⢐⢑
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢑
                                                {
                                                    output += '⢑';
                                                }
                                                else // ⢐
                                                {
                                                    output += '⢐';
                                                }
                                            }
                                        }
                                    }
                                    else // ⢀⢁⢂⢃⢈⢉⢊⢋
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 2]) // ⢂⢃⢊⢋
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢊⢋
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢋
                                                {
                                                    output += '⢋';
                                                }
                                                else // ⢊
                                                {
                                                    output += '⢊';
                                                }
                                            }
                                            else // ⢂⢃
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢃
                                                {
                                                    output += '⢃';
                                                }
                                                else // ⢂
                                                {
                                                    output += '⢂';
                                                }
                                            }
                                        }
                                        else // ⢀⢁⢈⢉
                                        {
                                            if (sigPix[cntr, cntr2 - 3]) // ⢈⢉
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢉
                                                {
                                                    output += '⢉';
                                                }
                                                else // ⢈
                                                {
                                                    output += '⢈';
                                                }
                                            }
                                            else // ⢀⢁
                                            {
                                                if (sigPix[cntr - 1, cntr2 - 3]) // ⢁
                                                {
                                                    output += '⢁';
                                                }
                                                else // ⢀
                                                {
                                                    output += '⢀';
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (sigPix[cntr - 1, cntr2]) // ⡀⡁⡂⡃⡄⡅⡆⡇⡈⡉⡊⡋⡌⡍⡎⡏⡐⡑⡒⡓⡔⡕⡖⡗⡘⡙⡚⡛⡜⡝⡞⡟⡠⡡⡢⡣⡤⡥⡦⡧⡨⡩⡪⡫⡬⡭⡮⡯⡰⡱⡲⡳⡴⡵⡶⡷⡸⡹⡺⡻⡼⡽⡾⡿
                    {
                        if (sigPix[cntr, cntr2 - 1]) // ⡠⡡⡢⡣⡤⡥⡦⡧⡨⡩⡪⡫⡬⡭⡮⡯⡰⡱⡲⡳⡴⡵⡶⡷⡸⡹⡺⡻⡼⡽⡾⡿
                        {
                            if (sigPix[cntr - 1, cntr2 - 1]) // ⡤⡥⡦⡧⡬⡭⡮⡯⡴⡵⡶⡷⡼⡽⡾⡿
                            {
                                if (sigPix[cntr, cntr2 - 2]) // ⡴⡵⡶⡷⡼⡽⡾⡿
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡶⡷⡾⡿
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡾⡿
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡿
                                            {
                                                output += '⡿';
                                            }
                                            else // ⡾
                                            {
                                                output += '⡾';
                                            }
                                        }
                                        else // ⡶⡷
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡷
                                            {
                                                output += '⡷';
                                            }
                                            else // ⡶
                                            {
                                                output += '⡶';
                                            }
                                        }
                                    }
                                    else // ⡴⡵⡼⡽
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡼⡽
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡽
                                            {
                                                output += '⡽';
                                            }
                                            else // ⡼
                                            {
                                                output += '⡼';
                                            }
                                        }
                                        else // ⡴⡵
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡵
                                            {
                                                output += '⡵';
                                            }
                                            else // ⡴
                                            {
                                                output += '⡴';
                                            }
                                        }
                                    }
                                }
                                else // ⡤⡥⡦⡧⡬⡭⡮⡯
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡦⡧⡮⡯
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡮⡯
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡯
                                            {
                                                output += '⡯';
                                            }
                                            else // ⡮
                                            {
                                                output += '⡮';
                                            }
                                        }
                                        else // ⡦⡧
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡧
                                            {
                                                output += '⡧';
                                            }
                                            else // ⡦
                                            {
                                                output += '⡦';
                                            }
                                        }
                                    }
                                    else // ⡤⡥⡬⡭
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡬⡭
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡭
                                            {
                                                output += '⡭';
                                            }
                                            else // ⡬
                                            {
                                                output += '⡬';
                                            }
                                        }
                                        else // ⡤⡥
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡥
                                            {
                                                output += '⡥';
                                            }
                                            else // ⡤
                                            {
                                                output += '⡤';
                                            }
                                        }
                                    }
                                }
                            }
                            else // ⡠⡡⡢⡣⡨⡩⡪⡫⡰⡱⡲⡳⡸⡹⡺⡻
                            {
                                if (sigPix[cntr, cntr2 - 2]) // ⡰⡱⡲⡳⡸⡹⡺⡻
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡲⡳⡺⡻
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡺⡻
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡻
                                            {
                                                output += '⡻';
                                            }
                                            else // ⡺
                                            {
                                                output += '⡺';
                                            }
                                        }
                                        else // ⡲⡳
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡳
                                            {
                                                output += '⡳';
                                            }
                                            else // ⡲
                                            {
                                                output += '⡲';
                                            }
                                        }
                                    }
                                    else // ⡰⡱⡸⡹
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡸⡹
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡹
                                            {
                                                output += '⡹';
                                            }
                                            else // ⡸
                                            {
                                                output += '⡸';
                                            }
                                        }
                                        else // ⡰⡱
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡱
                                            {
                                                output += '⡱';
                                            }
                                            else // ⡰
                                            {
                                                output += '⡰';
                                            }
                                        }
                                    }
                                }
                                else // ⡠⡡⡢⡣⡨⡩⡪⡫
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡢⡣⡪⡫
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡪⡫
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡫
                                            {
                                                output += '⡫';
                                            }
                                            else // ⡪
                                            {
                                                output += '⡪';
                                            }
                                        }
                                        else // ⡢⡣
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡣
                                            {
                                                output += '⡣';
                                            }
                                            else // ⡢
                                            {
                                                output += '⡢';
                                            }
                                        }
                                    }
                                    else // ⡠⡡⡨⡩
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡨⡩
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡩
                                            {
                                                output += '⡩';
                                            }
                                            else // ⡨
                                            {
                                                output += '⡨';
                                            }
                                        }
                                        else // ⡠⡡
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡡
                                            {
                                                output += '⡡';
                                            }
                                            else // ⡠
                                            {
                                                output += '⡠';
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else // ⡀⡁⡂⡃⡄⡅⡆⡇⡈⡉⡊⡋⡌⡍⡎⡏⡐⡑⡒⡓⡔⡕⡖⡗⡘⡙⡚⡛⡜⡝⡞⡟
                        {
                            if (sigPix[cntr - 1, cntr2 - 1]) // ⡄⡅⡆⡇⡌⡍⡎⡏⡔⡕⡖⡗⡜⡝⡞⡟
                            {
                                if (sigPix[cntr, cntr2 - 2]) // ⡔⡕⡖⡗⡜⡝⡞⡟
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡖⡗⡞⡟
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡞⡟
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡟
                                            {
                                                output += '⡟';
                                            }
                                            else // ⡞
                                            {
                                                output += '⡞';
                                            }
                                        }
                                        else // ⡖⡗
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡗
                                            {
                                                output += '⡗';
                                            }
                                            else // ⡖
                                            {
                                                output += '⡖';
                                            }
                                        }
                                    }
                                    else // ⡔⡕⡜⡝
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡜⡝
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡝
                                            {
                                                output += '⡝';
                                            }
                                            else // ⡜
                                            {
                                                output += '⡜';
                                            }
                                        }
                                        else // ⡔⡕
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡕
                                            {
                                                output += '⡕';
                                            }
                                            else // ⡔
                                            {
                                                output += '⡔';
                                            }
                                        }
                                    }
                                }
                                else // ⡄⡅⡆⡇⡌⡍⡎⡏
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡆⡇⡎⡏
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡎⡏
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡏
                                            {
                                                output += '⡏';
                                            }
                                            else // ⡎
                                            {
                                                output += '⡎';
                                            }
                                        }
                                        else // ⡆⡇
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡇
                                            {
                                                output += '⡇';
                                            }
                                            else // ⡆
                                            {
                                                output += '⡆';
                                            }
                                        }
                                    }
                                    else // ⡄⡅⡌⡍
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡌⡍
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡍
                                            {
                                                output += '⡍';
                                            }
                                            else // ⡌
                                            {
                                                output += '⡌';
                                            }
                                        }
                                        else // ⡄⡅
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡅
                                            {
                                                output += '⡅';
                                            }
                                            else // ⡄
                                            {
                                                output += '⡄';
                                            }
                                        }
                                    }
                                }
                            }
                            else // ⡀⡁⡂⡃⡈⡉⡊⡋⡐⡑⡒⡓⡘⡙⡚⡛
                            {
                                if (sigPix[cntr, cntr2 - 2]) // ⡐⡑⡒⡓⡘⡙⡚⡛
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡒⡓⡚⡛
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡚⡛
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡛
                                            {
                                                output += '⡛';
                                            }
                                            else // ⡚
                                            {
                                                output += '⡚';
                                            }
                                        }
                                        else // ⡒⡓
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡓
                                            {
                                                output += '⡓';
                                            }
                                            else // ⡒
                                            {
                                                output += '⡒';
                                            }
                                        }
                                    }
                                    else // ⡐⡑⡘⡙
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡘⡙
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡙
                                            {
                                                output += '⡙';
                                            }
                                            else // ⡘
                                            {
                                                output += '⡘';
                                            }
                                        }
                                        else // ⡐⡑
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡑
                                            {
                                                output += '⡑';
                                            }
                                            else // ⡐
                                            {
                                                output += '⡐';
                                            }
                                        }
                                    }
                                }
                                else // ⡀⡁⡂⡃⡈⡉⡊⡋
                                {
                                    if (sigPix[cntr - 1, cntr2 - 2]) // ⡂⡃⡊⡋
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡊⡋
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡋
                                            {
                                                output += '⡋';
                                            }
                                            else // ⡊
                                            {
                                                output += '⡊';
                                            }
                                        }
                                        else // ⡂⡃
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡃
                                            {
                                                output += '⡃';
                                            }
                                            else // ⡂
                                            {
                                                output += '⡂';
                                            }
                                        }
                                    }
                                    else // ⡀⡁⡈⡉
                                    {
                                        if (sigPix[cntr, cntr2 - 3]) // ⡈⡉
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡉
                                            {
                                                output += '⡉';
                                            }
                                            else // ⡈
                                            {
                                                output += '⡈';
                                            }
                                        }
                                        else // ⡀⡁
                                        {
                                            if (sigPix[cntr - 1, cntr2 - 3]) // ⡁
                                            {
                                                output += '⡁';
                                            }
                                            else // ⡀
                                            {
                                                output += '⡀';
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } // ⠀⠁⠂⠃⠄⠅⠆⠇⠈⠉⠊⠋⠌⠍⠎⠏⠐⠑⠒⠓⠔⠕⠖⠗⠘⠙⠚⠛⠜⠝⠞⠟⠠⠡⠢⠣⠤⠥⠦⠧⠨⠩⠪⠫⠬⠭⠮⠯⠰⠱⠲⠳⠴⠵⠶⠷⠸⠹⠺⠻⠼⠽⠾⠿
                    else if (sigPix[cntr, cntr2 - 1]) // ⠠⠡⠢⠣⠤⠥⠦⠧⠨⠩⠪⠫⠬⠭⠮⠯⠰⠱⠲⠳⠴⠵⠶⠷⠸⠹⠺⠻⠼⠽⠾⠿
                    {
                        if (sigPix[cntr - 1, cntr2 - 1]) // ⠤⠥⠦⠧⠬⠭⠮⠯⠴⠵⠶⠷⠼⠽⠾⠿
                        {
                            if (sigPix[cntr, cntr2 - 2]) // ⠴⠵⠶⠷⠼⠽⠾⠿
                            {
                                if (sigPix[cntr - 1, cntr2 - 2]) // ⠶⠷⠾⠿
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠾⠿
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠿
                                        {
                                            output += '⠿';
                                        }
                                        else // ⠾
                                        {
                                            output += '⠾';
                                        }
                                    }
                                    else // ⠶⠷
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠷
                                        {
                                            output += '⠷';
                                        }
                                        else // ⠶
                                        {
                                            output += '⠶';
                                        }
                                    }
                                }
                                else // ⠴⠵⠼⠽
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠼⠽
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠽
                                        {
                                            output += '⠽';
                                        }
                                        else // ⠼
                                        {
                                            output += '⠼';
                                        }
                                    }
                                    else // ⠴⠵
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠵
                                        {
                                            output += '⠵';
                                        }
                                        else // ⠴
                                        {
                                            output += '⠴';
                                        }
                                    }
                                }
                            }
                            else // ⠤⠥⠦⠧⠬⠭⠮⠯
                            {
                                if (sigPix[cntr - 1, cntr2 - 2]) // ⠦⠧⠮⠯
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠮⠯
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠯
                                        {
                                            output += '⠯';
                                        }
                                        else // ⠮
                                        {
                                            output += '⠮';
                                        }
                                    }
                                    else // ⠦⠧
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠧
                                        {
                                            output += '⠧';
                                        }
                                        else // ⠦
                                        {
                                            output += '⠦';
                                        }
                                    }
                                }
                                else // ⠤⠥⠬⠭
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠬⠭
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠭
                                        {
                                            output += '⠭';
                                        }
                                        else // ⠬
                                        {
                                            output += '⠬';
                                        }
                                    }
                                    else // ⠤⠥
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠥
                                        {
                                            output += '⠥';
                                        }
                                        else // ⠤
                                        {
                                            output += '⠤';
                                        }
                                    }
                                }
                            }
                        }
                        else // ⠠⠡⠢⠣⠨⠩⠪⠫⠰⠱⠲⠳⠸⠹⠺⠻
                        {
                            if (sigPix[cntr, cntr2 - 2]) // ⠰⠱⠲⠳⠸⠹⠺⠻
                            {
                                if (sigPix[cntr - 1, cntr2 - 2]) // ⠲⠳⠺⠻
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠺⠻
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠻
                                        {
                                            output += '⠻';
                                        }
                                        else // ⠺
                                        {
                                            output += '⠺';
                                        }
                                    }
                                    else // ⠲⠳
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠳
                                        {
                                            output += '⠳';
                                        }
                                        else // ⠲
                                        {
                                            output += '⠲';
                                        }
                                    }
                                }
                                else // ⠰⠱⠸⠹
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠸⠹
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠹
                                        {
                                            output += '⠹';
                                        }
                                        else // ⠸
                                        {
                                            output += '⠸';
                                        }
                                    }
                                    else // ⠰⠱
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠱
                                        {
                                            output += '⠱';
                                        }
                                        else // ⠰
                                        {
                                            output += '⠰';
                                        }
                                    }
                                }
                            }
                            else // ⠠⠡⠢⠣⠨⠩⠪⠫
                            {
                                if (sigPix[cntr - 1, cntr2 - 2]) // ⠢⠣⠪⠫
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠪⠫
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠫
                                        {
                                            output += '⠫';
                                        }
                                        else // ⠪
                                        {
                                            output += '⠪';
                                        }
                                    }
                                    else // ⠢⠣
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠣
                                        {
                                            output += '⠣';
                                        }
                                        else // ⠢
                                        {
                                            output += '⠢';
                                        }
                                    }
                                }
                                else // ⠠⠡⠨⠩
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠨⠩
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠩
                                        {
                                            output += '⠩';
                                        }
                                        else // ⠨
                                        {
                                            output += '⠨';
                                        }
                                    }
                                    else // ⠠⠡
                                    {
                                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠡
                                        {
                                            output += '⠡';
                                        }
                                        else // ⠠
                                        {
                                            output += '⠠';
                                        }
                                    }
                                }
                            }
                        }
                    } // ⠀⠁⠂⠃⠄⠅⠆⠇⠈⠉⠊⠋⠌⠍⠎⠏⠐⠑⠒⠓⠔⠕⠖⠗⠘⠙⠚⠛⠜⠝⠞⠟
                    else if (sigPix[cntr - 1, cntr2 - 1]) // ⠄⠅⠆⠇⠌⠍⠎⠏⠔⠕⠖⠗⠜⠝⠞⠟
                    {
                        if (sigPix[cntr, cntr2 - 2]) // ⠔⠕⠖⠗⠜⠝⠞⠟
                        {
                            if (sigPix[cntr - 1, cntr2 - 2]) // ⠖⠗⠞⠟
                            {
                                if (sigPix[cntr, cntr2 - 3]) // ⠞⠟
                                {
                                    if (sigPix[cntr - 1, cntr2 - 3]) // ⠟
                                    {
                                        output += '⠟';
                                    }
                                    else // ⠞
                                    {
                                        output += '⠞';
                                    }
                                }
                                else // ⠖⠗
                                {
                                    if (sigPix[cntr - 1, cntr2 - 3]) // ⠗
                                    {
                                        output += '⠗';
                                    }
                                    else // ⠖
                                    {
                                        output += '⠖';
                                    }
                                }
                            }
                            else // ⠔⠕⠜⠝
                            {
                                if (sigPix[cntr, cntr2 - 3]) // ⠜⠝
                                {
                                    if (sigPix[cntr - 1, cntr2 - 3]) // ⠜
                                    {
                                        output += '⠜';
                                    }
                                    else // ⠝
                                    {
                                        output += '⠝';
                                    }
                                }
                                else // ⠔⠕
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠕
                                    {
                                        output += '⠕';
                                    }
                                    else // ⠔
                                    {
                                        output += '⠔';
                                    }
                                }
                            }
                        }
                        else // ⠄⠅⠆⠇⠌⠍⠎⠏
                        {
                            if (sigPix[cntr - 1, cntr2 - 2]) // ⠆⠇⠎⠏
                            {
                                if (sigPix[cntr, cntr2 - 3]) // ⠎⠏
                                {
                                    if (sigPix[cntr - 1, cntr2 - 3]) // ⠏
                                    {
                                        output += '⠏';
                                    }
                                    else // ⠎
                                    {
                                        output += '⠎';
                                    }
                                }
                                else // ⠆⠇
                                {
                                    if (sigPix[cntr - 1, cntr2 - 3]) // ⠇
                                    {
                                        output += '⠇';
                                    }
                                    else // ⠆
                                    {
                                        output += '⠆';
                                    }
                                }
                            }
                            else // ⠄⠅⠌⠍
                            {
                                if (sigPix[cntr, cntr2 - 3]) // ⠌⠍
                                {
                                    if (sigPix[cntr - 1, cntr2 - 3]) // ⠍
                                    {
                                        output += '⠍';
                                    }
                                    else // ⠌
                                    {
                                        output += '⠌';
                                    }
                                }
                                else // ⠄⠅
                                {
                                    if (sigPix[cntr, cntr2 - 3]) // ⠅
                                    {
                                        output += '⠅';
                                    }
                                    else // ⠄
                                    {
                                        output += '⠄';
                                    }
                                }
                            }
                        }
                    } // ⠀⠁⠂⠃⠈⠉⠊⠋⠐⠑⠒⠓⠘⠙⠚⠛
                    else if (sigPix[cntr, cntr2 - 2]) // ⠐⠑⠒⠓⠘⠙⠚⠛
                    {
                        if (sigPix[cntr - 1, cntr2 - 2]) // ⠒⠓⠚⠛
                        {
                            if (sigPix[cntr, cntr2 - 3]) // ⠚⠛
                            {
                                if (sigPix[cntr - 1, cntr2 - 3]) // ⠛
                                {
                                    output += '⠛';
                                }
                                else // ⠚
                                {
                                    output += '⠚';
                                }
                            }
                            else // ⠒⠓
                            {
                                if (sigPix[cntr - 1, cntr2 - 3]) // ⠓
                                {
                                    output += '⠓';
                                }
                                else // ⠒
                                {
                                    output += '⠒';
                                }
                            }
                        }
                        else // ⠐⠑⠘⠙
                        {
                            if (sigPix[cntr, cntr2 - 3]) // ⠘⠙
                            {
                                if (sigPix[cntr - 1, cntr2 - 3]) // ⠙
                                {
                                    output += '⠙';
                                }
                                else // ⠘
                                {
                                    output += '⠘';
                                }
                            }
                            else // ⠐⠑
                            {
                                if (sigPix[cntr - 1, cntr2 - 3]) // ⠑
                                {
                                    output += '⠑';
                                }
                                else // ⠐
                                {
                                    output += '⠐';
                                }
                            }
                        }
                    } // ⠀⠁⠂⠃⠈⠉⠊⠋
                    else if (sigPix[cntr - 1, cntr2 - 2]) // ⠂⠃⠊⠋
                    {
                        if (sigPix[cntr, cntr2 - 3]) // ⠊⠋
                        {
                            if (sigPix[cntr - 1, cntr2 - 3]) // ⠋
                            {
                                output += '⠋';
                            }
                            else // ⠃
                            {
                                output += '⠃';
                            }
                        }
                        else // ⠂
                        {
                            output += '⠂';
                        }
                    } // ⠀⠁⠈⠉
                    else if (sigPix[cntr, cntr2 - 3]) // ⠈⠉
                    {
                        if (sigPix[cntr - 1, cntr2 - 3]) // ⠉
                        {
                            output += '⠉';
                        }
                        else // ⠈
                        {
                            output += '⠈';
                        }
                    } // ⠀⠁
                    else if (sigPix[cntr - 1, cntr2 - 3]) // ⠁
                    {
                        output += '⠁';
                    } // ⠀
                    else // ⠀
                    {
                        output += '⠀';
                    }
                    if (cntr == boundsX - 2) // Right side of image needs endline character
                    {
                        output += "\n";
                    }
                }
            }
            return output;
        }
        #endregion
    }
}

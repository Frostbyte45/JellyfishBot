/** Jellyfish Commands
 * Author(s): Nia Specht, Ryan Rieger
 * Date: 11/20/2017
 * Description: Commands for the "Jellyfish" bot.
 * Usage: DO NOT USE CODE WITHOUT CREDIT TO AUTHORS. The toText command took a lot of time to write and is Nia's side-project; credit is due.
 * Version: 1.0
 * Completion date: N/A
 * TODO: Add logs for each command's usage, and also count the following variables:
 *      -"Who's That Pokemon" wins
 *      -"Help" calls (lol)
 *      -"89" calls (of course)
 *      -Times rolled an 89 with "roll"
 *      Also, add formatting to help section! (@Ryan)
 *      
 */

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Timers;
using System.Linq;

namespace Jellyfish.Modules
{
    public class JellyfishCommands : ModuleBase<SocketCommandContext>
    {
        public String pokemonName;
        #region help
        [Command("help")]
        public async Task HelpAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            IDMChannel c = await Context.User.GetOrCreateDMChannelAsync();
            string msg = "Helpu has arrived.\n```";

            // Command "help"
            msg += "help <cmd> - get command list, or get specific command usage\n";

            // Command "invite"
            msg += "invite - get a link to add the bot to your server\n";

            // Command "goodnight"
            msg += "goodnight <@user> - say goodnight properly to someone\n";

            // Command "89"
            msg += "89 - 89 made of 89's. What could be better?\n";

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
                    msg2 += "Help Usage:\n\"help <command>\" to get usage on a specific command, ";
                    msg2 += "or \"help\" to get a list of commands.\n";
                    break;

                // Invite usage
                case "invite":
                    msg2 += "Invite Usage:\n\"invite\" to get a link for adding the bot to a server. ";
                    msg2 += "Note: you must have permission to manage bots on the server!";
                    break;

                // Goodnight usage
                case "goodnight":
                    msg2 += "Goodnight Usage:\n\"goodnight <@user>\" to say goodnight to a specific user, ";
                    msg2 += "or \"goodnight\" to wish yourself goodnight.\n";
                    break;

                // 89 usage
                case "89":
                    msg2 += "89 Usage:\n\"89\" to get 89 made of 89's.";
                    break;

                // Echo usage
                case "echo":
                    msg2 += "Echo Usage:\n\"echo <text>\" to repeat text.";
                    break;
                
                // Roll usage
                case "roll":
                    msg2 += "Roll Usage:\n\"roll <range>\" to return a random number from 1 to range.";
                    break;

                // Pingu usage
                case "pingu":
                    msg2 += "Pingu Usage:\n\"pingu\" for noots.";
                    break;
                
                // Avatar usage
                case "avatar":
                    msg2 += "Avatar Usage:\n\"avatar <@user>\" to get that user's avatar.";
                    break;

                // Textify usage
                case "textify":
                    msg2 += "textify Usage:\n\"textify <@user OR link>\" to get that user's avatar or link's picture as text (use mentions).";
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

        #region invite
        [Command ("invite")]
        public async Task InviteAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            string link = "https://discordapp.com/oauth2/authorize?&client_id=380421738841767936&scope=bot&permissions=0";
            await ReplyAsync("Let\'s convert another server to our religion!: " + link);
        }
        #endregion

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

        #region ping
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

        #region echo
        [Command("echo")]
        public async Task EchoAsync([Remainder] string stuffToEcho)
        {
            await Context.Channel.TriggerTypingAsync();
            stuffToEcho = "*" + stuffToEcho + "*";

            await ReplyAsync(stuffToEcho/* + "\n" + stuffToEcho + "\n" + stuffToEcho*/);
        }
        #endregion

        #region roll
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

        #region pingu
        [Command("pingu")]
        public async Task PinguAsync()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync("https://www.youtube.com/watch?v=Fs3BHRIyF2E");
        }
        #endregion

        #region whosThat
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

        #region avatar
        public async Task AvatarAsync([Remainder] SocketUser mention)
        {
            await Context.Channel.TriggerTypingAsync();
            var picUrl = mention.GetAvatarUrl(Discord.ImageFormat.Jpeg);
            await ReplyAsync("Here's the avatar url, " + Context.User.Mention + "!: " + picUrl);
        }
        #endregion

        #region textify
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

        #region downloader
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

                        // Braille set
                        char[] braille = new char[256];
                        for (int cntr = 10240; cntr < 10496; cntr++)
                        {
                            braille[cntr - 10240] = (char)cntr;
                        }

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
                            await ReplyAsync(output);
                            await CountdownAsync(10);

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

        #region timer
        // Countdown Timer
        private async Task CountdownAsync(int time)
        {
            Timer timer = new Timer(1000);

            // The message that needs to be edited
            var Message = await Context.Channel.SendMessageAsync("Time remaining: 15 seconds");

            timer.Start();
            timer.Elapsed += (sender, e) => ElapsedEventHandler(sender, e, Message);
        }

        // Event handler for elapsed time (Who's That Pokemon?)
        private async void ElapsedEventHandler(object sender, ElapsedEventArgs e, RestUserMessage Message)
        {
            Timer timer = (Timer)sender;
            string time = Message.Content.Substring(16, 2);
            if (time == "10" || time == "11" || time == "12" || time == "13" || time == "14" || time == "15")
                await Message.ModifyAsync(msg2 => msg2.Content = "Time remaining: " + (Convert.ToInt32(Message.Content.Substring(16, 2)) - 1) + " seconds");
            else if (Message.Content.Substring(16, 1) == "0")
            {
                timer.Stop();
                await Message.ModifyAsync(msg2 => msg2.Content = "Time\'s up!");
                await ReplyAsync("It\'s " + pokemonName.Substring(0, 1).ToUpper() + pokemonName.Substring(1, pokemonName.Length - 1) + "!");
                ISocketMessageChannel chan = Context.Channel;
                IAsyncEnumerable<IReadOnlyCollection<IMessage>> guessesEnum = chan.GetMessagesAsync(50);
                IReadOnlyCollection<IMessage> messages = await guessesEnum.ElementAt(1);
                Boolean originReached = false;
                Boolean guessedIt = false;
                for(int x = messages.Count - 1; x > 0; x--)
                {
                    if(messages.ElementAt(x) == null)
                    {
                        continue;
                    }
                    if(messages.ElementAt(x).Id == Message.Id)
                    {
                        originReached = true;
                    }
                    else if (originReached && !messages.ElementAt(x).Author.IsBot && messages.ElementAt(x).Content.ToLower().Contains(pokemonName))
                    {
                        await ReplyAsync("Congratulations, you got it right first " + messages.ElementAt(x).Author.Mention + "!");
                        guessedIt = true;
                        break;
                    }
                }
                if (!guessedIt)
                {
                    await ReplyAsync("Nobody guessed it! Better luck next time ~");
                }
            }
            else
            {
                await Message.ModifyAsync(msg2 => msg2.Content = "Time remaining: " + (Convert.ToInt32(Message.Content.Substring(16, 1)) - 1) + " seconds");
            }
        }
        #endregion

        #region resizeImage
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

        #region toBraille
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

/** Jellyfish Commands
 * Author(s): Nia Specht, Ryan Rieger
 * Date: 11/20/2017
 * Description: Commands for the "Jellyfish" bot.
 * Usage: DO NOT USE CODE WITHOUT CREDIT TO AUTHORS. The toText command took a lot of time to write and is Nia's side-project; credit is due.
 * Version: 1.0
 * Completion date: N/A
 */

using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.IO;
using System.Drawing;

namespace Jellyfish.Modules
{
    public class JellyfishCommands : ModuleBase<SocketCommandContext>
    {
        #region help
        [Command("help")]
        public async Task HelpAsync()
        {
            IDMChannel c = await Context.User.GetOrCreateDMChannelAsync();
            string msg = "Helpu has arrived.\n```";

            // Command "help"
            msg += "help <cmd> - get command list, or get specific command usage\n";

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

            //Command "textify"
            msg += "textify <@user> - converts mentioned user's profile picture into pasteable unicode text\n";

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

                // Goodnight usage
                case "goodnight":
                    msg2 += "Goodnight Usage:\n\"goodnight <@user>\" to say goodnight to a specific user, ";
                    msg2 += "or \"goodnight\" to wish yourself goodnight.\n";
                    break;

                // 89 usage
                case "89":
                    msg2 += "89 Usage:\n\"89\" to get 89 made of 89's.";
                    break;

                // Ping usage
                case "ping":
                    msg2 += "Ping Usage:\n\"ping\" to get a pong back.";
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

                // Textify usage
                case "textify":
                    msg2 += "textify Usage:\n\"toText <user>\" to get that user's avatar as text (use mentions).";
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

        #region goodnight
        [Command("goodnight")]
        public async Task GoodnightAsync()
        {
            await ReplyAsync(Context.User.Mention + ", May the 89 jellyfish gods bless your dreams with their presence.");
        }

        [Command("goodnight")]
        public async Task GoodnightAsync(SocketUser user)
        {
            await ReplyAsync(user.Mention + ", May the 89 jellyfish gods bless your dreams with their presence.");
        }
        #endregion

        #region 89
        [Command("89")]
        public async Task EightyNineAsync()
        {
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
            stuffToEcho = "*" + stuffToEcho + "*";

            await ReplyAsync(stuffToEcho/* + "\n" + stuffToEcho + "\n" + stuffToEcho*/);
        }
        #endregion

        #region roll
        [Command("roll")]
        public async Task RollAsync(int num)
        {
            if(num == 11)
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
            await ReplyAsync("https://www.youtube.com/watch?v=Fs3BHRIyF2E");
        }
        #endregion

        #region textify
        [Command("textify")]
        public async Task TextifyAsync()
        {
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

        // Event handler for download of User avatar (called by Overloader as well)
        private async void DownloadDataCallback(Object sender,DownloadDataCompletedEventArgs e) //Overloader for the download
        {
            try
            {
                // If the request was not canceled and did not throw
                // an exception, start conversion.
                if (!e.Cancelled && e.Error == null)
                {
                    // Get the raw result bytes and declare Bitmap object
                    byte[] data = (byte[])e.Result;
                    Bitmap bmp;

                    // Read image data into a memory stream and use the stream to initialize Bitmap object
                    using (MemoryStream ms = new MemoryStream(data, 0, data.Length))
                    {
                        ms.Write(data, 0, data.Length);
                        bmp = new System.Drawing.Bitmap(ms);
                    }
                    
                    Console.WriteLine("Image requested, bounds: (" + bmp.Width + "," + bmp.Height + ")");
                    // Get Pixels
                    int[,] pixel = new int[bmp.Width, bmp.Height]; // Grayscale
                    // System.Drawing.Color[,] pixels = new System.Drawing.Color[bmp.Width,bmp.Height];
                    for(int cntr = 0; cntr < bmp.Width; cntr++)
                    {
                        for(int cntr2 = 0; cntr2 < bmp.Height; cntr2++)
                        {
                            // Console.WriteLine("Counter: " + cntr + "Counter2: " + cntr2 + "     Pixel: (R): " + bmp.GetPixel(cntr,cntr2).R + " (G): " + bmp.GetPixel(cntr,cntr2).G + " (B): " + bmp.GetPixel(cntr,cntr2).B);
                            // pixels[cntr, cntr2] = bmp.GetPixel(cntr,cntr2);
                            pixel[cntr, cntr2] = (bmp.GetPixel(cntr, cntr2).R + bmp.GetPixel(cntr, cntr2).G + bmp.GetPixel(cntr, cntr2).B) / 3;
                        }
                    }


                    // Get background color by checking sides? Really bad implementation, fix later
                    int background = 255;
                    if(pixel[0, pixel.GetLength(1) / 2] == pixel[pixel.GetLength(0) - 1, pixel.GetLength(1) / 2])
                    {
                        background = pixel[0, pixel.GetLength(1) / 2];
                    }

                    // Get significant gradience?
                    // int maxGradient = 0;

                    // TODO: CHANGE PROGRAM TO USE SHADING TECHNIQUES
                    // Begin conversion; check pixels for significance by ignoring duplicate colors
                    int lastPixel = background;
                    Boolean[,] sigPix = new Boolean[pixel.GetLength(0), pixel.GetLength(1)];
                    for (int cntr = 0; cntr < pixel.GetLength(0); cntr++) // Horizontal significance checker
                    {
                        for (int cntr2 = 0; cntr2 < pixel.GetLength(1); cntr2++)
                        {
                            // Use random
                            sigPix[cntr, cntr2] = false; // Default significance value
                            if(lastPixel < pixel[cntr, cntr2] + 10 || pixel[cntr, cntr2] > 175/*(pixel[cntr, cntr2] - 10 < background && background < pixel[cntr, cntr2] + 10)*/)
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
                    /*for (int cntr = 0; cntr < pixel.GetLength(1); cntr++) // Vertical significance checker
                    {
                        for (int cntr2 = 0; cntr2 < pixel.GetLength(0); cntr2++)
                        {
                            
                            if(pixel[cntr, cntr2] - 25 < lastPixel && lastPixel < pixel[cntr, cntr2] + 25)
                            {
                                // Ignore pixels that are duplicates, since they won't be edges in the image
                            }
                            else
                            {
                                sigPix[cntr, cntr2] = true;
                            }
                            lastPixel = pixel[cntr, cntr2];
                        }
                    }*/

                    // Braille set
                    char[] braille = new char[256];
                    for (int cntr = 10240; cntr < 10496; cntr++)
                    {
                        braille[cntr - 10240] = (char)cntr;
                    }

                    // With sixPix as the material and unicode braille as the medium, construct a textual representation of the image
                    string output = ToBraille(sigPix);

                    // Final output
                    if(output.Length > 2000)
                    {
                        Console.WriteLine("Image too large; size: " + output.Length);
                        output = output.Substring(0, 1999);
                    }
                    await ReplyAsync("Conversion complete, your image is on the way " + Context.User.ToString() +"!");

                    // Should we DM them the image text since it would be spam-like otherwise?
                    IDMChannel ch = await Context.User.GetOrCreateDMChannelAsync();
                    await ch.SendMessageAsync(output);

                }
            }catch(Exception err)
            {
                Console.WriteLine(err);
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
                                    if (sigPix[cntr - 1, cntr - 3]) // ⠗
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

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

namespace JellyfishBot.Modules
{
    public class JellyfishBotCommands : ModuleBase<SocketCommandContext>
    {
        #region help
        [Command("help")]
        public async Task HelpAsync()
        {
            IDMChannel c = await Context.User.GetOrCreateDMChannelAsync();
            string msg = "Helpu has arrived.\n```";

            // Command "help"
            msg += "help <cmd> - get command list, or get specific command usage\n";

            // Command "ping"
            msg += "ping - get pong'd\n";

            // Command "echo"
            msg += "echo <text> - repeats after you\n";

            // Command "roll"
            msg += "roll <range> - rolls the dice (default is 1-6)\n";

            // Command "pingu"
            msg += "pingu - noot noot\n";

            //Command "toText"
            msg += "toText <user> - converts mentioned user's profile picture into pasteable unicode text\n";

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

                // ToText usage
                case "toText":
                    msg2 += "toText Usage:\n\"toText <user>\" to get that user's avatar as text (use mentions).";
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

        #region toText
        [Command("toText")]
        public async Task ToTextAsync()
        {
            // toText Stub
            await ReplyAsync("Not yet supported.");

            // Get user's profile picture
            var picUrl = Context.User.GetAvatarUrl(ImageFormat.Jpeg);
            Uri picUri = new Uri(picUrl);
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += new DownloadDataCompletedEventHandler(DownloadDataCallback);
            webClient.DownloadDataAsync(picUri);
            

        }

        private void DownloadDataCallback(Object sender,DownloadDataCompletedEventArgs e) //Overloader for the download
        {
            // return e.Result;
            try
            {
                // If the request was not canceled and did not throw
                // an exception, display the resource.
                if (!e.Cancelled && e.Error == null)
                {
                    byte[] data = (byte[])e.Result;
                    string textData = System.Text.Encoding.UTF8.GetString(data);
                    Bitmap bmp;
                    using (var ms = new MemoryStream(data))
                    {
                        bmp = new Bitmap(ms);
                    }
                    Console.WriteLine(textData); // Temp
                }
            }catch(Exception err)
            {
                Console.WriteLine(err);
            }
        }
        #endregion
        
    }
}

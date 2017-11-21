using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JellyfishBot.Modules
{
    public class JellyfishBotCommands : ModuleBase<SocketCommandContext>
    {
        #region ping
        [Command("ping")]
        public async Task PingAsync()
        {
            EmbedBuilder builder = new EmbedBuilder();
            Color myRgbColor = new Color(255, 102, 179);

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

            await ReplyAsync(stuffToEcho + "\n" + stuffToEcho + "\n" + stuffToEcho);
        }
        #endregion

        #region roll
        [Command("roll")]
        public async Task RollAsync(int num)
        {
            //Roll Stub
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
        #endregion

        #region toText
        [Command("toText")]
        public async Task toText()
        {
            //toText Stub
            await ReplyAsync("Not yet supported.");
        }
        #endregion

        //EIGHTY_NINE
    }
}

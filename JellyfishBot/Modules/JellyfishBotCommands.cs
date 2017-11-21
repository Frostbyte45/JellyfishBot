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
    }
}

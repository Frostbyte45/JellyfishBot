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
    public class CardGames : InteractiveBase
    {
        #region blackjack
        public async Task BlackjackAsync()
        {
            // Blackjack stub
            // await ReplyAsync("Not yet supported... sorry!");

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
            dealerCards.Add(new Bitmap(dHand[0].GetBitmap()));
            dealerCards.Add(new Bitmap(dHand[1].GetBitmap()));

            // Add player cards
            hand.Add(deck.NextCard());
            hand.Add(deck.NextCard());
            playerCards.Add(new Bitmap(hand[0].GetBitmap()));
            playerCards.Add(new Bitmap(hand[1].GetBitmap()));

            playCards = await Task.Run(() => MergeAsync(playerCards));
            dealCards = await Task.Run(() => MergeAsync(dealerCards));


            bool stop = true;
            bool gameOver = false;

            // Request user's response until they stand
            IMessageChannel chan = Context.Channel;
            int index = 0;
            do
            {
                // Send hand (REMODEL THIS TO SEND 1 IMAGE THAT HAS ALL CARDS!)
                await chan.SendFileAsync(playCards.ToString());
                await chan.TriggerTypingAsync();
                await Task.Delay(1500);
                bool contin = false;
                bool hit = false;
                //IMessage Request;
                //IMessage Response;
                IMessage Request;
                SocketMessage Response;
               do
                {
                    Request = await ReplyAsync("Hit or stand, " + Context.User.Mention + "?");
                    Response = await NextMessageAsync(true, true, new TimeSpan(0, 0, 45));
                    // Check for next message from the user
                    Console.WriteLine("Hand size: " + hand.Count + " Index: " + index);

                    if(Response.Content.ToLower().Contains("hit") && Response.Content.ToLower().Contains("stand"))
                    {
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Please say only \"hit\", or only \"stand\" in your response."); //  :White_Check_Mark:
                    }
                    else if (Response.Content.ToLower().Contains("hit"))
                    {
                        hit = true;
                        contin = true;
                    }
                    else if (Response.Content.ToLower().Contains("stand"))
                    {
                        hit = false;
                        contin = true;
                    }
                    else
                    {
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Please say \"hit\" or \"stand\" in your response.");
                    }
                } while (!contin);

                // Hit or stand
                if (hit)
                {
                    hand.Add(deck.NextCard());
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
                index++;
            } while (!stop);
            if (gameOver)
            {
                await chan.TriggerTypingAsync();
                await Task.Delay(1500);

                // Player's final hand
                // TODO @Ryan Add embed builder here for images? idrk
                await ReplyAsync("You\'ve lost " + Context.User.Mention + "...!\nYour card count went over 21. Unfortunate!");
                EmbedBuilder build = new EmbedBuilder();
                string cards = "";
                string dCards = "";
                for (int x = 0; x < hand.Count; x++) { cards += " " + hand[x]; }
                for (int x = 0; x < dHand.Count; x++) { dCards += " " + dHand[x]; }
                Discord.Color myRgbColor = new Discord.Color(255, 102, 179);
                build.AddField("Player Cards",cards)
                    .WithColor(myRgbColor);
                build.AddField("Dealer Cards", dCards)
                    .WithColor(myRgbColor);
                await chan.TriggerTypingAsync();
                await Task.Delay(1500);
                await ReplyAsync("Here\'s your hand:", false, build.Build());
            }
            else // Dealer's turn
            {
                await chan.TriggerTypingAsync();
                await Task.Delay(1500);
                do
                {
                    // Dealer abuse
                    dHand.Add(deck.NextCard());

                    // Check to see if the dealer lost already!
                    if (deck.Count(dHand) > 21)
                    {
                        await ReplyAsync("You\'ve won " + Context.User.Mention + "!\nThe dealer\'s card count went over 21. Unfortunate!");
                        // Dealer's final hand


                        // Player's final hand
                        EmbedBuilder build = new EmbedBuilder();
                        string cards = "";
                        string dCards = "";
                        for (int x = 0; x < hand.Count; x++) { cards += " " + hand[x]; }
                        for (int x = 0; x < dHand.Count; x++) { dCards += " " + dHand[x]; }
                        Discord.Color myRgbColor = new Discord.Color(255, 102, 179);
                        build.AddField("Player Cards", cards)
                            .WithColor(myRgbColor);
                        build.AddField("Dealer Cards", dCards)
                            .WithColor(myRgbColor);
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Here\'s your hand:", false, build.Build());

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
                        await ReplyAsync("You\'ve won " + Context.User.Mention + "!\nGood game! The dealer\'s card count went over 21.");
                        // Dealer's final hand


                        // Player's final hand
                        EmbedBuilder build = new EmbedBuilder();
                        string cards = "";
                        string dCards = "";
                        for (int x = 0; x < hand.Count; x++) { cards += " " + hand[x]; }
                        for (int x = 0; x < dHand.Count; x++) { dCards += " " + dHand[x]; }
                        Discord.Color myRgbColor = new Discord.Color(255, 102, 179);
                        build.AddField("Player Cards", cards)
                            .WithColor(myRgbColor);
                        build.AddField("Dealer Cards", dCards)
                            .WithColor(myRgbColor);
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Here\'s your hand:", false, build.Build());

                        // Exit game
                    }
                    else if (deck.Count(dHand) > deck.Count(hand)) // Dealer wins
                    {
                        await ReplyAsync("You\'ve lost " + Context.User.Mention + "...!\nGood game! The dealer\'s hand was better.");
                        // Dealer's final hand


                        // Player's final hand
                        EmbedBuilder build = new EmbedBuilder();
                        string cards = "";
                        string dCards = "";
                        for (int x = 0; x < hand.Count; x++) { cards += " " + hand[x]; }
                        for (int x = 0; x < dHand.Count; x++) { dCards += " " + dHand[x]; }
                        Discord.Color myRgbColor = new Discord.Color(255, 102, 179);
                        build.AddField("Player Cards", cards)
                            .WithColor(myRgbColor);
                        build.AddField("Dealer Cards", dCards)
                            .WithColor(myRgbColor);
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Here\'s your hand:", false, build.Build());

                        // Exit game
                    }
                    else // Player and Dealer tie
                    {
                        await ReplyAsync("You\'ve tied with the dealer " + Context.User.Mention + ".\nGood game! The dealer\'s hand and yours are equal");
                        // Dealer's final hand


                        // Player's final hand
                        EmbedBuilder build = new EmbedBuilder();
                        string cards = "";
                        string dCards = "";
                        for (int x = 0; x < hand.Count; x++) { cards += " " + hand[x]; }
                        for (int x = 0; x < dHand.Count; x++) { dCards += " " + dHand[x]; }
                        Discord.Color myRgbColor = new Discord.Color(255, 102, 179);
                        build.AddField("Player Cards", cards)
                            .WithColor(myRgbColor);
                        build.AddField("Dealer Cards", dCards)
                            .WithColor(myRgbColor);
                        await chan.TriggerTypingAsync();
                        await Task.Delay(1500);
                        await ReplyAsync("Here\'s your hand:", false, build.Build());

                        // Exit game
                    }
                }
            }
        }
        #endregion

        #region Merge

        public async Task<Bitmap> MergeAsync(List<Bitmap> images)
        {
            return images[0]; // TEMP
        }

        #endregion
    }
}

using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;

namespace Jellyfish
{
    class Deck
    {
        protected List <Card> deck;
        protected IEnumerator<Card> deal;
        protected static readonly int NUMCARDS = 52;
        public Deck()
        {
            deck = new List <Card>();
            string image;
            int[] fiends = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10 + 1, 12, 13, 14 };

            int rankNum;
            foreach (Card.Suit suit in Card.valuesS)
            {
                rankNum = 2;
                for(int x=0;x<13;x++)
                {
                    Card.Rank rank = Card.ranks[x];
                    if (x > 8)
                    {
                        image = "PNG-cards-1.3/" + rank.ToString().ToLower() + "_of_" + suit.ToString().ToLower() + "s.png";
                    }
                    else
                    {
                        image = "PNG-cards-1.3/" + rankNum + "_of_" + suit.ToString().ToLower() + "s.png";
                    }
                    deck.Add(new Card(suit, rank, image, rankNum));
                    rankNum++;
                }
            }

            deal = deck.GetEnumerator();
        }

        public void Shuffle()
        {
            Random rand = new Random();
            int randLoc;
            Card temp;

            for (int i = (NUMCARDS - 1); i > 0; i--)
            {
                randLoc = rand.Next(i);  // Random integer between 0 and i - 1
                temp = deck[randLoc];
                deck[randLoc] = deck[i];
                deck[i] = temp;
            }

            deal = deck.GetEnumerator();
        }

        public Card nextCard()
        {
            if (deal.MoveNext())
                return deal.Current;
            else
            {
                return null;
            }
        }
    }
    class Card
    {
        protected Suit suit;
        protected Rank rank;
        protected string img;
        private int ranku; // Counter to fix value errors
        // private string face; // Also rank

        public Card(Suit suit, Rank rank, string img, int ranku)
        {
            this.rank = rank;
            this.suit = suit;
            this.img = img;
            this.ranku = ranku;
        }

        public static Rank[] ranks = { Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven, Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace };
        public static int[] valuesR = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 10 + 1 };
        public enum Rank
        {
            Two=2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack=10, Queen=10, King=10, Ace
        }

        public static int[] valuesS = { 0, 1, 2, 3 };
        public enum Suit
        {
            Club, Diamond, Heart, Spade
        }

        public int GetRanku()
        {
            return ranku;
        }

        public Rank GetRank()
        {
            return rank;
        }

        public Suit GetSuit()
        {
            return suit;
        }

        public string GetBitmap()
        {
            return img;
        }

        public int CompareTo(Card other)
        {
            if(this.rank > other.GetRank())
            {
                return -1;
            }
            else if(this.rank < other.GetRank())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            else if (obj == null || obj.GetType() != this.GetType())
                return false;
            else
            {
                Card c = (Card)obj;
                return (this.rank == c.rank);
            }
        }
        public override string ToString()
        {
            string str = "";
            str += suit + " ";
            str += rank;
            return str;
        }

    }
}

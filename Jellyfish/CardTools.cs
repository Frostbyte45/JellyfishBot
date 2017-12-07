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

            int rankNum;
            foreach (Card.Suit suit in Card.valuesS)
            {
                rankNum = 2;
                for(int x=0;x<13;x++)
                {
                    Card.Rank rank = Card.ranks[x];
                    if (x > 7)
                    {
                        switch (x)
                        {
                            case 8:
                                image = "PNG-cards-1.3/" + "10" + "_of_" + suit.ToString().ToLower() + "s.png";
                                rank = Card.Rank.Ten;
                                break;
                            case 9:
                                image = "PNG-cards-1.3/" + "jack" + "_of_" + suit.ToString().ToLower() + "s.png";
                                rank = Card.Rank.Jack;
                                break;
                            case 10:
                                image = "PNG-cards-1.3/" + "queen" + "_of_" + suit.ToString().ToLower() + "s.png";
                                rank = Card.Rank.Queen;
                                break;
                            case 10 + 1:
                                image = "PNG-cards-1.3/" + "king" + "_of_" + suit.ToString().ToLower() + "s.png";
                                rank = Card.Rank.King;
                                break;
                            case 12:
                                image = "PNG-cards-1.3/" + "ace" + "_of_" + suit.ToString().ToLower() + "s.png";
                                rank = Card.Rank.Ace;
                                break;
                            default:
                                Console.WriteLine("FIENDADJWANDWJAKWA\nTHAT\'S NOT 89!!!");
                                image = "PNG-cards-1.3/" + "2" + "_of_" + suit.ToString().ToLower() + "s.png";
                                rank = Card.Rank.Ten;
                                break;
                        }
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

        public Card NextCard()
        {
            if (deal.MoveNext())
                return deal.Current;
            else
            {
                return null;
            }
        }

        public int Count(List<Card> hand)
        {
            int count = 0;
            int aceCount = 0; // For counting aces later
            int minAceCount = 0; // Also for counting aces later

            // Face cards are worth 10, aces are worth 1 or the number after 10
            for (int x = 0; x < hand.Count; x++)
            {

                // Save ace counting for later
                if (hand[x].GetRank() == Card.Rank.Ace)
                {
                    //Console.WriteLine("Ace++!: " + hand[x].ToString()); // Debug
                    aceCount++;
                }

                // If card is face, it is worth 10
                else if (hand[x].GetRank().CompareTo(Card.Rank.Jack) >= 0)
                {
                    //Console.WriteLine("Face++!: " + hand[x].ToString());
                    count += 10;
                }

                // If a card is not an ace or a face, the value is on the base (I hope this is fun code to read)
                else
                {
                    //Console.WriteLine("Count: " + count);
                    //Console.WriteLine("Regular++!: " + hand[x].ToString());
                    Predicate<Card.Rank> wantedRank = (Card.Rank r) => { return r == hand[x].GetRank(); };
                    //Console.WriteLine("Rank index of card: " + Card.ranks.ToList().FindIndex(wantedRank));
                    count += Card.ranks.ToList().FindIndex(wantedRank) + 2;
                    //Console.WriteLine("Count after: " + count);
                }
            }

            // Optimization for best ace usage
            while (aceCount > 0)
            {
                if (aceCount * (10 + 1) + count + minAceCount > 21)
                {
                    count++;
                    minAceCount++;
                    aceCount--;
                }
                else
                {
                    count += 10 + 1;
                    aceCount--;
                }
            }
            return count;
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
        { //2      3      4     5     6    7      8      9     10   10    10     10    89
            Two=2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
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

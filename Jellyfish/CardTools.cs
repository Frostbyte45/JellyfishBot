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
            Bitmap image;

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
                                image = Program.playingCards.ElementAt(8 * ((int)suit + 1));
                                rank = Card.Rank.Ten;
                                break;
                            case 9:
                                image = Program.playingCards.ElementAt(9 * ((int)suit + 1));
                                rank = Card.Rank.Jack;
                                break;
                            case 10:
                                image = Program.playingCards.ElementAt(10 * ((int)suit + 1));
                                rank = Card.Rank.Queen;
                                break;
                            case 10 + 1:
                                image = Program.playingCards.ElementAt((10+1) * ((int)suit + 1));
                                rank = Card.Rank.King;
                                break;
                            case 12:
                                image = Program.playingCards.ElementAt(12 * ((int)suit + 1));
                                rank = Card.Rank.Ace;
                                break;
                            default:
                                Console.WriteLine("FIENDADJWANDWJAKWA\nTHAT\'S NOT 89!!!");
                                image = Program.playingCards.ElementAt(0);
                                rank = Card.Rank.Ten;
                                break;
                        }
                    }
                    else
                    {
                        image = Program.playingCards.ElementAt(x * ((int)suit + 1));
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

        public bool HasRank(Card.Rank whichRank, List <Card> whichHand)
        {
            for(int x = 0; x < whichHand.Count; x++)
            {
                if(whichHand[x].GetRank() == whichRank)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RankMatch(List <Card> whichHand)
        {
            return false;
        }
    }
    class Card
    {
        protected Suit suit;
        protected Rank rank;
        protected Bitmap img;
        private int ranku; // Counter to fix value errors
        // private string face; // Also rank

        public Card(Suit suit, Rank rank, Bitmap img, int ranku)
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

        public Bitmap GetBitmap()
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
    class Incremental
    {
        protected int min;
        protected int max;
        protected int current;
        protected int start;
        protected int turn;
        protected int last;

        public Incremental(int min, int max, int start)
        {
            this.min = min;
            this.max = max;
            this.start = start;
            current = start;
            turn = 1;
            if (start > min) last = start - 1;
            else last = max;
        }

        public int Reset()
        {
            current = start;
            turn = 1;
            if (start > min) last = start - 1;
            else last = max;
            return current;
        }

        public int Turn()
        {
            return turn;
        }

        public int Current()
        {
            return current;
        }

        public int Last()
        {
            return last;
        }

        public int Next()
        {
            if (HasNext())
            {
                last = current;
                current++;
                if (current + 1 == start)
                    turn = 1;
                else turn++;
                return current;
            }
            else
            {
                last = current;
                current = min;
                return current;
            }
        }

        public bool HasNext()
        {
            return (current + 1) <= max;
        }

        public bool RoundEnd()
        {
            if (HasNext())
                return current + 1 == start;
            else return min == start;
        }

        public int GetMin()
        {
            return min;
        }

        public int GetMax()
        {
            return max;
        }

        public int Peek()
        {
            if (HasNext())
                return current + 1;
            else return min;
        }
    }
    class KeyWords
    {
        // Checks if string is a card rank, if none found throws an error that must be caught
        public static Card.Rank DecodeRank(string rank)
        {
            Card.Rank returnRank;

            // Modify input for weird typing
            rank = Format(rank);

            // Switch for different rank aliases
            switch (rank)
            {
                case "ace":
                    returnRank = Card.Rank.Ace;
                    break;
                case "aces":
                    returnRank = Card.Rank.Ace;
                    break;
                case "ace\'s":
                    returnRank = Card.Rank.Ace;
                    break;
                case "acez":
                    returnRank = Card.Rank.Ace;
                    break;
                case "two":
                    returnRank = Card.Rank.Two;
                    break;
                case "twos":
                    returnRank = Card.Rank.Two;
                    break;
                case "two\'s":
                    returnRank = Card.Rank.Two;
                    break;
                case "twoz":
                    returnRank = Card.Rank.Two;
                    break;
                case "three":
                    returnRank = Card.Rank.Three;
                    break;
                case "threes":
                    returnRank = Card.Rank.Three;
                    break;
                case "three\'s":
                    returnRank = Card.Rank.Three;
                    break;
                case "threez":
                    returnRank = Card.Rank.Three;
                    break;
                case "four":
                    returnRank = Card.Rank.Four;
                    break;
                case "fours":
                    returnRank = Card.Rank.Four;
                    break;
                case "four\'s":
                    returnRank = Card.Rank.Four;
                    break;
                case "fourz":
                    returnRank = Card.Rank.Four;
                    break;
                case "five":
                    returnRank = Card.Rank.Five;
                    break;
                case "fives":
                    returnRank = Card.Rank.Five;
                    break;
                case "five\'s":
                    returnRank = Card.Rank.Five;
                    break;
                case "fivez":
                    returnRank = Card.Rank.Five;
                    break;
                case "six":
                    returnRank = Card.Rank.Six;
                    break;
                case "sixes":
                    returnRank = Card.Rank.Six;
                    break;
                case "six\'s":
                    returnRank = Card.Rank.Six;
                    break;
                case "sixs":
                    returnRank = Card.Rank.Six;
                    break;
                case "sixz":
                    returnRank = Card.Rank.Six;
                    break;
                case "seven":
                    returnRank = Card.Rank.Seven;
                    break;
                case "sevens":
                    returnRank = Card.Rank.Seven;
                    break;
                case "seven\'s":
                    returnRank = Card.Rank.Seven;
                    break;
                case "sevenz":
                    returnRank = Card.Rank.Seven;
                    break;
                case "eight":
                    returnRank = Card.Rank.Eight;
                    break;
                case "eights":
                    returnRank = Card.Rank.Eight;
                    break;
                case "eight\'s":
                    returnRank = Card.Rank.Eight;
                    break;
                case "eightz":
                    returnRank = Card.Rank.Eight;
                    break;
                case "nine":
                    returnRank = Card.Rank.Nine;
                    break;
                case "nines":
                    returnRank = Card.Rank.Nine;
                    break;
                case "nine\'s":
                    returnRank = Card.Rank.Nine;
                    break;
                case "ninez":
                    returnRank = Card.Rank.Nine;
                    break;
                case "ten":
                    returnRank = Card.Rank.Ten;
                    break;
                case "tens":
                    returnRank = Card.Rank.Ten;
                    break;
                case "ten\'s":
                    returnRank = Card.Rank.Ten;
                    break;
                case "tenz":
                    returnRank = Card.Rank.Ten;
                    break;
                case "jack":
                    returnRank = Card.Rank.Jack;
                    break;
                case "jacks":
                    returnRank = Card.Rank.Jack;
                    break;
                case "jack\'s":
                    returnRank = Card.Rank.Jack;
                    break;
                case "jackz":
                    returnRank = Card.Rank.Jack;
                    break;
                case "queen":
                    returnRank = Card.Rank.Queen;
                    break;
                case "queens":
                    returnRank = Card.Rank.Queen;
                    break;
                case "queen\'s":
                    returnRank = Card.Rank.Queen;
                    break;
                case "queenz":
                    returnRank = Card.Rank.Queen;
                    break;
                case "king":
                    returnRank = Card.Rank.King;
                    break;
                case "kings":
                    returnRank = Card.Rank.King;
                    break;
                case "king\'s":
                    returnRank = Card.Rank.King;
                    break;
                case "kingz":
                    returnRank = Card.Rank.King;
                    break;
                default:
                    throw new Exception("Card not found!: " + rank);
            }

            return returnRank;
        }

        // Removes duplicate letters, since saying "threeeeeee's" or "aaacccceeeeessss" can be fun in the moment
        public static string Format(string input)
        {
            input = input.ToLower();
            string output = "";
            bool modified = false;
            int letCount = 0;
            int curLet = 0;

            // Removes duplicate letters
            while (letCount < input.Length)
            {
                if (letCount == 0) // Prevents indexing errors
                {
                    output += input[letCount];
                    curLet++;
                }
                else if (output[curLet-1] == input[letCount])
                {
                    modified = true;
                }
                else
                {
                    output += input[letCount];
                    curLet++;
                }
                letCount++;
            }

            // Three is the only card rank it will get wrong, since it has a double letter
            switch (output)
            {
                case "thre":
                    if (modified)
                        return "three";
                    else return output;
                case "thres":
                    if (modified)
                        return "threes";
                    else return output;
                case "threz":
                    if (modified)
                        return "threez";
                    else return output;
                case "thre\'s":
                    if (modified)
                        return "three\'s";
                    else return output;
                default:
                    return output;
            }
        }

        /*// Checks if string is a request
        public static bool IsRequest(string req)
        {
            switch (Format(req))
            {
                case ""
                default:
                    return false;
            }
        }*/
    }
}

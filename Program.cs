using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackPersonalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.Shuffle();
            Player dealer = new Player("Chives", deck.Deal(2));
            bool playing = true;
            Console.WriteLine("WELCOME TO BLACKJACK");
            Console.WriteLine("Please enter your name");
            Player player1 = new Player(Console.ReadLine(), deck.Deal(2));
            while (playing)
            {
                Console.Clear();
                Console.WriteLine("WELCOME TO BLACKJACK");
                Console.WriteLine(player1.Name + ", your hand is");
                Console.WriteLine(player1.GetHand());
                Console.WriteLine(player1.GetSplitHand());
                Console.WriteLine("The dealers hand is");
                Console.WriteLine(dealer.GetHand());
                int playerHandValue = 0;
                foreach (Card card in player1.Hand)
                {
                    playerHandValue += card.Value;
                }
                int dealerHandValue = 0;
                foreach (Card card in dealer.Hand)
                {
                    dealerHandValue += card.Value;
                }
                int hitTimes = 0;
                if (dealerHandValue < 17)
                {
                    hitTimes++;
                    dealer.Hand.Add(deck.Cards.First());
                    deck.Cards.Remove(deck.Cards.First());
                    Console.WriteLine("Dealer Hit " + hitTimes + " times");
                }
                if (playerHandValue >= 21)
                {
                    Console.WriteLine("BLACKJACK you won!!!!!");
                    playing = false;
                }
                else if (playerHandValue < 21)
                {
                    Console.WriteLine("Do you wanna hit or stand? Enter 1 for hit and 2 for no \nor if the value of your cards is the same you have the option to split using 3");
                    string userInput = Console.ReadLine();
                    if (userInput == "1")
                    {
                        player1.Hand.Add(deck.Cards.First());
                        deck.Cards.Remove(deck.Cards.First());
                    }
                    else if(userInput=="3"&&player1.Hand.First().Value==player1.Hand.Last().Value)
                    {
                        player1.SplitHand.Add(player1.Hand.Last());
                        player1.Hand.Remove(player1.Hand.Last());
                        player1.SplitHand.Add(deck.Cards.First());
                        deck.Cards.Remove(deck.Cards.First());
                        player1.Hand.Add(deck.Cards.First());
                        deck.Cards.Remove(deck.Cards.First());
                        Console.WriteLine("To preform actions on first hand hit 1, on your Split Hand hit 2, or if you dont wanna do anything hit 3.");
                        string userInput2 = Console.ReadLine();
                        if(userInput2=="1")
                        {
                            Console.WriteLine("Do you want to hit or stand.");
                            string userInput3 = Console.ReadLine();
                            if(userInput3=="1")
                            {
                                player1.Hand.Add(deck.Cards.First());
                                deck.Cards.Remove(deck.Cards.First());
                            }
                            else
                            {

                            }
                        }


                        
                    }
                    else
                    {
                        if (dealerHandValue < playerHandValue)
                        {
                            Console.WriteLine("You beat the dealer");
                            playing = false;
                        }
                        else
                        {
                            Console.WriteLine("You lost against the dealer");
                            playing = false;
                        }
                    }

                }

            }
            if (playing == false)
            {
                Console.WriteLine("Do you want to play again? 1 for yes and 2 for no");
                string userInput = Console.ReadLine();
                if (userInput == "1")
                {
                    playing = true;
                }

            }
            Console.ReadKey();
        }
    }
    class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public List<Card> SplitHand { get; set; }
        public int HandValue { get; set; }


        public Player(string name, List<Card> hand)
        {
            this.Name = name;
            this.Hand = hand;

        }
        public string GetHand()
        {
            return this.Name + ": " + string.Join(" - ", this.Hand.Select(x => x.GetCardInfo()));


        }
        public string GetSplitHand()
        {
            if(this.SplitHand==null)
            {
                return "";
            }
            else
            {
                return "Split Hand: " + string.Join(" - ", this.SplitHand.Select(x => x.GetCardInfo()));
            }
        }

    }

    enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    enum Suit
    {
        Club,
        Spade,
        Heart,
        Diamond
    }

    class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }
        public int Value { get; set; }

        public Card(Suit s, Rank r)
        {

            this.Suit = s;
            this.Rank = r;
            switch (Rank)
            {
                case Rank.Two:
                    this.Value = 2;
                    break;
                case Rank.Three:
                    this.Value = 3;
                    break;
                case Rank.Four:
                    this.Value = 4;
                    break;
                case Rank.Five:
                    this.Value = 5;
                    break;
                case Rank.Six:
                    this.Value = 6;
                    break;
                case Rank.Seven:
                    this.Value = 7;
                    break;
                case Rank.Eight:
                    this.Value = 8;
                    break;
                case Rank.Nine:
                    this.Value = 9;
                    break;
                case Rank.Ten:
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    this.Value = 10;
                    break;
                case Rank.Ace:
                    this.Value = 1;
                    break;

                default:
                    break;
            }
        }

        public string GetCardInfo()
        {

            return this.Rank + " of " + this.Suit + " and has a value of " + this.Value;
        }
    }
    class Deck
    {
        public List<Card> Cards { get; set; }
        public List<Card> DealtCards { get; set; }

        public Deck()
        {
            Cards = new List<Card>();
            DealtCards = new List<Card>();
            for (int i = 1; i <= 52; i++)
            {

                int suitCounter = i % 4;
                int rankCounter = i % 13 + 2;
                Suit s = (Suit)suitCounter;
                Rank r = (Rank)rankCounter;
                Card tmp = new Card(s, r);
                Cards.Add(tmp);
            }
        }


        public void Shuffle()
        {
            this.Cards.AddRange(DealtCards);
            this.DealtCards.Clear();
            Random rng = new Random();
            for (int i = 0; i < this.Cards.Count; i++)
            {
                int k = rng.Next(i, Cards.Count);
                Card tmp = Cards[k];
                this.Cards[k] = this.Cards[i];
                this.Cards[i] = tmp;
            }

        }

        public List<Card> Deal(int x)
        {
            List<Card> Hand = new List<Card>();

            for (int i = 0; i < x; i++)
            {
                Hand.Add(this.Cards.First());
                this.DealtCards.Add(this.Cards.First());
                this.Cards.Remove(this.Cards.First());
            }
            return Hand;
        }
    }
}

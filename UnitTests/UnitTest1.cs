using System.Security.Cryptography;
using System.Text;

namespace UnitTests
{
    public class Card
    {
        public string CardFace { get; set; }
        public int Score { get; set; }
    }
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckScoreForACard()
        {
            CardsGame cardsGame = new CardsGame();
            bool isJoker = false;
            Card card = GetACard();
            int score = cardsGame.GetScoreforACard(card.CardFace, out isJoker);
            Assert.That(score == card.Score);
        }
        
        [Test]
        public void CheckScoreForAJoker()
        {
            CardsGame cardsGame = new CardsGame();
            bool isJoker = false;
            int score = cardsGame.GetScoreforACard("JR", out isJoker);
            Assert.That(isJoker);
            Assert.That(score == 0);            
        }
        [Test]
        public void CheckForListOfCards()
        {
            List<Card> cards = new List<Card>();
            int count = 0;
            while (count != 10)
            {
                Card card = GetACard();
                if (cards.Any(x => x.CardFace == card.CardFace))
                    continue;
                cards.Add(card);
                count++;
            }
            int finalScore = 0;
            StringBuilder listOfCards = new StringBuilder();
            foreach(Card c in cards)
            {
                finalScore += c.Score;
                listOfCards.Append(c.CardFace);
                listOfCards.Append(",");
            }
            listOfCards.Remove(listOfCards.Length-1,1);
            CardsGame cardsGame = new CardsGame();
            cardsGame.GetScore(listOfCards.ToString());
            Assert.That(finalScore == cardsGame.FinalScore);
        }
        [Test]
        public void CheckForListOfCardsWithJoker()
        {
            List<Card> cards = new List<Card>();
            int count = 0;
            int pos1 = RandomNumberGenerator.GetInt32(0, 10);
            while (count != 10)
            {
                Card card;
                if (count == pos1)
                {
                    card = GetAJokerCard();
                    cards.Add(card);
                    count++;
                }
                else
                {
                    card = GetACard();
                    if (!cards.Any(x => x.CardFace == card.CardFace))
                    {
                        cards.Add(card);
                        count++;
                    }
                }
            }
            int finalScore = 0;
            StringBuilder listOfCards = new StringBuilder();
            foreach (Card c in cards)
            {
                finalScore += c.Score;
                listOfCards.Append(c.CardFace);
                listOfCards.Append(",");
            }
            listOfCards.Remove(listOfCards.Length - 1, 1);
            finalScore *= 2;
            CardsGame cardsGame = new CardsGame();
            cardsGame.GetScore(listOfCards.ToString());
            Assert.That(finalScore == cardsGame.FinalScore);
        }
        [Test]
        public void CheckForListOfCardsWithTwoJokers()
        {
            List<Card> cards = new List<Card>();
            int count = 0;
            int pos1 = RandomNumberGenerator.GetInt32(0, 10);
            int pos2 = RandomNumberGenerator.GetInt32(0, 10);
            while (pos1 == pos2)
                pos2 = RandomNumberGenerator.GetInt32(0, 10);
            while (count != 10)
            {
                Card card = null;
                if (count == pos1 || count == pos2)
                {
                    card = GetAJokerCard();
                    cards.Add(card);
                    count++;
                }
                else
                {
                    card = GetACard();
                    if (!cards.Any(x => x.CardFace == card.CardFace))
                    {
                        cards.Add(card);
                        count++;
                    }
                }
            }
            int finalScore = 0;
            StringBuilder listOfCards = new StringBuilder();
            foreach (Card c in cards)
            {
                finalScore += c.Score;
                listOfCards.Append(c.CardFace);
                listOfCards.Append(",");
            }
            listOfCards.Remove(listOfCards.Length - 1, 1);
            finalScore *= 2;
            CardsGame cardsGame = new CardsGame();
            cardsGame.GetScore(listOfCards.ToString());
            Assert.That(finalScore == cardsGame.FinalScore);
        }
        [Test]
        public void CheckForListOfCardsWithThreeJokers()
        {
            List<Card> cards = new List<Card>();
            int count = 0;
            int pos1 = RandomNumberGenerator.GetInt32(0, 10);
            int pos2 = RandomNumberGenerator.GetInt32(0, 10);
            while (pos1 == pos2)
                pos2 = RandomNumberGenerator.GetInt32(0, 10);
            int pos3 = RandomNumberGenerator.GetInt32(0, 10);
            while (pos3 == pos1 || pos3 == pos2)
                pos3 = RandomNumberGenerator.GetInt32(0, 10);
            while (count != 10)
            {
                Card card;
                if (count == pos1 || count == pos2 || count == pos3)
                {
                    card = GetAJokerCard();
                    cards.Add(card);
                    count++;
                }
                else
                {
                    card = GetACard();
                    if (!cards.Any(x => x.CardFace == card.CardFace))
                    {
                        cards.Add(card);
                        count++;
                    }
                }                
            }
            int finalScore = 0;
            StringBuilder listOfCards = new StringBuilder();
            foreach (Card c in cards)
            {
                finalScore += c.Score;
                listOfCards.Append(c.CardFace);
                listOfCards.Append(",");
            }
            listOfCards.Remove(listOfCards.Length - 1, 1);
            finalScore *= 2;
            CardsGame cardsGame = new CardsGame();
            cardsGame.GetScore(listOfCards.ToString());
            
            Assert.That(cardsGame.Error.StartsWith("A hand cannot contain more than two Jokers"));
        }
        [Test]
        public void CheckForListOfCardsWithDuplicates()
        {
            List<Card> cards = new List<Card>();
            int count = 0;
            Card duplicatecard = GetACard();
            int pos1 = RandomNumberGenerator.GetInt32(0, 10);
            int pos2 = RandomNumberGenerator.GetInt32(0, 10);
            while(pos1 == pos2)
                pos2 = RandomNumberGenerator.GetInt32(0, 10);
            while (count != 10)
            {
                Card card;
                if (count == pos1 || count == pos2)
                {
                    cards.Add(duplicatecard);
                    count++;
                }
                else
                {
                    card = GetACard();
                    if (!cards.Any(x => x.CardFace == card.CardFace))
                    {
                        cards.Add(card);
                        count++;
                    }
                }
            }            
           
            int finalScore = 0;
            StringBuilder listOfCards = new StringBuilder();
            foreach (Card c in cards)
            {
                finalScore += c.Score;
                listOfCards.Append(c.CardFace);
                listOfCards.Append(",");
            }
            listOfCards.Remove(listOfCards.Length - 1, 1);
            finalScore *= 2;
            CardsGame cardsGame = new CardsGame();
            cardsGame.GetScore(listOfCards.ToString());

            Assert.That(cardsGame.Error.StartsWith("Cards cannot be duplicated"));
        }
        [Test]
        public void CheckForListOfCardsWithAnInvalidCard()
        {
            List<Card> cards = new List<Card>();
            int count = 0;
            int pos1 = RandomNumberGenerator.GetInt32(0, 10);
            while (count != 10)
            {
                Card card;
                if (count == pos1)
                {
                    card = GetAnInvalidCard();
                    cards.Add(card);
                    count++;
                }
                else
                {
                    card = GetACard();
                    if (!cards.Any(x => x.CardFace == card.CardFace))
                    {
                        cards.Add(card);
                        count++;
                    }
                }
            }
            int finalScore = 0;
            StringBuilder listOfCards = new StringBuilder();
            foreach (Card c in cards)
            {
                finalScore += c.Score;
                listOfCards.Append(c.CardFace);
                listOfCards.Append(",");
            }
            listOfCards.Remove(listOfCards.Length - 1, 1);
            CardsGame cardsGame = new CardsGame();
            cardsGame.GetScore(listOfCards.ToString());

            Assert.That(cardsGame.Error.StartsWith("Card not recognised"));
        }
        public Card GetACard()
        {
            int facevalue = RandomNumberGenerator.GetInt32(2, 15);
            char faceValueType = facevalue switch
            {
                10 => 'T',
                11 => 'J',
                12 => 'Q',
                13 => 'K',
                14 => 'A',
                _ => facevalue.ToString().ToCharArray()[0]
            };
            int cardTypeValue = RandomNumberGenerator.GetInt32(1, 5);
            char cardType = cardTypeValue switch
            {
                1 => 'C',
                2 => 'D',
                3 => 'H',
                4 => 'S',
                _ => 'C'
            };
            string cardValue = string.Concat(faceValueType, cardType);
            Card card = new Card { CardFace=cardValue, Score=facevalue*cardTypeValue};
            return card;
        }
        public Card GetAJokerCard()
        {
            Card card = new Card { CardFace = "JR", Score = 0 };
            return card;
        }
        public Card GetAnInvalidCard()
        {
            List<string> listOfInvalidCombinations = new List<string>{ "1C", "10C", "JokerWithSpades", "JC|AS" };
            int randomValue = RandomNumberGenerator.GetInt32(0, 3);
            return new Card { CardFace = listOfInvalidCombinations[0], Score=0 };            
        }       
    }
}
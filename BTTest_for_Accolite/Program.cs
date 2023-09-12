namespace BTTest_for_Accolite
{
    public class CardsGame
    {
        public List<string> ListOfCards { get; set; }
        public int FinalScore { get; set; }
        public List<int> Scores { get; set; }
        public string Error { get; set; }
        static void Main(string[] args)
        {            
            if (string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("*********Please enter card values seperated with comma like 4C, 2D, TS, JH, QH, KS, AS, 7S, 8C, 4H, 5D. Joker should be mentioned as JR. The letter C, D, H, S represents Clubs, Diamonds, Heart and Spade. The card face value must be 2-9, T (for 10), J, K, Q and A.********");
            }
            CardsGame cardsGame = new CardsGame();
            cardsGame.GetScore(args[0]);
            if(cardsGame.FinalScore == 0)
            {
                Console.WriteLine(cardsGame.Error);
                Console.WriteLine("*********Please enter card values seperated with comma like 4C, 2D, TS, JH, QH, KS, AS, 7S, 8C, 4H, 5D. Joker should be mentioned as JR. The letter C, D, H, S represents Clubs, Diamonds, Heart and Spade. The card face value must be 2-9, T (for 10), J, K, Q and A.**************");
                return;
            }
            Console.WriteLine($"Final score is :{cardsGame.FinalScore}.");
            Console.ReadLine();

        }
        public CardsGame()
        {
            ListOfCards = null;
            FinalScore = 0;
            Scores = null;
            Error = null;
        }
        public void GetScore(string cards)
        {
            if (string.IsNullOrEmpty(cards))
            {
                FinalScore = 0;
                Error = "Invalid input string";
                return;
            }
            ListOfCards = cards.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            if (ListOfCards == null || ListOfCards.Count == 0)
            {
                FinalScore=0;
                Error = "Invalid input string";
                return;
            }
            int jokerCount = 0;            
            Scores = new List<int>();
            List<string> listOfProcessedCards = new List<string>();
            foreach (string card in ListOfCards)
            {
                if(card.Length > 2)
                {
                    FinalScore = 0;
                    Error = "Invalid input string";
                    return;
                }
                bool hasJoker = false;
                int score = GetScoreforACard(card, out hasJoker);
                if (score == 0 && !hasJoker)
                {
                    Error = $"Card not recognised. card sent is {card}";
                    FinalScore = 0;
                    return;
                }
                if (hasJoker) jokerCount++;
                if (jokerCount > 2)
                {
                    Error = $"A hand cannot contain more than two Jokers";
                    FinalScore = 0;
                    return;
                }
                
                if(listOfProcessedCards.Count > 0)
                {
                    if (listOfProcessedCards.Any(x => x == card && x != "JR"))
                    {
                        Error = $"Cards cannot be duplicated. Duplicate card  is {card}.";
                        FinalScore = 0;
                        return;
                    }
                }
                listOfProcessedCards.Add(card);
                Scores.Add(score);
                FinalScore += score;                
            }
            if (jokerCount > 0)
                FinalScore *= 2;
        }
        public int GetScoreforACard(string cardCode, out bool isJoker)
        {
            isJoker = false;
            if (string.IsNullOrEmpty(cardCode)) return 0;
            if (cardCode.Length > 2) return 0;
            isJoker = cardCode == "JR";
            if (isJoker) return 0;
            char faceValueType = cardCode[0];
            char cardType = cardCode[1];
            int score = 0;
            int outValue = 0;

            int faceValue = faceValueType switch
            {
                'T' => 10,
                'J' => 11,
                'Q' => 12,
                'K' => 13,
                'A' => 14,
                _ => int.TryParse(faceValueType.ToString(), out outValue) switch
                {
                    false => 0,
                    _ => outValue
                }
            };
            if (faceValue < 2 || faceValue > 14) return 0;
            int cardTypeValue = cardType switch
            {
                'C' => 1,
                'D' => 2,
                'H' => 3,
                'S' => 4,
                _ => 0
            };
            if (cardTypeValue == 0) return 0;
            score = faceValue * cardTypeValue;
            return score;
        }
    }
}
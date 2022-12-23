namespace AppConsole
{
    public class Reversal : Card, IActionable
    {
        public bool[] Actions {get; set;}
        public string Id{ get; private set; }
        public Reversal() : base("Reversal", new string[]{"purple"}, 0)
        {
            this.Actions = new bool[] {false, false, true, false, false, false};
            this.Id = GameUtils.CreateId();
        }
        private void GiveActions();
        private void SaveCards(int index);
        public void ExecuteGetDeck(IPlayer a)
        {
            List<Card> indexs = new List<Card>();
            for(int i=0; i<2; i++)
            {
                indexs[i] = a.SelectCardDeck();
            }

            while(indexs.Count>0)
            {
                GameUtils.Move(a.Table.Deck, a.Table.DiscardPile, indexs[indexs.Count-1]);
                indexs.RemoveAt(indexs.Count-1);
            }
        }
        private void Attack(int index,IPlayer a);
        private void Trash(Bank bank, IPlayer a);
        private void GainCard(Bank bank, IPlayer a);

        /*Informacion de la carta:
        1. Coge dos cartas del deck
        */
    }
}
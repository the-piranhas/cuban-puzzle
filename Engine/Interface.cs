namespace engine_cuban_puzzle;

public interface IPlayer
{
    public string Name { get; }
    public TablePlayer Table { get; set; }
    public bool Exit();
    public bool ExistIActionable();
    public int SelectCardDeck();
    public int SelectCardDiscardPile();
    public int SelectCardHand();
    public int SelectCardOnGoing();
    public BankCard SelectCardBank();
    public void ExecuteAction(IActionable card);
    public int SelectGem();
    public IPlayer SelectPlayer();
    public BankCard PlayBuyPhase();
    public bool PlayNextBuyPhases();
}
public interface IActionable
{
    public void Action();
}

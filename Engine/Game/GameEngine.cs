namespace engine_cuban_puzzle;
using System.Collections.Generic;

public static class GameEngine
{
    public static int CantActionsPerTurn = 1;
    public static int CantMoneyPerTurn = 0;
    public static List<IPlayer> Players;
    public static GameTurns Turns;
    public static Bank bank;

    public static IPlayer PlayGame(List<IPlayer> players,Bank b)
    {
        Players = players;
        Turns = new GameTurns(players);
        bank = b;
        while(true)
        {
            Turns.MoveNext();
            Turns.Current.Table.ToGemPile(bank.Get(0));
            ActionPhase(Turns.Current);
            BuyPhase(Turns.Current);
            CantActionsPerTurn = 1;
            CantMoneyPerTurn = 0;

            if( CleanUpPhase(Turns.Current) >= 10 )
            {
                int cantmingems = int.MaxValue;
                int cantgems;
                List<IPlayer> winplayers=new List<IPlayer>();

                foreach(IPlayer a in players)
                {
                    cantgems = a.Table.CantGem();

                    if(cantgems<cantmingems)
                    {
                        cantmingems = cantgems;
                        winplayers.Clear();
                        winplayers.Add(a);
                    }
                    else if(cantgems == cantmingems) winplayers.Add(a);
                }
                if(winplayers.Count == 1)
                {
                    return winplayers[0];
                }

                return PlayGame(winplayers,bank);
            }
        }
    }

    public static void ActionPhase(IPlayer a)
    {
        bool RealiceAction = false;
        while( CantActionsPerTurn > 0 )
        {
            GamePrint.PrintTable(Players);
            if(a.Exit()) return;
            else{
                GamePrint.PrintTable(Players);
                if(a.ExistIActionable())//commit
                {
                    int index =a.SelectCardHand();
                    if(!(a.Table.HandCards[index] is Gem1 || a.Table.HandCards[index] is Gem2 || a.Table.HandCards[index] is Gem3 || a.Table.HandCards[index] is Gem4))
                    {
                        a.Table.HandToOnGoing(index);//moverla hacia el ongoing     
                        GamePrint.PrintTable(Players);
                        RealiceAction = true;
                        a.ExecuteAction((IActionable)a.Table.OnGoing[a.Table.OnGoing.Count-1]);//escogiendo y ejecutando accion de la carta
                    }
                    else
                    {
                        System.Console.WriteLine("Ha seleccionado gemas que no tienen acciones.");
                    }
                }
            }

            if(RealiceAction){ RealiceAction = false; CantActionsPerTurn--;}
        }
    }

    public static void BuyPhase(IPlayer a)
    {
        CantMoneyPerTurn += a.Table.CantMoneyBuyPhases();
        if ( CantMoneyPerTurn <= 0 )
        {
            a.Table.ToDiscardPile(bank.GetCant(bank.keys[7],1-CantMoneyPerTurn));     
        }
        else
        {
            while( CantMoneyPerTurn > 0 )
            {
                BankCard BuyCard = a.PlayBuyPhase();

                if(BuyCard == null)
                {
                    continue;
                }

                if(CantMoneyPerTurn-BuyCard.Cost >= 0)
                {
                    BuyCard = bank.Get(BuyCard);
                    if(BuyCard != null)
                    {
                        a.Table.ToOnGoing((Card) BuyCard);
                        CantMoneyPerTurn -= BuyCard.Cost;
                    }
                }

                if(a.PlayNextBuyPhases()) break;
            }
        }
    }

    public static int CleanUpPhase(IPlayer a)
    {
        return a.Table.CleanUp();
    }
}

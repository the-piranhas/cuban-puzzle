namespace engine_cuban_puzzle;
using System;
public class HeroCard_Node
{
    public string Text;
    public string Color;
    public int Money;
    private Dictionary<string,string> BasicPropieties{ get; set;}
    public Interpreter Action {get;private set;}

    public HeroCard_Node(string text)
    {
        this.Text = text.ToLower();
        BasicPropieties = new Dictionary<string, string>();
        ReadPropieties();
        this.Color = BasicPropieties["color"];
        try
        {
            this.Money = int.Parse(BasicPropieties["money"]);
        }
        catch
        {
            throw new System.Exception("Se esperaba un numero en la propiedad money.");
        }
    }

    private void ReadPropieties()
    {
        int color = Text.IndexOf("color");
        if(color == -1) throw new Exception("No esta definida la propiedad color.");
        BasicPropieties.Add("color",ReadAssing(color+5));

        int money = Text.IndexOf("money");
        if(money == -1) throw new Exception("No esta definida la propiedad money.");
        BasicPropieties.Add("money",ReadAssing(money+5));

    }
    private string ReadAssing(int n)
    {
        string result = "";

        n = SkipSpaces(n);

        if(Text[n] != '=') throw new Exception("Se esperaba =.");

        n++;

        n = SkipSpaces(n);

        if(!(IsInt(Text[n])|| IsLetter(Text[n]))) throw new Exception("Se espera la entrada de algun valor.");

        while(IsInt(Text[n])|| IsLetter(Text[n]))
        {
            result += Text[n];
            n++;
        }

        n = SkipSpaces(n);

        if(Text[n] != ';') throw new Exception("Se esperaba ;.");

        return result;
    }
    private int SkipSpaces(int n)
    {
        while(Text[n] == ' ')
        {
            n++;
        }
        return n;
    }
    private bool IsInt(char caracter)
    {
        return (caracter >= 48 && caracter <= 57);
    }
    private bool IsLetter(char caracter)
    {
        return (caracter >= 97 && caracter <= 122);
    }
}

public class ActionCard_Node
{
    public string Text;
    public int Cost;
    public string Color;
    public int Money;
    private Dictionary<string,string> BasicPropieties{ get; set;}
    public Interpreter Action { get; private set; }

    public ActionCard_Node(string text)
    {
        this.Text = text;
        BasicPropieties = new Dictionary<string, string>();
        ReadPropieties();
        try
        {
            this.Money = int.Parse(BasicPropieties["money"]);
        }
        catch
        {
            throw new System.Exception("Se esperaba un numero en la propiedad money.");
        }

        try
        {
            this.Cost = int.Parse(BasicPropieties["cost"]);
        }
        catch 
        {
            throw new System.Exception("Se esperaba un numero en la propiedad cost.");
        }
    }
    private void ReadPropieties()
    {
        int cost = Text.IndexOf("cost");
        if(cost == -1) throw new Exception("No esta definida la propiedad cost.");
        BasicPropieties.Add("cost",ReadAssing(cost+4));

        int color = Text.IndexOf("color");
        if(color == -1) throw new Exception("No esta definida la propiedad color.");
        BasicPropieties.Add("color",ReadAssing(color+5));

        int money = Text.IndexOf("money");
        if(money == -1) throw new Exception("No esta definida la propiedad money.");
        BasicPropieties.Add("money",ReadAssing(money+5));
    }

    private string ReadAssing(int n)
    {
        string result = "";

        n = SkipSpaces(n);

        if(Text[n] != '=') throw new Exception("Se esperaba =.");

        n++;

        n = SkipSpaces(n);

        if(!(IsInt(Text[n])|| IsLetter(Text[n]))) throw new Exception("Se espera la entrada de algun valor.");

        while(IsInt(Text[n])|| IsLetter(Text[n]))
        {
            result += Text[n];
            n++;
        }

        n = SkipSpaces(n);

        if(Text[n] != ';') throw new Exception("Se esperaba ;.");
        
        return result;
    }
    private int SkipSpaces(int n)
    {
        while(Text[n] == ' ')
        {
            n++;
        }
        return n;
    }
    private bool IsInt(char caracter)
    {
        return (caracter >= 48 && caracter <= 57);
    }
    private bool IsLetter(char caracter)
    {
        return (caracter >= 97 && caracter <= 122);
    }
}
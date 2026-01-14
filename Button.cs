using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class QuestionButton : Button
{
    public QuestionButton(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    { }
    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            // invoke event
            Console.WriteLine($"{text}: Invoke event");
            GameState.currentMenu = Menu.VerifyAnswer;
            GameState.scheduleGroupDestruction.AddRange(tileGroup);
            if (correctQuestion)
            {
                GameState.questionState = QuestionState.correctAnswer;
                Console.WriteLine(text);
                Console.WriteLine("You answer correct!");
            }
            else
            {
                GameState.questionState = QuestionState.incorrectAnswer;
                Console.WriteLine(text);
                Console.WriteLine("You answer wrong");
                if (GameState.hearts > 0)
                {
                    GameState.hearts--;
                }
            }
        }
    }
}
public class Button : Container
{
    MouseState previousState;
    MouseState mState;
    public Rectangle Hitbox { get => new Rectangle((int)PositionTopLeft.X, (int)PositionTopLeft.Y, (int)Size.X, (int)Size.Y); }

    public string text;

    public bool correctQuestion = false; // 

    public List<int> tileGroup = [];
    public Button(string text, Vector2 Position, Vector2 Size)
    {
        this.text = text;
        this.PositionTopLeft = Position;
        this.Size = Size;
        Embed(new Text(text));
    }
    public void UpdateText(string text)
    {
        children.Clear();
        Embed(new Text(text));
    }
    public bool isClicked()
    {
        previousState = mState;
        mState = Mouse.GetState();
        return Hitbox.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released; // fix this
    }
    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            // invoke event
            Console.WriteLine($"{text}: Invoke event");

        }
    }
    public override void Draw(SpriteBatch sb, GameTime gameTime)
    {
        sb.Draw(Textures[Texture.pixel], Hitbox, Color.Gray);
        foreach (var child in children)
        {
            child.Draw(sb, gameTime);
        }
    }
}

public class StartButton : Button
{
    public StartButton(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            // invoke event
            Console.WriteLine($"{text}: Invoke event");
            GameState.currentMenu = Menu.LevelSelect;
        }
    }
}

public class BackToGame : Button
{
    public BackToGame(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }
    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            GameState.currentMenu = Menu.Main;
            Console.WriteLine("Going down south");
            GameState.CurrentHintText = "";
        }

    }
}

public class OptionButton : Button
{
    public OptionButton(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.currentMenu = Menu.Instruction;
        }
    }
}

public class CheckavostkiButton : Button
{
    public CheckavostkiButton(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.currentMenu = Menu.Hints;
        }
    }
}

public class hint1Button : Button
{
    public hint1Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.CurrentHintText = "Ciekawostka 1: Co powinno być w apteczce?\nDobrze wyposażona apteczka to podstawa pierwszej pomocy.\nWarto mieć w niej m.in. jałowe gaziki i bandaże, plastry,\nrękawiczki jednorazowe, nożyczki, chustę trójkątną, środek do dezynfekcji ran oraz koc ratunkowy(folię NRC).\nPamiętaj - co jakiś czas sprawdź,\nczy czegoś nie brakuje i czy leki nie są przeterminowane!";
        }
    }
}

public class hint2Button : Button
{
    public hint2Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.CurrentHintText = "Ciekawostka 2: 112 - jeden numer w całej Europie\nNumer alarmowy 112 działa w całej Unii Europejskiej.\nCo ważne, możesz go wybrać nawet wtedy, gdy nie masz pieniędzy na koncie,\ntelefon jest zablokowany albo nie masz zasięgu swojej sieci.\nW nagłych sytuacjach po prostu dzwoń - pomoc zawsze spróbuje dotrzeć.";
        }
    }
}

public class hint3Button : Button
{
    public hint3Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.CurrentHintText = "Ciekawostka 3: Najważniejsza zasada pierwszej pomocy\nZanim pomożesz innym, zadbaj o własne bezpieczeństwo.\nJeśli w pobliżu jest ogień, prąd albo ruch uliczny - najpierw usuń zagrożenie lub wezwij pomoc.\nPamiętaj: ranny ratownik nikomu nie pomoże.";
        }
    }
}

public class hint4Button : Button
{
    public hint4Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.CurrentHintText = "Ciekawostka 4: Jak sprawdzić, czy ktoś oddycha?\nAby sprawdzić oddech, pochyl się nad twarzą poszkodowanego.\nPrzez około 10 sekund słuchaj, czy słychać oddech, czuj go na swoim policzku i obserwuj, czy klatka piersiowa się unosi.\nJeśli nie ma oddechu - natychmiast wezwij pomoc i rozpocznij RKO.";
        }
    }
}

public class hint5Button : Button
{
    public hint5Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.CurrentHintText = "Ciekawostka 5: Zimny okład jest pomocny, ale z umiarem.\nPrzy stłuczeniach i skręceniach zimny okład może zmniejszyć ból i obrzęk.\nPamiętaj jednak, by nie przykładać lodu bezpośrednio do skóry, zawsze owiń go w ręcznik.\nChłodź miejsce przez 10-15 minut i w razie potrzeby powtórz po kilku godzinach.\nW nagłych sytuacjach nawet mrożony groszek z zamrażarki może się przydać.";
        }
    }
}
public class ExitButton : Button
{
    public ExitButton(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.kill = true;
        }
    }
}

public class level1Button : Button
{
    public level1Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
           // GameState.currentMenu = Menu.LevelSelect;
           GameState.currentMenu = Menu.None;
           GameState.LoadLevelDirective = 0;
        }
    }
}

public class level2Button : Button
{
    public level2Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.currentMenu = Menu.None;
            GameState.LoadLevelDirective = 1;
        }
    }
}

public class level3Button : Button
{
    public level3Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.currentMenu = Menu.None;
            GameState.LoadLevelDirective = 2;
        }
    }
}

public class level4Button : Button
{
    public level4Button(string text, Vector2 Position, Vector2 Size) : base(text, Position, Size)
    {
    }

    public override void Update(GameTime gameTime)
    {
        if (isClicked())
        {
            Console.WriteLine($"{text}: Invoke event");
            GameState.currentMenu = Menu.None;
            GameState.LoadLevelDirective = 3;
        }
    }
}
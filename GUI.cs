using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class GUI
{
    public static SpriteFont font;
    string answerString = "";
    public static Dictionary<Texture, Texture2D> Textures;
    Random Random = new Random();
    Point Resolution;
    //Container StartMenu;
    List<Button> startMenu = new();
    List<Button> instructionMenu = new();

    List<Button> hintsMenu = new();

    List<Button> levelMenu = new();
    Container Dialogue;

    public GUI(Point resolution)
    {
        Resolution = resolution;
        Console.WriteLine(resolution);
        //StartMenu = new Container(Vector2.Zero, Resolution.ToVector2());
        var size = new Vector2(220, 125);
        startMenu = [
            new StartButton("Start", new(355, 500), new(647-355, 665-500)),
            new OptionButton("Opcje", new(400, 350), size),
            new CheckavostkiButton("Ciekawostki", new(110, 350), size),
            new ExitButton("Wyjdz", new(700, 350), size)
        ];
        instructionMenu = [
            new BackToGame("WrocDoMenu", new(800, 630), size)
        ];
        hintsMenu = [
             new BackToGame("WrocDoMenu", new(700, 340), size),
             new hint1Button("hint1", new(125, 200), size),
             new hint2Button("hint2", new(420, 200), size),
             new hint3Button("hint3", new(700, 200), size),
             new hint4Button("hint4", new(120, 350), size),
             new hint5Button("hint5", new(415, 350), size)
        ];
        levelMenu = [
             new BackToGame("WrocDoMenu", new(400, 510), size),
             new level1Button("level1", new(260, 210), size),
             new level2Button("level2", new(550, 210), size),
             new level3Button("level3", new(260, 350), size),
             new level4Button("level4", new(550, 360), size)
        ];
        var margin = new Vector2(10, 10);
        Dialogue = new Container(
            new Vector2(40, Resolution.Y - 225), new Vector2(Resolution.X - 80, Resolution.Y / 4)
            );

        Dialogue.Embed(new Text("", new Vector2(Resolution.X / 2, 590)));
        Dialogue.Embed(new QuestionButton("", Dialogue.PositionTopLeft + new Vector2(0, Dialogue.Size.Y / 2) + margin, Dialogue.Size / 2 - margin * 2));
        Dialogue.Embed(new QuestionButton("", Dialogue.PositionTopLeft + margin + Dialogue.Size / 2, Dialogue.Size / 2 - margin * 2));

        // VerifyAnswer = new Container(new Vector2(100, 100), new Vector2(400, 100));
        // VerifyAnswer.Embed(new Text(""));

    }

    public void Update(GameTime gameTime)
    {
        if (GameState.questionState == QuestionState.initializeDialogue)
        { // if triggered a question event, setup dialogue interafce
            GameState.questionState = QuestionState.selectAnswer;

            Text text = Dialogue.children[0] as Text;
            QuestionButton leftButton = Dialogue.children[1] as QuestionButton;
            QuestionButton rightButton = Dialogue.children[2] as QuestionButton;

            var q = GameState.currentQuestion;

            text.text = q.step; // assign question

            if (Random.Next() % 2 == 0)
            {
                leftButton.UpdateText(q.correct);
                leftButton.correctQuestion = true;
                leftButton.tileGroup.Add(q.correctTile);
                rightButton.UpdateText(q.incorrect);
                rightButton.correctQuestion = false;
                rightButton.tileGroup.Add(q.incorrectTile);
                rightButton.tileGroup.Add(q.correctTile);
            }
            else
            {
                leftButton.UpdateText(q.incorrect);
                leftButton.correctQuestion = false;
                leftButton.tileGroup.Add(q.incorrectTile);
                leftButton.tileGroup.Add(q.correctTile);
                rightButton.UpdateText(q.correct);
                rightButton.correctQuestion = true;
                rightButton.tileGroup.Add(q.correctTile);
            }

        }
        switch (GameState.currentMenu)
        {
            case Menu.None:
                return;
            case Menu.Main:
                //StartMenu.Update(gameTime);
                foreach (var Button in startMenu)
                {
                    Button.Update(gameTime);
                }
                break;
            case Menu.LevelSelect:
                foreach (var Button in levelMenu)
                {
                    Button.Update(gameTime);
                }
                break;
            case Menu.Hints:
                foreach (var Button in hintsMenu)
                {
                    Button.Update(gameTime);
                }
                break;
            case Menu.Instruction:
                foreach (var Button in instructionMenu)
                {
                    Button.Update(gameTime);
                }
                break;
            case Menu.Dialogue:
                Dialogue.Update(gameTime);
                break;
            case Menu.VerifyAnswer:
                answerString = GameState.questionState == QuestionState.correctAnswer
                    ? "Poprawna odpowiedź.\nMożesz kontynuować właściwą ścieżką."
                    : "Nie tym razem!\nZły wybór sprawia, że zegar tyka szybciej...";
                // VerifyAnswer.Update(gameTime);
                if (GameState.timer.TimerBelow("verifyanswer", 0f))
                {
                    GameState.timer.RemoveTimer("verifyanswer");
                    GameState.questionState = QuestionState.none;
                    GameState.currentMenu = Menu.None;
                }
                else
                {
                    GameState.timer.StartTimer("verifyanswer", 5f);
                    break;
                }
                break;
            default:
                break;
        }
    }

    public void Draw(SpriteBatch sb, GameTime gameTime)
    {
        switch (GameState.currentMenu)
        {
            case Menu.None:
                return;
            case Menu.Main:
                //StartMenu.Draw(sb, gameTime);
                sb.Draw(Textures[Texture.main_menu], Vector2.Zero, Color.White);
                foreach (var Button in startMenu)
                {
                    //  Button.Draw(sb, gameTime);
                }
                break;
            case Menu.LevelSelect:
                sb.Draw(Textures[Texture.level_select], Vector2.Zero, Color.White);
                break;
            case Menu.Hints:
                sb.Draw(Textures[Texture.facts], Vector2.Zero, Color.White);
                sb.DrawString(font, GameState.CurrentHintText, new Vector2(580, 620), Color.Black);
                break;
            case Menu.Instruction:
                sb.Draw(Textures[Texture.instruction], Vector2.Zero, Color.White);
                foreach (var Button in instructionMenu)
                {
                    //  Button.Draw(sb, gameTime);
                }
                break;

            case Menu.Dialogue:
                sb.Draw(Textures[Texture.dialogue], Vector2.Zero, Color.White);
                Dialogue.Draw(sb, gameTime);
                break;
            case Menu.VerifyAnswer:
                var scale = 6;
                var t = GameState.questionState == QuestionState.correctAnswer ? Textures[Texture.happy_face] : Textures[Texture.sad_face];
                sb.Draw(t, new Rectangle(0, 0, t.Width * scale, t.Height * scale), Color.White);
                sb.DrawString(font, answerString, new Vector2(500, 160), Color.Black);
                //VerifyAnswer.Draw(sb, gameTime);
                break;
            default:
                break;
        }
    }
}

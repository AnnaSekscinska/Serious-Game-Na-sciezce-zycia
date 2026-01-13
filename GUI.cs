using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    Container StartMenu;
    List<Button> startMenu = new();
    Container Dialogue;

    public GUI(Point resolution)
    {
        Resolution = resolution;
        Console.WriteLine(resolution);
        StartMenu = new Container(Vector2.Zero, Resolution.ToVector2());

        startMenu = [
            new Button("Start", new(100, 100), new(200, 80)),
            new Button("Opcje", new(100, 200), new(200, 80)),
            new Button("Ciekawostki", new(100, 300), new(200, 80)),
            new Button("Wyjdz", new(100, 400), new(200, 80))
        ];
        var margin = new Vector2(10, 10);
        Dialogue = new Container(
            new Vector2(40, Resolution.Y - 225), new Vector2(Resolution.X - 80, Resolution.Y / 4)
            );

        Dialogue.Embed(new Text("", new Vector2(Resolution.X / 2, 590)));
        Dialogue.Embed(new Button("", Dialogue.PositionTopLeft + new Vector2(0, Dialogue.Size.Y / 2) + margin, Dialogue.Size / 2 - margin * 2));
        Dialogue.Embed(new Button("", Dialogue.PositionTopLeft + margin + Dialogue.Size / 2, Dialogue.Size / 2 - margin * 2));

        // VerifyAnswer = new Container(new Vector2(100, 100), new Vector2(400, 100));
        // VerifyAnswer.Embed(new Text(""));

    }

    public void Update(GameTime gameTime)
    {
        if (GameState.questionState == QuestionState.initializeDialogue)
        { // if triggered a question event, setup dialogue interafce
            GameState.questionState = QuestionState.selectAnswer;

            Text text = Dialogue.children[0] as Text;
            Button leftButton = Dialogue.children[1] as Button;
            Button rightButton = Dialogue.children[2] as Button;

            var q = GameState.currentQuestion;

            text.text = q.step; // assign question
            leftButton.questionButton = true;
            rightButton.questionButton = true;

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
                
                break;
            case Menu.LevelSelect:
                break;
            case Menu.Hints:
                break;
            case Menu.Options:
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
                StartMenu.Draw(sb, gameTime);
                break;
            case Menu.LevelSelect:
                break;
            case Menu.Hints:
                break;
            case Menu.Options:
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

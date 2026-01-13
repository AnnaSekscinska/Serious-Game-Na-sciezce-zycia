using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Serious_Game_Na_sciezce_zycia;

public class GUI
{
    Random Random = new Random();
    Point Resolution;
    Container StartMenu;
    Container Dialogue;
    Container VerifyAnswer;
    public GUI(Point resolution) {
        Resolution = resolution;
        Console.WriteLine(resolution);
        StartMenu = new Container(Vector2.Zero, Resolution.ToVector2());

        StartMenu.Embed(new Text("NA SCIEZCE ZYCIA", true));
        StartMenu.Embed(new StartButton("Start", new(100,100), new(200,80)));
        StartMenu.Embed(new Button("Opcje", new(100,200), new(200,80)));
        StartMenu.Embed(new Button("Ciekawostki", new(100,300), new(200,80)));
        StartMenu.Embed(new Button("Wyjdz", new(100,400), new(200,80)));
        var margin = new Vector2(10, 10);
        Dialogue = new Container(
            new Vector2(40, Resolution.Y-200), new Vector2(Resolution.X-80, Resolution.Y/4)
            );
        
        Dialogue.Embed(new Text(""));
        Dialogue.Embed(new Button("", Dialogue.PositionTopLeft + new Vector2(0, Dialogue.Size.Y / 2) + margin, Dialogue.Size/2 - margin*2));
        Dialogue.Embed(new Button("", Dialogue.PositionTopLeft+margin + Dialogue.Size/2, Dialogue.Size/2-margin*2));

        VerifyAnswer = new Container(new Vector2(100,100), new Vector2(400,100));
        VerifyAnswer.Embed(new Text(""));
    }

    public void Update(GameTime gameTime) {
        if (GameState.questionState == QuestionState.initializeDialogue) { // if triggered a question event, setup dialogue interafce
            GameState.questionState = QuestionState.selectAnswer;

            Text text          = Dialogue.children[0] as Text;
            Button leftButton  = Dialogue.children[1] as Button;
            Button rightButton = Dialogue.children[2] as Button;
            
            var q = GameState.currentQuestion;

            text.text = q.step; // assign question
            leftButton.questionButton = true;
            rightButton.questionButton = true;

            if (Random.Next() % 2 == 0) {
                leftButton.UpdateText(q.correct);
                leftButton.correctQuestion = true;
                leftButton.tileGroup.Add( q.correctTile);
                rightButton.UpdateText(q.incorrect);
                rightButton.correctQuestion = false;
                rightButton.tileGroup.Add(q.incorrectTile);
                rightButton.tileGroup.Add(q.correctTile);
            }
            else {
                leftButton.UpdateText(q.incorrect);
                leftButton.correctQuestion = false;
                leftButton.tileGroup.Add(q.incorrectTile);
                leftButton.tileGroup.Add(q.correctTile);
                rightButton.UpdateText(q.correct);
                rightButton.correctQuestion = true;
                rightButton.tileGroup.Add(q.correctTile); 
            }

        }
        switch (GameState.currentMenu) {
            case Menu.None:
                return;
            case Menu.Main:
                StartMenu.Update(gameTime);
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
                ((Text)VerifyAnswer.children[0]).text = GameState.questionState == QuestionState.correctAnswer 
                    ? "POPRAWNA ODPOWIEDZ!\nPoprawna droga się przed tobą otwiera!"
                    : "NIEPOPRAWNA ODPOWIEDZ\nPacjent ma coraz mniej czasu, a droga nie jest prostsza...";
                VerifyAnswer.Update(gameTime);
                if (GameState.timer.TimerBelow("verifyanswer", 0f)) {
                    GameState.timer.RemoveTimer("verifyanswer");
                    GameState.questionState = QuestionState.none;
                    GameState.currentMenu = Menu.None;
                }
                else {
                    GameState.timer.StartTimer("verifyanswer", 5f);
                    break;
                }
                break;
            default:
                break;
        }
    }

    public void Draw(SpriteBatch sb, GameTime gameTime) {
        switch (GameState.currentMenu) {
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
                Dialogue.Draw(sb, gameTime);
                break;
            case Menu.VerifyAnswer:
                VerifyAnswer.Draw(sb, gameTime);
                break;
            default:
                break;
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class Button : Container
{
    Rectangle Hitbox { get => new Rectangle((int)PositionTopLeft.X, (int)PositionTopLeft.Y, (int)Size.X, (int)Size.Y); }
    string text;
    public bool backToGame = false;
    public bool correctQuestion = false;
    public bool questionButton = false;
    public List<int> tileGroup = [];
    public Button(string text, Vector2 Position, Vector2 Size) {
        this.text = text;
        this.PositionTopLeft = Position;
        this.Size = Size;
        Embed(new Text(text));
    }
    public void UpdateText(string text) {
        children.Clear();
        Embed(new Text(text));
    }

    public override void Update(GameTime gameTime) {
        var mState = Mouse.GetState();
        if (Hitbox.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed) {
            // invoke event
            Console.WriteLine($"{text}: Invoke event");
            Console.WriteLine(questionButton);
            if (questionButton) { // this should have been solved with class overrides
                GameState.currentMenu = Menu.VerifyAnswer;
                GameState.scheduleGroupDestruction.AddRange(tileGroup);
                if (correctQuestion) {
                    GameState.questionState = QuestionState.correctAnswer;
                    Console.WriteLine(text);
                    Console.WriteLine("You answer correct!");
                }
                else {
                    GameState.questionState = QuestionState.incorrectAnswer;
                    Console.WriteLine(text);
                    Console.WriteLine("You answer wrong");
                    if (GameState.hearts > 0) {
                        GameState.hearts--;
                    }
                }
            }
            if (backToGame) {
                GameState.currentMenu = Menu.None;
            }
        }
    }
    public override void Draw(SpriteBatch sb, GameTime gameTime) {
        sb.Draw(Textures[Texture.pixel], Hitbox, Color.Gray);
        foreach (var child in children) {
            child.Draw(sb, gameTime);
        }
    }
}

public class StartButton : Container
{
    Rectangle Hitbox { get => new Rectangle((int)PositionTopLeft.X, (int)PositionTopLeft.Y, (int)Size.X, (int)Size.Y); }
    string text;
    public StartButton(string text, Vector2 Position, Vector2 Size) {
        this.text = text;
        this.PositionTopLeft = Position;
        this.Size = Size;
        Embed(new Text(text));
    }
    public override void Update(GameTime gameTime) {
        var mState = Mouse.GetState();
        if (Hitbox.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed) {
            // invoke event
            Console.WriteLine($"{text}: Invoke event");
        }
    }
    public override void Draw(SpriteBatch sb, GameTime gameTime) {
        sb.Draw(Textures[Texture.pixel], Hitbox, Color.Gray);
        foreach (var child in children) {
            child.Draw(sb, gameTime);
        }
    }
}


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Serious_Game_Na_sciezce_zycia;


public class EndZone : Zone
{
    public EndZone(Vector2 position, Point size) : base(null, null, position, size) {
        group = [115];
    }
    public override void Update(GameTime gameTime) {
        if (Collider.Contains(GameState.PlayerPosition)) {
            GameState.currentMenu = Menu.WinLevel;
            GameState.scheduleGroupDestruction.AddRange(group);
            Console.WriteLine("endzone");
        }
    }
}

public class Zone : GameObject
{
    Question question;
    Color color = new Color(255, 255, 255, 128);
    public Zone(List<int> group, Question question, Vector2 position, Point size) : base(position, size) {
        this.group = group;
        this.question = question;
        hasCollision = false;
    }
    public override void Update(GameTime gameTime) {
        if (Collider.Contains(GameState.PlayerPosition)) {
            GameState.currentMenu = Menu.Dialogue;
            GameState.currentQuestion = question;
            GameState.questionState = QuestionState.initializeDialogue;
            GameState.scheduleGroupDestruction.AddRange(group);
        }
    }
    public override void Draw(SpriteBatch sb, GameTime gameTime) {
        sb.Draw(Textures[Texture.pixel], new Rectangle((int)position.X, (int)position.Y, size.X, size.Y), new Color(240,120,120,120));
        // sb.DrawString(Font, $"{position}", position, Color.Black);
    }
}

public class Tile : GameObject
{
    public static int scale = 1;
    public static int tileWidth = 101;
    public static int tileHeight = 164;
    public static int tileWidthScaled = tileWidth / scale;
    public static int tileHeightScaled = tileHeight / scale;
    public static int offsetX = 100 / scale;
    public static int offsetY = 70 / scale;

    public Color color;
    public Tile(Vector2 position, Point size, Vector2 colliderPositionOffset, Point colliderSize, Color color, List<int> group)
        : base(position, size, colliderPositionOffset, colliderSize) {
        this.group = group;
        this.color = color;
        foreach (int i in group) {
            Console.Write("tile");
            Console.Write($" {i} \n");
        }

    }

    public override void Draw(SpriteBatch sb, GameTime gameTime) {
        sb.Draw(Textures[Texture.single], new Rectangle((int)position.X, (int)position.Y, tileWidthScaled, tileHeightScaled), new Rectangle(0, 0, tileWidth, tileHeight), color);
        //sb.Draw(Textures[Serious_Game_Na_sciezce_zycia.Texture.pixel], Collider, Color.Yellow);
        // sb.DrawString(Font, $"{position}", position, Color.Black);
    }
}

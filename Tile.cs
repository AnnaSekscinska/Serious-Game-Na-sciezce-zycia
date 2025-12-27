using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Serious_Game_Na_sciezce_zycia;

public class Tile : GameObject
{
    public static int scale = 1;
    public static int tileWidth = 101;
    public static int tileHeight = 164;
    public static int tileWidthScaled = tileWidth / scale;
    public static int tileHeightScaled = tileHeight / scale;
    public static int offsetX = 100 / scale;
    public static int offsetY = 70 / scale;
    public Tile(Vector2 position, Point size, Vector2 colliderPositionOffset, Point colliderSize) : base(position, size, colliderPositionOffset, colliderSize) {
    }

    public override void Draw(SpriteBatch sb, GameTime gameTime) {
        sb.Draw(Textures[Texture.single], new Rectangle((int)position.X, (int)position.Y, tileWidthScaled, tileHeightScaled), new Rectangle(0, 0, tileWidth, tileHeight), Color.Orange);
        sb.DrawString(Font, $"{position}", position, Color.Black);
    }
}

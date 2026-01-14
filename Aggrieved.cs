using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Serious_Game_Na_sciezce_zycia;

class Aggrieved : GameObject
{
    public Aggrieved(Vector2 position, Point size) : base(position, size)
    {
        
    }

    public override void Draw(SpriteBatch sb, GameTime gameTime)
    {
        //base.Draw(sb, gameTime);
        Texture2D t;
        
        t = GameState.hearts > 0?  Textures[Serious_Game_Na_sciezce_zycia.Texture.dude_alive]: Textures[Serious_Game_Na_sciezce_zycia.Texture.dude_dead];
        sb.Draw(t, new Rectangle((int)position.X, (int)position.Y, t.Width,t.Height), Color.White);
    }

    
}
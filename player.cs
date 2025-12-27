using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Serious_Game_Na_sciezce_zycia;

public class Player : GameObject
{
    public Player(Vector2 position, Point size, Vector2 colliderPositionOffset, Point colliderSize) : base(position, size, colliderPositionOffset, colliderSize)
    {

    }

    public override void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();


        if (keyboardState.IsKeyDown(Keys.Down))
        {
            velocity = new Vector2(0, 2);
        }
        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            velocity = new Vector2(0, -2);
        }
        else if (keyboardState.IsKeyDown(Keys.Left))
        {
            velocity = new Vector2(-2, 0);
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            velocity = new Vector2(2, 0);
        }

        base.Update(gameTime);
    }

}
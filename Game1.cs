using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Serious_Game_Na_sciezce_zycia;

public class Aaa
{
    public int x;
    int y;
    float aaa;
}

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D pixel;
    private Rectangle pixelRect;
    private Vector2 pixelPosition;
    private Vector2 pixelVelocity;
    private Rectangle collider = new(100, 20, 70, 90);

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new Color[] { Color.White });
        pixelPosition = new Vector2(0, 0);
        pixelRect = new Rectangle(0, 0, 10, 15);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var keyboardState = Keyboard.GetState();
        
        
        if (keyboardState.IsKeyDown(Keys.Down))
        {
            pixelVelocity = new Vector2(0, 2);
        }
        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            pixelVelocity = new Vector2(0, -2);
        }
        else if (keyboardState.IsKeyDown(Keys.Left))
        {
            pixelVelocity = new Vector2(-2, 0);
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            pixelVelocity = new Vector2(2, 0);
        }


        if (!collider.Intersects(new Rectangle(
                pixelRect.X + (int)pixelVelocity.X,
                pixelRect.Y + (int)pixelVelocity.Y,
                pixelRect.Width, pixelRect.Height
            )
            )) {
            pixelPosition += pixelVelocity;
        }
        pixelVelocity = Vector2.Zero;
        pixelRect = new Rectangle((int)pixelPosition.X, (int)pixelPosition.Y, 10, 15);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();



        _spriteBatch.Draw(pixel, collider, Color.Yellow);
        _spriteBatch.Draw(pixel, pixelRect, Color.Red);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

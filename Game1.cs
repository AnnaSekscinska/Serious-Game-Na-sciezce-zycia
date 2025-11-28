using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Serious_Game_Na_sciezce_zycia;


public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    static Texture2D pixel;

    Player player = new(new(0, 0), new(10, 15));
    GameObject rect1 = new(new(100, 20), new(70, 90));

    List<GameObject> gameObjects = new();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        gameObjects.Add(player);
        gameObjects.Add(rect1);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        pixel = new Texture2D(GraphicsDevice, 1, 1);
        pixel.SetData(new Color[] { Color.White });
        GameObject.pixel = pixel;
        player.position = new Vector2(0, 0);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var gameObj in gameObjects)
        {
            gameObj.Update(gameTime);
        }

        foreach (var gameObj in gameObjects)
        {
            if (gameObj.id == player.id) { continue; }
            if (player.rect.Intersects(gameObj.rect))
            {
                player.position -= player.velocity;
            }
        }
        foreach (var gameObj in gameObjects)
        {
            gameObj.physicsUpdate(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        foreach (var gameObj in gameObjects)
        {
            gameObj.Draw(_spriteBatch, gameTime);
        }



        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

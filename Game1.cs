using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Serious_Game_Na_sciezce_zycia;



public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private static readonly Dictionary<Texture, Texture2D> Textures = [];

    Point Resolution = new Point { X = 1024, Y = 768 };
    Player player = new(new Vector2(110, 450), new Point(50, 90), new Vector2(0,40), new Point(50));
    GameObject rect1 = new(new(100, 20), new(70, 90));
    Camera camera;
    Viewport viewport;
    SpriteFont font;
    List<GameObject> gameObjects = new();
    Level level;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        viewport = new Viewport(0, 0, Resolution.X, Resolution.Y);
        _graphics.PreferredBackBufferWidth = Resolution.X;
        _graphics.PreferredBackBufferHeight = Resolution.Y;
        _graphics.ApplyChanges();
        camera = new Camera(viewport, player);
        gameObjects.Add(player);
        gameObjects.Add(rect1);
        level = new Level(gameObjects);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Textures[Texture.pixel] = new Texture2D(GraphicsDevice, 1, 1);
        Textures[Texture.pixel].SetData(new Color[] { Color.White });
        Textures[Texture.single] = Content.Load<Texture2D>("cropped_tile");
        Textures[Texture.bump] = Content.Load<Texture2D>("bump");
        font = Content.Load<SpriteFont>("default");
        GameObject.Textures = Textures;
        GameObject.Font = font;
        Level.Textures = Textures;
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
            if (player.Collider.Intersects(gameObj.Collider))
            {
                player.position -= player.velocity;
            }
        }
        foreach (var gameObj in gameObjects)
        {
            gameObj.physicsUpdate(gameTime);
        }
        camera.UpdateCamera(viewport);
        // sort objects by Y axis
        gameObjects.Sort((a,b)=> a.position.Y.CompareTo(b.position.Y));

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Brown);
        _spriteBatch.Begin(transformMatrix: camera.Transform);

        
        level.Draw(_spriteBatch, gameTime);

        //_spriteBatch.Draw(Textures[Texture.single], new Rectangle(0,0,456/8,712/8), Color.White);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
    Player player = new(new Vector2(3150, 4250), new Point(50, 90), new Vector2(0,40), new Point(50));
    Camera camera;
    Viewport viewport;
    SpriteFont font;
    SpriteFont title;
    List<GameObject> gameObjects = new();
    Level level;
    GUI gui;

    int fps;
    int frameCounter;
    double elapsedTime;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        viewport = new Viewport(0, 0, Resolution.X, Resolution.Y);
        // IsFixedTimeStep = false;
        // _graphics.SynchronizeWithVerticalRetrace = false;
        _graphics.PreferredBackBufferWidth = Resolution.X;
        _graphics.PreferredBackBufferHeight = Resolution.Y;
        _graphics.ApplyChanges();
        camera = new Camera(viewport, player);
        gameObjects.Add(player);
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
        title = Content.Load<SpriteFont>("default");
        GameObject.Textures = Textures;
        GameObject.Font = font;
        Level.Textures = Textures;
        Container.Textures = Textures;
        Container.font = font;
        Container.title = title;
        gui = new GUI(Resolution);
    }

    protected override void Update(GameTime gameTime)
    {
        elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        frameCounter++;

        if (elapsedTime >= 1) {
            fps = frameCounter;
            frameCounter = 0;
            elapsedTime = 0;
        }


        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var gameObj in gameObjects)
        {
            gameObj.Update(gameTime);
        }

        foreach (var gameObj in gameObjects)
        {
            if (gameObj.id == player.id) { continue; }
            if (player.Collider.Intersects(gameObj.Collider) && gameObj.hasCollision)
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
        gui.Update(gameTime);


        // remove objects
        for (int i = gameObjects.Count - 1; i >= 0; i--) {
            if (gameObjects[i].destroy || gameObjects[i].group.Intersect(GameState.scheduleGroupDestruction).Any()) {
                gameObjects.RemoveAt(i);
            }
        }
        if (GameState.scheduleGroupDestruction.Any()) {
            Console.Write($"delete schedule list: ");
        }
        foreach (var delet in GameState.scheduleGroupDestruction) {
            Console.Write($" {delet} ");
        }
        if (GameState.scheduleGroupDestruction.Any()) {
            Console.Write($"\n");
        }
        GameState.scheduleGroupDestruction.Clear();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Brown);
        _spriteBatch.Begin(transformMatrix: camera.Transform);
        

        level.Draw(_spriteBatch, gameTime);

        //_spriteBatch.Draw(Textures[Texture.single], new Rectangle(0,0,456/8,712/8), Color.White);
        _spriteBatch.End();
        _spriteBatch.Begin();

        _spriteBatch.DrawString( font, $"FPS: {fps}", new Vector2(10, 10), Color.White );

        gui.Draw( _spriteBatch, gameTime);
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

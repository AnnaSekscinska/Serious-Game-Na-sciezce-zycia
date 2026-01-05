using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class GameObject
{
    private static int globalId = 0;
    public int id;
    public List<int> group = [];
    public bool destroy = false;
    public static Dictionary<Texture, Texture2D> Textures;
    public static SpriteFont Font;
    public Point size;
    public Vector2 position;
    public bool hasCollision = true;
    public Vector2 colliderPositionOffset;
    public Point colliderSize;
    public Vector2 velocity;
    public Rectangle Collider
    {
        get => new Rectangle((colliderPositionOffset + position).ToPoint(), colliderSize);
    }
    public Rectangle DrawDestination
    {
        get => new Rectangle(position.ToPoint(), size);
    }
    public GameObject(Vector2 position, Point size, Vector2 colliderPositionOffset, Point colliderSize)
    {
        id = globalId++;
        this.position = position;
        this.size = size;
        this.colliderPositionOffset = colliderPositionOffset;
        this.colliderSize = colliderSize;
    }
    public GameObject(Vector2 position, Point size) {
        id = globalId++;
        this.position = position;
        this.size = size;
        this.colliderPositionOffset = Vector2.Zero;
        this.colliderSize = size;
    }

    public virtual void Update(GameTime gameTime)
    {
        
    }
    public virtual void physicsUpdate(GameTime gameTime)
    {
        velocity = Vector2.Zero;
    }

    public virtual void Draw(SpriteBatch sb, GameTime gameTime)
    {
        sb.Draw(Textures[Texture.pixel], DrawDestination, Color.Black);
    }
}
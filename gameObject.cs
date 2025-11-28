using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameObject
{
    private static int globalId = 0;
    public int id;
    public static Texture2D pixel;
    public Point size;
    public Vector2 position;
    public Vector2 velocity;
    public Rectangle rect
    {
        get => new Rectangle(position.ToPoint(),size);
    }

    public GameObject(Vector2 position, Point size)
    {
        id = globalId++;
        this.position = position;
        this.size = size;
    }

    public virtual void Update(GameTime gameTime)
    {
        position += velocity;
    }
    public virtual void physicsUpdate(GameTime gameTime)
    {
        velocity = Vector2.Zero;
    }

    public virtual void Draw(SpriteBatch sb, GameTime gameTime)
    {
        sb.Draw(pixel, rect, Color.Red);
    }
}
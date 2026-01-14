using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class Text : Container
{
    Vector2? SetPos;
    public string text;
    SpriteFont f = font;
    public new Vector2 PositionTopLeft
    {
        get
        {
            if (SetPos != null)
            {
                return SetPos.GetValueOrDefault() - Size / 2;
            }
            return parent.PositionTopLeft + (parent.Size - Size) / 2;
        }
    }
    public new Vector2 Size { get => f.MeasureString(text); }
    public Text(string text, Vector2 SetPos, bool isTitle = false)
    {
        this.SetPos = SetPos;
        this.text = text;
        if (isTitle)
        {
            f = title;
        }
    }
    public Text(string text, bool isTitle = false)
    {
        this.text = text;
        if (isTitle)
        {
            f = title;
        }
    }
    public override void Draw(SpriteBatch sb, GameTime gameTime)
    {
        sb.DrawString(f, text, PositionTopLeft, Color.Black);
    }
}

public class Container
{
    public Container parent;
    public static Dictionary<Texture, Texture2D> Textures;
    public Vector2 PositionTopLeft { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 PositionCenter { get => new Vector2(PositionTopLeft.X + Size.X / 2, PositionTopLeft.Y + Size.Y / 2); }

    public List<Container> children;

    // fonts
    public static SpriteFont font;
    public static SpriteFont title;

    public Container(Vector2 Position, Vector2 Size)
    { // static
        children = new List<Container>();
        this.PositionTopLeft = Position;
        this.Size = Size;
    }

    public Container()
    { // dynamic
        children = new List<Container>();
    }
    public void Embed(Container element)
    {
        element.parent = this;
        //var offset = Layout.GetOffset(element, this);
        //element.PositionTopLeft = PositionTopLeft + offset;
        children.Add(element);
    }
    public virtual void Update(GameTime gameTime)
    {

        foreach (var child in children)
        {
            child.Update(gameTime);
        }
    }
    public virtual void Draw(SpriteBatch sb, GameTime gameTime)
    {
        //sb.Draw(Textures[Texture.pixel], new Rectangle((int)PositionTopLeft.X, (int)PositionTopLeft.Y, (int)Size.X, (int)Size.Y), Color.Blue);
        foreach (var child in children)
        {
            child.Draw(sb, gameTime);
        }
    }

}


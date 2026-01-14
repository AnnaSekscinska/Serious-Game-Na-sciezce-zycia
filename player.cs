using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Serious_Game_Na_sciezce_zycia;

public class Player : GameObject
{
    static int width = 80;
    static int height = 120;
    AnimatedTexture mForward;
    AnimatedTexture mSide;
    AnimatedTexture mBack;
    AnimatedTexture currentAnim;
    bool flipX = false;
    float speed = 10f;
    int animationSpeed = 9;
    Vector2 origin = new Vector2(width / 2, height / 2);
    float rot = 0f;
    float scale = 1f;
    KeyboardState prevState;
    KeyboardState curState;
    public Player(Point size, Vector2 colliderPositionOffset, Point colliderSize, ContentManager content) : base(Vector2.Zero, size, colliderPositionOffset, colliderSize)
    {

        mForward = new AnimatedTexture(origin, rot, scale);
        mForward.Load(content, "sprite_forward", 3, animationSpeed);
        mBack = new AnimatedTexture(origin, rot, scale);
        mBack.Load(content, "sprite_back", 3, animationSpeed);
        mSide = new AnimatedTexture(origin, rot, scale);
        mSide.Load(content, "sprite_side", 3, animationSpeed);


        currentAnim = mForward;
    }
    public override void Draw(SpriteBatch sb, GameTime gameTime)
    {
        //base.Draw(sb, gameTime);
        //sb.Draw(Textures[Serious_Game_Na_sciezce_zycia.Texture.spriteBoy], DrawDestination, Color.White);


        currentAnim.DrawFrame(sb, position + new Vector2(40, 40), flipX);
        // sb.Draw(Textures[Serious_Game_Na_sciezce_zycia.Texture.pixel], Collider, Color.Yellow);
    }

    public override void Update(GameTime gameTime)
    {
        if (GameState.isMenu) { return; }
        prevState = curState;
        curState = Keyboard.GetState();
        if (curState.IsKeyDown(Keys.Q))
        {
            GameState.currentMenu = Menu.Main;
        }

        if (curState.IsKeyDown(Keys.Down))
        {
            velocity = new Vector2(0, speed);
            currentAnim = mForward;
            flipX = false;
        }
        else if (curState.IsKeyDown(Keys.Up))
        {
            velocity = new Vector2(0, -speed);
            currentAnim = mBack;
            flipX = false;
        }
        else if (curState.IsKeyDown(Keys.Left))
        {
            velocity = new Vector2(-speed, 0);
            currentAnim = mSide;
            flipX = false;
        }
        else if (curState.IsKeyDown(Keys.Right))
        {
            velocity = new Vector2(speed, 0);
            currentAnim = mSide;
            flipX = true;
        }
        if (curState.IsKeyDown(Keys.Space) && prevState.IsKeyUp(Keys.Space))
        {
            AnimatedTexture.color = AnimatedTexture.color == Color.Purple ? Color.Silver : Color.Purple;
        }
        if (curState.IsKeyDown(Keys.Q) && prevState.IsKeyUp(Keys.Q))
        {
            GameState.currentMenu = Menu.Main;
        }

        if (velocity != Vector2.Zero)
        {
            currentAnim.Play();
        }
        else
        {
            currentAnim.Reset();
        }

        currentAnim.UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);

        position += velocity;
        GameState.PlayerPosition = position;
    }

}
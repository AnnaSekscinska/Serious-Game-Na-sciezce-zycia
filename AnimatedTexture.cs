
namespace Serious_Game_Na_sciezce_zycia;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


public class AnimatedTexture
{
    // Number of frames in the animation.
    private int frameCount;

    // The animation spritesheet.
    private Texture2D myTexture;

    // The number of frames to draw per second.
    private float timePerFrame;

    private int frame; // which frame is being shown

    // Total amount of time the animation has been running.
    private float totalElapsed;

    // Is the animation currently running?
    private bool isPaused;
    public bool isFlipHorizontal;
    public bool isFlipVertical;

    // The current rotation, scale and draw depth for the animation.
    public float Rotation, Scale;

    // The origin point of the animated texture.
    public Vector2 Origin;

    public Rectangle FirstFrame; // ff

    public static Color color = Color.Purple;

    public AnimatedTexture(Vector2 origin, float rotation, float scale) {
        this.Origin = origin;
        this.Rotation = rotation;
        this.Scale = scale;
    }

    public void Load(ContentManager content, string asset, int frameCount, int framesPerSec) {
        this.frameCount = frameCount;
        myTexture = content.Load<Texture2D>(asset);
        timePerFrame = (float)1 / framesPerSec;
        frame = 0;
        totalElapsed = 0;
        isPaused = false;
    }

    public void UpdateFrame(float elapsed) {
        if (isPaused)
            return;
        totalElapsed += elapsed;
        if (totalElapsed > timePerFrame) {
            frame++;
            // Keep the Frame between 0 and the total frames, minus one.
            frame %= frameCount;
            totalElapsed -= timePerFrame;
        }
    }

    public void DrawFrame(SpriteBatch batch, Vector2 screenPos, bool flipX) {
        DrawFrame(batch, frame, screenPos, flipX);
    }

    public void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos, bool flipX) {
        int FrameWidth = myTexture.Width / frameCount;
        Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
            FrameWidth, myTexture.Height);
        batch.Draw(
            myTexture, screenPos, sourcerect, color,
            Rotation, Origin, Scale,
            flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 
            0);
    }

    public bool IsPaused
    {
        get { return isPaused; }
    }

    public void Reset() {
        frame = 0;
        totalElapsed = 0f;
    }

    public void Stop() {
        Pause();
        Reset();
    }

    public void Play() {
        isPaused = false;
    }

    public void Pause() {
        isPaused = true;
    }
}

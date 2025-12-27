using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Serious_Game_Na_sciezce_zycia;

public class Camera
{
    private GameObject followObj;
    public float Zoom { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle Bounds { get; protected set; }
    public Rectangle VisibleArea { get; protected set; }
    public Matrix Transform { get; protected set; }
    public Matrix InverseViewMatrix { get; protected set; }
    private float currentMouseWheelValue, previousMouseWheelValue;

    public Camera(Viewport viewport, GameObject followObject) {
        Bounds = viewport.Bounds;
        followObj = followObject;
        Zoom = 1f;
        Position = new Vector2(viewport.Width / 2, viewport.Height / 2);
    }

    private void UpdateMatrix() {
        Vector3 translation = new Vector3(
                    Bounds.Width / (2 * Zoom) - followObj.position.X,
                    Bounds.Height / (2 * Zoom) - followObj.position.Y, 0f);
        Transform = Matrix.CreateTranslation(translation)
                    * Matrix.CreateScale(Zoom);
        InverseViewMatrix = Matrix.Invert(Transform);
    }
    public Vector2 ToWorld(Vector2 ScreenPosition) {
        return Vector2.Transform(ScreenPosition, InverseViewMatrix);
    }

    public void AdjustZoom(float zoomAmount) {
        Zoom += zoomAmount;
        if (Zoom < 1f) {
            Zoom = 1f;
        }
        if (Zoom > 5f) {
            Zoom = 5f;
        }
    }

    public void UpdateCamera(Viewport bounds) {
        Bounds = bounds.Bounds;
        UpdateMatrix();


        previousMouseWheelValue = currentMouseWheelValue;
        currentMouseWheelValue = Mouse.GetState().ScrollWheelValue;

        if (currentMouseWheelValue > previousMouseWheelValue) {
            AdjustZoom(.05f);
        }

        if (currentMouseWheelValue < previousMouseWheelValue) {
            AdjustZoom(-.05f);
        }

    }
}
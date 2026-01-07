using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class Level
{
    public static Dictionary<Texture, Texture2D> Textures;
    public List<GameObject> gameObjects;
    public int currentLevelId = 0;
    public Level(List<GameObject> gameObjects) {
        this.gameObjects = gameObjects;
        ConvertLevelToGameObjects();
    }

    private void ConvertLevelToGameObjects() {
        var width = MapData.mapWidth;
        var height = MapData.mapHeight;
        var map = MapData.map;
        var questionSet = MapData.questionair;
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                var a = map[j + i * width];
                var position = new Vector2(Tile.offsetX * j, (Tile.offsetY * i));
                if (a == '#') {
                    gameObjects.Add(new Tile(
                            position: position,
                            size: new Point(),
                            colliderPositionOffset: new Vector2(0, 70),
                            colliderSize: new Point(100, 94),
                            color: Color.Orange,
                            group: []
                            )
                        );
                }
                else if (char.IsLower(a)) {
                    //Console.WriteLine($"{a} and {a-'0'}");
                    gameObjects.Add(new Tile(
                        group: new List<int> { questionSet[a - 'a'].correctTile },
                        position: position,
                        size: new Point(),
                        colliderPositionOffset: new Vector2(0, 70),
                        colliderSize: new Point(100, 94),
                        color: Color.Green
                        )
                    );
                }
                else if (char.IsUpper(a)) { // incorrect tile (should be the same colour)
                    //Console.WriteLine($"{a} and {a - '0'}");
                    gameObjects.Add(new Tile(
                        group: new List<int> { questionSet[a - 'A'].incorrectTile },
                        position: position,
                        size: new Point(),
                        colliderPositionOffset: new Vector2(0, 70),
                        colliderSize: new Point(100, 94),
                        color: Color.Red
                        )
                    );
                }
                else if (char.IsNumber(a)) {
                    gameObjects.Add(new Zone(
                        group: new List<int> { a - '0' },
                        question: questionSet[a - '0'],
                        position: position,
                        size: new Point(100, 94)
                        )
                    );

                }
            }
        }
    }

    private void DrawLevel(SpriteBatch sb, GameTime gameTime) {
        var scale = 1;
        var tileWidth = 100;
        var tileHeight = 164;
        var tileWidthScaled = tileWidth / scale;
        var tileHeightScaled = tileHeight / scale;
        var offsetX = 100 / scale;
        var offsetY = 70 / scale;

        var width = MapData.mapWidth;
        var height = MapData.mapHeight;
        var map = MapData.map;

        // FLOR draw  TODO: create a single texture for potential performance improvement
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                var position = new Point(offsetX * j, (offsetY * i));
                sb.Draw(Textures[Texture.bump], new Rectangle(position.X, position.Y, offsetX, offsetY), Color.Brown);
            }
        }
        // GAMEOBJECTS draw



    }

    public void Draw(SpriteBatch sb, GameTime gameTime) {
        DrawLevel(sb, gameTime);
        foreach (var gameObj in gameObjects) {
            gameObj.Draw(sb, gameTime);
        }
    }
}
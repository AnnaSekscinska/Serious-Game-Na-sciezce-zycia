using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class Level
{
    public static Dictionary<Texture, Texture2D> Textures;
    public List<GameObject> gameObjects;
    public int currentLevelId = 0;
    public Map currentLevel;
    Player player;
    public Level(List<GameObject> gameObjects, Player player)
    {
        this.gameObjects = gameObjects;
        this.player = player;
        LoadLevel();
    }

    public void LoadLevel()
    {

        ConvertLevelToGameObjects();
        GameState.hearts = 3;
    }

    private void ConvertLevelToGameObjects()
    {
        currentLevel = MapData.GetLevel(currentLevelId);
        GameState.currentScenario = currentLevel.scenario;
        gameObjects.Clear();
        gameObjects.Add(player);
        var width = currentLevel.mapWidth;
        var height = currentLevel.mapHeight;
        var map = currentLevel.map;
        var questionSet = currentLevel.questionair;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var a = map[j + i * width];
                var position = new Vector2(Tile.offsetX * j, (Tile.offsetY * i));
                if (a == '#')
                {
                    gameObjects.Add(new Tile(
                        position: position,
                        size: new Point(),
                        colliderPositionOffset: new Vector2(0, 70),
                        colliderSize: new Point(100, 94),
                        color: Color.Green,
                        group: []
                        )
                    );
                }
                else if (a == '@')
                { // this a player
                    player.position = position;
                    GameState.PlayerPosition = position;
                }
                else if (a == 'x')
                { // finish zone
                    gameObjects.Add(new EndZone(
                        position: position,
                        size: new Point(100, 94)
                        )
                    );
                }
                else if (a == '%')
                { // dead or alive person
                    gameObjects.Add(new Aggrieved(
                        position: position,
                        size: new Point(100, 94)
                        )
                    );
                }
                else if (char.IsLower(a))
                {
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
                else if (char.IsUpper(a))
                { // incorrect tile (should be the same colour)
                    //Console.WriteLine($"{a} and {a - '0'}");
                    gameObjects.Add(new Tile(
                        group: new List<int> { questionSet[a - 'A'].incorrectTile },
                        position: position,
                        size: new Point(),
                        colliderPositionOffset: new Vector2(0, 70),
                        colliderSize: new Point(100, 94),
                        color: Color.Green
                        )
                    );
                }
                else if (char.IsNumber(a))
                {
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

    private void DrawLevel(SpriteBatch sb, GameTime gameTime)
    {
        var offsetX = 100;
        var offsetY = 70;

        var width = currentLevel.mapWidth;
        var height = currentLevel.mapHeight + 2;
        // FLOR draw 
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var position = new Point(offsetX * j, offsetY * i);
                sb.Draw(Textures[Texture.bump], new Rectangle(position.X, position.Y, offsetX, offsetY), Color.SandyBrown);
            }
        }
        // GAMEOBJECTS draw



    }

    public void Draw(SpriteBatch sb, GameTime gameTime)
    {
        DrawLevel(sb, gameTime);
        foreach (var gameObj in gameObjects)
        {
            gameObj.Draw(sb, gameTime);
        }
    }
}
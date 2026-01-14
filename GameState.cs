using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public static class GameState
{
    public static string currentScenario;
    public static int LoadLevelDirective = -1;
    public static string CurrentHintText = "";
    public static bool kill = false;
    public static bool isWin = false;
    public static int hearts = 3;
    public static Menu currentMenu = Menu.Main;
    public static bool isMenu { get => currentMenu != Menu.None; }
    public static Vector2 PlayerPosition = new Vector2(0, 0);

    // question events
    public static Question currentQuestion;
    public static QuestionState questionState;

    public static List<int> scheduleGroupDestruction = [];

    public static TimerService timer = new TimerService();
}

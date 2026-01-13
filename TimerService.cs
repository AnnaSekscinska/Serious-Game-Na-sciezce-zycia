using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Serious_Game_Na_sciezce_zycia;

public class TimerService {
    private Dictionary<string, float> timers;

    public TimerService() {
        timers = new Dictionary<string, float>();
    }

    public void StartTimer(string name, float time) {
        if (!timers.ContainsKey(name)) {
            timers.Add(name, time);
            Console.WriteLine("add timer");
        }
    }

    public bool TimerExists(string name) {
        return timers.ContainsKey(name);
    }
    public void RemoveTimer(string name) {
        timers.Remove(name);
    }

    public bool TimerBelow(string name, float x) {
        if (timers.ContainsKey(name)) {
            if (timers[name] < x) {
                return true;
            }
        }
        return false;
    }

    public void Update(GameTime gameTime) {
        var s = (float)gameTime.ElapsedGameTime.TotalSeconds;
        foreach (var name in timers.Keys) {
            timers[name] -= s;
        }
    }


}

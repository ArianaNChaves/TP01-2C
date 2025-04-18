using System.Collections.Generic;
using UnityEngine;

public static class AnimationConstants
{
    public static readonly Dictionary<string, string> NPCAnimations = new Dictionary<string, string>()
    {
        { "Idle", "Idle" },
        { "Attacked", "Attacked" }
    };

    public static readonly Dictionary<string, string> EnemyAnimations = new Dictionary<string, string>()
    {
        { "Idle", "Idle" },
        { "Walk", "Walk" },
        { "Attack", "Attack" },
        { "Death", "Enemy_Death" }
    };
}

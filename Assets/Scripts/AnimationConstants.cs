using System.Collections.Generic;
using UnityEngine;

public static class AnimationConstants
{
    public static readonly Dictionary<string, string> NPCAnimations = new Dictionary<string, string>()
    {
        { "Idle", "Player_Idle" },
        { "Attacked", "Player_Attacked" }
    };

    public static readonly Dictionary<string, string> EnemyAnimations = new Dictionary<string, string>()
    {
        { "Idle", "Enemy_Idle" },
        { "Walk", "Enemy_Walk" },
        { "Attack", "Enemy_Attack" },
        { "Death", "Enemy_Death" }
    };
}

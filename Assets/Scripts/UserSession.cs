using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserSession
{
    internal static string username;
    internal static float totalScore;
    internal static int levelsCompleted;
    internal static List<float> highScores = new();
    internal static List<int> maxStars = new();
    internal static bool isSfxEnabled = true;
    internal static bool isBgmEnabled = true;
    internal static float bgmVolume = 0.2f;
    internal static float sfxVolume = 0.6f;
    internal static float levelGenerationSeed;
    
    internal static void ClearSession()
    {
        username = string.Empty;
        totalScore = 0.0f;
        levelsCompleted = 0;
        highScores.Clear();
        maxStars.Clear();
        isSfxEnabled = true;
        isBgmEnabled = true;
        bgmVolume = 0.2f;
        sfxVolume = 0.6f;
    }
    
    internal static void ResetSession()
    {
        totalScore = 0.0f;
        levelsCompleted = 0;
        highScores.Clear();
        maxStars.Clear();
    }
}

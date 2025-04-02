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
    
    internal static void ClearSession()
    {
        username = string.Empty;
        totalScore = 0.0f;
        levelsCompleted = 0;
        highScores.Clear();
        maxStars.Clear();
    }
    
    internal static void ResetSession()
    {
        totalScore = 0.0f;
        levelsCompleted = 0;
        highScores.Clear();
        maxStars.Clear();
    }
}

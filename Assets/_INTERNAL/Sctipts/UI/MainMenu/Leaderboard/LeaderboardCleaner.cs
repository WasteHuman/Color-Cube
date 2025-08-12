﻿using Core.Data;
using System;
using UnityEngine;
using YG;

namespace UI.MainMenu.Leaderboard
{
    public class LeaderboardCleaner
    {
        public void CleanLeaderboard(LeaderboardYG leaderboard)
        {
            long serverTime = YG2.ServerTime() / 1000;
            DateTime currentServerTime = DateTimeOffset.FromUnixTimeSeconds(serverTime).UtcDateTime;

            string saved = PlayerPrefs.GetString(PlayerPrefsConsts.LAST_LEADERBOARD_RESET, "");

            if (DateTime.TryParse(saved, out DateTime lastReset))
            {
                if ((currentServerTime - lastReset).TotalDays >= 7)
                {
                    PlayerPrefs.SetInt(PlayerPrefsConsts.BEST_SCORE, 0);
                    leaderboard.ResetLeaderboard();
                    PlayerPrefs.SetString(PlayerPrefsConsts.LAST_LEADERBOARD_RESET, currentServerTime.ToString("O"));
                }
            }
            else
            {
                PlayerPrefs.SetString(PlayerPrefsConsts.LAST_LEADERBOARD_RESET, currentServerTime.ToString("O"));
            }
        }
    }
}
using System;
using System.Collections.Generic;
//using Microsoft.Xna.Framework.GamerServices;

namespace Sexy
{
	
	internal static class ReportAchievement
	{

		public static event ReportAchievement.AchievementHandler AchievementsChanged;

		public static int EarnedGamerScore { get; private set; }

		public static int MaxGamerScore { get; private set; }

		
		public static void Initialise()
		{
			//SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(ReportAchievement.GamerSignedInCallback);
		}

		
		//private static void GamerSignedInCallback(object sender, SignedInEventArgs args)
		//{
		//}

		
		public static bool GiveAchievement(AchievementId achievement)
		{
			return ReportAchievement.GiveAchievement(achievement, false);
		}

		
		public static bool GiveAchievement(AchievementId achievement, bool forceGive)
		{
			if (!forceGive && ReportAchievement.pendingAchievements.Contains(achievement))
			{
				return false;
			}
			if (Achievements.GetAchievementKey(achievement) == null)
			{
				return false;
			}
			lock (ReportAchievement.achievementLock)
			{
				if (!ReportAchievement.pendingAchievements.Contains(achievement))
				{
					ReportAchievement.pendingAchievements.Add(achievement);
				}
				if (SexyAppBase.IsInTrialMode)
				{
					ReportAchievement.pendingAchievementAlerts.Enqueue(new TrialAchievementAlert(achievement));
				}
			}
			if (ReportAchievement.AchievementsChanged != null)
			{
				ReportAchievement.AchievementsChanged();
			}
			return true;
		}

		
		public static void GivePendingAchievements()
		{
			if (ReportAchievement.pendingAchievementAlerts.Count > 0)
			{
				GlobalStaticVars.gSexyAppBase.ShowAchievementMessage(ReportAchievement.pendingAchievementAlerts.Dequeue());
			}
		}

		
		public static void StartGetAchievements()
		{
			if (ReportAchievement.AchievementsChanged != null)
			{
				ReportAchievement.AchievementsChanged();
			}
		}

		
		private static Queue<TrialAchievementAlert> pendingAchievementAlerts = new Queue<TrialAchievementAlert>(10);

		
		public static object achievementLock = new object();

		
		private static List<AchievementId> pendingAchievements = new List<AchievementId>();

		
		private static ReportAchievement.GameState gamestate = ReportAchievement.GameState.WaitingForSignIn;


		public delegate void AchievementHandler();

		
		private enum GameState
		{
			
			Error,
			
			WaitingForSignIn,
			
			WaitingForAchivements,
			
			Ready
		}
	}
}

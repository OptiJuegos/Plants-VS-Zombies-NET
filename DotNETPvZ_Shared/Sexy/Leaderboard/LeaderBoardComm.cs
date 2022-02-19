using System;
using System.Collections.Generic;
//using Microsoft.Xna.Framework.GamerServices;

namespace Sexy
{
	
	internal static class LeaderBoardComm
	{
		public static LeaderBoardComm.ConnectionState State { get; private set; } = LeaderBoardComm.ConnectionState.Connecting;

		public static event LeaderBoardComm.LoadingCompletedHandler LoadingCompleted;

		public static Image UnknownPlayerImage
		{
			get
			{
				return null;
			}
		}

		
		public static void RecordResult(LeaderboardGameMode gameMode, int score)
		{
			if (!SexyAppBase.UseLiveServers)
			{
				return;
			}
			if (SexyAppBase.IsInTrialMode)
			{
				return;
			}
			LeaderBoardHelper.IsModeSupported(gameMode);
		}

		
		public static bool IsPlayer(Object signedInGamer, int index, LeaderboardState state)
		{
			return index == 10;
		}

		
		public static void LoadInitialLeaderboard()
		{
			for (int i = 0; i < 3; i++)
			{
				LeaderBoardComm.LoadResults((LeaderboardGameMode)i);
			}
		}

		
		public static int GetMaxEntries(LeaderboardState state)
		{
			LeaderBoardLoader loader = LeaderBoardComm.GetLoader(state);
			return loader.LeaderboardEntryCount;
		}

		
		public static Image GetGamerImage(Object gamer)
		{
			return LeaderBoardComm.UnknownPlayerImage;
		}

		
		public static Image GetLeaderboardGamerImage(int index, LeaderboardState state)
		{
			return LeaderBoardComm.UnknownPlayerImage;
		}

		
		public static Object GetLeaderboardGamer(int index, LeaderboardState state)
		{
			return null;
		}

		
		public static int GetSignedInGamerIndex(LeaderboardState state)
		{
			LeaderBoardLoader loader = LeaderBoardComm.GetLoader(state);
			return loader.SignedInGamerIndex;
		}

		
		private static LeaderBoardLoader GetLoader(LeaderboardState state)
		{
			return LeaderBoardComm.leaderboardLoaders[LeaderBoardHelper.GetLeaderboardNumber(state)];
		}

		
		private static LeaderBoardLoader GetLoader(LeaderboardGameMode mode)
		{
			return LeaderBoardComm.leaderboardLoaders[LeaderBoardHelper.GetLeaderboardNumber(mode)];
		}

		
		//private static LeaderboardEntry GetEntry(int index, LeaderboardState state)
		//{
		//	LeaderBoardLoader leaderBoardLoader = LeaderBoardComm.leaderboardLoaders[LeaderBoardHelper.GetLeaderboardNumber(state)];
		//	LeaderboardEntry result;
		//	if (!leaderBoardLoader.LeaderboardEntries.TryGetValue(index, ref result))
		//	{
		//		leaderBoardLoader.LoadEntry(index);
		//	}
		//	return result;
		//}

		
		public static long GetLeaderboardScore(int index, LeaderboardState state)
		{
			if (state == LeaderboardState.Adventure)
			{
				if (!LeaderBoardComm.classicScores.ContainsKey(index))
				{
					LeaderBoardComm.classicScores.Add(index, LeaderBoardComm.rand.Next(10000) + 100);
				}
				return (long)LeaderBoardComm.classicScores[index];
			}
			if (state == LeaderboardState.IZombie)
			{
				if (!LeaderBoardComm.izombieScores.ContainsKey(index))
				{
					LeaderBoardComm.izombieScores.Add(index, LeaderBoardComm.rand.Next(100000) + 1000);
				}
				return (long)LeaderBoardComm.izombieScores[index];
			}
			if (state == LeaderboardState.Vasebreaker)
			{
				if (!LeaderBoardComm.vasebreakerScores.ContainsKey(index))
				{
					LeaderBoardComm.vasebreakerScores.Add(index, LeaderBoardComm.rand.Next(100000) + 1000);
				}
				return (long)LeaderBoardComm.vasebreakerScores[index];
			}
			return 0L;
		}

		
		public static void SetCache(LeaderboardGameMode gameMode)
		{
			LeaderBoardLoader loader = LeaderBoardComm.GetLoader(gameMode);
			loader.CACHE_DURATION = 10;
		}

		
		public static int LoadResults(LeaderboardGameMode gameMode)
		{
			if (!SexyAppBase.UseLiveServers || LeaderBoardComm.State == LeaderBoardComm.ConnectionState.CannotConnect)
			{
				if (LeaderBoardComm.State == LeaderBoardComm.ConnectionState.CannotConnect && (DateTime.UtcNow - LeaderBoardComm.cannotConnectSince).TotalSeconds > 2147483647.0)
				{
					LeaderBoardComm.State = LeaderBoardComm.ConnectionState.Connecting;
				}
				return -2;
			}
			return 50;
		}

		
		private const int cannotConnectDelay = 2147483647;

		
		public static object LeaderboardLock = new object();

		
		private static DateTime cannotConnectSince;

		
		private static string[] columnIndexStrings = new string[]
		{
			"SCORE"
		};

		
		private static LeaderBoardLoader[] leaderboardLoaders = new LeaderBoardLoader[3];

		
		private static Dictionary<string, Image> gamerImages = new Dictionary<string, Image>();

		
		private static string e;

		
		private static List<Object> loadingGamers = new List<Object>();

		
		private static Dictionary<int, int> classicScores = new Dictionary<int, int>();

		
		private static Dictionary<int, int> izombieScores = new Dictionary<int, int>();

		
		private static Dictionary<int, int> vasebreakerScores = new Dictionary<int, int>();

		
		private static Random rand = new Random();

		
		public enum ConnectionState
		{
			
			Connected,
			
			Connecting,
			
			CannotConnect
		}

		
		private enum LeaderboardMode
		{
			
			Adventure,
			
			IZombie,
			
			Vasebreaker,
			
			MAX
		}

		
		private enum ColumnIndices
		{
			
			Score
		}

		public delegate void LoadingCompletedHandler();
	}
}

using System;
using System.Collections.Generic;

namespace Sexy
{
	
	internal static class Achievements
	{
		
		public static AchievementItem GetAchievementItem(AchievementId index)
		{
			return Achievements.gAchievementList[(int)index];
		}

		
		public static string GetAchievementKey(AchievementId index)
		{
			AchievementItem achievementItem = Achievements.GetAchievementItem(index);
			if (achievementItem == null)
			{
				return null;
			}
			return achievementItem.Key;
		}

		
		public static void ClearAchievements()
		{
			foreach (AchievementItem achievementItem in Achievements.gAchievementList)
			{
				achievementItem.Dispose();
			}
			Achievements.gAchievementList.Clear();
		}

		
		public static int GetNumberOfAchievements()
		{
			return Achievements.gAchievementList.Count;
		}

		
		public static void AddAchievement(AchievementItem item)
		{
			Achievements.gAchievementList.Add(item);
		}

		
		static Achievements()
		{
			Achievements.CreateNonSPAAchievements();
		}

		
		public static void CreateNonSPAAchievements()
		{
			Achievements.gAchievementList.Clear();
			Achievements.gAchievementList.Add(new AchievementItem("Home Lawn Security", "Complete Adventure Mode.", AtlasResources.IMAGE_ACHIEVEMENT_ICON_HOME_SECURITY, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Master of Mosticulture", "Collect all 49 plants.", AtlasResources.IMAGE_ACHIEVEMENT_ICON_MORTICULTURALIST, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Better Off Dead", "Better off Dead", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("China Shop", "China Shop", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Beyond the Grave", "Beyond the Grave", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Crash of the Titan", "Crash of the Titan", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Soil Your Plants", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Explodonator", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_EXPLODONATOR, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Close Shave", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Shopaholic", "Shopaholic", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Nom Nom Nom", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("No Fungus Among Us", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_NO_FUNGUS_AMONG_US, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Don't Pea in the Pool", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_DONT_PEA_IN_POOL, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Grounded", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_GROUNDED, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Good Morning", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_GOOD_MORNING, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Popcorn Party", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_POPCORN_PARTY, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Roll Some Heads", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_ROLL_SOME_HEADS, 25));
			Achievements.gAchievementList.Add(new AchievementItem("Disco is Undead", "Test Description 123456 Testing...... Description goes here and here, maybe here", AtlasResources.IMAGE_ACHIEVEMENT_ICON_SUNNY_DAYS, 25));
		}

		
		public static readonly string[] ACHIEVEMENT_KEYS;

		
		private static List<AchievementItem> gAchievementList = new List<AchievementItem>();
	}
}

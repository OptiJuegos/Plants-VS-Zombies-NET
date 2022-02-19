using System;

namespace Sexy
{
	
	internal class AchievementItem
	{
		
		public string Key { get; private set; }

		public bool IsEarned { get; private set; }

		public int GamerScore
		{
			get
			{
				return this.gamerScore;
			}
			protected set
			{
				this.gamerScore = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			protected set
			{
				this.name = value;
			}
		}


		public string Description
		{
			get
			{
				return this.description;
			}
			protected set
			{
				this.description = value;
			}
		}


		public Image AchievementImage
		{
			get
			{
				return this.achievementImage;
			}
			protected set
			{
				this.achievementImage = value;
			}
		}

		
		public AchievementItem(string name, string descr, Image img, int score)
		{
			this.Name = name;
			this.Description = descr;
			this.GamerScore = score;
			this.AchievementImage = img;
			this.Key = name;
		}

		
		public void Dispose()
		{
		}

		
		public static bool operator ==(AchievementItem a, AchievementItem b)
		{
			return ((a is null) && (b is null)) ? true : !((a is null) || (b is null)) && (a.Equals(b));
		}

		
		public static bool operator !=(AchievementItem a, AchievementItem b)
		{
			return !(a == b);
		}

		
		public override bool Equals(object obj)
		{
			if (!(obj is AchievementItem))
			{
				return false;
			}
			AchievementItem achievementItem = obj as AchievementItem;
			return achievementItem.Name == this.Name;
		}

		
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		
		public override string ToString()
		{
			return this.name;
		}

		
		private int gamerScore;

		
		private string name;

		
		private string description;

		
		private Image achievementImage;
	}
}

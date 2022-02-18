using System;

namespace Lawn
{
	
	internal class ZombiePicker
	{
		
		public int mZombieCount;

		
		public int mZombiePoints;

		
		public int[] mZombieTypeCount = new int[(int)ZombieType.NUM_ZOMBIE_TYPES];

		
		public int[] mAllWavesZombieTypeCount = new int[(int)ZombieType.NUM_ZOMBIE_TYPES];
	}
}

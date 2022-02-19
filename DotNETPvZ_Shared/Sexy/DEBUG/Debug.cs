using System;



namespace Sexy
{
	
	internal static class Debug
	{
		
		public static void OutputDebug<T>(T t)
		{
#if DEBUG
			Console.WriteLine(String.Format("[DEBUG] {0}", t.ToString()));
#endif
		}

		
		public static void OutputDebug<T1, T2>(T1 t1, T2 t2)
		{
#if DEBUG
			Console.WriteLine(String.Format("[DEBUG] {0}:{1}",t1.ToString(),t2.ToString()));
#endif
		}


		public static void ASSERT(bool value)
		{
#if DEBUG
			if (!value) 
			{
				throw new ApplicationException("[DEBUG] Assert failed.");
			}
			
#endif
		}
	}
}

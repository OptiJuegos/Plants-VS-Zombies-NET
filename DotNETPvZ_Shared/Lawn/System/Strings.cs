using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Lawn
{
	
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	[DebuggerNonUserCode]
	internal class Strings
	{
		
		internal Strings()
		{
		}

		
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Strings.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Lawn.Strings", typeof(Strings).Assembly);
					Strings.resourceMan = resourceManager;
				}
				return Strings.resourceMan;
			}
		}


		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Strings.resourceCulture;
			}
			set
			{
				Strings.resourceCulture = value;
			}
		}
	
		internal static string CANCEL
		{
			get
			{
				return "取消"; //Strings.ResourceManager.GetString("CANCEL", Strings.resourceCulture);
			}
		}

		internal static string CURRENCY_SYMBOL
		{
			get
			{
				return "";//Strings.ResourceManager.GetString("CURRENCY_SYMBOL", Strings.resourceCulture);
			}
		}

		internal static string NO
		{
			get
			{
				return "否";//Strings.ResourceManager.GetString("NO", Strings.resourceCulture);
			}
		}

		internal static string OK
		{
			get
			{
				return "好"; //Strings.ResourceManager.GetString("OK", Strings.resourceCulture);
			}
		}

		internal static string PEASHOOTER
		{
			get
			{
				return "豌豆射手";  //Strings.ResourceManager.GetString("PEASHOOTER", Strings.resourceCulture);
			}
		}

		internal static string SUNFLOWER
		{
			get
			{
				return "向日葵"; //Strings.ResourceManager.GetString("SUNFLOWER", Strings.resourceCulture);
			}
		}

		internal static string SUNFLOWER_TOOLTIP
		{
			get
			{
				return "为你提供更多阳光";//Strings.ResourceManager.GetString("SUNFLOWER_TOOLTIP", Strings.resourceCulture);
			}
		}

		internal static string TEST_STRING
		{
			get
			{
				return "加载中...";//return Strings.ResourceManager.GetString("TEST_STRING", Strings.resourceCulture);
			}
		}

		internal static string YES
		{
			get
			{
				return "是";//Strings.ResourceManager.GetString("YES", Strings.resourceCulture);
			}
		}

		internal static string ZOMBIES_KILLED
		{
			get
			{
				return "消灭僵尸"; //Strings.ResourceManager.GetString("ZOMBIES_KILLED", Strings.resourceCulture);
			}
		}


		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;
	}
}

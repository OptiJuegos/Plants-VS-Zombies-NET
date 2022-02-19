using System;
using System.Globalization;
using System.Diagnostics;
using Lawn; 
//using Microsoft.Phone.Info;
//using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sexy
{
	public class Main : Game
	{
		private void GameSpecificCheatInputCheck()
		{
		}

		private static void SetupForResolution()
		{
			Strings.Culture = CultureInfo.CurrentCulture;
			if (Strings.Culture.TwoLetterISOLanguageName == "fr")
			{
				Constants.Language = Constants.LanguageIndex.fr;
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "de")
			{
				Constants.Language = Constants.LanguageIndex.de;
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "es")
			{
				Constants.Language = Constants.LanguageIndex.es;
			}
			else if (Strings.Culture.TwoLetterISOLanguageName == "it")
			{
				Constants.Language = Constants.LanguageIndex.it;
			}
			else
			{
				Constants.Language = Constants.LanguageIndex.en;
			}
			//if ((Main.graphics.GraphicsDevice.PresentationParameters.BackBufferWidth == 480 && Main.graphics.GraphicsDevice.PresentationParameters.BackBufferHeight == 800) || (Main.graphics.GraphicsDevice.PresentationParameters.BackBufferWidth == 800 && Main.graphics.GraphicsDevice.PresentationParameters.BackBufferHeight == 480))
			{
				AtlasResources.mAtlasResources = new AtlasResources_480x800();
				//Constants.Load1080x1920();

				Constants.LoadVirtual_480x800();
				return;
			}
			throw new Exception("Unsupported Resolution");
		}

		public Main()
		{
			Main.SetupTileSchedule();
			Main.graphics = Graphics.GetNew(this);
			//Main.SetLowMem();
			Main.graphics.IsFullScreen = false;
			//Guide.SimulateTrialMode = false;


			GraphicsState.mGraphicsDeviceManager.SupportedOrientations = Constants.SupportedOrientations;
			GraphicsState.mGraphicsDeviceManager.DeviceCreated += new EventHandler<EventArgs>(this.graphics_DeviceCreated);
			GraphicsState.mGraphicsDeviceManager.DeviceReset += new EventHandler<EventArgs>(this.graphics_DeviceReset);
			GraphicsState.mGraphicsDeviceManager.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(this.mGraphicsDeviceManager_PreparingDeviceSettings);
			base.TargetElapsedTime = TimeSpan.FromSeconds(1.0/60/*0.03333333333333333*/);
			base.Exiting += new EventHandler<EventArgs>(this.Main_Exiting);

			base.Window.AllowUserResizing = true;
			base.Window.ClientSizeChanged += new EventHandler<EventArgs>(this.OnResize);

			//PhoneApplicationService.Current.UserIdleDetectionMode = 0;
			//PhoneApplicationService.Current.Launching += new EventHandler<LaunchingEventArgs>(this.Game_Launching);
			//PhoneApplicationService.Current.Activated += new EventHandler<ActivatedEventArgs>(this.Game_Activated);
			//PhoneApplicationService.Current.Closing += new EventHandler<ClosingEventArgs>(this.Current_Closing);
			//PhoneApplicationService.Current.Deactivated += new EventHandler<DeactivatedEventArgs>(this.Current_Deactivated);
		}


		//private void Current_Deactivated(object sender, DeactivatedEventArgs e)
		//{
		//	GlobalStaticVars.gSexyAppBase.Tombstoned();
		//}


		//private void Current_Closing(object sender, ClosingEventArgs e)
		//{
		//	//PhoneApplicationService.Current.State.Clear();
		//}


		//private void Game_Activated(object sender, ActivatedEventArgs e)
		//{
		//}


		//private void Game_Launching(object sender, LaunchingEventArgs e)
		//{
		//	PhoneApplicationService.Current.State.Clear();
		//}

		public void OnResize(object sender, EventArgs e) 
		{
			
			int DefaultW = 800;
			int DefaultH = 480;
			Rectangle bounds = Window.ClientBounds;
			int W, H;
			/*if ((bounds.Height * DefaultW) >= (bounds.Width * DefaultH))
			{
				W = bounds.Width;
				H = (int)(DefaultH * bounds.Width / DefaultW);
            }
            else 
			{
				W = (int)(DefaultW * bounds.Height / DefaultH);
				H = bounds.Height;
			}*/
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				GlobalStaticVars.gSexyAppBase.mScreenScales.Init(bounds.Width, bounds.Height, DefaultW, DefaultH);
			}
			
			//GraphicsState.mGraphicsDeviceManager.PreferredBackBufferWidth = W;
			//GraphicsState.mGraphicsDeviceManager.PreferredBackBufferHeight = H;
			//GraphicsState.mGraphicsDeviceManager.ApplyChanges();
		}
		public static bool RunWhenLocked
		{
			get;
			//{
			//	return PhoneApplicationService.Current.ApplicationIdleDetectionMode == 1;
			//}
			set;
			//{
			//	try
			//	{
			//		PhoneApplicationService.Current.ApplicationIdleDetectionMode = (value ? 1 : 0);
			//	}
			//	catch
			//	{
			//	}
			//}
		}

		private static void SetupTileSchedule()
		{
		}

		private void mGraphicsDeviceManager_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
		{
		}

		private void graphics_DeviceReset(object sender, EventArgs e)
		{
		}

		private void graphics_DeviceCreated(object sender, EventArgs e)
		{
			base.GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
		}

		private void Main_Exiting(object sender, EventArgs e)
		{
			GlobalStaticVars.gSexyAppBase.AppExit();
		}

		protected override void Initialize()
		{
			this.IsMouseVisible = true;
			base.Window.OrientationChanged += new EventHandler<EventArgs>(this.Window_OrientationChanged);
			ReportAchievement.Initialise();
			base.Initialize();
			GlobalStaticVars.gSexyAppBase.mScreenScales.Init(1600, 960, 800, 480);
			Main.graphics.PreferredBackBufferWidth = 1600;
			Main.graphics.PreferredBackBufferHeight = 960;
			GraphicsState.mGraphicsDeviceManager.ApplyChanges();
			GlobalStaticVars.gSexyAppBase.mIMEHandler = new MonoGame.IMEHelper.WinFormsIMEHandler(this);
			GlobalStaticVars.gSexyAppBase.mIMEHandler.TextInput += (s, e) =>
			{
				Debug.OutputDebug<string>(String.Format("input:{0}", e.Character));
				GlobalStaticVars.gSexyAppBase.KeyChar(new SexyChar(e.Character));
			};
		}

		protected override void LoadContent()
		{
			GraphicsState.Init();
			Main.SetupForResolution();
			GlobalStaticVars.initialize(this);
			GlobalStaticVars.mGlobalContent.LoadSplashScreen();
			GlobalStaticVars.gSexyAppBase.StartLoadingThread();
		}

		protected override void UnloadContent()
		{
			GlobalStaticVars.mGlobalContent.cleanUp();
		}

		protected override void BeginRun()
		{
			base.BeginRun();
		}

		public void CompensateForSlowUpdate()
		{
			//base.ResetElapsedTime();
			if (_gameTimer != null)
			{
				_gameTimer.Reset();
				_gameTimer.Start();
			}
		}

		public static bool LOW_MEMORY_DEVICE { get; private set; }
		public static bool DO_LOW_MEMORY_OPTIONS { get; private set; }

		protected override void Update(GameTime gameTime)
		{
			while (_texturefuncs.Count > 0)
				_texturefuncs.Dequeue().RunSynchronously();

			if (!base.IsActive)
			{
				return;
			}
			if (GlobalStaticVars.gSexyAppBase.WantsToExit)
			{
				base.Exit();
			}
			this.HandleInput(gameTime);
			GlobalStaticVars.gSexyAppBase.UpdateApp(gameTime);
			if (!Main.trialModeChecked)
			{
				Main.trialModeChecked = true;
				bool flag = Main.trialModeCachedValue;
				//Main.SetLowMem();
				Main.trialModeCachedValue = false; // Guide.IsTrialMode;
				if (flag != Main.trialModeCachedValue && flag)
				{
					this.LeftTrialMode();
				}
			}

			base.Update(gameTime);
		}


		//private static void SetLowMem()
		//{
		//	object obj;
		//	DeviceExtendedProperties.TryGetValue("DeviceTotalMemory", ref obj);
		//	Main.DO_LOW_MEMORY_OPTIONS = (Main.LOW_MEMORY_DEVICE = ((long)obj / 1024L / 1024L <= 256L));
		//	Main.LOW_MEMORY_DEVICE = false;
		//}

		private void LeftTrialMode()
		{
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				GlobalStaticVars.gSexyAppBase.LeftTrialMode();
			}
			this.Window_OrientationChanged(null, null);
		}

		public static void SuppressNextDraw()
		{
			Main.wantToSuppressDraw = true;
		}


		//public static SignedInGamer GetGamer()
		//{
		//	if (Gamer.SignedInGamers.Count == 0)
		//	{
		//		return null;
		//	}
		//	return Gamer.SignedInGamers[PlayerIndex.One];
		//}

		public static void NeedToSetUpOrientationMatrix(UI_ORIENTATION orientation)
		{
			Main.orientationUsed = orientation;
			Main.newOrientation = true;
		}

		private static void SetupOrientationMatrix(UI_ORIENTATION orientation)
		{
			Main.newOrientation = false;
		}

		private void Window_OrientationChanged(object sender, EventArgs e)
		{
			this.SetupInterfaceOrientation();
		}

		private void SetupInterfaceOrientation()
		{
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				if (base.Window.CurrentOrientation == DisplayOrientation.LandscapeLeft || base.Window.CurrentOrientation == DisplayOrientation.LandscapeRight)
				{
					GlobalStaticVars.gSexyAppBase.InterfaceOrientationChanged(UI_ORIENTATION.UI_ORIENTATION_LANDSCAPE_LEFT);
					return;
				}
				GlobalStaticVars.gSexyAppBase.InterfaceOrientationChanged(UI_ORIENTATION.UI_ORIENTATION_PORTRAIT);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			if (Main.newOrientation)
			{
				Main.SetupOrientationMatrix(Main.orientationUsed);
			}
			
			lock (ResourceManager.DrawLocker)
			{

				base.GraphicsDevice.Clear(Color.Black);
				GlobalStaticVars.gSexyAppBase.DrawGame(gameTime);

				

				base.Draw(gameTime);

				
			}
		}

		public void HandleInput(GameTime gameTime)
		{
			if (LoadingScreen.IsLoading)
			{
				return;
			}
			GamePadState state = GamePad.GetState(PlayerIndex.One);
			if (state.Buttons.Back == ButtonState.Pressed && this.previousGamepadState.Buttons.Back == ButtonState.Released)
			{
				GlobalStaticVars.gSexyAppBase.BackButtonPress();
			}
			//TouchCollection state2 = TouchPanel.GetState();
			MouseState ms = Mouse.GetState();
			if (ms.LeftButton == ButtonState.Pressed && this.previousMouseState.LeftButton == ButtonState.Released)
			{
				_Touch touch = default(_Touch);
				touch.location.mX = GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchX(ms.X);
				touch.location.mY = GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchY(ms.Y);
				GlobalStaticVars.gSexyAppBase.TouchBegan(touch);

			}
			else if (ms.LeftButton == ButtonState.Released && this.previousMouseState.LeftButton == ButtonState.Pressed)
			{

				_Touch touch = default(_Touch);
				touch.location.mX = GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchX(ms.X);
				touch.location.mY = GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchY(ms.Y);
				GlobalStaticVars.gSexyAppBase.TouchEnded(touch);
			} else if(ms.X != this.previousMouseState.X && ms.Y != this.previousMouseState.Y 
				&& ms.LeftButton == ButtonState.Pressed && this.previousMouseState.LeftButton == ButtonState.Pressed)
			{
				_Touch touch = default(_Touch);
				touch.location.mX = GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchX(ms.X);
				touch.location.mY = GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchY(ms.Y);
				GlobalStaticVars.gSexyAppBase.TouchMoved(touch);
			}
			else if (ms.RightButton == ButtonState.Pressed && this.previousMouseState.RightButton == ButtonState.Released)
			{
				GlobalStaticVars.gSexyAppBase.BackButtonPress();
			}else if (ms.X != this.previousMouseState.X && ms.Y != this.previousMouseState.Y)
			{
				GlobalStaticVars.gSexyAppBase.MouseMove(
						(int)(GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchX(ms.X)),
						(int)(GlobalStaticVars.gSexyAppBase.mScreenScales.inverseMapTouchY(ms.Y)));
			}

			List<string> keynames = new List<string>();
			KeyboardState keys = Keyboard.GetState();
			foreach (Keys it in keys.GetPressedKeys()) 
			{
				if (previousKeyboardState.IsKeyUp(it)) 
				{
					keynames.Add(it.ToString());
				}
			}
			if (keynames.Count > 0)
				GlobalStaticVars.gSexyAppBase.UpdateKeys(keynames);


			this.previousMouseState = ms;
			this.previousGamepadState = state;
			this.previousKeyboardState = keys;
		}

		protected override void OnActivated(object sender, EventArgs args)
		{
			Main.trialModeChecked = false;
			if (GlobalStaticVars.gSexyAppBase != null)
			{
				GlobalStaticVars.gSexyAppBase.GotFocus();
				if (!GlobalStaticVars.gSexyAppBase.mMusicInterface.isStopped)
				{
					GlobalStaticVars.gSexyAppBase.mMusicInterface.ResumeMusic();
				}
			}
			base.OnActivated(sender, args);
		}

		protected override void OnDeactivated(object sender, EventArgs args)
		{
			GlobalStaticVars.gSexyAppBase.LostFocus();
			if (!GlobalStaticVars.gSexyAppBase.mMusicInterface.isStopped)
			{
				GlobalStaticVars.gSexyAppBase.mMusicInterface.PauseMusic();
			}
			GlobalStaticVars.gSexyAppBase.AppEnteredBackground();
			base.OnDeactivated(sender, args);
		}

		public static bool IsInTrialMode
		{
			get
			{
				return Main.trialModeCachedValue;
			}
		}

		private static SexyTransform2D orientationTransform;

		private static UI_ORIENTATION orientationUsed;

		private static bool newOrientation;

		public static bool trialModeChecked = false;

		private static bool trialModeCachedValue = true;

		internal static Graphics graphics;

		private int mFrameCnt;

		private static bool startedProfiler;

		private static bool wantToSuppressDraw;

		private GamePadState previousGamepadState = default(GamePadState);

		private MouseState previousMouseState = default(MouseState);

		private KeyboardState previousKeyboardState = default(KeyboardState);

		private Stopwatch _gameTimer = new Stopwatch();

		private static Queue<Task<Texture2D>> _texturefuncs = new 
			Queue<Task<Texture2D>>();

		internal static void Enqueue(Task<Texture2D> func)
		{
			if (func == null) return;
			_texturefuncs.Enqueue(func);
		}

		

	}
}

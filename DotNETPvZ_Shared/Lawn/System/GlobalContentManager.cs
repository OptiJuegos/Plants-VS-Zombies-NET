using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sexy;

namespace Lawn
{
	
	internal class GlobalContentManager
	{
		
		public GlobalContentManager(Main m)
		{
			this.main = m;
			this.content = this.main.Content;
			this.graphicsDevice = this.main.GraphicsDevice;
			this.content.RootDirectory = "Content";
		}

		
		public void cleanUp()
		{
		}

		
		public void initialize()
		{
			this.splash_sprite = new SpriteBatch(this.graphicsDevice);
			this.ui_sprite = new SpriteBatch(this.graphicsDevice);
		}

		
		public void LoadSplashScreen()
		{
			new ContentManager(this.main.Services);
		}

		
		public void LoadGameContent()
		{
			this.cursor_texture = this.content.Load<Texture2D>(".\\Cursor");
		}

		
		public void LoadFonts()
		{
			this.YaheiFont = this.content.Load<SpriteFont>("fonts\\Yahei");
			this.ArialFont = this.content.Load<SpriteFont>("fonts\\Arial");
		}

		
		public void LoadSounds()
		{
		}

		
		public virtual void LoadLevelBackdrops()
		{
		}

		
		public void SetLocalizeFont(Loc_Font selFont)
		{
			this.mSelFont = selFont;
		}

		
		public Loc_Font GetLocalizeFontId()
		{
			return this.mSelFont;
		}

		
		public SpriteFont GetLocalizedFont()
		{
			switch (this.mSelFont)
			{
			case Loc_Font.kArial:
				return this.ArialFont;
			case Loc_Font.kYahei:
				return this.YaheiFont;
			default:
				return this.YaheiFont;
			}
		}

		
		public int GetLocalizedFontSize()
		{
			switch (this.mSelFont)
			{
			case Loc_Font.kArial:
				return this.sizeArialFont;
			case Loc_Font.kYahei:
				return this.sizeYaheiFont;
			default:
				return this.sizeArialFont;
			}
		}

		
		public void SetLocalizedFontSize(int size)
		{
			switch (this.mSelFont)
			{
			case Loc_Font.kArial:
				this.sizeArialFont = size;
				return;
			case Loc_Font.kYahei:
				this.sizeYaheiFont = size;
				return;
			default:
				return;
			}
		}

		
		public float GetLocalizedFontScale()
		{
			switch (this.mSelFont)
			{
			case Loc_Font.kArial:
				return this.scaleArialFont;
			case Loc_Font.kYahei:
				return this.scaleYaheiFont;
			default:
				return 1f;
			}
		}

		
		public void SetLocalizedFontScale(float scale)
		{
			switch (this.mSelFont)
			{
			case Loc_Font.kArial:
				this.scaleArialFont = scale;
				return;
			case Loc_Font.kYahei:
				this.scaleYaheiFont = scale;
				return;
			default:
				return;
			}
		}

		
		public Main main;

		
		public ContentManager content;

		
		public GraphicsDevice graphicsDevice;

		
		public SpriteBatch splash_sprite;

		
		public SpriteBatch ui_sprite;

		
		public SpriteFont YaheiFont;

		
		public SpriteFont ArialFont;

		
		private Loc_Font mSelFont;

		
		private int sizeArialFont = 18;

		
		private int sizeYaheiFont = 21;


		private float scaleArialFont = 0.88f; //0.88f;

		
		private float scaleYaheiFont = 0.87f;

		
		public Texture2D splashScreen_texture;

		
		public Texture2D splashScreen_ring;

		
		public Texture2D cursor_texture;
	}
}

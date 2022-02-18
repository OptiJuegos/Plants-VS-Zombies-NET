﻿using System;
using Sexy;

namespace Lawn
{
	
	internal class CursorPreview : GameObject
	{
		
		public CursorPreview()
		{
			this.mX = 0;
			this.mY = 0;
			this.mWidth = 80;
			this.mHeight = 80;
			this.mGridX = 0;
			this.mGridY = 0;
			this.mVisible = false;
		}

		
		public void Update()
		{
		}

		
		public override bool LoadFromFile(Sexy.Buffer b)
		{
			return false;
		}

		
		public override bool SaveToFile(Sexy.Buffer b)
		{
			return false;
		}

		
		public void Draw(Graphics g)
		{
			SeedType seedTypeInCursor = this.mBoard.GetSeedTypeInCursor();
			Debug.OutputDebug<string>(String.Format("CP render: seed = {0}", seedTypeInCursor));
		}

		
		public int mGridX;

		
		public int mGridY;
	}
}

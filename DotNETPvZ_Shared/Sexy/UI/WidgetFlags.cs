﻿using System;

namespace Sexy
{
	
	public enum WidgetFlags
	{
		
		WIDGETFLAGS_UPDATE = 1,
		
		WIDGETFLAGS_MARK_DIRTY,
		
		WIDGETFLAGS_DRAW = 4,
		
		WIDGETFLAGS_CLIP = 8,
		
		WIDGETFLAGS_ALLOW_MOUSE = 16,
		
		WIDGETFLAGS_ALLOW_FOCUS = 32
	}
}

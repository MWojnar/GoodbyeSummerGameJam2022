using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam
{
	public class StateHandler
	{
		public KeyboardState KeyboardState;
		public MouseState MouseState;
		public GamePadState[] GamePadState;

		public StateHandler()
		{
		}

		public void reloadStates()
		{
			KeyboardState = Keyboard.GetState();
			MouseState = Mouse.GetState();
			GamePadState = new GamePadState[] { GamePad.GetState(PlayerIndex.One), GamePad.GetState(PlayerIndex.Two), GamePad.GetState(PlayerIndex.Three), GamePad.GetState(PlayerIndex.Four) };
		}
	}
}

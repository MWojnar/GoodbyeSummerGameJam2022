using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	class Menu : Entity
	{
		private bool mouseDown = false;

		public Menu(World world, Vector2 pos = new Vector2()) : base(world, pos) { }

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if (state.MouseState.LeftButton == ButtonState.Pressed)
				mouseDown = true;
			else if (mouseDown)
				world.LoadLevel(1);
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			base.draw(time, batch);
		}
	}
}

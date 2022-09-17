using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam
{
	public class Player : Entity
	{
		private int speed;

		public Player(World world, Vector2 pos = new Vector2()) : base(world, pos)
		{
			setSprite(this.world.Assets.SpritePlayerTest);
			speed = 2;
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			float movement = (float)(speed * (time.ElapsedGameTime.TotalSeconds * 60));
			if (state.KeyboardState.IsKeyDown(Keys.A))
				addPos(-movement, 0);
			if (state.KeyboardState.IsKeyDown(Keys.S))
				addPos(0, movement);
			if (state.KeyboardState.IsKeyDown(Keys.D))
				addPos(movement, 0);
			if (state.KeyboardState.IsKeyDown(Keys.W))
				addPos(0, -movement);
		}
	}
}

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
			setSprite(this.world.Assets.spritePlayerTest);
			speed = 4;
		}

		public override void update(GameTime time, StateHandler stateHandler)
		{
			base.update(time, stateHandler);
			float movement = (float)(speed * (time.ElapsedGameTime.TotalSeconds * 60));
			if (stateHandler.KeyboardState.IsKeyDown(Keys.A))
				addPos(-movement, 0);
			if (stateHandler.KeyboardState.IsKeyDown(Keys.S))
				addPos(0, movement);
			if (stateHandler.KeyboardState.IsKeyDown(Keys.D))
				addPos(movement, 0);
			if (stateHandler.KeyboardState.IsKeyDown(Keys.W))
				addPos(0, -movement);
		}
	}
}

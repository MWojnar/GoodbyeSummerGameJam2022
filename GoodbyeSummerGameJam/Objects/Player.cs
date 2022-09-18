using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam
{
	public class Player : Entity
	{
		private int speed;
		private double animationTimer;

		public Player(World world, Vector2 pos = new Vector2()) : base(world, pos)
		{
			setSprite(this.world.Assets.SpritePlayer);
			speed = 2;
			setAnimationFrameRate(5);
			setDepth(.25f);
			animationTimer = 0;
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			float movement = (float)(speed * (time.ElapsedGameTime.TotalSeconds * 60));
			if (state.KeyboardState.IsKeyDown(Keys.A))
			{
				addPos(-movement, 0);
				setFlipped(false);
			}
			if (state.KeyboardState.IsKeyDown(Keys.S))
				addPos(0, movement);
			if (state.KeyboardState.IsKeyDown(Keys.D))
			{
				addPos(movement, 0);
				setFlipped(true);
			}
			if (state.KeyboardState.IsKeyDown(Keys.W))
				addPos(0, -movement);
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			animationTimer += time.ElapsedGameTime.TotalSeconds;
			getSprite()?.draw(getPos(), getDepth(), (int)(animationTimer * getAnimationFrameRate()), flip: getFlipped());
		}
	}
}

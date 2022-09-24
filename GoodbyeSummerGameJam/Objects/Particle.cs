using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Particle : Entity
	{
		private double animationTimer;

		public Particle(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			animationTimer = 0;
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			//base.draw(time, batch);
			if (isVisible())
			{
				animationTimer += time.ElapsedGameTime.TotalSeconds;
				getSprite()?.draw(getPos(), getDepth(), (int)(animationTimer * getAnimationFrameRate()), color: getColor(), flip: getFlipped());
			}
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if ((int)(animationTimer * getAnimationFrameRate()) >= getSprite().getFrames())
			{
				world.RemoveEntity(this);
			}
		}
	}
}

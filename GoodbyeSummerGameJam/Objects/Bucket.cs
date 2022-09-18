using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Bucket : Entity
	{
		private ColorWheel colorWheel;
		private bool hovering;

		public Bucket(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false) : base(world, pos, sprite, depth, animationFrameRate, flipped)
		{
			setSprite(world.Assets.SpriteBucket);
			colorWheel = new ColorWheel(world, world.GetPlayer(), this);
			colorWheel.setVisible(false);
			world.AddEntity(colorWheel);
			hovering = false;
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			if (isVisible())
			{
				base.draw(time, batch);
				world.Assets.SpriteBucketPaint1.draw(getPos(), getDepth());
				world.Assets.SpriteBucketPaint2.draw(getPos(), getDepth());
				world.Assets.SpriteBucketPaint3.draw(getPos(), getDepth());
			}
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if (hovering)
				colorWheel.setVisible(true);
			else
				colorWheel.setVisible(false);
			hovering = false;
		}

		public void hover()
		{
			hovering = true;
		}
	}
}

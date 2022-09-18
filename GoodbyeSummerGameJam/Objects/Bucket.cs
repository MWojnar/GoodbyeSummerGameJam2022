using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Bucket : Entity
	{
		public Bucket(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false) : base(world, pos, sprite, depth, animationFrameRate, flipped)
		{
			setSprite(world.Assets.SpriteBucket);
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
	}
}

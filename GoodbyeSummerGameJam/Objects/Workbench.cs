using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Workbench : Entity
	{
		private Bucket bucket;

		public Workbench(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false) : base(world, pos, sprite, depth, animationFrameRate, flipped)
		{
			setSprite(world.Assets.SpriteWorkbench);
			setAnimationFrameRate(5);
			setDepth(.55f);
			bucket = new Bucket(world, getPos() - new Vector2(40, 14));
			world.AddEntity(bucket);
		}

		public Bucket GetBucket()
		{
			return bucket;
		}
	}
}

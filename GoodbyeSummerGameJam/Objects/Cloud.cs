using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Cloud : Entity
	{
		private Sun sun;

		public Cloud(World world, Sun sun, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			setSprite(world.Assets.SpriteCloud);
			this.sun = sun;
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			double dist = distance(getPos(), sun.getPos());
			if (dist < 100)
				world.setCloudiness(100 - dist);
		}

		private double distance(Vector2 pos1, Vector2 pos2)
		{
			return Math.Sqrt(Math.Pow(pos1.X - pos2.X, 2) + Math.Pow(pos1.Y - pos2.Y, 2));
		}
	}
}

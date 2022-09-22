using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class PumpkinWorkbench : Entity
	{
		private Random rand;

		public PumpkinWorkbench(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			rand = new Random();
			randomizeSprite();
		}

		public void randomizeSprite()
		{
			setSprite(rand.NextDouble() > .5 ? world.Assets.SpritePumpkin1 : world.Assets.SpritePumpkin2);
		}
	}
}

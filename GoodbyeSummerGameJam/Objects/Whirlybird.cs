using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Whirlybird : Entity
	{
		private float lowestY, speed;
		private Random rand;

		public Whirlybird(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			setSprite(world.Assets.SpriteWhirlybird);
			lowestY = pos.Y + 50;
			speed = .5f;
			rand = new Random();
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if (getPos().Y < lowestY)
				addPos(0, speed);
			else if (getFrame() < 0)
			{
				setAnimationFrameRate(0);
				setFrame(rand.Next(6));
			}
		}
	}
}

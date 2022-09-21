using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class WateringCan : Entity
	{
		public WateringCan(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			setSprite(world.Assets.SpriteWateringCan);
		}
	}
}

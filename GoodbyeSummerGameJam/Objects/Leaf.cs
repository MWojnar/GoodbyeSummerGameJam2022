using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Leaf : Entity
	{
		public Leaf(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15) : base(world, pos, sprite, depth, animationFrameRate)
		{
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			base.draw(time, batch);
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
		}
	}
}

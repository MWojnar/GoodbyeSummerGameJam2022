using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Bush : Entity
	{
		private Pallete lastPallete;

		public Bush(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			setSprite(world.Assets.SpriteBush);
			lastPallete = world.GetPalletes()[0];
			setColor(lastPallete.GetRandomColor());
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			base.draw(time, batch);
			world.Assets.SpriteBushGrass.draw(getPos(), getDepth() - .001f);
		}

		public void Water()
		{
			setSprite(world.Assets.SpriteBushBerries);
		}

		public bool Colorable(Pallete newPallete)
		{
			return lastPallete != newPallete;
		}

		public void Colorify(Pallete pallete)
		{
			if (lastPallete != pallete)
			{
				lastPallete = pallete;
				setColor(pallete.GetRandomColor());
			}
		}
	}
}

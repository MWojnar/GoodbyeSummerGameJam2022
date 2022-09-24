using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Background : Entity
	{
		private Sprite background, backgroundCloudy;
		private float transitionAmount;

		public Background(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			background = world.Assets.BackgroundPark;
			backgroundCloudy = world.Assets.BackgroundParkCloudy;
			transitionAmount = 0;
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			background.draw(getPos(), .99f, color: Color.White * (1.0f - transitionAmount));
			backgroundCloudy.draw(getPos(), .99f, color: Color.White * transitionAmount);
		}

		public void setTransitionAmount(float transitionAmount)
		{
			this.transitionAmount = transitionAmount;
		}

		public float getTransitionAmount()
		{
			return transitionAmount;
		}
	}
}

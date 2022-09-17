using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam
{
	public class Entity
	{

		private Vector2 pos;
		private Sprite sprite;
		protected World world;
		private int animationFrameRate;
		private float depth;
		private double animationTimer;

		public Entity(World world, Vector2 pos = new Vector2(), Sprite sprite = null, float depth = .5f, int animationFrameRate = 15)
		{
			this.world = world;
			this.pos = pos;
			this.sprite = sprite;
			this.depth = depth;
			this.animationFrameRate = animationFrameRate;
		}

		public void setSprite(Sprite sprite)
		{
			this.sprite = sprite;
			animationTimer = 0;
		}

		public Sprite getSprite()
		{
			return sprite;
		}

		public void setPos(float x, float y)
		{
			pos = new Vector2(x, y);
		}

		public void setPos(Vector2 pos)
		{
			this.pos = new Vector2(pos.X, pos.Y);
		}

		public void addPos(float x, float y)
		{
			pos.X += x;
			pos.Y += y;
		}

		public void addPos(Vector2 pos)
		{
			this.pos += pos;
		}

		public Vector2 getPos()
		{
			return new Vector2(pos.X, pos.Y);

		}

		public void setDepth(float depth)
		{
			this.depth = depth;
		}

		public float getDepth()
		{
			return depth;
		}

		public void setAnimationFrameRate(int frameRate)
		{
			this.animationFrameRate = frameRate;
		}

		public int getAnimationFrameRate()
		{
			return animationFrameRate;
		}

		public virtual void update(GameTime time, StateHandler state)
		{
			
		}

		public virtual void draw(GameTime time, SpriteBatch batch)
		{
			animationTimer += time.ElapsedGameTime.TotalSeconds;
			sprite?.draw(pos, depth, (int)(animationTimer * animationFrameRate));
		}

	}
}

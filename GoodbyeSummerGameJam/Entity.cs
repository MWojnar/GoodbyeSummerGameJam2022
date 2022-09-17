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
		private int depth, animationFrameRate;
		private double animationTimer;

		public Entity(World world, Vector2 pos = new Vector2())
		{
			this.world = world;
			this.pos = pos;
			sprite = null;
			depth = 0;
			animationFrameRate = 15;
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

		public void setDepth(int depth)
		{
			this.depth = depth;
		}

		public int getDepth()
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

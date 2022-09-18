using GoodbyeSummerGameJam.Objects;
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
		private List<RectangleF> collisionBoxes;
		private Color? color;
		private int animationFrameRate;
		private float depth;
		private double animationTimer;
		private bool flipped, visible;

		public Entity(World world, Vector2 pos = new Vector2(), Sprite sprite = null, float depth = .5f, int animationFrameRate = 15, bool flipped = false, Color? color = null)
		{
			this.world = world;
			this.pos = pos;
			this.sprite = sprite;
			this.depth = depth;
			this.animationFrameRate = animationFrameRate;
			this.flipped = flipped;
			this.color = color;
			visible = true;
			collisionBoxes = new List<RectangleF>();
		}

		public void setSprite(Sprite sprite)
		{
			if (this.sprite != sprite)
			{
				this.sprite = sprite;
				animationTimer = 0;
				setCollisionBox(new RectangleF(new Vector2(), new Size2(sprite.getWidth(), sprite.getHeight())));
			}
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

		public void setFlipped(bool flipped)
		{
			this.flipped = flipped;
		}

		public bool getFlipped()
		{
			return flipped;
		}

		public void setVisible(bool visible)
		{
			this.visible = visible;
		}

		public bool isVisible()
		{
			return visible;
		}

		public void setColor(Color? color)
		{
			this.color = color;
		}

		public Color? getColor()
		{
			return color;
		}

		public virtual void update(GameTime time, StateHandler state)
		{
			
		}

		public virtual void draw(GameTime time, SpriteBatch batch)
		{
			if (visible)
			{
				animationTimer += time.ElapsedGameTime.TotalSeconds;
				sprite?.draw(pos, depth, (int)(animationTimer * animationFrameRate), color: color, flip: flipped);
				/*if (sprite != null && collisionBoxes.Count > 0)
				{
					RectangleF collisionBox = new RectangleF((getPos() - sprite.getOrigin()) + collisionBoxes[0].TopLeft, collisionBoxes[0].Size);
					batch.DrawRectangle(collisionBox, Color.Red);
				}*/
			}
		}

		public void setCollisionBoxes(List<RectangleF> collisionBoxes)
		{
			if (collisionBoxes != null)
			{
				this.collisionBoxes.Clear();
				this.collisionBoxes.AddRange(collisionBoxes);
			}
		}

		public void setCollisionBox(RectangleF collisionBox)
		{
			if (collisionBox != null)
			{
				collisionBoxes.Clear();
				collisionBoxes.Add(collisionBox);
			}
		}

		public List<RectangleF> getCollisionBoxes()
		{
			return new List<RectangleF>(collisionBoxes);
		}

		public bool colliding(Entity entity)
		{
			foreach (RectangleF sourceCollisionBox in collisionBoxes)
			{
				RectangleF collisionBox = new RectangleF((getPos() - sprite.getOrigin()) + sourceCollisionBox.TopLeft, sourceCollisionBox.Size);
				foreach (RectangleF sourceCollideeBox in entity.collisionBoxes)
				{
					RectangleF collideeBox = new RectangleF((entity.getPos() - entity.sprite.getOrigin()) + sourceCollideeBox.TopLeft, sourceCollideeBox.Size);
					if (collisionBox.Intersects(collideeBox))
						return true;
				}
			}
			return false;
		}

	}
}

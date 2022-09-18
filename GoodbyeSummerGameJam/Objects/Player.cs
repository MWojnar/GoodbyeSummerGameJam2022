using GoodbyeSummerGameJam.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodbyeSummerGameJam
{
	public class Player : Entity
	{
		private int speed;
		private double animationTimer;
		private bool holdingBucket, bucketEmpty;
		private Pallete bucketPallete;

		public Player(World world, Vector2 pos = new Vector2()) : base(world, pos)
		{
			setSprite(this.world.Assets.SpritePlayer);
			speed = 2;
			setAnimationFrameRate(5);
			setDepth(.25f);
			animationTimer = 0;
			holdingBucket = false;
			bucketPallete = null;
			bucketEmpty = false;
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			float movement = (float)(speed * (time.ElapsedGameTime.TotalSeconds * 60));
			if (state.KeyboardState.IsKeyDown(Keys.A))
			{
				addPos(-movement, 0);
				setFlipped(false);
			}
			if (state.KeyboardState.IsKeyDown(Keys.S))
				addPos(0, movement);
			if (state.KeyboardState.IsKeyDown(Keys.D))
			{
				addPos(movement, 0);
				setFlipped(true);
			}
			if (state.KeyboardState.IsKeyDown(Keys.W))
				addPos(0, -movement);
			foreach (Entity entity in world.GetEntities())
			{
				if (entity is Bucket)
				{
					if (!holdingBucket)
					{
						if (colliding(entity))
						{
							((Bucket)entity).hover();
						}
						if (state.KeyboardState.IsKeyDown(Keys.Space) && entity.isVisible() && colliding(entity))
						{
							PickupBucket((Bucket)entity, world.GetPalletes()[1]);
						}
					}
					else if ((bucketEmpty || state.KeyboardState.IsKeyDown(Keys.Enter)) && !entity.isVisible() && colliding(entity))
					{
						dropBucket((Bucket)entity);
					}
				}
				else if (entity is Tree)
				{
					if (state.KeyboardState.IsKeyDown(Keys.Space) && holdingBucket && colliding(entity))
					{
						if (((Tree)entity).Colorable(bucketPallete) && !bucketEmpty)
						{
							((Tree)entity).Colorify(bucketPallete);
							bucketEmpty = true;
						}
					}
				}
			}
			if (holdingBucket)
				setSprite(world.Assets.SpritePlayerHoldBucket);
			else
				setSprite(world.Assets.SpritePlayer);
		}

		public void PickupBucket(Bucket bucket, Pallete pallete)
		{
			bucketPallete = pallete;
			holdingBucket = true;
			bucketEmpty = false;
			bucket.setVisible(false);
		}

		private void dropBucket(Bucket bucket)
		{
			bucket.setVisible(true);
			holdingBucket = false;
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			animationTimer += time.ElapsedGameTime.TotalSeconds;
			int currentFrame = (int)(animationTimer * getAnimationFrameRate());
			getSprite()?.draw(getPos(), getDepth(), currentFrame, flip: getFlipped());
			if (holdingBucket)
			{
				Vector2 bucketPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12 - currentFrame % 2);
				world.Assets.SpriteBucket.draw(bucketPos, getDepth());
				if (!bucketEmpty)
				{
					world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth(), color: bucketPallete.GetColorsAndWeights().First().Key);
					world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth(), color: bucketPallete.GetColorsAndWeights().ToList()[1].Key);
					world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth(), color: bucketPallete.GetColorsAndWeights().ToList()[2].Key);
				} else
				{
					world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth(), color: Color.Gray);
					world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth(), color: Color.Gray);
					world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth(), color: Color.Gray);
				}
			}
		}
	}
}

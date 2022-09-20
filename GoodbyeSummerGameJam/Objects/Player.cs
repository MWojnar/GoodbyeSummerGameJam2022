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
		private double animationTimer, gameTimer, maxGameTime;
		private bool bucketEmpty, ended, mouseDown;
		private Entity heldEntity;
		private Pallete bucketPallete;
		private Workbench bench;
		private Vector2 grabOffset;

		public Player(World world, Workbench bench, Vector2 pos = new Vector2()) : base(world, pos)
		{
			setSprite(this.world.Assets.SpritePlayer);
			speed = 2;
			setAnimationFrameRate(5);
			setDepth(.25f);
			animationTimer = 0;
			gameTimer = -1;
			maxGameTime = 120;
			heldEntity = null;
			bucketPallete = null;
			bucketEmpty = false;
			ended = false;
			this.bench = bench;
			mouseDown = false;
			grabOffset = new Vector2();
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);

			if (!ended)
			{
				if (gameTimer == -1)
					gameTimer = time.TotalGameTime.TotalSeconds;
				if (maxGameTime < time.TotalGameTime.TotalSeconds - gameTimer)
					endLevel();
				bool shaking = false;
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
						if (heldEntity == null)
						{
							if (colliding(entity))
							{
								((Bucket)entity).hover();
							}
						}
						else if ((bucketEmpty || state.KeyboardState.IsKeyDown(Keys.Enter)) && !entity.isVisible() && colliding(entity))
						{
							dropBucket((Bucket)entity);
						}
					}
					else if (entity is Cloud)
					{
						if (heldEntity == null && colliding(entity) && state.KeyboardState.IsKeyDown(Keys.Space))
						{
							heldEntity = entity;
							grabOffset = entity.getPos() - getPos();
						}
					}
					else if (entity is Tree)
					{
						if (state.KeyboardState.IsKeyDown(Keys.Space) && colliding(entity))
						{
							if (heldEntity is Bucket)
							{
								if (((Tree)entity).Colorable(bucketPallete) && !bucketEmpty)
								{
									((Tree)entity).Colorify(bucketPallete);
									bucketEmpty = true;
								}
							}
							else if (heldEntity == null)
							{
								shaking = true;
								((Tree)entity).shake(time.TotalGameTime.TotalSeconds);
							}
						}
					}
				}
				if (heldEntity != null)
				{
					setSprite(world.Assets.SpritePlayerHoldBucket);
					if (heldEntity is Cloud)
					{
						heldEntity.setPos(getPos() + grabOffset);
						if (!state.KeyboardState.IsKeyDown(Keys.Space))
							heldEntity = null;
					}
				}
				else if (shaking)
					setSprite(world.Assets.SpritePlayerShake);
				else
					setSprite(world.Assets.SpritePlayer);
			}
			else
			{
				if (state.MouseState.LeftButton == ButtonState.Pressed)
					mouseDown = true;
				else if (mouseDown)
					world.LoadLevel(0);
			}
		}

		public void PickupBucket(Bucket bucket, Pallete pallete)
		{
			bucketPallete = pallete;
			heldEntity = bucket;
			bucketEmpty = false;
			bucket.setVisible(false);
		}

		private void dropBucket(Bucket bucket)
		{
			bucket.setVisible(true);
			heldEntity = null;
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			if (!ended)
			{
				animationTimer += time.ElapsedGameTime.TotalSeconds;
				int currentFrame = (int)(animationTimer * getAnimationFrameRate());
				getSprite()?.draw(getPos(), getDepth(), currentFrame, flip: getFlipped());
				if (heldEntity is Bucket)
				{
					Vector2 bucketPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12 - currentFrame % 2);
					world.Assets.SpriteBucket.draw(bucketPos, getDepth());
					if (!bucketEmpty)
					{
						world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth(), color: bucketPallete.GetColorsAndWeights().First().Key);
						world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth(), color: bucketPallete.GetColorsAndWeights().ToList()[1].Key);
						world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth(), color: bucketPallete.GetColorsAndWeights().ToList()[2].Key);
					}
					else
					{
						world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth(), color: Color.Gray);
						world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth(), color: Color.Gray);
						world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth(), color: Color.Gray);
					}
				}
				int timeLeft = (int)Math.Ceiling(maxGameTime - (time.TotalGameTime.TotalSeconds - gameTimer));
				if (timeLeft < 0)
					timeLeft = 0;
				string text = (timeLeft / 60) + ":" + (timeLeft % 60).ToString("00");
				batch.DrawString(world.Assets.FontTest, text, new Vector2(world.GetDimensions().X / 2 - world.Assets.FontTest.MeasureString(text).X / 2, world.GetDimensions().Y - world.Assets.FontTest.MeasureString(text).Y), Color.Red);
			}
			else
			{
				string text = "Autumn is here!  Great job!";
				batch.DrawString(world.Assets.FontTest, text, new Vector2(world.GetDimensions().X / 2 - world.Assets.FontTest.MeasureString(text).X / 2, world.Assets.FontTest.MeasureString(text).Y), Color.Red);
				text = "Click anywhere to continue";
				batch.DrawString(world.Assets.FontTest, text, new Vector2(world.GetDimensions().X / 2 - world.Assets.FontTest.MeasureString(text).X / 2, world.GetDimensions().Y / 2 - world.Assets.FontTest.MeasureString(text).Y / 2), Color.Red);
			}
		}

		private void endLevel()
		{
			ended = true;
			bench.endLevel();
		}
	}
}

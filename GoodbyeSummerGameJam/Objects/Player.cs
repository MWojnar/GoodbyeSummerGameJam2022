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
		private bool bucketEmpty, ended, mouseDown, spaceDown, displaySpacebarIcon;
		private Entity heldEntity;
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
			bucketEmpty = false;
			ended = false;
			this.bench = bench;
			mouseDown = false;
			spaceDown = false;
			displaySpacebarIcon = false;
			grabOffset = new Vector2();
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);

			displaySpacebarIcon = false;
			if (!ended)
			{
				bool spaceUp = spaceDown && !state.KeyboardState.IsKeyDown(Keys.Space);
				bool wasHolding = heldEntity != null;
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
				spaceDown = false;
				if (state.KeyboardState.IsKeyDown(Keys.Space))
					spaceDown = true;
				foreach (Entity entity in world.GetEntities())
				{
					if (entity is Bucket)
					{
						if (heldEntity == null)
						{
							if (pointColliding(entity))
								((Bucket)entity).hover();
						}
					}
					else if (entity is WateringCan)
					{
						if (heldEntity == null)
						{
							if (pointColliding(entity))
							{
								displaySpacebarIcon = true;
								if (spaceUp)
									PickupWorkbenchItem(entity);
							}
						}
					}
					else if (entity is Workbench)
					{
						if (pointColliding(entity) && heldEntity != null && spaceUp && wasHolding)
							dropWorkbenchItem(heldEntity);
					}
					else if (entity is Cloud)
					{
						if (heldEntity == null && pointColliding(entity) && state.KeyboardState.IsKeyDown(Keys.Space))
						{
							heldEntity = entity;
							grabOffset = entity.getPos() - getPos();
						}
					}
					else if (entity is Tree)
					{
						if (pointColliding(entity))
						{
							if (heldEntity is Bucket && spaceUp)
							{
								if (((Tree)entity).Colorable(((Bucket)heldEntity).GetPallete()) && !bucketEmpty)
								{
									((Tree)entity).Colorify(((Bucket)heldEntity).GetPallete());
									bucketEmpty = true;
								}
							}
							else if (heldEntity == null && state.KeyboardState.IsKeyDown(Keys.Space))
							{
								shaking = true;
								((Tree)entity).shake(time.TotalGameTime.TotalSeconds);
							}
						}
					}
					else if (entity is Bush)
					{
						if (spaceUp && pointColliding(entity))
						{
							if (heldEntity is Bucket)
							{
								if (((Bush)entity).Colorable(((Bucket)heldEntity).GetPallete()) && !bucketEmpty)
								{
									((Bush)entity).Colorify(((Bucket)heldEntity).GetPallete());
									bucketEmpty = true;
								}
							}
							else if (heldEntity is WateringCan)
							{
								((Bush)entity).Water();
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

		public void PickupWorkbenchItem(Entity workbenchItem)
		{
			heldEntity = workbenchItem;
			workbenchItem.setVisible(false);
			bucketEmpty = false;
		}

		private void dropWorkbenchItem(Entity workbenchItem)
		{
			workbenchItem.setVisible(true);
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
						world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth(), color: ((Bucket)heldEntity).GetPallete().GetColorsAndWeights().First().Key);
						world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth(), color: ((Bucket)heldEntity).GetPallete().GetColorsAndWeights().ToList()[1].Key);
						world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth(), color: ((Bucket)heldEntity).GetPallete().GetColorsAndWeights().ToList()[2].Key);
					}
					else
					{
						world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth(), color: Color.Gray);
						world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth(), color: Color.Gray);
						world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth(), color: Color.Gray);
					}
				}
				else if (heldEntity is WateringCan)
				{
					Vector2 canPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12 - currentFrame % 2);
					world.Assets.SpriteWateringCan.draw(canPos, getDepth(), flip: getFlipped());
				}
				int timeLeft = (int)Math.Ceiling(maxGameTime - (time.TotalGameTime.TotalSeconds - gameTimer));
				if (timeLeft < 0)
					timeLeft = 0;
				string text = (timeLeft / 60) + ":" + (timeLeft % 60).ToString("00");
				batch.DrawString(world.Assets.FontTest, text, new Vector2(world.GetDimensions().X / 2 - world.Assets.FontTest.MeasureString(text).X / 2, world.GetDimensions().Y - world.Assets.FontTest.MeasureString(text).Y), Color.Red);
				if (displaySpacebarIcon)
				{
					//TODO
				}
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

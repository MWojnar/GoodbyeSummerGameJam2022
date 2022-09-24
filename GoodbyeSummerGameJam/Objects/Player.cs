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
		private int speed, temperature;
		private double animationTimer, gameTimer, maxGameTime, fanTimerStart, fanTimerMax;
		private bool bucketEmpty, ended, mouseDown, spaceDown, displaySpacebarIcon;
		private Entity heldEntity;
		private Workbench bench;
		private Vector2 grabOffset;
		private Sprite pumpkinSprite;
		private Random rand;

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
			pumpkinSprite = null;
			rand = new Random();
			temperature = 0;
			fanTimerStart = 0;
			fanTimerMax = .25;
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
					else if (entity is WateringCan || entity is SeedBag || entity is Fan)
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
					else if (entity is PumpkinWorkbench)
					{
						if (heldEntity == null)
						{
							if (pointColliding(entity))
							{
								displaySpacebarIcon = true;
								if (spaceUp)
								{
									PickupWorkbenchItem(entity);
									world.Assets.SoundPumpkinPickup.Play();
								}
							}
						}
					}
					else if (entity is Workbench)
					{
						if (pointColliding(entity) && heldEntity != null)
						{
							displaySpacebarIcon = true;
							if (spaceUp && wasHolding)
								dropWorkbenchItem(heldEntity);
						}
					}
					else if (entity is Cloud)
					{
						if (heldEntity == null)
						{
							if (pointColliding(entity))
							{
								if (state.KeyboardState.IsKeyDown(Keys.Space))
								{
									heldEntity = entity;
									grabOffset = entity.getPos() - getPos();
								}
								else
									displaySpacebarIcon = true;
							}
						}
					}
					else if (entity is Tree)
					{
						if (pointColliding(entity))
						{
							if (heldEntity is Bucket)
							{
								if (((Tree)entity).Colorable(((Bucket)heldEntity).GetPallete()) && !bucketEmpty)
								{
									displaySpacebarIcon = true;
									if (spaceUp)
									{
										((Tree)entity).Colorify(((Bucket)heldEntity).GetPallete());
										world.Assets.SoundPaintApply.Play();
										bucketEmpty = true;
									}
								}
							}
							else if (heldEntity == null)
							{
								if (state.KeyboardState.IsKeyDown(Keys.Space))
								{
									shaking = true;
									((Tree)entity).shake(time.TotalGameTime.TotalSeconds);
								}
								else
								{
									displaySpacebarIcon = true;
								}
							}
						}
					}
					else if (entity is Bush)
					{
						if (pointColliding(entity))
						{
							if (heldEntity is Bucket)
							{
								if (((Bush)entity).Colorable(((Bucket)heldEntity).GetPallete()) && !bucketEmpty)
								{
									displaySpacebarIcon = true;
									if (spaceUp)
									{
										((Bush)entity).Colorify(((Bucket)heldEntity).GetPallete());
										world.Assets.SoundPaintApply.Play();
										bucketEmpty = true;
									}
								}
							}
							else if (heldEntity is WateringCan)
							{
								displaySpacebarIcon = true;
								if (spaceUp)
								{
									((Bush)entity).Water();
									if (rand.NextDouble() > .5)
										world.Assets.SoundWaterPour1.Play();
									else 
										world.Assets.SoundWaterPour2.Play(); 
								}
							}
						}
					}
				}
				if (heldEntity != null)
				{
					if (!(heldEntity is Fan))
						setSprite(world.Assets.SpritePlayerHoldBucket);
					if (heldEntity is Cloud)
					{
						heldEntity.setPos(getPos() + grabOffset);
						if (!state.KeyboardState.IsKeyDown(Keys.Space))
							heldEntity = null;
					}
					else if (heldEntity is PumpkinWorkbench)
					{
						if (spaceUp && getPos().Y > 100)
						{
							Vector2 pumpkinPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12);
							Pumpkin pumpkin = new Pumpkin(world, pumpkinPos, pumpkinSprite, .5f - (pumpkinPos.Y + world.Assets.SpritePumpkin1.getHeight() / 2) / 1000000f);
							world.AddEntity(pumpkin);
							world.Assets.SoundPumpkinPlace.Play();
							heldEntity = null;
						}
					}
					else if (heldEntity is SeedBag)
					{
						if (spaceUp && getPos().Y > 50)
						{
							Vector2 seedPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12);
							Whirlybird seed = new Whirlybird(world, seedPos, pumpkinSprite, .5f - (seedPos.Y + 50) / 1000000f);
							world.AddEntity(seed);
						}
					}
					else if (heldEntity is Fan)
					{
						if (time.TotalGameTime.TotalSeconds - fanTimerStart > fanTimerMax)
						{
							setSprite(world.Assets.SpritePlayerHoldBucket);
							if (spaceUp)
							{
								fanTimerStart = time.TotalGameTime.TotalSeconds;
								temperature++;
								if (temperature > 31)
									temperature = 31;
								if (rand.NextDouble() > .5)
									world.Assets.SoundWind1.Play();
								else
									world.Assets.SoundWind2.Play();
								animationTimer += time.ElapsedGameTime.TotalSeconds;
								int currentFrame = (int)(animationTimer * getAnimationFrameRate());
								Vector2 windPos = getPos() - new Vector2(26 - (getFlipped() ? 52 : 0), 6 - currentFrame % 2);
								world.AddEntity(new Particle(world, windPos, world.Assets.SpriteWind, flipped: getFlipped()));
							}
						}
						else
							setSprite(world.Assets.SpritePlayerBlow);
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
			if (!(workbenchItem is PumpkinWorkbench))
				workbenchItem.setVisible(false);
			else
			{
				pumpkinSprite = workbenchItem.getSprite();
				((PumpkinWorkbench)workbenchItem).randomizeSprite();
			}
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
				if (displaySpacebarIcon)
					world.Assets.SpriteSpacebar.draw(getPos() + new Vector2(0, 30), getDepth(), currentFrame);
				if (heldEntity is Bucket)
				{
					Vector2 bucketPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12 - currentFrame % 2);
					world.Assets.SpriteBucket.draw(bucketPos, getDepth());
					if (!bucketEmpty)
					{
						world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth() - .0000001f, color: ((Bucket)heldEntity).GetPallete().GetColorsAndWeights().First().Key);
						world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth() - .0000001f, color: ((Bucket)heldEntity).GetPallete().GetColorsAndWeights().ToList()[1].Key);
						world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth() - .0000001f, color: ((Bucket)heldEntity).GetPallete().GetColorsAndWeights().ToList()[2].Key);
					}
					else
					{
						world.Assets.SpriteBucketPaint1.draw(bucketPos, getDepth() - .0000001f, color: Color.Gray);
						world.Assets.SpriteBucketPaint2.draw(bucketPos, getDepth() - .0000001f, color: Color.Gray);
						world.Assets.SpriteBucketPaint3.draw(bucketPos, getDepth() - .0000001f, color: Color.Gray);
					}
				}
				else if (heldEntity is WateringCan)
				{
					Vector2 canPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12 - currentFrame % 2);
					world.Assets.SpriteWateringCan.draw(canPos, getDepth() - .0000001f, flip: getFlipped());
				}
				else if (heldEntity is SeedBag)
				{
					Vector2 seedBagPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12 - currentFrame % 2);
					world.Assets.SpriteSeedBag.draw(seedBagPos, getDepth() - .0000001f);
				}
				else if (heldEntity is PumpkinWorkbench)
				{
					Vector2 pumpkinPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), -12 - currentFrame % 2);
					pumpkinSprite?.draw(pumpkinPos, getDepth() - .0000001f);
				}
				else if (heldEntity is Fan)
				{
					Vector2 fanPos = getPos() - new Vector2(14 - (getFlipped() ? 28 : 0), 4 - currentFrame % 2);
					if (time.TotalGameTime.TotalSeconds - fanTimerStart > fanTimerMax)
						world.Assets.SpriteFan.draw(fanPos, getDepth() - .0000001f, flip: getFlipped());
					else
						world.Assets.SpriteFanBlow.draw(fanPos, getDepth() - .0000001f, (int)((time.TotalGameTime.TotalSeconds - fanTimerStart) * 15) % 2, flip: getFlipped());
				}
				int timeLeft = (int)Math.Ceiling(maxGameTime - (time.TotalGameTime.TotalSeconds - gameTimer));
				if (timeLeft < 0)
					timeLeft = 0;
				/*string text = (timeLeft / 60) + ":" + (timeLeft % 60).ToString("00");
				batch.DrawString(world.Assets.FontTest, text, new Vector2(world.GetDimensions().X / 2 - world.Assets.FontTest.MeasureString(text).X / 2, world.GetDimensions().Y - world.Assets.FontTest.MeasureString(text).Y), Color.Red);*/
				world.Assets.SpriteTimer.draw(new Vector2(world.Assets.SpriteTimer.getWidth() / 2, world.GetDimensions().Y - world.Assets.SpriteTimer.getHeight() / 2), 0, 60 - timeLeft / 2);
				world.Assets.SpriteThermometer.draw(world.GetDimensions() - world.Assets.SpriteThermometer.getDimensions() / 2, 0, temperature);
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

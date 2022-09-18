using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class ColorWheel : Entity
	{
		private List<Sprite> wedges;
		private List<double> animationStartTimes;
		private List<bool> isGrowing;
		private Bucket bucket;
		private Player player;
		private float minSize, maxSize, growTime;
		private bool mouseDown;

		public ColorWheel(World world, Player player, Bucket bucket, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false, Color? color = null) : base(world, pos, sprite, depth, animationFrameRate, flipped, color)
		{
			mouseDown = false;
			this.bucket = bucket;
			this.player = player;
			wedges = new List<Sprite>();
			wedges.Add(world.Assets.SpriteColorWheel1);
			wedges.Add(world.Assets.SpriteColorWheel2);
			wedges.Add(world.Assets.SpriteColorWheel3);
			wedges.Add(world.Assets.SpriteColorWheel4);
			wedges.Add(world.Assets.SpriteColorWheel5);
			wedges.Add(world.Assets.SpriteColorWheel6);
			wedges.Add(world.Assets.SpriteColorWheel7);
			wedges.Add(world.Assets.SpriteColorWheel8);
			animationStartTimes = new List<double>();
			isGrowing = new List<bool>();
			for (int i = 0; i < 8; i++)
			{
				animationStartTimes.Add(0);
				isGrowing.Add(false);
			}
			minSize = .05f;
			maxSize = minSize * 1.25f;
			growTime = .25f;
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			if (isVisible())
				for (int i = 0; i < 8; i++)
				{
					Sprite wedge = wedges[i];
					double startTime = animationStartTimes[i];
					bool growing = isGrowing[i];
					float difference = (maxSize - minSize) * (float)Math.Min((time.TotalGameTime.TotalSeconds - startTime) / growTime, 1);
					float scaleOffset = growing ? difference : (maxSize - minSize) - difference;
					wedge.draw(getPos(), getDepth(), scale: minSize + scaleOffset);
				}
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if (player == null)
				player = world.GetPlayer();
			setPos(player.getPos() - new Vector2(25, 0));
			if (isVisible())
			{
				double mouseAngle = 2 * Math.PI - (Math.Atan2(state.MouseState.X / 5 - getPos().X, state.MouseState.Y / 5 - getPos().Y) + Math.PI);
				double radius = wedges[0].getWidth() * minSize / 2;
				double distance = Math.Sqrt(Math.Pow(state.MouseState.X / 5 - getPos().X, 2) + Math.Pow(state.MouseState.Y / 5 - getPos().Y, 2));
				int selected = (int)Math.Floor((mouseAngle * 8) / (2 * Math.PI));
				for (int i = 0; i < 8; i++)
				{
					bool previous = isGrowing[i];
					isGrowing[i] = selected == i && distance < radius;
					if (previous != isGrowing[i])
						animationStartTimes[i] = time.TotalGameTime.TotalSeconds;
				}
				if (mouseDown && state.MouseState.LeftButton == ButtonState.Released && distance < radius)
					player.PickupBucket(bucket, world.GetPalletes()[selected]);
			}
			if (state.MouseState.LeftButton == ButtonState.Pressed)
				mouseDown = true;
			else
				mouseDown = false;
		}
	}
}

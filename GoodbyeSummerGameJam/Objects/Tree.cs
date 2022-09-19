using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Tree : Entity
	{
		private List<Pair<Sprite, Color>> bushSprites;
		private List<Leaf> leaves;
		private Pallete lastPallete;
		private Random rand;
		private bool shaking;
		private bool autoShaking;
		private double shakeStartTime, shakeMax, shakeMaxTime, shakeIntervalTime, shakeRotMax;
		private double autoShakeStart;
		private int state = 0;

		public Tree(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15) : base(world, pos, sprite, depth, animationFrameRate)
		{
			setSprite(world.Assets.SpriteTree);
			lastPallete = null;
			leaves = new List<Leaf>();
			bushSprites = new List<Pair<Sprite, Color>>();
			bushSprites.Add(new Pair<Sprite, Color>(world.Assets.SpriteTreeBush1, Color.DarkGreen));
			bushSprites.Add(new Pair<Sprite, Color>(world.Assets.SpriteTreeBush2, Color.Green));
			bushSprites.Add(new Pair<Sprite, Color>(world.Assets.SpriteTreeBush3, Color.LightGreen));
			Colorify(world.GetPalletes().First());
			shaking = false;
			shakeStartTime = 0;
			shakeMax = 1;
			shakeMaxTime = .75;
			shakeIntervalTime = .05;
			autoShaking = false;
			autoShakeStart = 0;
			shakeRotMax = .01;
			rand = new Random();
		}
		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if (autoShaking && time.TotalGameTime.TotalSeconds - autoShakeStart > 5)
			{
				shake(time.TotalGameTime.TotalSeconds);
				autoShakeStart = time.TotalGameTime.TotalSeconds;
			}
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			getSprite()?.draw(!shaking ? getPos() : getShakePos(0, time), getDepth() + .01f, 0, rotation: getShakeRot(0, time));
			double i = shakeIntervalTime;
			float depthOffset = .001f;
			foreach (Pair<Sprite, Color> bushSprite in bushSprites)
			{
				bushSprite.First?.draw(!shaking ? getPos() : getShakePos(i, time), getDepth() - depthOffset, 0, rotation: getShakeRot(i, time), color: bushSprite.Second);
				i += shakeIntervalTime;
				depthOffset += .001f;
			}
		}


		private Vector2 getShakePos(double offset, GameTime time)
		{
			double elapsed = time.TotalGameTime.TotalSeconds - shakeStartTime;
			elapsed -= offset;
			if (elapsed > 0 && elapsed < shakeMaxTime)
			{
				return getPos() + new Vector2((float)(Math.Sin(elapsed * 2 * Math.PI / shakeMaxTime) * shakeMax), 0);
			}
			return getPos();
		}

		private float getShakeRot(double offset, GameTime time)
		{
			double elapsed = time.TotalGameTime.TotalSeconds - shakeStartTime;
			elapsed -= offset;
			if (elapsed > 0 && elapsed < shakeMaxTime)
			{
				return (float)(Math.Sin(elapsed * 2 * Math.PI / shakeMaxTime) * shakeRotMax);
			}
			return 0.0f;
		}

		public void Colorify(Pallete pallete)
		{
			if (lastPallete != pallete)
			{
				if (bushSprites.Count <= pallete.Count())
				{
					foreach (Pair<Sprite, Color> bushSprite in bushSprites)
						bushSprite.Second = Color.Transparent;
					while (bushSprites.Where(e => e.Second == Color.Transparent).Count() > 0)
					{
						Color randomColor = pallete.GetRandomColor();
						if (bushSprites.Where(e => e.Second == randomColor).Count() < 1)
							bushSprites.First(e => e.Second == Color.Transparent).Second = randomColor;
					}
				}
				else
					foreach (Pair<Sprite, Color> bushSprite in bushSprites)
						bushSprite.Second = pallete.GetRandomColor();
				foreach (Leaf leaf in leaves)
					leaf.setColor(pallete.GetRandomColor());
				lastPallete = pallete;
			}
		}

		public bool Colorable(Pallete pallete)
		{
			return lastPallete != pallete;
		}

		public void shake(double startTime)
		{
			if ((!shaking || startTime - shakeStartTime > shakeMaxTime * 3) && state < 2)
			{
				shaking = true;
				shakeStartTime = startTime;
				generateLeaves(20);
				incrementState();
				world.Assets.SoundTreeShake.Play();
			}
		}

		private void incrementState()
		{
			state++;
			switch(state)
			{
				case 1: bushSprites[0].First = world.Assets.SpriteLesserTreeBush1;
					bushSprites[1].First = world.Assets.SpriteLesserTreeBush2;
					bushSprites[2].First = world.Assets.SpriteLesserTreeBush3;
					break;
				case 2:
					bushSprites[0].First = null;
					bushSprites[1].First = null;
					bushSprites[2].First = null;
					break;
			}
		}

		public Pallete GetPallete()
		{
			return lastPallete;
		}

		public void generateLeaves(int amount)
		{
			for (int i = 0; i < amount; i++)
			{
				leaves.Add(new Leaf(world, this, getPos() + new Vector2((float)(rand.NextDouble() - .5) * (getSprite().getWidth() - 30), (float)(rand.NextDouble() - .5) * 20 - getSprite().getHeight() / 2), depth: getDepth() - .01f));
				world.AddEntity(leaves.Last());
			}
		}
	}

	public class Pair<T, U>
	{
		public Pair()
		{
		}

		public Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		public T First { get; set; }
		public U Second { get; set; }
	};
}

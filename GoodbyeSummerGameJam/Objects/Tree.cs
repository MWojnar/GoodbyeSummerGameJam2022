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

		public Tree(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15) : base(world, pos, sprite, depth, animationFrameRate)
		{
			bushSprites = new List<Pair<Sprite, Color>>();
			bushSprites.Add(new Pair<Sprite, Color>(null, Color.Transparent));
			bushSprites.Add(new Pair<Sprite, Color>(null, Color.Transparent));
			bushSprites.Add(new Pair<Sprite, Color>(null, Color.Transparent));
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			base.draw(time, batch);
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
		}

		public void Colorify(Pallete pallete)
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
				foreach(Pair<Sprite, Color> bushSprite in bushSprites)
					bushSprite.Second = pallete.GetRandomColor();
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

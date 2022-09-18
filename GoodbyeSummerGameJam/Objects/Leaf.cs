using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Leaf : Entity
	{
		private Tree tree;
		private float fallSpeed;

		public Leaf(World world, Tree tree, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15) : base(world, pos, sprite, depth, animationFrameRate)
		{
			this.tree = tree;
			setColor(tree.GetPallete().GetRandomColor());
			fallSpeed = 0.5f;
			Random rand = new Random();
			int choice = rand.Next(5);
			switch(choice)
			{
				case 0: setSprite(world.Assets.SpriteLeaf1); break;
				case 1: setSprite(world.Assets.SpriteLeaf2); break;
				case 2: setSprite(world.Assets.SpriteLeaf3); break;
				case 3: setSprite(world.Assets.SpriteLeaf4); break;
				case 4: setSprite(world.Assets.SpriteLeaf5); break;
			}
		}

		public override void draw(GameTime time, SpriteBatch batch)
		{
			base.draw(time, batch);
		}

		public override void update(GameTime time, StateHandler state)
		{
			base.update(time, state);
			if (getPos().Y < tree.getPos().Y)
			{
				addPos(0, fallSpeed * (float)time.ElapsedGameTime.TotalSeconds * 60.0f);
			}
		}
	}
}

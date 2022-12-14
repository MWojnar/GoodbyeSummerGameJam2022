using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodbyeSummerGameJam.Objects
{
	public class Workbench : Entity
	{
		private Bucket bucket;
		private WateringCan wateringCan;
		private PumpkinWorkbench pumpkin;
		private SeedBag seedBag;
		private Fan fan;

		public Workbench(World world, Vector2 pos = default, Sprite sprite = null, float depth = 0.5F, int animationFrameRate = 15, bool flipped = false) : base(world, pos, sprite, depth, animationFrameRate, flipped)
		{
			setSprite(world.Assets.SpriteWorkbench);
			setAnimationFrameRate(5);
			setDepth(.55f);
			bucket = new Bucket(world, getPos() - new Vector2(45, 14));
			world.AddEntity(bucket);
			wateringCan = new WateringCan(world, getPos() - new Vector2(23, 14));
			world.AddEntity(wateringCan);
			pumpkin = new PumpkinWorkbench(world, getPos() - new Vector2(-2, 14));
			world.AddEntity(pumpkin);
			seedBag = new SeedBag(world, getPos() - new Vector2(-27, 14));
			world.AddEntity(seedBag);
			fan = new Fan(world, getPos() - new Vector2(-50, 14));
			world.AddEntity(fan);
		}

		public Bucket GetBucket()
		{
			return bucket;
		}

		public void endLevel()
		{
			setVisible(false);
			bucket.setVisible(false);
			wateringCan.setVisible(false);
			pumpkin.setVisible(false);
			seedBag.setVisible(false);
			fan.setVisible(false);
		}
	}
}

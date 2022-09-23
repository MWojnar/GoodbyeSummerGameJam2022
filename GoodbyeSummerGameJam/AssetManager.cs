using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GoodbyeSummerGameJam
{
	public class AssetManager
	{
		public Sprite SpritePlayerTest, SpriteTree, SpriteTreeBush1, SpriteTreeBush2, SpriteTreeBush3, SpriteLesserTreeBush1, SpriteLesserTreeBush2, SpriteLesserTreeBush3,
			SpritePlayButton, SpriteInstructionsButton, SpriteCreditsButton, SpriteExitButton, SpriteInstructions, SpriteCredits, SpritePlayer, SpritePlayerHoldBucket,
			SpriteWorkbench, SpriteBucket, SpriteBucketPaint1, SpriteBucketPaint2, SpriteBucketPaint3, SpriteLeaf1, SpriteLeaf2, SpriteLeaf3, SpriteLeaf4, SpriteLeaf5,
			SpriteColorWheel1, SpriteColorWheel2, SpriteColorWheel3, SpriteColorWheel4, SpriteColorWheel5, SpriteColorWheel6, SpriteColorWheel7, SpriteColorWheel8,
			SpriteBush, SpriteBushBerries, SpriteBushGrass, SpritePlayerShake, SpriteCloud, SpriteSun, SpriteWateringCan, SpritePumpkin1, SpritePumpkin2, SpriteFountain,
			SpriteSpacebar;
		public Sprite BackgroundPark, BackgroundTitleScreen;
		public SpriteFont FontTest;
		public Song Ambience;
		public SoundEffect SoundTreeShake, SoundWind1, SoundWind2, SoundPaintSelect, SoundPaintApply, SoundPumpkinPickup, SoundPumpkinPlace, SoundWaterPour1, SoundWaterPour2;

		private ContentManager content;
		private SpriteBatch batch;

		public AssetManager(ContentManager content, SpriteBatch batch)
		{
			this.content = content;
			this.batch = batch;
		}
		public void Load()
		{
			SpritePlayerTest = new Sprite(batch, content.Load<Texture2D>("test"), 12);
			SpritePlayer = new Sprite(batch, content.Load<Texture2D>("spritePlayer"), 2);
			SpritePlayerHoldBucket = new Sprite(batch, content.Load<Texture2D>("spritePlayerHoldBucket"), 2);
			SpritePlayerShake = new Sprite(batch, content.Load<Texture2D>("spritePlayerShake"), 2);
			SpriteWorkbench = new Sprite(batch, content.Load<Texture2D>("spriteWorkbench"), 2);
			SpriteBush = new Sprite(batch, content.Load<Texture2D>("spriteBush"));
			SpriteBushBerries = new Sprite(batch, content.Load<Texture2D>("spriteBushBerries"));
			SpriteBushGrass = new Sprite(batch, content.Load<Texture2D>("spriteBushGrass"));
			SpriteCloud = new Sprite(batch, content.Load<Texture2D>("spriteCloud"));
			SpriteSun = new Sprite(batch, content.Load<Texture2D>("spriteSun"));
			SpriteWateringCan = new Sprite(batch, content.Load<Texture2D>("spriteWateringCan"));
			SpriteSpacebar = new Sprite(batch, content.Load<Texture2D>("spriteSpacebar"), 2);
			SpritePumpkin1 = new Sprite(batch, content.Load<Texture2D>("spritePumpkin1"));
			SpritePumpkin2 = new Sprite(batch, content.Load<Texture2D>("spritePumpkin2"));
			SpriteFountain = new Sprite(batch, content.Load<Texture2D>("spriteFountain"), 3);
			SpriteBucket = new Sprite(batch, content.Load<Texture2D>("spriteBucket"));
			SpriteBucketPaint1 = new Sprite(batch, content.Load<Texture2D>("spriteBucketPaint1"));
			SpriteBucketPaint2 = new Sprite(batch, content.Load<Texture2D>("spriteBucketPaint2"));
			SpriteBucketPaint3 = new Sprite(batch, content.Load<Texture2D>("spriteBucketPaint3"));
			SpriteLeaf1 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf1"));
			SpriteLeaf2 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf2"));
			SpriteLeaf3 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf3"));
			SpriteLeaf4 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf4"));
			SpriteLeaf5 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf5"));
			SpriteColorWheel1 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel1"));
			SpriteColorWheel2 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel2"));
			SpriteColorWheel3 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel3"));
			SpriteColorWheel4 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel4"));
			SpriteColorWheel5 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel5"));
			SpriteColorWheel6 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel6"));
			SpriteColorWheel7 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel7"));
			SpriteColorWheel8 = new Sprite(batch, content.Load<Texture2D>("spriteColorWheel8"));
			Texture2D textureTree = content.Load<Texture2D>("spriteTree");
			SpriteTree = new Sprite(batch, textureTree, origin: new Vector2(textureTree.Width / 2.0f, textureTree.Height));
			Texture2D textureTreeBush1 = content.Load<Texture2D>("spriteTreeBush1");
			SpriteTreeBush1 = new Sprite(batch, textureTreeBush1, origin: new Vector2(textureTreeBush1.Width / 2.0f, textureTreeBush1.Height));
			Texture2D textureTreeBush2 = content.Load<Texture2D>("spriteTreeBush2");
			SpriteTreeBush2 = new Sprite(batch, textureTreeBush2, origin: new Vector2(textureTreeBush2.Width / 2.0f, textureTreeBush2.Height));
			Texture2D textureTreeBush3 = content.Load<Texture2D>("spriteTreeBush3");
			SpriteTreeBush3 = new Sprite(batch, textureTreeBush3, origin: new Vector2(textureTreeBush3.Width / 2.0f, textureTreeBush3.Height));
			Texture2D textureLesserTreeBush1 = content.Load<Texture2D>("spriteLesserTreeBush1");
			SpriteLesserTreeBush1 = new Sprite(batch, textureLesserTreeBush1, origin: new Vector2(textureLesserTreeBush1.Width / 2.0f, textureLesserTreeBush1.Height));
			Texture2D textureLesserTreeBush2 = content.Load<Texture2D>("spriteLesserTreeBush2");
			SpriteLesserTreeBush2 = new Sprite(batch, textureLesserTreeBush2, origin: new Vector2(textureLesserTreeBush2.Width / 2.0f, textureLesserTreeBush2.Height));
			Texture2D textureLesserTreeBush3 = content.Load<Texture2D>("spriteLesserTreeBush3");
			SpriteLesserTreeBush3 = new Sprite(batch, textureLesserTreeBush3, origin: new Vector2(textureLesserTreeBush3.Width / 2.0f, textureLesserTreeBush3.Height));
			SpritePlayButton = new Sprite(batch, content.Load<Texture2D>("spritePlayButton"));
			SpriteInstructionsButton = new Sprite(batch, content.Load<Texture2D>("spriteInstructionsButton"));
			SpriteCreditsButton = new Sprite(batch, content.Load<Texture2D>("spriteCreditsButton"));
			SpriteExitButton = new Sprite(batch, content.Load<Texture2D>("spriteExitButton"));
			SpriteInstructions = new Sprite(batch, content.Load<Texture2D>("spriteInstructions"));
			SpriteCredits = new Sprite(batch, content.Load<Texture2D>("spriteCredits"));
			BackgroundTitleScreen = new Sprite(batch, content.Load<Texture2D>("titleScreenBackground"));
			BackgroundPark = new Sprite(batch, content.Load<Texture2D>("parkBackground"));

			FontTest = content.Load<SpriteFont>("testFont");

			Ambience = content.Load<Song>("ambience");

			SoundTreeShake = content.Load<SoundEffect>("soundTreeShake");
			SoundWind1 = content.Load<SoundEffect>("soundWind1");
			SoundWind2 = content.Load<SoundEffect>("soundWind2");
			SoundPaintSelect = content.Load<SoundEffect>("soundPaintSelect");
			SoundPaintApply = content.Load<SoundEffect>("soundPaintApply");
			SoundPumpkinPickup = content.Load<SoundEffect>("soundPumpkinPickup");
			SoundPumpkinPlace = content.Load<SoundEffect>("soundPumpkinPlace");
			SoundWaterPour1 = content.Load<SoundEffect>("soundWaterPour1");
			SoundWaterPour2 = content.Load<SoundEffect>("soundWaterPour2");
		}
	}
}
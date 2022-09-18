using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GoodbyeSummerGameJam
{
	public class AssetManager
	{
		public Sprite SpritePlayerTest, SpriteTree, SpriteTreeBush1, SpriteTreeBush2, SpriteTreeBush3, SpriteLesserTreeBush1, SpriteLesserTreeBush2, SpriteLesserTreeBush3,
			SpritePlayButton, SpriteInstructionsButton, SpriteCreditsButton, SpriteExitButton, SpriteInstructions, SpriteCredits, SpritePlayer, SpritePlayerHoldBucket,
			SpriteWorkbench, SpriteBucket, SpriteLeaf1, SpriteLeaf2, SpriteLeaf3, SpriteLeaf4, SpriteLeaf5;
		public Sprite BackgroundPark, BackgroundTitleScreen;
		public SpriteFont FontTest;
		public Song Ambience;

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
			SpriteWorkbench = new Sprite(batch, content.Load<Texture2D>("spriteWorkbench"), 2);
			SpriteBucket = new Sprite(batch, content.Load<Texture2D>("spriteBucket"));
			SpriteLeaf1 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf1"));
			SpriteLeaf2 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf2"));
			SpriteLeaf3 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf3"));
			SpriteLeaf4 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf4"));
			SpriteLeaf5 = new Sprite(batch, content.Load<Texture2D>("spriteLeaf5"));
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
		}
	}
}
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GoodbyeSummerGameJam
{
	public class AssetManager
	{
		public Sprite SpritePlayerTest;
		public Sprite BackgroundPark;
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
			BackgroundPark = new Sprite(batch, content.Load<Texture2D>("parkBackground"));
			FontTest = content.Load<SpriteFont>("testFont");
			Ambience = content.Load<Song>("ambience");
		}
	}
}
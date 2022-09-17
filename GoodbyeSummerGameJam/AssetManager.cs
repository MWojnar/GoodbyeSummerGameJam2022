using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GoodbyeSummerGameJam
{
	public class AssetManager
	{
		public Sprite spritePlayerTest;
		private ContentManager content;
		private SpriteBatch batch;

		public AssetManager(ContentManager content, SpriteBatch batch)
		{
			this.content = content;
			this.batch = batch;
		}
		public void Load()
		{
			spritePlayerTest = new Sprite(batch, content.Load<Texture2D>("test"), 12);
		}
	}
}
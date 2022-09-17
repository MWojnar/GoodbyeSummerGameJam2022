using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GoodbyeSummerGameJam
{
	public class World : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private List<Entity> entities;

		public AssetManager Assets;

		public World()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();
			entities = new List<Entity>();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			AssetManager.Load();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			base.Update(gameTime);
			foreach (Entity entity in entities)
			{
				entity.update(gameTime);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			base.Draw(gameTime);
			foreach (Entity entity in entities)
			{
				entity.draw(gameTime);
			}
			
			spriteBatch.End();
		}
	}
}

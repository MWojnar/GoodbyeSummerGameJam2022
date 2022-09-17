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
		private StateHandler stateHandler;

		public AssetManager Assets;

		public World()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			stateHandler = new StateHandler();
		}

		protected override void Initialize()
		{
			base.Initialize();
			entities = new List<Entity>();
			entities.Add(new Player(this));
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			Assets = new AssetManager(Content, spriteBatch);
			Assets.Load();
		}

		protected override void Update(GameTime gameTime)
		{
			stateHandler.reloadStates();
			if (stateHandler.GamePadState[0].Buttons.Back == ButtonState.Pressed || stateHandler.KeyboardState.IsKeyDown(Keys.Escape))
				Exit();

			base.Update(gameTime);
			foreach (Entity entity in entities)
			{
				entity.update(gameTime, stateHandler);
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

using GoodbyeSummerGameJam.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace GoodbyeSummerGameJam
{
	public class World : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private List<Entity> entities, entitiesToAdd, entitiesToRemove;
		private StateHandler stateHandler;
		private Matrix viewScaleMatrix;
		private int targetFrameRate, viewScale, currentViewScale;

		public AssetManager Assets;

		public World()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			stateHandler = new StateHandler();
			targetFrameRate = 60;
			entities = new List<Entity>();
			entitiesToAdd = new List<Entity>();
			entitiesToRemove = new List<Entity>();
			viewScale = 5;
			currentViewScale = 5;
			viewScaleMatrix = Matrix.CreateScale(viewScale, viewScale, 1);
		}

		protected override void Initialize()
		{
			base.Initialize();
			graphics.PreferredBackBufferWidth = 1920;
			graphics.PreferredBackBufferHeight = 1080;
			graphics.ApplyChanges();
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Play(Assets.Ambience);
			LoadLevel(0);
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
			spriteBatch.Begin(SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp, transformMatrix: viewScaleMatrix);
			base.Draw(gameTime);
			foreach (Entity entity in entities)
			{
				entity.draw(gameTime, spriteBatch);
			}
			processEntityChanges();
			
			spriteBatch.End();
		}

		public int GetTargetFrameRate()
		{
			return targetFrameRate;
		}

		public void LoadLevel(int level)
		{
			ClearEntities();
			switch (level)
			{
				case 0: currentViewScale = 1; AddEntity(new Menu(this)); break;
				case 1: currentViewScale = viewScale; /*AddEntity(new Player(this, GetDimensions() / 2)); */AddEntity(new Tree(this, GetDimensions() / 2 + new Vector2(-40, 35))); AddEntity(new Entity(this, GetDimensions() / 2, Assets.BackgroundPark, .75f)); break;
			}
			viewScaleMatrix = Matrix.CreateScale(currentViewScale, currentViewScale, 1);
		}

		public void AddEntity(Entity entity)
		{
			entitiesToAdd.Add(entity);
		}

		public void RemoveEntity(Entity entity)
		{
			entitiesToRemove.Add(entity);
		}

		public void ClearEntities()
		{
			entitiesToRemove.AddRange(entities);
		}

		private void processEntityChanges()
		{
			foreach (Entity entity in entitiesToRemove)
				entities.Remove(entity);
			entitiesToRemove.Clear();
			foreach (Entity entity in entitiesToAdd)
				entities.Add(entity);
			entitiesToAdd.Clear();
		}

		public Vector2 GetDimensions()
		{
			return new Vector2(graphics.PreferredBackBufferWidth / currentViewScale, graphics.PreferredBackBufferHeight / currentViewScale);
		}

	}
}

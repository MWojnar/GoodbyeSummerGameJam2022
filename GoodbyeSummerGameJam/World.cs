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
		private List<Pallete> palletes;
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
			palletes = new List<Pallete>() { new Pallete(new Dictionary<Color, int>() { { new Color(31, 104, 39), 1 }, { new Color(39, 128, 31), 1 }, { new Color(59, 148, 31), 1 } }),
			new Pallete(new Dictionary<Color, int>() { { new Color(101, 154, 40), 1 }, { new Color(92, 196, 26), 1 }, { new Color(140, 193, 45), 1 } }),
			new Pallete(new Dictionary<Color, int>() { { new Color(128, 84, 17), 1 }, { new Color(151, 122, 28), 1 }, { new Color(166, 153, 22), 1 } }),
			new Pallete(new Dictionary<Color, int>() { { new Color(151, 36, 23), 1 }, { new Color(145, 72, 32), 1 }, { new Color(166, 100, 32), 1 } }),
			new Pallete(new Dictionary<Color, int>() { { new Color(31, 47, 128), 1 }, { new Color(37, 85, 139), 1 }, { new Color(36, 134, 163), 1 } }),
			new Pallete(new Dictionary<Color, int>() { { new Color(110, 18, 116), 1 }, { new Color(145, 31, 112), 1 }, { new Color(196, 48, 107), 1 } }),
			new Pallete(new Dictionary<Color, int>() { { new Color(31, 47, 128), 1 }, { new Color(151, 36, 23), 1 }, { new Color(31, 104, 39), 1 } }),
			new Pallete(new Dictionary<Color, int>() { { new Color(196, 48, 107), 1 }, { new Color(36, 134, 163), 1 }, { new Color(92, 196, 26), 1 } }), };
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

		internal List<Entity> GetEntities()
		{
			return entities;
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
				case 1: currentViewScale = viewScale;
					Workbench bench = new Workbench(this, new Vector2(GetDimensions().X / 2, 25));
					AddEntity(bench);
					Player player = new Player(this, bench, GetDimensions() / 2);
					AddEntity(player);
					AddEntity(new Tree(this, GetDimensions() / 2 + new Vector2(-40, 35)));
					AddEntity(new Bush(this, GetDimensions() / 2 + new Vector2(0, 65)));
					AddEntity(new Cloud(this, new Vector2(40, 30), depth: .44f));
					AddEntity(new Sun(this, new Vector2(GetDimensions().X - 30, 30), depth: .45f));
					AddEntity(new Entity(this, GetDimensions() / 2, Assets.BackgroundPark, .75f));
					AddEntity(new Entity(this, GetDimensions() / 2 + new Vector2(70, 12), Assets.spriteFountain));
					break;
			}
			viewScaleMatrix = Matrix.CreateScale(currentViewScale, currentViewScale, 1);
		}

		public Player GetPlayer()
		{
			foreach (Entity entity in entities)
				if (entity is Player)
					return (Player)entity;
			return null;
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

		public List<Pallete> GetPalletes()
		{
			return palletes;
		}

	}
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace sprint0.Sprites
{
    public static class Texture2DStorage
{
	public static Texture2D linkSpriteSheet;
	public static Texture2D blockSpriteSheet;
	public static Texture2D bossSpriteSheet;
	public static Texture2D enemiesSpriteSheet;
	public static Texture2D itemSpriteSheet;
	public static Texture2D miscSpriteSheet;
	public static Texture2D dungeonBorder;
	public static SpriteFont font;
	
	private static GraphicsDevice graphicsDevice;
	private static Dictionary<string, Texture2D> textureCache;
	private static Texture2D whitePixel;

	public static void LoadAllTextures(ContentManager Content)
	{
		blockSpriteSheet = Content.Load<Texture2D>("legendofzelda_blocks_sheet");
		linkSpriteSheet = Content.Load<Texture2D>("legendofzelda_link_sheet");
		bossSpriteSheet = Content.Load<Texture2D>("legendofzelda_bosses_sheet");
		enemiesSpriteSheet = Content.Load<Texture2D>("Sprites/legendofzelda_enemies_sheet_final");
		itemSpriteSheet = Content.Load<Texture2D>("legendofzelda_items_sheet");
		miscSpriteSheet = Content.Load<Texture2D>("legendofzelda_misccharacters_sheet");
		dungeonBorder = Content.Load<Texture2D>("Room2Border");
		font = Content.Load<SpriteFont>("Font/font");
	}

	public static Texture2D GetLinkSpriteSheet()
	{
		return linkSpriteSheet;
	}

	public static Texture2D GetBlockSpriteSheet()
	{
		return blockSpriteSheet;
	}
	public static Texture2D GetBossSpriteSheet()
	{
		return bossSpriteSheet;
	}
	public static Texture2D GetEnemiesSpriteSheet()
	{
		return enemiesSpriteSheet;
	}
	public static Texture2D GetItemSpriteSheet()
	{
		return itemSpriteSheet;
	}
	public static Texture2D GetMiscSpriteSheet()
	{
		return miscSpriteSheet;
	}

	public static Texture2D GetDungeonBorder()
	{
		return dungeonBorder;
	}
	public static SpriteFont GetFont()
	{
		return font;
	}
	
	public static void Init(GraphicsDevice device)
	{
		graphicsDevice = device;
		textureCache = new Dictionary<string, Texture2D>();
		
		// Create a white pixel texture for fallback drawing
		whitePixel = new Texture2D(device, 1, 1);
		whitePixel.SetData(new[] { Color.White });
	}
	
	public static Texture2D GetTexture(string name)
	{
		// Return cached texture if available
		if (textureCache != null && textureCache.ContainsKey(name))
		{
			return textureCache[name];
		}
		
		// For now, return white pixel as fallback
		// You'll need to load actual textures from sprite sheets or content pipeline
		if (whitePixel != null)
			return whitePixel;
		
		// If Init() hasn't been called, return null
		// This should be handled by the caller
		return null;
	}
	
	public static void RegisterTexture(string name, Texture2D texture)
	{
		if (textureCache == null)
			textureCache = new Dictionary<string, Texture2D>();
		textureCache[name] = texture;
	}
}
}

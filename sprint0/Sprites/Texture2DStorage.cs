using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0.Interfaces;
using sprint0.Classes;
using System;
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
	public static SpriteFont font;

	public static void LoadAllTextures(ContentManager Content)
	{
		blockSpriteSheet = Content.Load<Texture2D>("legendofzelda_blocks_sheet");
		linkSpriteSheet = Content.Load<Texture2D>("legendofzelda_link_sheet");
		bossSpriteSheet = Content.Load<Texture2D>("legendofzelda_bosses_sheet");
		enemiesSpriteSheet = Content.Load<Texture2D>("legendofzelda_enemies_sheet_final");
		itemSpriteSheet = Content.Load<Texture2D>("legendofzelda_items_sheet");
		miscSpriteSheet = Content.Load<Texture2D>("legendofzelda_misccharacters_sheet");
		font = Content.Load<SpriteFont>("font");
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
	public static SpriteFont GetFont()
	{
		return font;
	}
}
}

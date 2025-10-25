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
	public static Texture2D dungeonBorder;
	public static SpriteFont font;

	private static Texture2D LoadTextureSafe(ContentManager content, GraphicsDevice gd, string assetName)
	{
		try
		{
			return content.Load<Texture2D>(assetName);
		}
		catch (ContentLoadException)
		{
			var tex = new Texture2D(gd, 1, 1);
			tex.SetData(new[] { Color.Magenta }); // visible placeholder
			return tex;
		}
	}

	private static Texture2D TryLoadAny(ContentManager content, GraphicsDevice gd, params string[] names)
	{
		foreach (var n in names)
		{
			try
			{
				return content.Load<Texture2D>(n);
			}
			catch { /* try next */ }
		}
		// last-resort placeholder
		var tex = new Texture2D(gd, 1, 1);
		tex.SetData(new[] { Microsoft.Xna.Framework.Color.Magenta });
		return tex;
	}

	public static void LoadAllTextures(ContentManager content, GraphicsDevice gd)
	{
		// Border
		dungeonBorder = TryLoadAny(content, gd, "Room2Border", "Content/Room2Border");

		// Blocks / tiles
		blockSpriteSheet = TryLoadAny(content, gd,
			"legendofzelda_blocks_sheet",
			"Sprites/legendofzelda_blocks_sheet");

		// Enemies
		enemiesSpriteSheet = TryLoadAny(content, gd,
			"Sprites/legendofzelda_enemies_sheet_final",
			"legendofzelda_enemies_sheet_final",
			"Sprites/legendofzelda_enemies_sheet");

		// Items
		itemSpriteSheet = TryLoadAny(content, gd,
			"legendofzelda_items_sheet",
			"Sprites/legendofzelda_items_sheet");

		// Link
		linkSpriteSheet = TryLoadAny(content, gd,
			"legendofzelda_link_sheet",
			"Sprites/legendofzelda_link_sheet");

		// Misc
		miscSpriteSheet = TryLoadAny(content, gd,
			"legendofzelda_misccharacters_sheet",
			"Sprites/legendofzelda_misccharacters_sheet");

		// Boss sheet - keep existing name as fallback
		bossSpriteSheet = TryLoadAny(content, gd, "legendofzelda_bosses_sheet", "Sprites/legendofzelda_bosses_sheet");

		// Font (no safe fallback)
		font = content.Load<SpriteFont>("Font/font");
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
}
}

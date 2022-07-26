using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Map;
using Terraria.DataStructures;
using System;
using System.Windows.Input;

namespace ATB.Items.Placeables
{
	public class replicator : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Replicator");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.createTile = ModContent.TileType<Items.replicator>(); // This sets the id of the tile that this item should place when used.

			Item.width = 48; // The item texture's width
			Item.height = 32; // The item texture's height

			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 15;

			Item.maxStack = 99;
			Item.consumable = true;
			Item.value = 150;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.IronBar, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
			Recipe recipe2 = CreateRecipe();
			recipe2.AddIngredient(ItemID.LeadBar, 10);
			recipe2.AddTile(TileID.WorkBenches);
			recipe2.Register();
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	}
}

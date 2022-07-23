using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

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

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
	}
}

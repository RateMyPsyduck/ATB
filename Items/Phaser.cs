using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace ATB.Items
{
    class Phaser:ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoot a laser beam that can eliminate anything...");
		}

		public override void SetDefaults() {
			Item.damage = 40;
			Item.noMelee = true;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = 5;
			Item.shootSpeed = 0.01f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<PhaserBeam>();
			Item.value = Item.sellPrice(silver: 3);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}

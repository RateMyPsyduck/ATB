using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace ATB.Items
{
	public class StarFleetHandAxe : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Handy in a crash or a fight.");
            DisplayName.SetDefault("Survival Axe");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 28;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; // Automatically re-swing/re-use this item after its swinging animation is over.

			Item.axe = 30; 
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile<Items.replicator>();
			recipe.Register();
		}
	}
}

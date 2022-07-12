using System;
using Terraria;
using Terraria.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using ReLogic.Content;

using static Terraria.ModLoader.ModContent;

namespace ATB.Items
{
    class MindMeld:ModItem
	{
		public int proj = 0;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("My mind to your mind...");
            DisplayName.SetDefault("Vulcan Mind Meld");
		}

		public override void SetDefaults() {
			Item.damage = 20;
			Item.noMelee = true;
			Item.channel = false; //Channel so that you can held the weapon [Important]
			Item.mana = 1;
			Item.rare = ItemRarityID.Pink;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 5;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = 14;
			Item.shootSpeed = 1.6f;
			Item.useAnimation = 5;
			Item.shoot = ProjectileType<MindMeldRing>();
			Item.value = Item.sellPrice(silver: 3);
            Item.autoReuse = true;
            Item.noUseGraphic = true;
			//Item.scale = 0.8f;
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

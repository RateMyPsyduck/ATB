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

using static Terraria.ModLoader.ModContent;

namespace ATB.Items
{
    class BambooBlaster:ModItem
	{
		int timer = 0;
		//SoundStyle Pew = new SoundSt6+yle($"{nameof(ATB)}/Items/PhotonLaunch");
		public int proj = 0;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Mythbuster!");
            DisplayName.SetDefault("Bamboo Launcher");
		}

		public override void SetDefaults() {
			Item.damage = 115;
			Item.noMelee = true;
			Item.channel = false; //Channel so that you can held the weapon [Important]
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = 5;
			Item.shootSpeed = 12f;
            Item.consumable = true;
			Item.useAnimation = 20;
			Item.shoot = 711;
			Item.value = Item.sellPrice(gold: 11);
			Item.scale = 0.9f;
			Item.UseSound = null;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override void UseAnimation(Player player) {
			if (player.whoAmI == Main.myPlayer){
				player.itemAnimation = 5;
			}
        }

		public override Vector2? HoldoutOffset()
        {
			Vector2 g;
            g.Y = -1;
            g.X = -3;
			return g;

        }

	}
}
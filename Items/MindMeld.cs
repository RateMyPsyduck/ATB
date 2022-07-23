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
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Map;
using Terraria.DataStructures;
using System;
using System.Windows.Input;
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
			Item.channel = false;
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
			Item.UseSound = new SoundStyle($"{nameof(ATB)}/Items/MindMeldSound") {
				Volume = 0.9f,
				PitchVariance = 0f,
				MaxInstances = 3,
			};
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile<Items.replicator>();
			recipe.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
            if(velocity.X >= 0f){
                position.X = position.X + 14f;
            }
            else{
                position.X = position.X - 14f;
            }
            position.Y = position.Y - 15f;
            player.eyeHelper.BlinkBecausePlayerGotHurt();
            Projectile.NewProjectileDirect(source, position, velocity, type, damage, 0f, player.whoAmI);
            return false;
		}
	}
}

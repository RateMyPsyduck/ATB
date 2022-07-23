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
    class PhotonLauncher:ModItem
	{
		int[] timers = {0,0,0,0};
		//SoundStyle Pew = new SoundSt6+yle($"{nameof(ATB)}/Items/PhotonLaunch");
		public int proj = 0;
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("The power of the sun in the palm of my hand!");
            DisplayName.SetDefault("Photon Launcher");
		}

		public override void SetDefaults() {
			Item.damage = 65;
			Item.noMelee = true;
			Item.channel = false; //Channel so that you can held the weapon [Important]
			Item.mana = 0;
			Item.rare = ItemRarityID.Pink;
			Item.width = 28;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = 5;
			Item.shootSpeed = 12f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<Photon>();
			Item.value = Item.sellPrice(gold: 11);
			Item.scale = 1f;
			Item.UseSound = null;
		}

		public override bool AltFunctionUse(Player player) {
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile<Items.replicator>();
			recipe.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback){
			bool set = false;
			for(int i = 0; i < 4; i++){
				if(timers[i] == 0){
					timers[i]++;
					SoundEngine.PlaySound(new SoundStyle($"{nameof(ATB)}/Items/PhotonLaunch"), player.position);
					return true;
				}
			}
			return false;
		}

		public override void UpdateInventory (Player player){
			for(int i = 0; i < 4; i++){
				if(timers[i] > 300){
					timers[i] = 0;
					SoundEngine.PlaySound(new SoundStyle($"{nameof(ATB)}/Items/PhotonReboot"), player.position);
					Dust.NewDust(player.Center, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.Red, 1.3f);
				}
				if(timers[i] > 0){
					timers[i]++;
				}
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

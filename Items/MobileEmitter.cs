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
using ATB;

namespace ATB.Items
{
    public class MobileEmitter:ModItem
	{

		// public override string Texture => $"Terraria/Images/Item_{ItemID.IceMirror}"; // Copies the texture for the Ice Mirror, make your own texture if need be.

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; // Amount of this item needed to research and become available in Journey mode's duplication menu. Amount used based upon vanilla Magic Mirror's amount needed.
		}

		public override void SetDefaults() {
			//Item.CloneDefaults(ItemID.IceMirror); // Copies the defaults from the Ice Mirror.
			Item.width = 24;
			Item.height = 28;
			Item.maxStack = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = 6;
			Item.consumable = true;
            Item.noUseGraphic = true;
			
			//Item.color = Color.Violet; // Sets the item Color
			Item.UseSound = new SoundStyle($"{nameof(ATB)}/Items/BeamUp") {
				Volume = 0.9f,
				PitchVariance = 0f,
				MaxInstances = 3,
			};
		}
		
		public override bool? UseItem(Player player) {
			if (player.whoAmI == Main.myPlayer) {
				// If the player using the item is the client
				// (explicitely excluded serverside here)
				SoundEngine.PlaySound(new SoundStyle($"{nameof(ATB)}/Items/BeamUp"), player.position);

				int type = ModContent.NPCType<Doctor>();

				NPC.NewNPC(null, (int)player.position.X + player.width, (int)player.position.Y + player.height, type, 0, 0f, 0f, 0f, 0f, 255);
			}
			return true;
		}
        

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile<Items.replicator>();
			recipe.Register();
		}
	}
}

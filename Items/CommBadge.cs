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
	[AutoloadEquip(EquipType.Shield)]
    public class CommBadge:ModItem
	{

		private Player p = null;
		private static readonly Color[] itemNameCycleColors = {
			new Color(254, 105, 47),
			new Color(190, 30, 209),
			new Color(34, 221, 151),    
			new Color(0, 106, 185),
		};

		// public override string Texture => $"Terraria/Images/Item_{ItemID.IceMirror}"; // Copies the texture for the Ice Mirror, make your own texture if need be.

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; // Amount of this item needed to research and become available in Journey mode's duplication menu. Amount used based upon vanilla Magic Mirror's amount needed.
		}

		public override void SetDefaults() {
			//Item.CloneDefaults(ItemID.IceMirror); // Copies the defaults from the Ice Mirror.
            Item.noUseGraphic = true;
			Item.width = 24;
			Item.height = 28;
            Item.useStyle = 3;
			Item.accessory = true;
			Item.useTime = 200;
			
			//Item.color = Color.Violet; // Sets the item Color
			Item.UseSound = new SoundStyle($"{nameof(ATB)}/Items/BeamUp") {
				Volume = 0.9f,
				PitchVariance = 0f,
				MaxInstances = 3,
			};
		}

	

        // public override bool PreDrawInWorld (SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI){
        //     return false;
        // }

        // public override bool PreDrawInInventory (SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale){
        //     return false;
        // }

		public override void UpdateAccessory(Player player, bool hideVisual) {
			// player.GetDamage(DamageClass.Generic) += 1f; // Increase ALL player damage by 100%
			// player.endurance = 1f - (0.1f * (1f - player.endurance));  // The percentage of damage reduction
			player.GetModPlayer<BeamPlayer>().CommBadgeOn = true;
			// if (player.controlDown) {
			//player.Teleport(new Vector2(100,100), 1);
			// }
			//p = player;

		}
		
		// private void KeyDown(KeyPressEventArgs e){
		// 	if (e.KeyCode == Keys.B && p != null){
		// 		player.Teleport(new Vector2(100,100), -1);
		// 	}
		// }


		// UseStyle is called each frame that the item is being actively used.
		public override void UseStyle(Player player, Rectangle heldItemFrame) {
			SoundStyle BU = new SoundStyle($"{nameof(ATB)}/Items/BeamUp");
			SoundStyle BD = new SoundStyle($"{nameof(ATB)}/Items/BeamDown");
			// Each frame, make some dust
			Random random = new Random();
			if (Main.rand.NextBool()) {
				Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, Color.White, 1.1f); // Makes dust from the player's position and copies the hitbox of which the dust may spawn. Change these arguments if needed.
			}

			// This sets up the itemTime correctly.
			Main.NewText(player.itemTime.ToString() + ", " + player.itemTimeMax.ToString(), 150, 0, 0);
			if (player.itemTime == 0) {
				player.ApplyItemTime(Item);
			}
			else if ((player.itemTime - 1) == player.itemTimeMax / 2) {
				// This code runs once halfway through the useTime of the Item. You'll notice with magic mirrors you are still holding the item for a little bit after you've teleported.

				// Make dust 70 times for a cool effect.
				for (int d = 0; d < 35; d++) {
					Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, player.velocity.X * 0.1f, player.velocity.Y * 0.1f, 150, default, 1.5f);
				}

				// This code releases all grappling hooks and kills/despawns them.
				player.grappling[0] = -1;
				player.grapCount = 0;

				for (int p = 0; p < 1000; p++) {
					if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7) {
						Main.projectile[p].Kill();
					}
				}

				// The actual method that moves the player back to bed/spawn.
				//Main.NewText(player.getLocation().ToString(), 150, 0, 0);
				Main.LocalPlayer.Teleport(new Vector2(random.Next(Main.maxTilesX * 16),random.Next(Main.maxTilesY * 16)), -1);
				//player.Spawn(PlayerSpawnContext.RecallFromItem);

				// Make dust 70 times for a cool effect. This dust is the dust at the destination.
				for (int d = 0; d < 35; d++) {
					Dust.NewDust(player.position, player.width, player.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
				}
				SoundEngine.PlaySound(BD, player.position);
				player.itemTime = 0;
			}
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			// This code shows using Color.Lerp,  Main.GameUpdateCount, and the modulo operator (%) to do a neat effect cycling between 4 custom colors.
			int numColors = itemNameCycleColors.Length;
			
			foreach (TooltipLine line2 in tooltips) {
				if (line2.Mod == "Terraria" && line2.Name == "ItemName") {
					float fade = (Main.GameUpdateCount % 60) / 60f;
					int index = (int)((Main.GameUpdateCount / 60) % numColors);
					int nextIndex = (index + 1) % numColors;

					line2.OverrideColor = Color.Lerp(itemNameCycleColors[index], itemNameCycleColors[nextIndex], fade);
				}
			}
		}


		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}

	public class BeamPlayer : ModPlayer
	{
		Player player = Main.LocalPlayer;
		//UISystem ui = new UISystem();
		UISystem ui = ModContent.GetInstance<UISystem>();

		public List<Vector2> BeamLocations = new List<Vector2>();
		public int BeeamLocationPointer = 0;

		public Vector2 BeamLocation = new Vector2(1,1);
		bool TimerTrig = false;
		bool TimerFin = false;
		SoundStyle BU = new SoundStyle($"{nameof(ATB)}/Items/BeamUp");
		SoundStyle BD = new SoundStyle($"{nameof(ATB)}/Items/BeamDown");
		public int Timer;
		public bool CommBadgeOn;
		Random random = new Random();
		public override void ProcessTriggers(TriggersSet triggersSet)
			{
				//Berserk minion hotkey
				if (ATB.beamKey.JustPressed && CommBadgeOn == true && BeamLocations.Count > 0) {
					TimerTrig = true;
					//Main.LocalPlayer.AddBuff(10, 20);
					SoundEngine.PlaySound(BU, Main.LocalPlayer.position);
					
				}
				else if(ATB.UIKey.JustPressed){
					ui.ShowMyUI();
				}
			}

		public override void ResetEffects() {
			CommBadgeOn = false;
		}

		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
			Main.LocalPlayer.itemTime = 0;
		}
		// public override void PreUpdate() {
		// 		Main.NewText(ATB.beamKey.JustPressed.ToString(), 150, 0, 0);
		// 		if (ATB.beamKey.JustPressed) {
		// 			Player.Teleport(new Vector2(0,0), -1);
		// 		}
		// }
		public override void PreUpdate(){
			Main.LocalPlayer.shroomiteStealth = true;
			Main.LocalPlayer.stealth = 0.1f;
			Main.LocalPlayer.aggro = 1;
			Main.LocalPlayer.stealthTimer = 0;
			// Main.LocalPlayer.stealthTimer = -750;
			// Main.NewText(Main.LocalPlayer.stealth.ToString(), 150, 0, 0);
			//Main.NewText(Main.LocalPlayer.shroomiteStealth.ToString(), 150, 0, 0);
			if(TimerTrig == true){
				Timer++;
				if(Timer > 100){		
					BeamLocation = BeamLocations[BeeamLocationPointer];
					BeamLocation.Y = BeamLocation.Y - Main.LocalPlayer.height;
			 		Main.LocalPlayer.Teleport((BeamLocation), -1);
					TimerTrig = false;
					Timer = 0;
					for (int d = 0; d < 35; d++) {
						Dust.NewDust(Main.LocalPlayer.position, Main.LocalPlayer.width, Main.LocalPlayer.height, DustID.MagicMirror, 0f, 0f, 150, default, 1.5f);
					}
					SoundEngine.PlaySound(BD, Main.LocalPlayer.position);
				}
				if (Timer % 5 == 0) {
				// This code runs once halfway through the useTime of the Item. You'll notice with magic mirrors you are still holding the item for a little bit after you've teleported.

				// Make dust 70 times for a cool effect.
				for (int d = 0; d < 15; d++) {
					Vector2 posChng = Main.LocalPlayer.position;
					posChng.X += 8;
					posChng.Y -= 11;
					Dust.NewDust(posChng, 0, 0, DustID.MagicMirror, 0, Main.LocalPlayer.velocity.Y + 2f, 150, default, 0.5f);
				}

				// This code releases all grappling hooks and kills/despawns them.
				player.grappling[0] = -1;
				player.grapCount = 0;

				for (int p = 0; p < 1000; p++) {
					if (Main.projectile[p].active && Main.projectile[p].owner == Main.LocalPlayer.whoAmI && Main.projectile[p].aiStyle == 7) {
						Main.projectile[p].Kill();
					}
				}

				}
			}
		}

		public void IncreaseBeamPointer(){
			BeeamLocationPointer++;
			if(BeeamLocationPointer > BeamLocations.Count - 1){
				BeeamLocationPointer = 0;
			}
		}

		public void DecreaseBeamPointer(){
			BeeamLocationPointer--;
			if(BeeamLocationPointer < 0){
				BeeamLocationPointer = BeamLocations.Count - 1;
			}
		}

		// public override void PostUpdate(){
		// 	Main.LocalPlayer.stealth = 0.1f;
		// 	Main.NewText(Main.LocalPlayer.stealth.ToString(), 0, 200, 0);
		// }

		// public override void PostUpdateBuffs(){
		// 	Main.NewText(Main.LocalPlayer.stealth.ToString(), 0, 0, 200);
		// 	Main.LocalPlayer.stealth = 0.1f;
		// }

		// public override void PostUpdateEquips(){
		// 	Main.NewText(Main.LocalPlayer.stealth.ToString(), 0, 0, 200);
		// 	Main.LocalPlayer.stealth = 0.1f;	
		// }

	}
}

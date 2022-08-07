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
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using System;
using System.Windows.Input;
using ATB;

namespace ATB.Items
{
	[AutoloadEquip(EquipType.Shield)]
    public class CommBadge:ModItem
	{

		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; // Amount of this item needed to research and become available in Journey mode's duplication menu. Amount used based upon vanilla Magic Mirror's amount needed.
		}

		public override void SetDefaults() {
            Item.noUseGraphic = true;
			Item.width = 24;
			Item.height = 28;
            Item.useStyle = 3;
			Item.accessory = true;
			Item.useTime = 200;
			Item.UseSound = new SoundStyle($"{nameof(ATB)}/Items/BeamUp") {
				Volume = 0.9f,
				PitchVariance = 0f,
				MaxInstances = 3,
			};
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<BeamPlayer>().CommBadgeOn = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddTile<Items.replicator>();
			recipe.Register();
		}
	}

	public class BeamPlayer : ModPlayer
	{
		Player player = Main.LocalPlayer;
		//UISystem ui = new UISystem();
		UISystem ui = ModContent.GetInstance<UISystem>();

		public List<Vector2> BeamLocations;
		public int BeeamLocationPointer = 0;

		public Vector2 BeamLocation = new Vector2(1,1);
		bool TimerTrig = false;
		bool TimerFin = false;
		SoundStyle BU = new SoundStyle($"{nameof(ATB)}/Items/BeamUp");
		SoundStyle BD = new SoundStyle($"{nameof(ATB)}/Items/BeamDown");
		public int Timer;
		public bool CommBadgeOn;
		public bool LightHat = false;
		Random random = new Random();

		public override void Initialize(){
			BeamLocations = new List<Vector2>();
		}

		public override void SaveData(TagCompound tag){
			tag["BeamLocations"] = BeamLocations;
		}

		public override void LoadData(TagCompound tag) {
			BeamLocations = tag.Get<List<Vector2>>("BeamLocations");
		}

		public override void OnEnterWorld(Player player){
			for(int i = 0; i < BeamLocations.Count; i++){
				player.Teleport((BeamLocation), -1);
			}
			player.Spawn(PlayerSpawnContext.SpawningIntoWorld);	
		}


		public override void ProcessTriggers(TriggersSet triggersSet)
			{
				if (ATB.beamKey.JustPressed && CommBadgeOn == true && BeamLocations.Count > 0) {
					TimerTrig = true;
					SoundEngine.PlaySound(BU, Main.LocalPlayer.position);
					
				}
				else if(ATB.UIKey.JustPressed){
					ui.ShowMyUI();
				}
			}

		public override void ResetEffects() {
			CommBadgeOn = false;
			LightHat = false;
		}

		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
			Main.LocalPlayer.itemTime = 0;
		}

		public override void PreUpdate(){
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
			if(LightHat == true){
				Lighting.AddLight(new Vector2(Main.LocalPlayer.Center.X, Main.LocalPlayer.BottomLeft.Y - Main.LocalPlayer.height - 2), 0.5f, 0f, 0f);
			}
		}

		public void IncreaseBeamPointer(){
			Main.NewText(Main.bgStyle.ToString());
			Main.NewText(Main.LocalPlayer.ZoneDesert.ToString());
			if(BeamLocations.Count == 0){
				return;
			}
			BeeamLocationPointer++;
			if(BeeamLocationPointer > BeamLocations.Count - 1){
				BeeamLocationPointer = 0;
			}
		}

		public void DecreaseBeamPointer(){
			if(BeamLocations.Count == 0){
				return;
			}
			BeeamLocationPointer--;
			if(BeeamLocationPointer < 0){
				BeeamLocationPointer = BeamLocations.Count - 1;
			}
		}

		public Vector2 getDrawPoint(){
			if(BeamLocations.Count == 0){
				return Main.LocalPlayer.BottomLeft;
			}
			return BeamLocations[BeeamLocationPointer];
		}

		public void DeleteEntry(){
			if(BeamLocations.Count == 0){
				return;
			}
			else{
				BeamLocations.RemoveAt(BeeamLocationPointer);
			}
		}
	}
}

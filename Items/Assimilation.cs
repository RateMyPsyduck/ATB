using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ATB.Items
{
	public class Assimilation : ModBuff
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Assimilation"); // Buff display name
			Description.SetDefault("Become one with the collective!"); // Buff description
			Main.debuff[Type] = true;  // Is it a debuff?
			Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
			Main.buffNoSave[Type] = true; // Causes this buff not to persist when exiting and rejoining the world
			BuffID.Sets.LongerExpertDebuff[Type] = true; // If this buff is a debuff, setting this to true will make this buff last twice as long on players in expert mode
		}

		// Allows you to make this buff give certain effects to the given player
		public override void Update(Player player, ref int buffIndex) {
			player.GetModPlayer<AssimilationPlayer>().lifeRegenDebuff= true;
		}
	}

	public class AssimilationPlayer : ModPlayer
	{
		// Flag checking when life regen debuff should be activated
		public bool lifeRegenDebuff;

		public override void ResetEffects() {
			lifeRegenDebuff = false;
		}

		// Allows you to give the player a negative life regeneration based on its state (for example, the "On Fire!" debuff makes the player take damage-over-time)
		// This is typically done by setting player.lifeRegen to 0 if it is positive, setting player.lifeRegenTime to 0, and subtracting a number from player.lifeRegen
		// The player will take damage at a rate of half the number you subtract per second
		public override void UpdateBadLifeRegen() {
			if (lifeRegenDebuff) {
				// These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects
				if (Player.lifeRegen > 0)
					Player.lifeRegen = 0;
				// Player.lifeRegenTime uses to increase the speed at which the player reaches its maximum natural life regeneration
				// So we set it to 0, and while this debuff is active, it never reaches it
				Player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second
				Player.lifeRegen -= 16;
			}
		}

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource){
            if(lifeRegenDebuff){
                int type = ModContent.NPCType<Borg>();
                //NPC.SpawnOnPlayer(Player.whoAmI, type);
                Borg borg = new Borg();
                NPC.NewNPC(null, (int)Player.position.X, (int)Player.position.Y + Player.height, type, 0, 0f, 0f, 0f, 0f, 255);
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource){
            if (lifeRegenDebuff) {
                genGore = false;
            }
            return true;
        }
	}
}

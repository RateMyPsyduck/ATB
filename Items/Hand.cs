using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.DataStructures;
using System.Collections.Generic;   
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using ReLogic.Content;

namespace ATB.Items
{
	// Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/tModLoader/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
	public class Hand : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Apollo's Hand");

			Main.npcFrameCount[Type] = Main.npcFrameCount[2];

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) { // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults() {
			NPC.width = 58;
			NPC.height = 45;
			NPC.damage = 100;
			NPC.defense = 5;
			NPC.lifeMax = 600;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = -1; // Fighter AI, important to choose the aiStyle that matches the NPCID that we want to mimic
            NPC.alpha = 70;
            NPC.stepSpeed = 10f;
            NPC.dripping = false;

			AIType = -1; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
			AnimationType = 2; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
			//SpawnModBiomes = new int[1] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // Associates this NPC with the ExampleSurfaceBiome in Bestiary
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) {
			return SpawnCondition.OverworldNightMonster.Chance * 0.2f; // Spawn with 1/5th the chance of a regular zombie.
		}

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor){
        SpriteEffects spriteEffects = SpriteEffects.None;
        NPC.rotation = NPC.velocity.X * 0.1f;
        if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            return true;
        }

		public override void AI() {
            Player target = null;
            bool foundTarget = false;
            for(int i = 0; i < Main.player.Length; i++){
                if(target == null || (NPC.position - Main.player[i].Center).Length() < (NPC.position - target.Center).Length()){
			        target = Main.player[i];
                    foundTarget = true;
                }
            }

			GeneralBehavior(target, out Vector2 vectorToPosition, out float distanceToPosition);
			Movement(foundTarget, target, distanceToPosition, vectorToPosition);
		}


		private void GeneralBehavior(Player target, out Vector2 vectorToPosition, out float distanceToPosition) {
			Vector2 PlayerPosition = target.Center;
			//idlePosition.Y -= 200f; // Go up 48 coordinates (three tiles from the center of the player)

			// Teleport to player if distance is too big
			vectorToPosition = PlayerPosition - NPC.Center;
			distanceToPosition = vectorToPosition.Length();

			// if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f) {
			// 	// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
			// 	// and then set netUpdate to true
			// 	Projectile.position = idlePosition;
			// 	Projectile.velocity *= 0.1f;
			// 	Projectile.netUpdate = true;
			// }

			// If your minion is flying, you want to do this independently of any conditions
		}

		private void Movement(bool foundTarget, Player Target, float distanceToPosition, Vector2 vectorToPosition) {
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget) {
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceToPosition > 40f) {
					// The immediate range around the target (so it doesn't latch onto it when close)
                    if(Target.Center.X < NPC.Center.X){
                        NPC.spriteDirection = -1;
                    }
                    else{
                        NPC.spriteDirection = 1;
                    }
					Vector2 direction = Target.Center - NPC.Center;
					direction.Normalize();
					direction *= speed;
                    
                    Vector2 s = (vectorToPosition * (inertia - 1) + direction) / inertia;
                    s.Normalize();
                    s *= speed;
					NPC.SimpleFlyMovement(s, 1f);
						// if ((targetCenter.X - Projectile.Center).X > 0f) {
						// 	projectile.spriteDirection = projectile.direction = -1;
						// }
						// else if ((targetPos - projectile.Center).X < 0f) {
						// 	projectile.spriteDirection = projectile.direction = 1;
						// }
				}
			}
			else {
				// Minion doesn't have a target: return to player and idle
                return;
			}
		}

    }
}
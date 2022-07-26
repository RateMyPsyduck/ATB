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
        float speed = 3f;
		static Random random = new Random();
		int randomAttack = random.Next(6) + 6;
		int GroundPoundTimer = 0;
		int GroundPoundAttacks = 3;

		public Vector2 GroundPoundLoc = new Vector2(0,0);

		public Vector2 FirstStageDestination {
			get => new Vector2(NPC.ai[1], NPC.ai[2]);
			set {
				NPC.ai[1] = value.X;
				NPC.ai[2] = value.Y;
			}
		}

		public Vector2 GroundPoundDestination {
			get => new Vector2(NPC.ai[1], NPC.ai[2]);
			set {
				NPC.ai[1] = value.X;
				NPC.ai[2] = value.Y;
			}
		}

		public Vector2 LastFirstStageDestination { get; set; } = Vector2.Zero;
		public Vector2 LastGroundPoundDestination { get; set; } = Vector2.Zero;
		public ref float FirstStageTimer => ref NPC.localAI[1];
		private const int FirstStageTimerMax = 75;
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Apollo's Hand");

			Main.npcFrameCount[Type] = Main.npcFrameCount[3];

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) { // Influences how the NPC looks in the Bestiary
				Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}

		public override void SetDefaults() {
            NPC.scale = 2f;
			NPC.width = 58;
			NPC.height = 45;
			NPC.damage = 55;
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
            NPC.noTileCollide = true;
            NPC.noGravity = true;


			AIType = -1; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
			AnimationType = 2; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
			//SpawnModBiomes = new int[1] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // Associates this NPC with the ExampleSurfaceBiome in Bestiary
		}

		// public override float SpawnChance(NPCSpawnInfo spawnInfo) {
		// 	return SpawnCondition.OverworldNightMonster.Chance * 0.2f; // Spawn with 1/5th the chance of a regular zombie.
		// }

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
			if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) {
				NPC.TargetClosest();
			}

			Player player = Main.player[NPC.target];

			if (player.dead) {
				// If the targeted player is dead, flee
				NPC.velocity.Y -= 0.04f;
				// This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
				NPC.EncourageDespawn(10);
				return;
			}

			if(randomAttack == 0){
				GroundPound(player);
			}
			else{
				DoFirstStage(player);
			}
		}

	private void DoFirstStage(Player player) {
			// Each time the timer is 0, pick a random position a fixed distance away from the player but towards the opposite side
			// The NPC moves directly towards it with fixed speed, while displaying its trajectory as a telegraph

			FirstStageTimer++;
			if (FirstStageTimer > FirstStageTimerMax) {
				FirstStageTimer = 0;
			}

			float distance = 200; // Distance in pixels behind the player

			if (FirstStageTimer == 0) {
				Vector2 fromPlayer = NPC.Center - player.Center;

				if (Main.netMode != NetmodeID.MultiplayerClient) {
					// Important multiplayer concideration: drastic change in behavior (that is also decided by randomness) like this requires
					// to be executed on the server (or singleplayer) to keep the boss in sync
					randomAttack--;

					float angle = fromPlayer.ToRotation();
					float twelfth = MathHelper.Pi / 6;

					angle += MathHelper.Pi + Main.rand.NextFloat(-twelfth, twelfth);
					if (angle > MathHelper.TwoPi) {
						angle -= MathHelper.TwoPi;
					}
					else if (angle < 0) {
						angle += MathHelper.TwoPi;
					}

					Vector2 relativeDestination = angle.ToRotationVector2() * distance;

					FirstStageDestination = player.Center + relativeDestination;
					FirstStageDestination = FirstStageDestination - new Vector2(10, 10);
					NPC.netUpdate = true;
				}
			}

			// Move along the vector
			Vector2 toDestination = FirstStageDestination - NPC.Center;
			Vector2 toDestinationNormalized = toDestination.SafeNormalize(Vector2.UnitY);
			float speed = Math.Min(distance, toDestination.Length());
			NPC.velocity = toDestinationNormalized * speed / 23;

			if (FirstStageDestination != LastFirstStageDestination) {
				// If destination changed
				NPC.TargetClosest(); // Pick the closest player target again

				// "Why is this not in the same code that sets FirstStageDestination?" Because in multiplayer it's ran by the server.
				// The client has to know when the destination changes a different way. Keeping track of the previous ticks' destination is one way
				if (Main.netMode != NetmodeID.Server) {
					// For visuals regarding NPC position, netOffset has to be concidered to make visuals align properly
					NPC.position += NPC.netOffset;

					NPC.position -= NPC.netOffset;
				}
			}
			LastFirstStageDestination = FirstStageDestination;

			// No damage during first phase

			// Fade in based on remaining total minion life

			NPC.rotation = NPC.velocity.ToRotation() - MathHelper.PiOver2;
		}

	public void GroundPound(Player player){
		if(GroundPoundTimer == 0){
			// GroundPoundLoc = (player.Center - new Vector2(0, 100));
			NPC.velocity = new Vector2(0,0);
			Vector2 fromPlayer = NPC.Center - (player.Center);
			if (Main.netMode != NetmodeID.MultiplayerClient) {
				GroundPoundAttacks--;

				float angle = fromPlayer.ToRotation();
				float twelfth = MathHelper.Pi / 6;

				angle += MathHelper.Pi + Main.rand.NextFloat(-twelfth, twelfth);
				if (angle > MathHelper.TwoPi) {
					angle -= MathHelper.TwoPi;
				}
				else if (angle < 0) {
					angle += MathHelper.TwoPi;
				}

				Vector2 relativeDestination = angle.ToRotationVector2() * 200;

				GroundPoundDestination = (player.Center - new Vector2(0, 100)) + relativeDestination;
				NPC.netUpdate = true;
			}
		}
		NPC.position = (player.position - new Vector2((NPC.width / 2), 250));
		GroundPoundTimer++;
		if(GroundPoundTimer % 60 == 0){
			Main.NewText("Ground Pounding!");
		}
		if(GroundPoundTimer > 180){
			GroundPoundTimer = 0;
			GroundPoundAttacks = 3;
			FirstStageTimer = 0;
			randomAttack = random.Next(6) + 6;
		}
	}

    }
}
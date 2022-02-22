using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealTimeInGameMod.NPCs
{
    class SkeletronPrime : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
            if (npc.type == 127)
                return false;
            return base.PreAI(npc);
        }
        private void EncourageDespawn(NPC npc, int despawnTime)
        {
            if (npc.timeLeft > despawnTime)
                npc.timeLeft = despawnTime;
        }
        public override void PostAI(NPC npc)
        {
			if (npc.type == 127)
			{
				npc.damage = npc.defDamage;
				npc.defense = npc.defDefense;
				if (npc.ai[0] == 0f && Main.netMode != 1)
				{
					npc.TargetClosest();
					npc.ai[0] = 1f;
					int num474 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 128, npc.whoAmI);
					Main.npc[num474].ai[0] = -1f;
					Main.npc[num474].ai[1] = npc.whoAmI;
					Main.npc[num474].target = npc.target;
					Main.npc[num474].netUpdate = true;
					num474 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 129, npc.whoAmI);
					Main.npc[num474].ai[0] = 1f;
					Main.npc[num474].ai[1] = npc.whoAmI;
					Main.npc[num474].target = npc.target;
					Main.npc[num474].netUpdate = true;
					num474 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 130, npc.whoAmI);
					Main.npc[num474].ai[0] = -1f;
					Main.npc[num474].ai[1] = npc.whoAmI;
					Main.npc[num474].target = npc.target;
					Main.npc[num474].ai[3] = 150f;
					Main.npc[num474].netUpdate = true;
					num474 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 131, npc.whoAmI);
					Main.npc[num474].ai[0] = 1f;
					Main.npc[num474].ai[1] = npc.whoAmI;
					Main.npc[num474].target = npc.target;
					Main.npc[num474].netUpdate = true;
					Main.npc[num474].ai[3] = 150f;
				}
				if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
				{
					npc.TargetClosest();
					if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
					{
						npc.ai[1] = 3f;
					}
				}
				if (npc.ai[1] == 0f)
				{
					npc.ai[2] += 1f;
					if (npc.ai[2] >= 600f)
					{
						npc.ai[2] = 0f;
						npc.ai[1] = 1f;
						npc.TargetClosest();
						npc.netUpdate = true;
					}
					npc.rotation = npc.velocity.X / 15f;
					float num475 = 0.1f;
					float num476 = 2f;
					float num477 = 0.1f;
					float num478 = 8f;
					if (Main.expertMode)
					{
						num475 = 0.03f;
						num476 = 4f;
						num477 = 0.07f;
						num478 = 9.5f;
					}
					if (npc.position.Y > Main.player[npc.target].position.Y - 200f)
					{
						if (npc.velocity.Y > 0f)
						{
							npc.velocity.Y *= 0.98f;
						}
						npc.velocity.Y -= num475;
						if (npc.velocity.Y > num476)
						{
							npc.velocity.Y = num476;
						}
					}
					else if (npc.position.Y < Main.player[npc.target].position.Y - 500f)
					{
						if (npc.velocity.Y < 0f)
						{
							npc.velocity.Y *= 0.98f;
						}
						npc.velocity.Y += num475;
						if (npc.velocity.Y < 0f - num476)
						{
							npc.velocity.Y = 0f - num476;
						}
					}
					if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + 100f)
					{
						if (npc.velocity.X > 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						npc.velocity.X -= num477;
						if (npc.velocity.X > num478)
						{
							npc.velocity.X = num478;
						}
					}
					if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						npc.velocity.X += num477;
						if (npc.velocity.X < 0f - num478)
						{
							npc.velocity.X = 0f - num478;
						}
					}
				}
				else if (npc.ai[1] == 1f)
				{
					npc.defense *= 2;
					npc.damage *= 2;
					npc.ai[2] += 1f;
					if (npc.ai[2] == 2f)
					{
						Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
					}
					if (npc.ai[2] >= 400f)
					{
						npc.ai[2] = 0f;
						npc.ai[1] = 0f;
					}
					npc.rotation += (float)npc.direction * 0.3f;
					Vector2 vector48 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num479 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector48.X;
					float num480 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector48.Y;
					float num481 = (float)Math.Sqrt(num479 * num479 + num480 * num480);
					float num482 = 2f;
					if (Main.expertMode)
					{
						num482 = 6f;
						if (num481 > 150f)
						{
							num482 *= 1.05f;
						}
						if (num481 > 200f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 250f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 300f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 350f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 400f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 450f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 500f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 550f)
						{
							num482 *= 1.1f;
						}
						if (num481 > 600f)
						{
							num482 *= 1.1f;
						}
					}
					num481 = num482 / num481;
					npc.velocity.X = num479 * num481;
					npc.velocity.Y = num480 * num481;
				}
				else if (npc.ai[1] == 2f)
				{
					npc.damage = 1000;
					npc.defense = 9999;
					npc.rotation += (float)npc.direction * 0.3f;
					Vector2 vector49 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num483 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector49.X;
					float num484 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector49.Y;
					float num485 = (float)Math.Sqrt(num483 * num483 + num484 * num484);
					float num486 = 10f;
					num486 += num485 / 100f;
					if (num486 < 8f)
					{
						num486 = 8f;
					}
					if (num486 > 32f)
					{
						num486 = 32f;
					}
					num485 = num486 / num485;
					npc.velocity.X = num483 * num485;
					npc.velocity.Y = num484 * num485;
				}
				else if (npc.ai[1] == 3f)
				{
					npc.velocity.Y += 0.1f;
					if (npc.velocity.Y < 0f)
					{
						npc.velocity.Y *= 0.95f;
					}
					npc.velocity.X *= 0.95f;
					EncourageDespawn(npc, 500);
				}
			}
		}
    }
}

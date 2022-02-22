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
    class Twins : GlobalNPC
    {
		private void EncourageDespawn(NPC npc, int despawnTime)
		{
			if (npc.timeLeft > despawnTime)
				npc.timeLeft = despawnTime;
		}
		private void DiscourageDespawn(NPC npc, int despawnTime)
		{
			if (npc.timeLeft < despawnTime)
				npc.timeLeft = despawnTime;
			//npc.despawnEncouraged = false;
		}
		public int GetAttackDamage_ForProjectiles(float normalDamage, float expertDamage)
		{
			float amount = Main.expertMode ? 1f : 0.0f;
			return (int)MathHelper.Lerp(normalDamage, expertDamage, amount);
		}
		public override bool PreAI(NPC npc)
        {
            if (npc.type == 125 || npc.type == 126)
                return false;
            return base.PreAI(npc);
        }
        public override void PostAI(NPC npc)
        {
            if (Main.dayTime)
            {
				if (npc.type == 125)
				{
					if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
					{
						npc.TargetClosest();
					}
					bool dead2 = Main.player[npc.target].dead;
					float num400 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
					float num401 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
					float num402 = (float)Math.Atan2(num401, num400) + 1.57f;
					if (num402 < 0f)
					{
						num402 += 6.283f;
					}
					else if ((double)num402 > 6.283)
					{
						num402 -= 6.283f;
					}
					float num403 = 0.1f;
					if (npc.rotation < num402)
					{
						if ((double)(num402 - npc.rotation) > 3.1415)
						{
							npc.rotation -= num403;
						}
						else
						{
							npc.rotation += num403;
						}
					}
					else if (npc.rotation > num402)
					{
						if ((double)(npc.rotation - num402) > 3.1415)
						{
							npc.rotation += num403;
						}
						else
						{
							npc.rotation -= num403;
						}
					}
					if (npc.rotation > num402 - num403 && npc.rotation < num402 + num403)
					{
						npc.rotation = num402;
					}
					if (npc.rotation < 0f)
					{
						npc.rotation += 6.283f;
					}
					else if ((double)npc.rotation > 6.283)
					{
						npc.rotation -= 6.283f;
					}
					if (npc.rotation > num402 - num403 && npc.rotation < num402 + num403)
					{
						npc.rotation = num402;
					}
					if (Main.rand.Next(5) == 0)
					{
						int num404 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f);
						Main.dust[num404].velocity.X *= 0.5f;
						Main.dust[num404].velocity.Y *= 0.1f;
					}
					if (Main.netMode != 1 && !Main.dayTime && !dead2 && npc.timeLeft < 10)
					{
						for (int num405 = 0; num405 < 200; num405++)
						{
							if (num405 != npc.whoAmI && Main.npc[num405].active && (Main.npc[num405].type == 125 || Main.npc[num405].type == 126))
							{
								DiscourageDespawn(npc, Main.npc[num405].timeLeft - 1);
							}
						}
					}
					if (dead2)
					{
						npc.velocity.Y -= 0.04f;
						EncourageDespawn(npc, 10);
						return;
					}
					if (npc.ai[0] == 0f)
					{
						if (npc.ai[1] == 0f)
						{
							float num406 = 7f;
							float num407 = 0.1f;
							if (Main.expertMode)
							{
								num406 = 8.25f;
								num407 = 0.115f;
							}
							int num408 = 1;
							if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
							{
								num408 = -1;
							}
							Vector2 vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							float num409 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num408 * 300) - vector40.X;
							float num410 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 300f - vector40.Y;
							float num411 = (float)Math.Sqrt(num409 * num409 + num410 * num410);
							float num412 = num411;
							num411 = num406 / num411;
							num409 *= num411;
							num410 *= num411;
							if (npc.velocity.X < num409)
							{
								npc.velocity.X += num407;
								if (npc.velocity.X < 0f && num409 > 0f)
								{
									npc.velocity.X += num407;
								}
							}
							else if (npc.velocity.X > num409)
							{
								npc.velocity.X -= num407;
								if (npc.velocity.X > 0f && num409 < 0f)
								{
									npc.velocity.X -= num407;
								}
							}
							if (npc.velocity.Y < num410)
							{
								npc.velocity.Y += num407;
								if (npc.velocity.Y < 0f && num410 > 0f)
								{
									npc.velocity.Y += num407;
								}
							}
							else if (npc.velocity.Y > num410)
							{
								npc.velocity.Y -= num407;
								if (npc.velocity.Y > 0f && num410 < 0f)
								{
									npc.velocity.Y -= num407;
								}
							}
							npc.ai[2] += 1f;
							if (npc.ai[2] >= 600f)
							{
								npc.ai[1] = 1f;
								npc.ai[2] = 0f;
								npc.ai[3] = 0f;
								npc.target = 255;
								npc.netUpdate = true;
							}
							else if (npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && num412 < 400f)
							{
								if (!Main.player[npc.target].dead)
								{
									npc.ai[3] += 1f;
									if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.9)
									{
										npc.ai[3] += 0.3f;
									}
									if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.8)
									{
										npc.ai[3] += 0.3f;
									}
									if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.7)
									{
										npc.ai[3] += 0.3f;
									}
									if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.6)
									{
										npc.ai[3] += 0.3f;
									}
								}
								if (npc.ai[3] >= 60f)
								{
									npc.ai[3] = 0f;
									vector40 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
									num409 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector40.X;
									num410 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector40.Y;
									if (Main.netMode != 1)
									{
										float num413 = 9f;
										int attackDamage_ForProjectiles3 = GetAttackDamage_ForProjectiles(20f, 19f);
										int num414 = 83;
										if (Main.expertMode)
										{
											num413 = 10.5f;
										}
										num411 = (float)Math.Sqrt(num409 * num409 + num410 * num410);
										num411 = num413 / num411;
										num409 *= num411;
										num410 *= num411;
										num409 += (float)Main.rand.Next(-40, 41) * 0.08f;
										num410 += (float)Main.rand.Next(-40, 41) * 0.08f;
										vector40.X += num409 * 15f;
										vector40.Y += num410 * 15f;
										int num415 = Projectile.NewProjectile(vector40.X, vector40.Y, num409, num410, num414, attackDamage_ForProjectiles3, 0f, Main.myPlayer);
									}
								}
							}
						}
						else if (npc.ai[1] == 1f)
						{
							npc.rotation = num402;
							float num416 = 12f;
							if (Main.expertMode)
							{
								num416 = 15f;
							}
							Vector2 vector41 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							float num417 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector41.X;
							float num418 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector41.Y;
							float num419 = (float)Math.Sqrt(num417 * num417 + num418 * num418);
							num419 = num416 / num419;
							npc.velocity.X = num417 * num419;
							npc.velocity.Y = num418 * num419;
							npc.ai[1] = 2f;
						}
						else if (npc.ai[1] == 2f)
						{
							npc.ai[2] += 1f;
							if (npc.ai[2] >= 25f)
							{
								npc.velocity.X *= 0.96f;
								npc.velocity.Y *= 0.96f;
								if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
								{
									npc.velocity.X = 0f;
								}
								if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
								{
									npc.velocity.Y = 0f;
								}
							}
							else
							{
								npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
							}
							if (npc.ai[2] >= 70f)
							{
								npc.ai[3] += 1f;
								npc.ai[2] = 0f;
								npc.target = 255;
								npc.rotation = num402;
								if (npc.ai[3] >= 4f)
								{
									npc.ai[1] = 0f;
									npc.ai[3] = 0f;
								}
								else
								{
									npc.ai[1] = 1f;
								}
							}
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.4)
						{
							npc.ai[0] = 1f;
							npc.ai[1] = 0f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.netUpdate = true;
						}
						return;
					}
					if (npc.ai[0] == 1f || npc.ai[0] == 2f)
					{
						if (npc.ai[0] == 1f)
						{
							npc.ai[2] += 0.005f;
							if ((double)npc.ai[2] > 0.5)
							{
								npc.ai[2] = 0.5f;
							}
						}
						else
						{
							npc.ai[2] -= 0.005f;
							if (npc.ai[2] < 0f)
							{
								npc.ai[2] = 0f;
							}
						}
						npc.rotation += npc.ai[2];
						npc.ai[1] += 1f;
						if (npc.ai[1] >= 100f)
						{
							npc.ai[0] += 1f;
							npc.ai[1] = 0f;
							if (npc.ai[0] == 3f)
							{
								npc.ai[2] = 0f;
							}
							else
							{
								Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y);
								for (int num420 = 0; num420 < 2; num420++)
								{
									Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 143);
									Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
									Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
								}
								for (int num421 = 0; num421 < 20; num421++)
								{
									Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
								}
								Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
							}
						}
						Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
						npc.velocity.X *= 0.98f;
						npc.velocity.Y *= 0.98f;
						if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
						{
							npc.velocity.X = 0f;
						}
						if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
						{
							npc.velocity.Y = 0f;
						}
						return;
					}
					npc.damage = (int)((double)npc.defDamage * 1.5);
					npc.defense = npc.defDefense + 10;
					npc.HitSound = SoundID.NPCHit4;
					if (npc.ai[1] == 0f)
					{
						float num422 = 8f;
						float num423 = 0.15f;
						if (Main.expertMode)
						{
							num422 = 9.5f;
							num423 = 0.175f;
						}
						Vector2 vector42 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num424 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector42.X;
						float num425 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 300f - vector42.Y;
						float num426 = (float)Math.Sqrt(num424 * num424 + num425 * num425);
						num426 = num422 / num426;
						num424 *= num426;
						num425 *= num426;
						if (npc.velocity.X < num424)
						{
							npc.velocity.X += num423;
							if (npc.velocity.X < 0f && num424 > 0f)
							{
								npc.velocity.X += num423;
							}
						}
						else if (npc.velocity.X > num424)
						{
							npc.velocity.X -= num423;
							if (npc.velocity.X > 0f && num424 < 0f)
							{
								npc.velocity.X -= num423;
							}
						}
						if (npc.velocity.Y < num425)
						{
							npc.velocity.Y += num423;
							if (npc.velocity.Y < 0f && num425 > 0f)
							{
								npc.velocity.Y += num423;
							}
						}
						else if (npc.velocity.Y > num425)
						{
							npc.velocity.Y -= num423;
							if (npc.velocity.Y > 0f && num425 < 0f)
							{
								npc.velocity.Y -= num423;
							}
						}
						npc.ai[2] += 1f;
						if (npc.ai[2] >= 300f)
						{
							npc.ai[1] = 1f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.TargetClosest();
							npc.netUpdate = true;
						}
						vector42 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						num424 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector42.X;
						num425 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector42.Y;
						npc.rotation = (float)Math.Atan2(num425, num424) - 1.57f;
						if (Main.netMode == 1)
						{
							return;
						}
						npc.localAI[1] += 1f;
						if ((double)npc.life < (double)npc.lifeMax * 0.75)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.5)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.25)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.1)
						{
							npc.localAI[1] += 2f;
						}
						if (npc.localAI[1] > 180f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							npc.localAI[1] = 0f;
							float num427 = 8.5f;
							int attackDamage_ForProjectiles4 = GetAttackDamage_ForProjectiles(25f, 23f);
							int num428 = 100;
							if (Main.expertMode)
							{
								num427 = 10f;
							}
							num426 = (float)Math.Sqrt(num424 * num424 + num425 * num425);
							num426 = num427 / num426;
							num424 *= num426;
							num425 *= num426;
							vector42.X += num424 * 15f;
							vector42.Y += num425 * 15f;
							int num429 = Projectile.NewProjectile(vector42.X, vector42.Y, num424, num425, num428, attackDamage_ForProjectiles4, 0f, Main.myPlayer);
						}
						return;
					}
					int num430 = 1;
					if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
					{
						num430 = -1;
					}
					float num431 = 8f;
					float num432 = 0.2f;
					if (Main.expertMode)
					{
						num431 = 9.5f;
						num432 = 0.25f;
					}
					Vector2 vector43 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num433 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num430 * 340) - vector43.X;
					float num434 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector43.Y;
					float num435 = (float)Math.Sqrt(num433 * num433 + num434 * num434);
					num435 = num431 / num435;
					num433 *= num435;
					num434 *= num435;
					if (npc.velocity.X < num433)
					{
						npc.velocity.X += num432;
						if (npc.velocity.X < 0f && num433 > 0f)
						{
							npc.velocity.X += num432;
						}
					}
					else if (npc.velocity.X > num433)
					{
						npc.velocity.X -= num432;
						if (npc.velocity.X > 0f && num433 < 0f)
						{
							npc.velocity.X -= num432;
						}
					}
					if (npc.velocity.Y < num434)
					{
						npc.velocity.Y += num432;
						if (npc.velocity.Y < 0f && num434 > 0f)
						{
							npc.velocity.Y += num432;
						}
					}
					else if (npc.velocity.Y > num434)
					{
						npc.velocity.Y -= num432;
						if (npc.velocity.Y > 0f && num434 < 0f)
						{
							npc.velocity.Y -= num432;
						}
					}
					vector43 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					num433 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector43.X;
					num434 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector43.Y;
					npc.rotation = (float)Math.Atan2(num434, num433) - 1.57f;
					if (Main.netMode != 1)
					{
						npc.localAI[1] += 1f;
						if ((double)npc.life < (double)npc.lifeMax * 0.75)
						{
							npc.localAI[1] += 0.5f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.5)
						{
							npc.localAI[1] += 0.75f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.25)
						{
							npc.localAI[1] += 1f;
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.1)
						{
							npc.localAI[1] += 1.5f;
						}
						if (Main.expertMode)
						{
							npc.localAI[1] += 1.5f;
						}
						if (npc.localAI[1] > 60f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							npc.localAI[1] = 0f;
							float num436 = 9f;
							int attackDamage_ForProjectiles5 = GetAttackDamage_ForProjectiles(18f, 17f);
							int num437 = 100;
							num435 = (float)Math.Sqrt(num433 * num433 + num434 * num434);
							num435 = num436 / num435;
							num433 *= num435;
							num434 *= num435;
							vector43.X += num433 * 15f;
							vector43.Y += num434 * 15f;
							int num438 = Projectile.NewProjectile(vector43.X, vector43.Y, num433, num434, num437, attackDamage_ForProjectiles5, 0f, Main.myPlayer);
						}
					}
					npc.ai[2] += 1f;
					if (npc.ai[2] >= 180f)
					{
						npc.ai[1] = 0f;
						npc.ai[2] = 0f;
						npc.ai[3] = 0f;
						npc.TargetClosest();
						npc.netUpdate = true;
					}
				}
				else if (npc.type == 126)
				{
					if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
					{
						npc.TargetClosest();
					}
					bool dead3 = Main.player[npc.target].dead;
					float num439 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
					float num440 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
					float num441 = (float)Math.Atan2(num440, num439) + 1.57f;
					if (num441 < 0f)
					{
						num441 += 6.283f;
					}
					else if ((double)num441 > 6.283)
					{
						num441 -= 6.283f;
					}
					float num442 = 0.15f;
					if (npc.rotation < num441)
					{
						if ((double)(num441 - npc.rotation) > 3.1415)
						{
							npc.rotation -= num442;
						}
						else
						{
							npc.rotation += num442;
						}
					}
					else if (npc.rotation > num441)
					{
						if ((double)(npc.rotation - num441) > 3.1415)
						{
							npc.rotation += num442;
						}
						else
						{
							npc.rotation -= num442;
						}
					}
					if (npc.rotation > num441 - num442 && npc.rotation < num441 + num442)
					{
						npc.rotation = num441;
					}
					if (npc.rotation < 0f)
					{
						npc.rotation += 6.283f;
					}
					else if ((double)npc.rotation > 6.283)
					{
						npc.rotation -= 6.283f;
					}
					if (npc.rotation > num441 - num442 && npc.rotation < num441 + num442)
					{
						npc.rotation = num441;
					}
					if (Main.rand.Next(5) == 0)
					{
						int num443 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f);
						Main.dust[num443].velocity.X *= 0.5f;
						Main.dust[num443].velocity.Y *= 0.1f;
					}
					if (Main.netMode != 1 && !Main.dayTime && !dead3 && npc.timeLeft < 10)
					{
						for (int num444 = 0; num444 < 200; num444++)
						{
							if (num444 != npc.whoAmI && Main.npc[num444].active && (Main.npc[num444].type == 125 || Main.npc[num444].type == 126))
							{
								DiscourageDespawn(npc, Main.npc[num444].timeLeft - 1);
							}
						}
					}
					if (dead3)
					{
						npc.velocity.Y -= 0.04f;
						EncourageDespawn(npc, 10);
						return;
					}
					if (npc.ai[0] == 0f)
					{
						if (npc.ai[1] == 0f)
						{
							npc.TargetClosest();
							float num445 = 12f;
							float num446 = 0.4f;
							int num447 = 1;
							if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
							{
								num447 = -1;
							}
							Vector2 vector44 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							float num448 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num447 * 400) - vector44.X;
							float num449 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
							float num450 = (float)Math.Sqrt(num448 * num448 + num449 * num449);
							float num451 = num450;
							num450 = num445 / num450;
							num448 *= num450;
							num449 *= num450;
							if (npc.velocity.X < num448)
							{
								npc.velocity.X += num446;
								if (npc.velocity.X < 0f && num448 > 0f)
								{
									npc.velocity.X += num446;
								}
							}
							else if (npc.velocity.X > num448)
							{
								npc.velocity.X -= num446;
								if (npc.velocity.X > 0f && num448 < 0f)
								{
									npc.velocity.X -= num446;
								}
							}
							if (npc.velocity.Y < num449)
							{
								npc.velocity.Y += num446;
								if (npc.velocity.Y < 0f && num449 > 0f)
								{
									npc.velocity.Y += num446;
								}
							}
							else if (npc.velocity.Y > num449)
							{
								npc.velocity.Y -= num446;
								if (npc.velocity.Y > 0f && num449 < 0f)
								{
									npc.velocity.Y -= num446;
								}
							}
							npc.ai[2] += 1f;
							if (npc.ai[2] >= 600f)
							{
								npc.ai[1] = 1f;
								npc.ai[2] = 0f;
								npc.ai[3] = 0f;
								npc.target = 255;
								npc.netUpdate = true;
							}
							else
							{
								if (!Main.player[npc.target].dead)
								{
									npc.ai[3] += 1f;
									if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.8)
									{
										npc.ai[3] += 0.6f;
									}
								}
								if (npc.ai[3] >= 60f)
								{
									npc.ai[3] = 0f;
									vector44 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
									num448 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector44.X;
									num449 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
									if (Main.netMode != 1)
									{
										float num452 = 12f;
										int attackDamage_ForProjectiles6 = GetAttackDamage_ForProjectiles(25f, 22f);
										int num453 = 96;
										if (Main.expertMode)
										{
											num452 = 14f;
										}
										num450 = (float)Math.Sqrt(num448 * num448 + num449 * num449);
										num450 = num452 / num450;
										num448 *= num450;
										num449 *= num450;
										num448 += (float)Main.rand.Next(-40, 41) * 0.05f;
										num449 += (float)Main.rand.Next(-40, 41) * 0.05f;
										vector44.X += num448 * 4f;
										vector44.Y += num449 * 4f;
										int num454 = Projectile.NewProjectile(vector44.X, vector44.Y, num448, num449, num453, attackDamage_ForProjectiles6, 0f, Main.myPlayer);
									}
								}
							}
						}
						else if (npc.ai[1] == 1f)
						{
							npc.rotation = num441;
							float num455 = 13f;
							if (Main.expertMode)
							{
								if ((double)npc.life < (double)npc.lifeMax * 0.9)
								{
									num455 += 0.5f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.8)
								{
									num455 += 0.5f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.7)
								{
									num455 += 0.55f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.6)
								{
									num455 += 0.6f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.5)
								{
									num455 += 0.65f;
								}
							}
							Vector2 vector45 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
							float num456 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector45.X;
							float num457 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector45.Y;
							float num458 = (float)Math.Sqrt(num456 * num456 + num457 * num457);
							num458 = num455 / num458;
							npc.velocity.X = num456 * num458;
							npc.velocity.Y = num457 * num458;
							npc.ai[1] = 2f;
						}
						else if (npc.ai[1] == 2f)
						{
							npc.ai[2] += 1f;
							if (npc.ai[2] >= 8f)
							{
								npc.velocity.X *= 0.9f;
								npc.velocity.Y *= 0.9f;
								if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
								{
									npc.velocity.X = 0f;
								}
								if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
								{
									npc.velocity.Y = 0f;
								}
							}
							else
							{
								npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
							}
							if (npc.ai[2] >= 42f)
							{
								npc.ai[3] += 1f;
								npc.ai[2] = 0f;
								npc.target = 255;
								npc.rotation = num441;
								if (npc.ai[3] >= 10f)
								{
									npc.ai[1] = 0f;
									npc.ai[3] = 0f;
								}
								else
								{
									npc.ai[1] = 1f;
								}
							}
						}
						if ((double)npc.life < (double)npc.lifeMax * 0.4)
						{
							npc.ai[0] = 1f;
							npc.ai[1] = 0f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.netUpdate = true;
						}
						return;
					}
					if (npc.ai[0] == 1f || npc.ai[0] == 2f)
					{
						if (npc.ai[0] == 1f)
						{
							npc.ai[2] += 0.005f;
							if ((double)npc.ai[2] > 0.5)
							{
								npc.ai[2] = 0.5f;
							}
						}
						else
						{
							npc.ai[2] -= 0.005f;
							if (npc.ai[2] < 0f)
							{
								npc.ai[2] = 0f;
							}
						}
						npc.rotation += npc.ai[2];
						npc.ai[1] += 1f;
						if (npc.ai[1] >= 100f)
						{
							npc.ai[0] += 1f;
							npc.ai[1] = 0f;
							if (npc.ai[0] == 3f)
							{
								npc.ai[2] = 0f;
							}
							else
							{
								Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y);
								for (int num459 = 0; num459 < 2; num459++)
								{
									Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 144);
									Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
									Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
								}
								for (int num460 = 0; num460 < 20; num460++)
								{
									Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
								}
								Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
							}
						}
						Dust.NewDust(npc.position, npc.width, npc.height, 5, (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f);
						npc.velocity.X *= 0.98f;
						npc.velocity.Y *= 0.98f;
						if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
						{
							npc.velocity.X = 0f;
						}
						if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
						{
							npc.velocity.Y = 0f;
						}
						return;
					}
					npc.HitSound = SoundID.NPCHit4;
					npc.damage = (int)((double)npc.defDamage * 1.5);
					npc.defense = npc.defDefense + 18;
					if (npc.ai[1] == 0f)
					{
						float num461 = 4f;
						float num462 = 0.1f;
						int num463 = 1;
						if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
						{
							num463 = -1;
						}
						Vector2 vector46 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num464 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + (float)(num463 * 180) - vector46.X;
						float num465 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector46.Y;
						float num466 = (float)Math.Sqrt(num464 * num464 + num465 * num465);
						if (Main.expertMode)
						{
							if (num466 > 300f)
							{
								num461 += 0.5f;
							}
							if (num466 > 400f)
							{
								num461 += 0.5f;
							}
							if (num466 > 500f)
							{
								num461 += 0.55f;
							}
							if (num466 > 600f)
							{
								num461 += 0.55f;
							}
							if (num466 > 700f)
							{
								num461 += 0.6f;
							}
							if (num466 > 800f)
							{
								num461 += 0.6f;
							}
						}
						num466 = num461 / num466;
						num464 *= num466;
						num465 *= num466;
						if (npc.velocity.X < num464)
						{
							npc.velocity.X += num462;
							if (npc.velocity.X < 0f && num464 > 0f)
							{
								npc.velocity.X += num462;
							}
						}
						else if (npc.velocity.X > num464)
						{
							npc.velocity.X -= num462;
							if (npc.velocity.X > 0f && num464 < 0f)
							{
								npc.velocity.X -= num462;
							}
						}
						if (npc.velocity.Y < num465)
						{
							npc.velocity.Y += num462;
							if (npc.velocity.Y < 0f && num465 > 0f)
							{
								npc.velocity.Y += num462;
							}
						}
						else if (npc.velocity.Y > num465)
						{
							npc.velocity.Y -= num462;
							if (npc.velocity.Y > 0f && num465 < 0f)
							{
								npc.velocity.Y -= num462;
							}
						}
						npc.ai[2] += 1f;
						if (npc.ai[2] >= 400f)
						{
							npc.ai[1] = 1f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.target = 255;
							npc.netUpdate = true;
						}
						if (!Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							return;
						}
						npc.localAI[2] += 1f;
						if (npc.localAI[2] > 22f)
						{
							npc.localAI[2] = 0f;
							Main.PlaySound(SoundID.Item34, npc.position);
						}
						if (Main.netMode != 1)
						{
							npc.localAI[1] += 1f;
							if ((double)npc.life < (double)npc.lifeMax * 0.75)
							{
								npc.localAI[1] += 1f;
							}
							if ((double)npc.life < (double)npc.lifeMax * 0.5)
							{
								npc.localAI[1] += 1f;
							}
							if ((double)npc.life < (double)npc.lifeMax * 0.25)
							{
								npc.localAI[1] += 1f;
							}
							if ((double)npc.life < (double)npc.lifeMax * 0.1)
							{
								npc.localAI[1] += 2f;
							}
							if (npc.localAI[1] > 8f)
							{
								npc.localAI[1] = 0f;
								float num467 = 6f;
								int attackDamage_ForProjectiles7 = GetAttackDamage_ForProjectiles(30f, 27f);
								int num468 = 101;
								vector46 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
								num464 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector46.X;
								num465 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector46.Y;
								num466 = (float)Math.Sqrt(num464 * num464 + num465 * num465);
								num466 = num467 / num466;
								num464 *= num466;
								num465 *= num466;
								num465 += (float)Main.rand.Next(-40, 41) * 0.01f;
								num464 += (float)Main.rand.Next(-40, 41) * 0.01f;
								num465 += npc.velocity.Y * 0.5f;
								num464 += npc.velocity.X * 0.5f;
								vector46.X -= num464 * 1f;
								vector46.Y -= num465 * 1f;
								int num469 = Projectile.NewProjectile(vector46.X, vector46.Y, num464, num465, num468, attackDamage_ForProjectiles7, 0f, Main.myPlayer);
							}
						}
					}
					else if (npc.ai[1] == 1f)
					{
						Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
						npc.rotation = num441;
						float num470 = 14f;
						if (Main.expertMode)
						{
							num470 += 2.5f;
						}
						Vector2 vector47 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num471 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector47.X;
						float num472 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector47.Y;
						float num473 = (float)Math.Sqrt(num471 * num471 + num472 * num472);
						num473 = num470 / num473;
						npc.velocity.X = num471 * num473;
						npc.velocity.Y = num472 * num473;
						npc.ai[1] = 2f;
					}
					else
					{
						if (npc.ai[1] != 2f)
						{
							return;
						}
						npc.ai[2] += 1f;
						if (Main.expertMode)
						{
							npc.ai[2] += 0.5f;
						}
						if (npc.ai[2] >= 50f)
						{
							npc.velocity.X *= 0.93f;
							npc.velocity.Y *= 0.93f;
							if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
							{
								npc.velocity.X = 0f;
							}
							if ((double)npc.velocity.Y > -0.1 && (double)npc.velocity.Y < 0.1)
							{
								npc.velocity.Y = 0f;
							}
						}
						else
						{
							npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) - 1.57f;
						}
						if (npc.ai[2] >= 80f)
						{
							npc.ai[3] += 1f;
							npc.ai[2] = 0f;
							npc.target = 255;
							npc.rotation = num441;
							if (npc.ai[3] >= 6f)
							{
								npc.ai[1] = 0f;
								npc.ai[3] = 0f;
							}
							else
							{
								npc.ai[1] = 1f;
							}
						}
					}
				}
			}
			
        }
    }
}

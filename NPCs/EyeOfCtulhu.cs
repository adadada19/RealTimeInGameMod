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
    class EyeOfCtulhu : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
			if (npc.type == 4)
				return false;
			return base.PreAI(npc);
        }
        public override void PostAI(NPC npc)
        {
				if (npc.type == 4)
            {
				bool flag2 = false;
				if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.12)
				{
					flag2 = true;
				}
				bool flag3 = false;
				if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.04)
				{
					flag3 = true;
				}
				float num4 = 20f;
				if (flag3)
				{
					num4 = 10f;
				}
				if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
				{
					npc.TargetClosest();
				}
				bool dead = Main.player[npc.target].dead;
				if (dead)
				{
					npc.velocity.Y -= 0.04f;
					if (Main.npc[(int)npc.ai[1]].ai[1] == 3f)
					{
						var player = Main.player[npc.target];
						if (!player.active || player.dead)
						{
							npc.velocity = new Vector2(0f, 10f);
							if (npc.timeLeft > 10)
							{
								npc.timeLeft = 10;
							}
						}
					}
					return;
				}
				float num5 = npc.position.X + (float)(npc.width / 2) - Main.player[npc.target].position.X - (float)(Main.player[npc.target].width / 2);
				float num6 = npc.position.Y + (float)npc.height - 59f - Main.player[npc.target].position.Y - (float)(Main.player[npc.target].height / 2);
				float num7 = (float)Math.Atan2(num6, num5) + 1.57f;
				if (num7 < 0f)
				{
					num7 += 6.283f;
				}
				else if ((double)num7 > 6.283)
				{
					num7 -= 6.283f;
				}
				float num8 = 0f;
				if (npc.ai[0] == 0f && npc.ai[1] == 0f)
				{
					num8 = 0.02f;
				}
				if (npc.ai[0] == 0f && npc.ai[1] == 2f && npc.ai[2] > 40f)
				{
					num8 = 0.05f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 0f)
				{
					num8 = 0.05f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 2f && npc.ai[2] > 40f)
				{
					num8 = 0.08f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 4f && npc.ai[2] > num4)
				{
					num8 = 0.15f;
				}
				if (npc.ai[0] == 3f && npc.ai[1] == 5f)
				{
					num8 = 0.05f;
				}
				if (Main.expertMode)
				{
					num8 *= 1.5f;
				}
				if (flag3 && Main.expertMode)
				{
					num8 = 0f;
				}
				if (npc.rotation < num7)
				{
					if ((double)(num7 - npc.rotation) > 3.1415)
					{
						npc.rotation -= num8;
					}
					else
					{
						npc.rotation += num8;
					}
				}
				else if (npc.rotation > num7)
				{
					if ((double)(npc.rotation - num7) > 3.1415)
					{
						npc.rotation += num8;
					}
					else
					{
						npc.rotation -= num8;
					}
				}
				if (npc.rotation > num7 - num8 && npc.rotation < num7 + num8)
				{
					npc.rotation = num7;
				}
				if (npc.rotation < 0f)
				{
					npc.rotation += 6.283f;
				}
				else if ((double)npc.rotation > 6.283)
				{
					npc.rotation -= 6.283f;
				}
				if (npc.rotation > num7 - num8 && npc.rotation < num7 + num8)
				{
					npc.rotation = num7;
				}
				if (Main.rand.Next(5) == 0)
				{
					int num9 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float)npc.height * 0.25f), npc.width, (int)((float)npc.height * 0.5f), 5, npc.velocity.X, 2f);
					Main.dust[num9].velocity.X *= 0.5f;
					Main.dust[num9].velocity.Y *= 0.1f;
				}
				if (npc.ai[0] == 0f)
				{
					if (npc.ai[1] == 0f)
					{
						float num10 = 5f;
						float num11 = 0.04f;
						if (Main.expertMode)
						{
							num11 = 0.15f;
							num10 = 7f;
						}
						Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num12 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector.X;
						float num13 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 200f - vector.Y;
						float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
						float num15 = num14;
						num14 = num10 / num14;
						num12 *= num14;
						num13 *= num14;
						if (npc.velocity.X < num12)
						{
							npc.velocity.X += num11;
							if (npc.velocity.X < 0f && num12 > 0f)
							{
								npc.velocity.X += num11;
							}
						}
						else if (npc.velocity.X > num12)
						{
							npc.velocity.X -= num11;
							if (npc.velocity.X > 0f && num12 < 0f)
							{
								npc.velocity.X -= num11;
							}
						}
						if (npc.velocity.Y < num13)
						{
							npc.velocity.Y += num11;
							if (npc.velocity.Y < 0f && num13 > 0f)
							{
								npc.velocity.Y += num11;
							}
						}
						else if (npc.velocity.Y > num13)
						{
							npc.velocity.Y -= num11;
							if (npc.velocity.Y > 0f && num13 < 0f)
							{
								npc.velocity.Y -= num11;
							}
						}
						npc.ai[2] += 1f;
						float num16 = 600f;
						if (Main.expertMode)
						{
							num16 *= 0.35f;
						}
						if (npc.ai[2] >= num16)
						{
							npc.ai[1] = 1f;
							npc.ai[2] = 0f;
							npc.ai[3] = 0f;
							npc.target = 255;
							npc.netUpdate = true;
						}
						else if ((npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && num15 < 500f) || (Main.expertMode && num15 < 500f))
						{
							if (!Main.player[npc.target].dead)
							{
								npc.ai[3] += 1f;
							}
							float num17 = 110f;
							if (Main.expertMode)
							{
								num17 *= 0.4f;
							}
							if (npc.ai[3] >= num17)
							{
								npc.ai[3] = 0f;
								npc.rotation = num7;
								float num18 = 5f;
								if (Main.expertMode)
								{
									num18 = 6f;
								}
								float num19 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector.X;
								float num20 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector.Y;
								float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
								num21 = num18 / num21;
								Vector2 vector2 = vector;
								Vector2 vector3 = default(Vector2);
								vector3.X = num19 * num21;
								vector3.Y = num20 * num21;
								vector2.X += vector3.X * 10f;
								vector2.Y += vector3.Y * 10f;
								if (Main.netMode != 1)
								{
									int num22 = NPC.NewNPC((int)vector2.X, (int)vector2.Y, 5);
									Main.npc[num22].velocity.X = vector3.X;
									Main.npc[num22].velocity.Y = vector3.Y;
									if (Main.netMode == 2 && num22 < 10)
									{
										NetMessage.SendData(23, -1, -1, null, num22);
									}
								}
								for (int m = 0; m < 10; m++)
								{
									Dust.NewDust(vector2, 20, 20, 5, vector3.X * 0.4f, vector3.Y * 0.4f);
								}
							}
						}
					}
					else if (npc.ai[1] == 1f)
					{
						npc.rotation = num7;
						float num23 = 6f;
						if (Main.expertMode)
						{
							num23 = 7f;
						}
						Vector2 vector4 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num24 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector4.X;
						float num25 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector4.Y;
						float num26 = (float)Math.Sqrt(num24 * num24 + num25 * num25);
						num26 = num23 / num26;
						npc.velocity.X = num24 * num26;
						npc.velocity.Y = num25 * num26;
						npc.ai[1] = 2f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
					}
					else if (npc.ai[1] == 2f)
					{
						npc.ai[2] += 1f;
						if (npc.ai[2] >= 40f)
						{
							npc.velocity *= 0.98f;
							if (Main.expertMode)
							{
								npc.velocity *= 0.985f;
							}
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
						int num27 = 150;
						if (Main.expertMode)
						{
							num27 = 100;
						}
						if (npc.ai[2] >= (float)num27)
						{
							npc.ai[3] += 1f;
							npc.ai[2] = 0f;
							npc.target = 255;
							npc.rotation = num7;
							if (npc.ai[3] >= 3f)
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
					float num28 = 0.5f;
					if (Main.expertMode)
					{
						num28 = 0.65f;
					}
					if ((float)npc.life < (float)npc.lifeMax * num28)
					{
						npc.ai[0] = 1f;
						npc.ai[1] = 0f;
						npc.ai[2] = 0f;
						npc.ai[3] = 0f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
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
					if (Main.expertMode && npc.ai[1] % 20f == 0f)
					{
						float num29 = 5f;
						Vector2 vector5 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num30 = Main.rand.Next(-200, 200);
						float num31 = Main.rand.Next(-200, 200);
						float num32 = (float)Math.Sqrt(num30 * num30 + num31 * num31);
						num32 = num29 / num32;
						Vector2 vector6 = vector5;
						Vector2 vector7 = default(Vector2);
						vector7.X = num30 * num32;
						vector7.Y = num31 * num32;
						vector6.X += vector7.X * 10f;
						vector6.Y += vector7.Y * 10f;
						if (Main.netMode != 1)
						{
							int num33 = NPC.NewNPC((int)vector6.X, (int)vector6.Y, 5);
							Main.npc[num33].velocity.X = vector7.X;
							Main.npc[num33].velocity.Y = vector7.Y;
							if (Main.netMode == 2 && num33 < 200)
							{
								NetMessage.SendData(23, -1, -1, null, num33);
							}
						}
						for (int n = 0; n < 10; n++)
						{
							Dust.NewDust(vector6, 20, 20, 5, vector7.X * 0.4f, vector7.Y * 0.4f);
						}
					}
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
							for (int num34 = 0; num34 < 2; num34++)
							{
								Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 8);
								Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7);
								Gore.NewGore(npc.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6);
							}
							for (int num35 = 0; num35 < 20; num35++)
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
				npc.defense = 0;
				int num36 = 23;
				int num37 = 18;
				if (Main.expertMode)
				{
					if (flag2)
					{
						npc.defense = -15;
					}
					if (flag3)
					{
						num37 = 20;
						npc.defense = -30;
					}
				}
				if (npc.ai[1] == 0f && flag2)
				{
					npc.ai[1] = 5f;
				}
				if (npc.ai[1] == 0f)
				{
					float num38 = 6f;
					float num39 = 0.07f;
					Vector2 vector8 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num40 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector8.X;
					float num41 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 120f - vector8.Y;
					float num42 = (float)Math.Sqrt(num40 * num40 + num41 * num41);
					if (num42 > 400f && Main.expertMode)
					{
						num38 += 1f;
						num39 += 0.05f;
						if (num42 > 600f)
						{
							num38 += 1f;
							num39 += 0.05f;
							if (num42 > 800f)
							{
								num38 += 1f;
								num39 += 0.05f;
							}
						}
					}
					num42 = num38 / num42;
					num40 *= num42;
					num41 *= num42;
					if (npc.velocity.X < num40)
					{
						npc.velocity.X += num39;
						if (npc.velocity.X < 0f && num40 > 0f)
						{
							npc.velocity.X += num39;
						}
					}
					else if (npc.velocity.X > num40)
					{
						npc.velocity.X -= num39;
						if (npc.velocity.X > 0f && num40 < 0f)
						{
							npc.velocity.X -= num39;
						}
					}
					if (npc.velocity.Y < num41)
					{
						npc.velocity.Y += num39;
						if (npc.velocity.Y < 0f && num41 > 0f)
						{
							npc.velocity.Y += num39;
						}
					}
					else if (npc.velocity.Y > num41)
					{
						npc.velocity.Y -= num39;
						if (npc.velocity.Y > 0f && num41 < 0f)
						{
							npc.velocity.Y -= num39;
						}
					}
					npc.ai[2] += 1f;
					if (npc.ai[2] >= 200f)
					{
						npc.ai[1] = 1f;
						npc.ai[2] = 0f;
						npc.ai[3] = 0f;
						if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.35)
						{
							npc.ai[1] = 3f;
						}
						npc.target = 255;
						npc.netUpdate = true;
					}
					if (Main.expertMode && flag3)
					{
						npc.TargetClosest();
						npc.netUpdate = true;
						npc.ai[1] = 3f;
						npc.ai[2] = 0f;
						npc.ai[3] -= 1000f;
					}
				}
				else if (npc.ai[1] == 1f)
				{
					Main.PlaySound(36, (int)npc.position.X, (int)npc.position.Y, 0);
					npc.rotation = num7;
					float num43 = 6.8f;
					if (Main.expertMode && npc.ai[3] == 1f)
					{
						num43 *= 1.15f;
					}
					if (Main.expertMode && npc.ai[3] == 2f)
					{
						num43 *= 1.3f;
					}
					Vector2 vector9 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num44 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector9.X;
					float num45 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector9.Y;
					float num46 = (float)Math.Sqrt(num44 * num44 + num45 * num45);
					num46 = num43 / num46;
					npc.velocity.X = num44 * num46;
					npc.velocity.Y = num45 * num46;
					npc.ai[1] = 2f;
					npc.netUpdate = true;
					if (npc.netSpam > 10)
					{
						npc.netSpam = 10;
					}
				}
				else if (npc.ai[1] == 2f)
				{
					float num47 = 40f;
					npc.ai[2] += 1f;
					if (Main.expertMode)
					{
						num47 = 50f;
					}
					if (npc.ai[2] >= num47)
					{
						npc.velocity *= 0.97f;
						if (Main.expertMode)
						{
							npc.velocity *= 0.98f;
						}
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
					int num48 = 130;
					if (Main.expertMode)
					{
						num48 = 90;
					}
					if (npc.ai[2] >= (float)num48)
					{
						npc.ai[3] += 1f;
						npc.ai[2] = 0f;
						npc.target = 255;
						npc.rotation = num7;
						if (npc.ai[3] >= 3f)
						{
							npc.ai[1] = 0f;
							npc.ai[3] = 0f;
							if (Main.expertMode && Main.netMode != 1 && (double)npc.life < (double)npc.lifeMax * 0.5)
							{
								npc.ai[1] = 3f;
								npc.ai[3] += Main.rand.Next(1, 4);
							}
							npc.netUpdate = true;
							if (npc.netSpam > 10)
							{
								npc.netSpam = 10;
							}
						}
						else
						{
							npc.ai[1] = 1f;
						}
					}
				}
				else if (npc.ai[1] == 3f)
				{
					if (npc.ai[3] == 4f && flag2 && npc.Center.Y > Main.player[npc.target].Center.Y)
					{
						npc.TargetClosest();
						npc.ai[1] = 0f;
						npc.ai[2] = 0f;
						npc.ai[3] = 0f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
					}
					else if (Main.netMode != 1)
					{
						npc.TargetClosest();
						float num49 = 20f;
						Vector2 vector10 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float num50 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector10.X;
						float num51 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector10.Y;
						float num52 = Math.Abs(Main.player[npc.target].velocity.X) + Math.Abs(Main.player[npc.target].velocity.Y) / 4f;
						num52 += 10f - num52;
						if (num52 < 5f)
						{
							num52 = 5f;
						}
						if (num52 > 15f)
						{
							num52 = 15f;
						}
						if (npc.ai[2] == -1f && !flag3)
						{
							num52 *= 4f;
							num49 *= 1.3f;
						}
						if (flag3)
						{
							num52 *= 2f;
						}
						num50 -= Main.player[npc.target].velocity.X * num52;
						num51 -= Main.player[npc.target].velocity.Y * num52 / 4f;
						num50 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
						num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
						if (flag3)
						{
							num50 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
							num51 *= 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
						}
						float num53 = (float)Math.Sqrt(num50 * num50 + num51 * num51);
						float num54 = num53;
						num53 = num49 / num53;
						npc.velocity.X = num50 * num53;
						npc.velocity.Y = num51 * num53;
						npc.velocity.X += (float)Main.rand.Next(-20, 21) * 0.1f;
						npc.velocity.Y += (float)Main.rand.Next(-20, 21) * 0.1f;
						if (flag3)
						{
							npc.velocity.X += (float)Main.rand.Next(-50, 51) * 0.1f;
							npc.velocity.Y += (float)Main.rand.Next(-50, 51) * 0.1f;
							float num55 = Math.Abs(npc.velocity.X);
							float num56 = Math.Abs(npc.velocity.Y);
							if (npc.Center.X > Main.player[npc.target].Center.X)
							{
								num56 *= -1f;
							}
							if (npc.Center.Y > Main.player[npc.target].Center.Y)
							{
								num55 *= -1f;
							}
							npc.velocity.X = num56 + npc.velocity.X;
							npc.velocity.Y = num55 + npc.velocity.Y;
							npc.velocity.Normalize();
							npc.velocity *= num49;
							npc.velocity.X += (float)Main.rand.Next(-20, 21) * 0.1f;
							npc.velocity.Y += (float)Main.rand.Next(-20, 21) * 0.1f;
						}
						else if (num54 < 100f)
						{
							if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
							{
								float num57 = Math.Abs(npc.velocity.X);
								float num58 = Math.Abs(npc.velocity.Y);
								if (npc.Center.X > Main.player[npc.target].Center.X)
								{
									num58 *= -1f;
								}
								if (npc.Center.Y > Main.player[npc.target].Center.Y)
								{
									num57 *= -1f;
								}
								npc.velocity.X = num58;
								npc.velocity.Y = num57;
							}
						}
						else if (Math.Abs(npc.velocity.X) > Math.Abs(npc.velocity.Y))
						{
							float num59 = (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) / 2f;
							float num60 = num59;
							if (npc.Center.X > Main.player[npc.target].Center.X)
							{
								num60 *= -1f;
							}
							if (npc.Center.Y > Main.player[npc.target].Center.Y)
							{
								num59 *= -1f;
							}
							npc.velocity.X = num60;
							npc.velocity.Y = num59;
						}
						npc.ai[1] = 4f;
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
					}
				}
				else if (npc.ai[1] == 4f)
				{
					if (npc.ai[2] == 0f)
					{
						Main.PlaySound(36, (int)npc.position.X, (int)npc.position.Y, -1);
					}
					float num61 = num4;
					npc.ai[2] += 1f;
					if (npc.ai[2] == num61 && Vector2.Distance(npc.position, Main.player[npc.target].position) < 200f)
					{
						npc.ai[2] -= 1f;
					}
					if (npc.ai[2] >= num61)
					{
						npc.velocity *= 0.95f;
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
					float num62 = num61 + 13f;
					if (npc.ai[2] >= num62)
					{
						npc.netUpdate = true;
						if (npc.netSpam > 10)
						{
							npc.netSpam = 10;
						}
						npc.ai[3] += 1f;
						npc.ai[2] = 0f;
						if (npc.ai[3] >= 5f)
						{
							npc.ai[1] = 0f;
							npc.ai[3] = 0f;
						}
						else
						{
							npc.ai[1] = 3f;
						}
					}
				}
				else if (npc.ai[1] == 5f)
				{
					float num63 = 600f;
					float num64 = 9f;
					float num65 = 0.3f;
					Vector2 vector11 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num66 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector11.X;
					float num67 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) + num63 - vector11.Y;
					float num68 = (float)Math.Sqrt(num66 * num66 + num67 * num67);
					num68 = num64 / num68;
					num66 *= num68;
					num67 *= num68;
					if (npc.velocity.X < num66)
					{
						npc.velocity.X += num65;
						if (npc.velocity.X < 0f && num66 > 0f)
						{
							npc.velocity.X += num65;
						}
					}
					else if (npc.velocity.X > num66)
					{
						npc.velocity.X -= num65;
						if (npc.velocity.X > 0f && num66 < 0f)
						{
							npc.velocity.X -= num65;
						}
					}
					if (npc.velocity.Y < num67)
					{
						npc.velocity.Y += num65;
						if (npc.velocity.Y < 0f && num67 > 0f)
						{
							npc.velocity.Y += num65;
						}
					}
					else if (npc.velocity.Y > num67)
					{
						npc.velocity.Y -= num65;
						if (npc.velocity.Y > 0f && num67 < 0f)
						{
							npc.velocity.Y -= num65;
						}
					}
					npc.ai[2] += 1f;
					if (npc.ai[2] >= 70f)
					{
						npc.TargetClosest();
						npc.ai[1] = 3f;
						npc.ai[2] = -1f;
						npc.ai[3] = Main.rand.Next(-3, 1);
						npc.netUpdate = true;
					}
				}
				if (flag3 && npc.ai[1] == 5f)
				{
					npc.ai[1] = 3f;
				}
				return;

			}
		}
    }
}

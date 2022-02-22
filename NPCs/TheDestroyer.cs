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
    class TheDestroyer : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
            if (npc.type == 134 && Main.dayTime)
                return false;
            return base.PreAI(npc);
        }
        private void EncourageDespawn(NPC npc, int despawnTime)
        {
            if (npc.timeLeft > despawnTime)
                npc.timeLeft = despawnTime;
        }
		public int GetAttackDamage_ForProjectiles(float normalDamage, float expertDamage)
		{
			float amount = Main.expertMode ? 1f : 0.0f;
			return (int)MathHelper.Lerp(normalDamage, expertDamage, amount);
		}
		public override void PostAI(NPC npc)
        {
            if (npc.type == 134 && Main.dayTime)
            {
				if (npc.ai[3] > 0f)
				{
					npc.realLife = (int)npc.ai[3];
				}
				if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
				{
					npc.TargetClosest();
				}
				if (npc.type >= 134 && npc.type <= 136)
				{
					npc.velocity.Length();
					if (npc.type == 134 || (npc.type != 134 && Main.npc[(int)npc.ai[1]].alpha < 128))
					{
						if (npc.alpha != 0)
						{
							for (int i = 0; i < 2; i++)
							{
								int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 182, 0f, 0f, 100, default(Color), 2f);
								Main.dust[num].noGravity = true;
								Main.dust[num].noLight = true;
							}
						}
						npc.alpha -= 42;
						if (npc.alpha < 0)
						{
							npc.alpha = 0;
						}
					}
				}
				if (npc.type > 134)
				{
					bool flag = false;
					if (npc.ai[1] <= 0f)
					{
						flag = true;
					}
					else if (Main.npc[(int)npc.ai[1]].life <= 0)
					{
						flag = true;
					}
					if (flag)
					{
						npc.life = 0;
						npc.HitEffect();
						npc.checkDead();
					}
				}
				if (Main.netMode != 1)
				{
					if (npc.ai[0] == 0f && npc.type == 134)
					{
						npc.ai[3] = npc.whoAmI;
						npc.realLife = npc.whoAmI;
						int num2 = 0;
						int num3 = npc.whoAmI;
						int num4 = 80;
						for (int j = 0; j <= num4; j++)
						{
							int num5 = 135;
							if (j == num4)
							{
								num5 = 136;
							}
							num2 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y + (float)npc.height), num5, npc.whoAmI);
							Main.npc[num2].ai[3] = npc.whoAmI;
							Main.npc[num2].realLife = npc.whoAmI;
							Main.npc[num2].ai[1] = num3;
							Main.npc[num3].ai[0] = num2;
							NetMessage.SendData(23, -1, -1, null, num2);
							num3 = num2;
						}
					}
					if (npc.type == 135)
					{
						npc.localAI[0] += Main.rand.Next(4);
						if (npc.localAI[0] >= (float)Main.rand.Next(1400, 26000))
						{
							npc.localAI[0] = 0f;
							npc.TargetClosest();
							if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
							{
								Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)(npc.height / 2));
								float num6 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector.X + (float)Main.rand.Next(-20, 21);
								float num7 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector.Y + (float)Main.rand.Next(-20, 21);
								float num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
								num8 = 8f / num8;
								num6 *= num8;
								num7 *= num8;
								num6 += (float)Main.rand.Next(-20, 21) * 0.05f;
								num7 += (float)Main.rand.Next(-20, 21) * 0.05f;
								int attackDamage_ForProjectiles = GetAttackDamage_ForProjectiles(22f, 18f);
								int num9 = 100;
								vector.X += num6 * 5f;
								vector.Y += num7 * 5f;
								int num10 = Projectile.NewProjectile(vector.X, vector.Y, num6, num7, num9, attackDamage_ForProjectiles, 0f, Main.myPlayer);
								Main.projectile[num10].timeLeft = 300;
								npc.netUpdate = true;
							}
						}
					}
				}
				int num11 = (int)(npc.position.X / 16f) - 1;
				int num12 = (int)((npc.position.X + (float)npc.width) / 16f) + 2;
				int num13 = (int)(npc.position.Y / 16f) - 1;
				int num14 = (int)((npc.position.Y + (float)npc.height) / 16f) + 2;
				if (num11 < 0)
				{
					num11 = 0;
				}
				if (num12 > Main.maxTilesX)
				{
					num12 = Main.maxTilesX;
				}
				if (num13 < 0)
				{
					num13 = 0;
				}
				if (num14 > Main.maxTilesY)
				{
					num14 = Main.maxTilesY;
				}
				bool flag2 = false;
				if (!flag2)
				{
					Vector2 vector2 = default(Vector2);
					for (int k = num11; k < num12; k++)
					{
						for (int l = num13; l < num14; l++)
						{
							if (Main.tile[k, l] != null && ((Main.tile[k, l].nactive() && (Main.tileSolid[Main.tile[k, l].type] || (Main.tileSolidTop[Main.tile[k, l].type] && Main.tile[k, l].frameY == 0))) || Main.tile[k, l].liquid > 64))
							{
								vector2.X = k * 16;
								vector2.Y = l * 16;
								if (npc.position.X + (float)npc.width > vector2.X && npc.position.X < vector2.X + 16f && npc.position.Y + (float)npc.height > vector2.Y && npc.position.Y < vector2.Y + 16f)
								{
									flag2 = true;
									break;
								}
							}
						}
					}
				}
				if (!flag2)
				{
					if (npc.type != 135 || npc.ai[2] != 1f)
					{
						Lighting.AddLight((int)((npc.position.X + (float)(npc.width / 2)) / 16f), (int)((npc.position.Y + (float)(npc.height / 2)) / 16f), 0.3f, 0.1f, 0.05f);
					}
					npc.localAI[1] = 1f;
					if (npc.type == 134)
					{
						Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
						int num15 = 1000;
						bool flag3 = true;
						if (npc.position.Y > Main.player[npc.target].position.Y)
						{
							for (int m = 0; m < 255; m++)
							{
								if (Main.player[m].active)
								{
									Rectangle rectangle2 = new Rectangle((int)Main.player[m].position.X - num15, (int)Main.player[m].position.Y - num15, num15 * 2, num15 * 2);
									if (rectangle.Intersects(rectangle2))
									{
										flag3 = false;
										break;
									}
								}
							}
							if (flag3)
							{
								flag2 = true;
							}
						}
					}
				}
				else
				{
					npc.localAI[1] = 0f;
				}
				float num16 = 16f;
				float num17 = 0.1f;
				float num18 = 0.15f;
				Vector2 vector3 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
				float num19 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2);
				float num20 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2);
				num19 = (int)(num19 / 16f) * 16;
				num20 = (int)(num20 / 16f) * 16;
				vector3.X = (int)(vector3.X / 16f) * 16;
				vector3.Y = (int)(vector3.Y / 16f) * 16;
				num19 -= vector3.X;
				num20 -= vector3.Y;
				float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
				if (npc.ai[1] > 0f && npc.ai[1] < (float)Main.npc.Length)
				{
					npc.rotation = (float)Math.Atan2(num20, num19) + 1.57f;
					num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
					int num22 = (int)(44f * npc.scale);
					num21 = (num21 - (float)num22) / num21;
					num19 *= num21;
					num20 *= num21;
					npc.velocity = Vector2.Zero;
					npc.position.X += num19;
					npc.position.Y += num20;
					return;
				}
				if (!flag2)
				{
					npc.TargetClosest();
					npc.velocity.Y += 0.15f;
					if (npc.velocity.Y > num16)
					{
						npc.velocity.Y = num16;
					}
					if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num16 * 0.4)
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X -= num17 * 1.1f;
						}
						else
						{
							npc.velocity.X += num17 * 1.1f;
						}
					}
					else if (npc.velocity.Y == num16)
					{
						if (npc.velocity.X < num19)
						{
							npc.velocity.X += num17;
						}
						else if (npc.velocity.X > num19)
						{
							npc.velocity.X -= num17;
						}
					}
					else if (npc.velocity.Y > 4f)
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X += num17 * 0.9f;
						}
						else
						{
							npc.velocity.X -= num17 * 0.9f;
						}
					}
				}
				else
				{
					if (npc.soundDelay == 0)
					{
						float num23 = num21 / 40f;
						if (num23 < 10f)
						{
							num23 = 10f;
						}
						if (num23 > 20f)
						{
							num23 = 20f;
						}
						npc.soundDelay = (int)num23;
						Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y);
					}
					num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
					float num24 = Math.Abs(num19);
					float num25 = Math.Abs(num20);
					float num26 = num16 / num21;
					num19 *= num26;
					num20 *= num26;
					if (((npc.velocity.X > 0f && num19 > 0f) || (npc.velocity.X < 0f && num19 < 0f)) && ((npc.velocity.Y > 0f && num20 > 0f) || (npc.velocity.Y < 0f && num20 < 0f)))
					{
						if (npc.velocity.X < num19)
						{
							npc.velocity.X += num18;
						}
						else if (npc.velocity.X > num19)
						{
							npc.velocity.X -= num18;
						}
						if (npc.velocity.Y < num20)
						{
							npc.velocity.Y += num18;
						}
						else if (npc.velocity.Y > num20)
						{
							npc.velocity.Y -= num18;
						}
					}
					if ((npc.velocity.X > 0f && num19 > 0f) || (npc.velocity.X < 0f && num19 < 0f) || (npc.velocity.Y > 0f && num20 > 0f) || (npc.velocity.Y < 0f && num20 < 0f))
					{
						if (npc.velocity.X < num19)
						{
							npc.velocity.X += num17;
						}
						else if (npc.velocity.X > num19)
						{
							npc.velocity.X -= num17;
						}
						if (npc.velocity.Y < num20)
						{
							npc.velocity.Y += num17;
						}
						else if (npc.velocity.Y > num20)
						{
							npc.velocity.Y -= num17;
						}
						if ((double)Math.Abs(num20) < (double)num16 * 0.2 && ((npc.velocity.X > 0f && num19 < 0f) || (npc.velocity.X < 0f && num19 > 0f)))
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y += num17 * 2f;
							}
							else
							{
								npc.velocity.Y -= num17 * 2f;
							}
						}
						if ((double)Math.Abs(num19) < (double)num16 * 0.2 && ((npc.velocity.Y > 0f && num20 < 0f) || (npc.velocity.Y < 0f && num20 > 0f)))
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X += num17 * 2f;
							}
							else
							{
								npc.velocity.X -= num17 * 2f;
							}
						}
					}
					else if (num24 > num25)
					{
						if (npc.velocity.X < num19)
						{
							npc.velocity.X += num17 * 1.1f;
						}
						else if (npc.velocity.X > num19)
						{
							npc.velocity.X -= num17 * 1.1f;
						}
						if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num16 * 0.5)
						{
							if (npc.velocity.Y > 0f)
							{
								npc.velocity.Y += num17;
							}
							else
							{
								npc.velocity.Y -= num17;
							}
						}
					}
					else
					{
						if (npc.velocity.Y < num20)
						{
							npc.velocity.Y += num17 * 1.1f;
						}
						else if (npc.velocity.Y > num20)
						{
							npc.velocity.Y -= num17 * 1.1f;
						}
						if ((double)(Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < (double)num16 * 0.5)
						{
							if (npc.velocity.X > 0f)
							{
								npc.velocity.X += num17;
							}
							else
							{
								npc.velocity.X -= num17;
							}
						}
					}
				}
				npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
				if (npc.type != 134)
				{
					return;
				}
				if (flag2)
				{
					if (npc.localAI[0] != 1f)
					{
						npc.netUpdate = true;
					}
					npc.localAI[0] = 1f;
				}
				else
				{
					if (npc.localAI[0] != 0f)
					{
						npc.netUpdate = true;
					}
					npc.localAI[0] = 0f;
				}
				if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
				{
					npc.netUpdate = true;
				}
			}
        }
    }
}

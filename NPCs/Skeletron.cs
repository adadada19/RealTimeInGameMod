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

    public class Skeletron : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
			if (npc.type == 35)
				return false;
			return base.PreAI(npc);
        }
        public override void PostAI(NPC npc)
        {
            if (npc.type == 35)
			{
				npc.defense = npc.defDefense;
				if (npc.ai[0] == 0f && Main.netMode != 1)
				{
					npc.TargetClosest();
					npc.ai[0] = 1f;
					if (npc.type != 68)
					{
						int num156 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 36, npc.whoAmI);
						Main.npc[num156].ai[0] = -1f;
						Main.npc[num156].ai[1] = npc.whoAmI;
						Main.npc[num156].target = npc.target;
						Main.npc[num156].netUpdate = true;
						num156 = NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)npc.position.Y + npc.height / 2, 36, npc.whoAmI);
						Main.npc[num156].ai[0] = 1f;
						Main.npc[num156].ai[1] = npc.whoAmI;
						Main.npc[num156].ai[3] = 150f;
						Main.npc[num156].target = npc.target;
						Main.npc[num156].netUpdate = true;
					}
				}
				if ((npc.type == 68 || Main.netMode == 1) && npc.localAI[0] == 0f)
				{
					npc.localAI[0] = 1f;
					Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
				}
				if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
				{
					npc.TargetClosest();
					if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
					{
						npc.ai[1] = 3f;
					}
				}
/*                if ((npc.type == 68 || Main.dayTime) && npc.ai[1] != 3f && npc.ai[1] != 2f)
                {
                    npc.ai[1] = 2f;
                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                }*/
                int num157 = 0;
				if (Main.expertMode)
				{
					for (int num158 = 0; num158 < 200; num158++)
					{
						if (Main.npc[num158].active && Main.npc[num158].type == npc.type + 1)
						{
							num157++;
						}
					}
					npc.defense += num157 * 25;
					if ((num157 < 2 || (double)npc.life < (double)npc.lifeMax * 0.75) && npc.ai[1] == 0f)
					{
						float num159 = 80f;
						if (num157 == 0)
						{
							num159 /= 2f;
						}
						if (Main.netMode != 1 && npc.ai[2] % num159 == 0f)
						{
							Vector2 center3 = npc.Center;
							float num160 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - center3.X;
							float num161 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - center3.Y;
							float num162 = (float)Math.Sqrt(num160 * num160 + num161 * num161);
							if (Collision.CanHit(center3, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
							{
								float num163 = 3f;
								if (num157 == 0)
								{
									num163 += 2f;
								}
								float num164 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - center3.X + (float)Main.rand.Next(-20, 21);
								float num165 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - center3.Y + (float)Main.rand.Next(-20, 21);
								float num166 = (float)Math.Sqrt(num164 * num164 + num165 * num165);
								num166 = num163 / num166;
								num164 *= num166;
								num165 *= num166;
								Vector2 vector19 = new Vector2(num164 * 1f + (float)Main.rand.Next(-50, 51) * 0.01f, num165 * 1f + (float)Main.rand.Next(-50, 51) * 0.01f);
								vector19.Normalize();
								vector19 *= num163;
								vector19 += npc.velocity;
								num164 = vector19.X;
								num165 = vector19.Y;
								int num167 = 270;
								center3 += vector19 * 5f;
							}
						}
					}
				}
				if (npc.ai[1] == 0f)
				{
					npc.damage = npc.defDamage;
					npc.ai[2] += 1f;
					if (npc.ai[2] >= 800f)
					{
						npc.ai[2] = 0f;
						npc.ai[1] = 1f;
						npc.TargetClosest();
						npc.netUpdate = true;
					}
					npc.rotation = npc.velocity.X / 15f;
					float num169 = 0.02f;
					float num170 = 2f;
					float num171 = 0.05f;
					float num172 = 8f;
					if (Main.expertMode)
					{
						num169 = 0.03f;
						num170 = 4f;
						num171 = 0.07f;
						num172 = 9.5f;
					}
					if (npc.position.Y > Main.player[npc.target].position.Y - 250f)
					{
						if (npc.velocity.Y > 0f)
						{
							npc.velocity.Y *= 0.98f;
						}
						npc.velocity.Y -= num169;
						if (npc.velocity.Y > num170)
						{
							npc.velocity.Y = num170;
						}
					}
					else if (npc.position.Y < Main.player[npc.target].position.Y - 250f)
					{
						if (npc.velocity.Y < 0f)
						{
							npc.velocity.Y *= 0.98f;
						}
						npc.velocity.Y += num169;
						if (npc.velocity.Y < 0f - num170)
						{
							npc.velocity.Y = 0f - num170;
						}
					}
					if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))
					{
						if (npc.velocity.X > 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						npc.velocity.X -= num171;
						if (npc.velocity.X > num172)
						{
							npc.velocity.X = num172;
						}
					}
					if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))
					{
						if (npc.velocity.X < 0f)
						{
							npc.velocity.X *= 0.98f;
						}
						npc.velocity.X += num171;
						if (npc.velocity.X < 0f - num172)
						{
							npc.velocity.X = 0f - num172;
						}
					}
				}
				else if (npc.ai[1] == 1f)
				{
					npc.defense -= 10;
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
					Vector2 vector20 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
					float num173 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector20.X;
					float num174 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector20.Y;
					float num175 = (float)Math.Sqrt(num173 * num173 + num174 * num174);
					float num176 = 1.5f;
					if (Main.expertMode)
					{
						num176 = 3.5f;
						if (num175 > 150f)
						{
							num176 *= 1.05f;
						}
						if (num175 > 200f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 250f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 300f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 350f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 400f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 450f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 500f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 550f)
						{
							num176 *= 1.1f;
						}
						if (num175 > 600f)
						{
							num176 *= 1.1f;
						}
						switch (num157)
						{
							case 0:
								num176 *= 1.1f;
								break;
							case 1:
								num176 *= 1.05f;
								break;
						}
					}
					num175 = num176 / num175;
					npc.velocity.X = num173 * num175;
					npc.velocity.Y = num174 * num175;
				}
				else if (npc.ai[1] == 2f)
				{
                    npc.damage = 1000;
                    npc.defense = 9999;
                    npc.rotation += (float)npc.direction * 0.3f;
                    Vector2 vector21 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                    float num177 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector21.X;
                    float num178 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector21.Y;
                    float num179 = (float)Math.Sqrt(num177 * num177 + num178 * num178);
                    num179 = 8f / num179;
                    npc.velocity.X = num177 * num179;
                    npc.velocity.Y = num178 * num179;
                }
				else if (npc.ai[1] == 3f)
				{
					npc.velocity.Y += 0.1f;
					if (npc.velocity.Y < 0f)
					{
						npc.velocity.Y *= 0.95f;
					}
					npc.velocity.X *= 0.95f;
				}
				if (npc.ai[1] != 2f && npc.ai[1] != 3f && npc.type != 68 && (num157 != 0 || !Main.expertMode))
				{
					int num180 = Dust.NewDust(new Vector2(npc.position.X + (float)(npc.width / 2) - 15f - npc.velocity.X * 5f, npc.position.Y + (float)npc.height - 2f), 30, 10, 5, (0f - npc.velocity.X) * 0.2f, 3f, 0, default(Color), 2f);
					Main.dust[num180].noGravity = true;
					Main.dust[num180].velocity.X *= 1.3f;
					Main.dust[num180].velocity.X += npc.velocity.X * 0.4f;
					Main.dust[num180].velocity.Y += 2f + npc.velocity.Y;
					for (int num181 = 0; num181 < 2; num181++)
					{
						num180 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 120f), npc.width, 60, 5, npc.velocity.X, npc.velocity.Y, 0, default(Color), 2f);
						Main.dust[num180].noGravity = true;
						Dust dust = Main.dust[num180];
						dust.velocity -= npc.velocity;
						Main.dust[num180].velocity.Y += 5f;
					}
				}
				return;
			}
		}
    }
}

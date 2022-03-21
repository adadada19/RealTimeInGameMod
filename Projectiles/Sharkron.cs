using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RealTimeInGameMod.Buffs;

namespace RealTimeInGameMod.Projectiles
{
    class Sharkron : ModProjectile
    {
        public override void AI()
        {
			{
				noTileCollide = true;
				int num1050 = 90;
				if (target < 0 || target == 255 || Main.player[target].dead)
				{
					TargetClosest(faceTarget: false);
					direction = 1;
					netUpdate = true;
				}
				if (this.ai[0] == 0f)
				{
					this.ai[1]++;
					_ = type;
					_ = 372;
					noGravity = true;
					dontTakeDamage = true;
					velocity.Y = this.ai[3];
					if (type == 373)
					{
						float num1051 = (float)Math.PI / 30f;
						float num1052 = this.ai[2];
						float num1053 = (float)(Math.Cos(num1051 * localAI[1]) - 0.5) * num1052;
						position.X -= num1053 * (float)(-direction);
						localAI[1]++;
						num1053 = (float)(Math.Cos(num1051 * localAI[1]) - 0.5) * num1052;
						position.X += num1053 * (float)(-direction);
						if (Math.Abs(Math.Cos(num1051 * localAI[1]) - 0.5) > 0.25)
						{
							spriteDirection = ((!(Math.Cos(num1051 * localAI[1]) - 0.5 >= 0.0)) ? 1 : (-1));
						}
						rotation = velocity.Y * (float)spriteDirection * 0.1f;
						if ((double)rotation < -0.2)
						{
							rotation = -0.2f;
						}
						if ((double)rotation > 0.2)
						{
							rotation = 0.2f;
						}
						alpha -= 6;
						if (alpha < 0)
						{
							alpha = 0;
						}
					}
					if (this.ai[1] >= (float)num1050)
					{
						this.ai[0] = 1f;
						this.ai[1] = 0f;
						if (!Collision.SolidCollision(position, width, height))
						{
							this.ai[1] = 1f;
						}
						SoundEngine.PlaySound(4, (int)base.Center.X, (int)base.Center.Y, 19);
						TargetClosest();
						spriteDirection = direction;
						Vector2 vector133 = Main.player[target].Center - base.Center;
						vector133.Normalize();
						velocity = vector133 * 16f;
						rotation = velocity.ToRotation();
						if (direction == -1)
						{
							rotation += (float)Math.PI;
						}
						netUpdate = true;
					}
				}
				else
				{
					if (this.ai[0] != 1f)
					{
						return;
					}
					noGravity = true;
					if (!Collision.SolidCollision(position, width, height))
					{
						if (this.ai[1] < 1f)
						{
							this.ai[1] = 1f;
						}
					}
					else
					{
						alpha -= 15;
						if (alpha < 150)
						{
							alpha = 150;
						}
					}
					if (this.ai[1] >= 1f)
					{
						alpha -= 60;
						if (alpha < 0)
						{
							alpha = 0;
						}
						dontTakeDamage = false;
						this.ai[1]++;
						if (Collision.SolidCollision(position, width, height))
						{
							if (DeathSound != null)
							{
								SoundEngine.PlaySound(DeathSound, position);
							}
							life = 0;
							HitEffect();
							active = false;
							return;
						}
					}
					if (this.ai[1] >= 60f)
					{
						noGravity = false;
					}
					rotation = velocity.ToRotation();
					if (direction == -1)
					{
						rotation += (float)Math.PI;
					}
				}
			}
		}
    }
}

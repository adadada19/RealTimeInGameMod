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
	class BabyDaySlime : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Baby Day Slime");
			Main.projFrames[projectile.type] = 6;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;

		}

		public sealed override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 16;
			projectile.aiStyle = 26;
			aiType = ProjectileID.BabySlime;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.minionSlots = 1f;
			projectile.light = 0.5f;
			
		}
		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			projectile.friendly = true;
            Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
            if (Main.rand.NextBool(20))
            {
                float xSpeed = Main.rand.NextFloat(-1f, 1f);
                float ySpeed = Main.rand.NextFloat(-1f, 1f);
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight, xSpeed, ySpeed, 200, Color.White);
            }
            if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<DayTimeSlimeBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<DayTimeSlimeBuff>()))
			{
				projectile.timeLeft = 2;
			}
		}
	}
}

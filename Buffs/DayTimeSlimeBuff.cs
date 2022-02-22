using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RealTimeInGameMod.Projectiles;

namespace RealTimeInGameMod.Buffs
{
    class DayTimeSlimeBuff : ModBuff
    {
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Day Slime Buff");
			Description.SetDefault("Shiny!");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}
		public override void Update(Player player, ref int buffIndex)

		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<BabyDaySlime>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}
}

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
using RealTimeInGameMod.Buffs;

namespace RealTimeInGameMod.Items
{
	class MinionCheck : ModPlayer
    {
		public float MinionSlotsUsed = new float();
		public override void PostUpdateMiscEffects()
		{
			MinionSlotsUsed = player.slotsMinions;
		}
    }
	class GolemHeadStaff : ModItem
	{
        bool isHeadAlive = false;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Golem's Head Staff");
			Tooltip.SetDefault("Summons ancient one to follow you");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // projectile lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}
		public override void SetDefaults()
		{
			item.damage = 20;
			item.knockBack = 3f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item44;
			item.shoot = ModContent.ProjectileType<GolemHead>();
			item.buffType = ModContent.BuffType<GolemHeadBuff>();
			item.melee = false;
			item.noMelee = true;
			item.summon = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            //float SlotsMinions = 0f;
            position = Main.MouseWorld;
			//Projectile p = Main.projectile[i];
			if (Main.projectile.Any(x => x.active && x.type == ModContent.ProjectileType<GolemHead>()) && isHeadAlive) //we are good - adjust position
			{
				var adjList = Main.projectile.Where(x => x.type == ModContent.ProjectileType<GolemHead>() && x.modProjectile is GolemHead);
				var minion = adjList.FirstOrDefault();
				if (minion != null)
				{
					if (player.GetModPlayer<MinionCheck>().MinionSlotsUsed < player.maxMinions -1)
					{
						minion.minionSlots++;
						minion.damage += 7;
						minion.knockBack += 0.5f;
						minion.active = true;
						Main.NewText("Huy", Color.Red);
					}
				}
			}
			else
			{
				isHeadAlive = false;
			}
			if (!isHeadAlive)
			{
				Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<GolemHead>(), damage, knockBack, player.whoAmI);
				isHeadAlive = true;
			}
			player.AddBuff(item.buffType, 2);
			return false;

		}
	}
}

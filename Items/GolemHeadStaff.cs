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
	class GolemHeadStaff : ModItem
	{
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
			position = Main.MouseWorld; 
			player.AddBuff(item.buffType, 2);
			return true;
		}
	}
}

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
		public float MinionSlotsUsed = 0f;
		public override void PostUpdateMiscEffects()
		{
			MinionSlotsUsed = player.slotsMinions;
			Main.NewText(player.slotsMinions, Color.Green);
		}
		
		public void MinionSlots(float MinionSlots)
        {
			MinionSlots = player.slotsMinions;
        }
    }
	class GolemHeadStaff : ModItem
	{
		public float SlotsMinions = 0f;
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
			bool isHeadAlive = false;
			for (int i = 0; i < 1000; i++)
            {
				Projectile p = Main.projectile[i];
				if (p.type == ModContent.ProjectileType<GolemHead>())
                {
					isHeadAlive = true;
					ModContent.GetInstance<MinionCheck>().MinionSlots(SlotsMinions);
					switch (SlotsMinions)
                    {
						case 0f:
                            {
								item.damage = 20;
								item.knockBack = 3f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(1f);
								break;
                            }
						case 1f:
                            {
								item.damage = 25;
								item.knockBack = 3.5f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(2f);
								break;
                            }
						case 2f:
                            {
								item.damage = 30;
								item.knockBack = 4f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(3f);
								break;
                            }
						case 3f:
                            {
								item.damage = 35;
								item.knockBack = 4.5f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(4f);
								break;
                            }
						case 4f:
							{
								item.damage = 40;
								item.knockBack = 5f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(5f);
								break;
							}
						case 5f:
							{
								item.damage = 45;
								item.knockBack = 5.5f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(6f);
								break;
							}
						case 6f:
							{
								item.damage = 50;
								item.knockBack = 6f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(7f);
								break;
							}
						case 7f:
							{
								item.damage = 55;
								item.knockBack = 6.5f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(8f);
								break;
							}
						case 8f:
							{
								item.damage = 60;
								item.knockBack = 7f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(9f);
								break;
							}
						case 9f:
							{
								item.damage = 65;
								item.knockBack = 7f;
								ModContent.GetInstance<GolemHead>().ProjectileMinionSlots(10f);
								break;
							}
						case 10f:
							{
								item.damage = 80;
								item.knockBack = 7f;
								break;
							}
					}
                }
				if (!isHeadAlive)
                {
					Projectile.NewProjectile(position, Vector2.Zero, ModContent.ProjectileType<GolemHead>(), damage, knockBack, player.whoAmI);
					isHeadAlive = true;
				}
            }
			player.AddBuff(item.buffType, 2);
			return false;
		}
	}
}

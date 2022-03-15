using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealTimeInGameMod.Items
{
    class DebugStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Debug Stick");
            DisplayName.SetDefault("Debug Stick");
        }
        public override void SetDefaults()
        {
            item.knockBack = 0f;
            item.width = 64;
            item.height = 64;
            item.maxStack = 1;
            item.rare = ItemRarityID.Yellow;
            item.melee = false;
            item.damage = 1;
            item.magic = true;
            item.mana = 0;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.noMelee = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 10;
            item.useAnimation = 10;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.NewText(String.Format("{0}" + "{1}", Main.dayTime, Main.time), Color.Lime);
            return false;
        }
    }
}

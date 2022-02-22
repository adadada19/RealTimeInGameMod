using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace RealTimeInGameMod.Items
{
    class SuspiciousLookingEye : GlobalItem
    {
        public override bool UseItem(Item item, Player player)
        {
            if (NPC.AnyNPCs(NPCID.EyeofCthulhu) == false && item.type == 43)
            {
                Main.PlaySound(SoundID.Roar, (int)item.position.X, (int)item.position.Y, 0);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, 4);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 4f);
                }
            }
            return true;
        }
    }
/*    class SuspiciousLookingEye : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("{$ItemTooltip.SuspiciousLookingEye}");
            DisplayName.SetDefault("{$ItemName.SuspiciousLookingEye}");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.SuspiciousLookingEye);
            item.consumable = true;
        }
        public override bool ConsumeItem(Player player)
        {
            if (Main.dayTime == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime == true && NPC.AnyNPCs(NPCID.EyeofCthulhu) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool UseItem(Player player)
        {
            if (Main.dayTime && NPC.AnyNPCs(NPCID.EyeofCthulhu) == false)
                {
                    Main.PlaySound(SoundID.Roar, (int)item.position.X, (int)item.position.Y, 0);
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.SpawnOnPlayer(player.whoAmI, 4);
                    }
                    else
                    {
                        NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 4f);
                    }
                }
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SuspiciousLookingEye, 1);
            //recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }*/
}

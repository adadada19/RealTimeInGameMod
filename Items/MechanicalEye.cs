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
    class MechanicalEye : GlobalItem
    {
        public override bool UseItem(Item item, Player player)
        {
            if (NPC.AnyNPCs(NPCID.Spazmatism) == false && NPC.AnyNPCs(NPCID.Retinazer) == false && item.type == ItemID.MechanicalEye)
            {
				Main.PlaySound(SoundID.Roar, (int)player.Center.X, (int)player.Center.Y, 0, 1f, 0f);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.SpawnOnPlayer(player.whoAmI, NPCID.Retinazer);
					NPC.SpawnOnPlayer(player.whoAmI, NPCID.Spazmatism);
				}
				else
				{
					NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 125f, 0f, 0f, 0, 0, 0);
					NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 126f, 0f, 0f, 0, 0, 0);
				}
			}
            return true;
        }
    }
}

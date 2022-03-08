using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RealTimeInGameMod.Items
{
    class MechanicalSkull : GlobalItem
    {
		public override bool UseItem(Item item, Player player)
		{
			if (NPC.AnyNPCs(NPCID.SkeletronPrime) == false && item.type == ItemID.MechanicalSkull)
			{
				Main.PlaySound(SoundID.Roar, (int)player.Center.X, (int)player.Center.Y, 0, 1f, 0f);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.SpawnOnPlayer(player.whoAmI, NPCID.SkeletronPrime);
				}
				else
				{
					NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 127f, 0f, 0f, 0, 0, 0);
				}
			}
			return true;
		}
	}
}

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
    class MechanicalWorm : GlobalItem
    {
		public override bool UseItem(Item item, Player player)
		{
			if (NPC.AnyNPCs(NPCID.TheDestroyer) == false && NPC.AnyNPCs(NPCID.TheDestroyerBody) == false && NPC.AnyNPCs(NPCID.TheDestroyerTail) == false && item.type == ItemID.MechanicalWorm)
			{
				Main.PlaySound(SoundID.Roar, (int)player.Center.X, (int)player.Center.Y, 0, 1f, 0f);
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.SpawnOnPlayer(player.whoAmI, NPCID.TheDestroyer);
				}
				else
				{
					NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, player.whoAmI, 134f, 0f, 0f, 0, 0, 0);
				}
			}
			return true;
		}
	}
}

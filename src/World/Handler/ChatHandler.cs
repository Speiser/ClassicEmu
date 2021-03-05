﻿using System.Threading.Tasks;
using Classic.World.Data;
using Classic.World.Messages.Client;

namespace Classic.World.Handler
{
    public class ChatHandler
    {
        [OpcodeHandler(Opcode.CMSG_MESSAGECHAT)]
        public static async Task OnMessageChat(HandlerArguments args)
        {
            var request = new CMSG_MESSAGECHAT(args.Data);

            // debugging stuff :D
            if (request.Message.StartsWith(".spawn"))
            {
                var spawnId = int.Parse(request.Message.Split(" ")[1]);
                var creature = new Creature { Model = spawnId, Position = args.Client.Character.Position.Copy() };
                await args.WorldState.SpawnCreature(creature);
            }
        }
    }
}

﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voltaire.Controllers.Helpers;
using Voltaire.Models;

namespace Voltaire.Controllers.Settings
{
    class BanIdentifier
    {
        public static async Task PerformAsync(SocketCommandContext context, string identifier, DataBase db)
        {
            if (identifier.Length != 4)
            {
                await context.Channel.SendMessageAsync("Please use the 4 digit number following the identifier to ban users.");
                return;
            }

            var guild = FindOrCreateGuild.Perform(context.Guild, db);

            if (guild.BannedIdentifiers.Any(x => x.Identifier == identifier))
            {
                await context.Channel.SendMessageAsync("That identifier is already banned!");
                return;
            }

            guild.BannedIdentifiers.Add(new BannedIdentifier { Identifier = identifier });
            db.SaveChanges();
            await context.Channel.SendMessageAsync(text: $"{identifier} is now banned");
        }
    }
}

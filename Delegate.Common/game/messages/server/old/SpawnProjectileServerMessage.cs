// Copyright (c) Gothos
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages.server
{
    public class SpawnProjectileServerMessage : ParsedMessage
    {
        public EntityId Id { get; private set; }
        public EntityId OwnerId { get; private set; }

        internal SpawnProjectileServerMessage(TeraMessageReader reader)
            : base(reader)
        {
            Id = reader.ReadEntityId();
            reader.Skip(37);
            OwnerId = reader.ReadEntityId();
        }
    }
}

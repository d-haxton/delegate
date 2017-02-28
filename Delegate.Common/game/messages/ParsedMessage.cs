﻿// Copyright (c) Gothos
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Delegate.Tera.Common.game.services;

namespace Delegate.Tera.Common.game.messages
{
    // Base class for parsed messages
    public abstract class ParsedMessage : Message
    {
        public string OpCodeName { get; private set; }

        internal ParsedMessage(TeraMessageReader reader)
            : base(reader.Message.Time, reader.Message.Direction, reader.Message.Data)
        {
            OpCodeName = reader.OpCodeName;
        }
    }
}

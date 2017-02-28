// Copyright (c) Gothos
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Delegate.Tera.Common.game
{
    public class Player
    {
        private UserEntity _user;
        public uint PlayerId => User.PlayerId;
        public string Name => User.Name;
        public bool IsHealer => Class == PlayerClass.Priest || Class == PlayerClass.Mystic;
        public string GuildName => User.GuildName;
        public RaceGenderClass RaceGenderClass => User.RaceGenderClass;
        public PlayerClass Class => RaceGenderClass.Class;

        public UserEntity User
        {
            get { return _user; }
            set
            {
                if (_user.PlayerId != value.PlayerId)
                    throw new ArgumentException("Users must represent the same Player");
                _user = value;
            }
        }

        public Player(UserEntity user)
        {
            _user = user;
        }

        public override string ToString()
        {
            return $"{Class} {Name} [{GuildName}]";
        }

        public override bool Equals(object obj)
        {
            var other = obj as Player;
            return PlayerId.Equals(other?.PlayerId);
        }

        public override int GetHashCode()
        {
            return PlayerId.GetHashCode();
        }
    }
}

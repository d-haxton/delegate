using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Delegate.Meter.events;
using Delegate.Meter.interfaces;
using Delegate.Meter.meter.data;
using Delegate.Meter.Properties;
using Delegate.Tera.Common.game;
using Delegate.Tera.Common.game.messages.server;
using Delegate.Tera.Common.game.services;
using Delegate.Tera.Network.interfaces;

namespace Delegate.Meter.meter
{
    public class DelegateNetworkWrapper : IDelegateNetworkWrapper
    {
        private TeraData _teraData;
        private EntityTracker _entityTracker;
        private PlayerTracker _playerTracker;
        private MessageFactory _messageFactory;
        public event EventHandler<SkillEventArgs> SkillEvent = delegate { }; 

        public DelegateNetworkWrapper(IDelegateSniffer sniffer)
        {
            sniffer.MessageReceived += OnMessageReceived;
            sniffer.NewConnection += OnNewConnection;
        }


        private void OnNewConnection(Server obj)
        {
            Console.WriteLine(@"Connection to tera found.");
            //todo :: version
            string verison = "311378";
            _teraData = new TeraData(verison);

            _entityTracker = new EntityTracker(_teraData.NpcDatabase);
            _playerTracker = new PlayerTracker(_entityTracker);
            _messageFactory = new MessageFactory(_teraData.OpCodeNamer, _teraData.SysMessageNamer, obj.Region, uint.Parse(verison));

            _playerTracker.UpdatedUser += OnUpdateUser;
        }

        private void OnUpdateUser(Player obj)
        {
            //todo
        }

        public Server Server { get; set; }

        private void OnMessageReceived(Message obj)
        {
            var message = _messageFactory.Create(obj);
            _entityTracker.Update(message);

            var skillResultMessage = message as EachSkillResultServerMessage;

            if (skillResultMessage?.IsUseless == false)
            {
                var skillResult = new SkillResult(skillResultMessage, _entityTracker, _playerTracker,
                    _teraData.SkillDatabase);

                if (skillResult.Skill != null)
                {
                    var args = new SkillEventArgs {SkillResult = skillResult};

                    SkillEvent?.Invoke(this, args);
                    Console.WriteLine($@"Skill: {skillResult.Skill.Name} did {skillResult.Damage} to {skillResult.Target} from {skillResult.Source}");
                }
            }
        }
    }
}

using Services.Interfaces;
using Services.Interfaces.Builders;
using Services.Interfaces.Builders.Parameters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Services.Classes.Commands.CreateCommands
{
    public class MessageTypeCreateCommand : SpSqlCommand
    {
        public MessageTypeCreateCommand(IMessageTypeCreateDropParameter parameterBuilder) : 
            base("sp_Create_MessageType", parameterBuilder)
        {
        }
    }
}

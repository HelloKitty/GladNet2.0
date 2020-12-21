﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using Common.Logging;

namespace GladNet
{
	public sealed class TCPEchoGladNetServerApplication : TcpGladNetServerApplication<TCPEchoManagedSession>
	{
		public TCPEchoGladNetServerApplication(NetworkAddressInfo serverAddress, ILog logger) 
			: base(serverAddress, logger)
		{

		}

		protected override bool IsClientAcceptable(Socket connection)
		{
			return true;
		}

		public override TCPEchoManagedSession Create(SessionCreationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			NetworkConnectionOptions options = new NetworkConnectionOptions(2, 2, 1024);
			var serializer = new StringMessageSerializer();

			SessionMessageBuildingServiceContext<string, string> messageServices = 
				new SessionMessageBuildingServiceContext<string, string>(new StringMessagePacketHeaderFactory(), serializer, serializer, new StringPacketHeaderSerializer());

			return new TCPEchoManagedSession(options, context.Connection, context.Details, messageServices);
		}
	}
}
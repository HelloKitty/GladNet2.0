﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladNet.Common
{
	public class NetworkMessageFactory
	{
		public NetworkMessage Create(PacketPayload payload, IResponsePayload responseParameters)
		{
			if (payload == null)
				throw new ArgumentNullException("payload", "Payload is null in Response NetworkMessage factory method.");

			if(responseParameters == null)
				throw new ArgumentNullException("responseParameters", "ResponseParameters is null in Response NetworkMessage factory method.");

			return new ResponseMessage(payload, responseParameters.ResponseCode);
		}

		public NetworkMessage Create(PacketPayload payload, IRequestPayload requestParameters)
		{
			if (payload == null)
				throw new ArgumentNullException("payload", "Payload is null in Response NetworkMessage factory method.");

			if (requestParameters == null)
				throw new ArgumentNullException("responseParameters", "ResponseParameters is null in Response NetworkMessage factory method.");

			return new RequestMessage(payload);
		}

		public NetworkMessage Create(PacketPayload payload, IEventPayload eventParameters)
		{
			if (payload == null)
				throw new ArgumentNullException("payload", "Payload is null in Response NetworkMessage factory method.");

			if (eventParameters == null)
				throw new ArgumentNullException("responseParameters", "ResponseParameters is null in Response NetworkMessage factory method.");

			return new EventMessage(payload);
		}
	}
}

﻿using GladNet.Common;
using GladNet.Payload;
using GladNet.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GladNet.Message
{
	/// <summary>
	/// <see cref="RequestMessage"/>s are <see cref="NetworkMessage"/>s that request remote peers for/to-do something.
	/// Generally these ellict <see cref="ResponseMessage"/> but there is no implict mechanism in either <see cref="NetworkMessage"/>
	/// Subtypes for such a thing.
	/// </summary>
	[GladNetSerializationContract]
	public class RequestMessage : NetworkMessage, IRequestMessage
	{
		/// <summary>
		/// Indicates the <see cref="OperationType"/> that this object maps to.
		/// </summary>
		public OperationType OperationTypeMappedValue { get { return OperationType.Request; } }

		/// <summary>
		/// Protected protobuf-net constructor.
		/// </summary>
		protected RequestMessage()
			: base()
		{

		}

		/// <summary>
		/// Constructor for <see cref="RequestMessage"/> that calls <see cref="NetworkMessage"/>.ctor
		/// </summary>
		/// <param name="payload"><see cref="PacketPayload"/> of the <see cref="NetworkMessage"/>.</param>
		public RequestMessage(PacketPayload payload)
			: base(payload)
		{
			//TODO: Exception for null packet
		}

		/// <summary>
		/// Protected instructor used for deep cloning the NetworkMessage.
		/// </summary>
		/// <param name="netSendablePayload">Shallow copy of the PacketPayload for copying.</param>
		protected RequestMessage(NetSendable<PacketPayload> netSendablePayload)
			: base(netSendablePayload)
		{
			//Used for deep cloning
		}

		/// <summary>
		/// Dispatches the <see cref="RequestMessage"/> (this) to the supplied <see cref="INetworkMessageReceiver"/>.
		/// </summary>
		/// <param name="receiver">The target <see cref="INetworkMessageReceiver"/>.</param>
		/// <exception cref="ArgumentNullException">Throws if either parameters are null.</exception>
		/// <param name="parameters">The <see cref="IMessageParameters"/> of the <see cref="RequestMessage"/>.</param>
		public override void Dispatch(INetworkMessageReceiver receiver, IMessageParameters parameters)
		{
			if (receiver == null) throw new ArgumentNullException(nameof(receiver), $"{nameof(INetworkMessageReceiver)} parameter is null in {this.GetType().Name}");
			if (parameters == null) throw new ArgumentNullException(nameof(parameters), $"{nameof(IMessageParameters)} parameter is null in {this.GetType().Name}");

			receiver.OnNetworkMessageReceive(this, parameters);
		}

		public override NetworkMessage DeepClone()
		{
			lock (syncObj)
				lock (Payload.syncObj)
					return new RequestMessage(Payload.ShallowClone());
		}
	}
}
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
	/// <see cref="ResponseMessage"/>s are <see cref="NetworkMessage"/>s in response to <see cref="RequestMessage"/> from remote peers.
	/// It contains additional fields/properties compared to <see cref="NetworkMessage"/> that provide information on the response.
	/// </summary>
	[GladNetSerializationContract]
	public class ResponseMessage : NetworkMessage, IResponseMessage
	{
		/// <summary>
		/// Indicates the <see cref="OperationType"/> that this object maps to.
		/// </summary>
		public OperationType OperationTypeMappedValue { get { return OperationType.Response; } }

		/// <summary>
		/// Protected protobuf-net constructor.
		/// </summary>
		protected ResponseMessage()
			: base()
		{

		}

		/// <summary>
		/// Constructor for <see cref="ResponseMessage"/> that calls <see cref="NetworkMessage"/>.ctor
		/// </summary>
		/// <param name="payload"><see cref="PacketPayload"/> of the <see cref="NetworkMessage"/>.</param>
		public ResponseMessage(PacketPayload payload)
			: base(payload)
		{

		}

		/// <summary>
		/// Protected instructor used for deep cloning the NetworkMessage.
		/// </summary>
		/// <param name="netSendablePayload">Shallow copy of the PacketPayload for copying.</param>
		protected ResponseMessage(NetSendable<PacketPayload> netSendablePayload)
			: base(netSendablePayload)
		{
			//Used for deep cloning
		}

		/// <summary>
		/// Dispatches the <see cref="ResponseMessage"/> (this) to the supplied <see cref="INetworkMessageReceiver"/>.
		/// </summary>
		/// <param name="receiver">The target <see cref="INetworkMessageReceiver"/>.</param>
		/// <exception cref="ArgumentNullException">Throws if either parameters are null.</exception>
		/// <param name="parameters">The <see cref="IMessageParameters"/> of the <see cref="ResponseMessage"/>.</param>
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
					return new ResponseMessage(Payload.ShallowClone());
		}
	}
}
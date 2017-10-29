﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GladNet;

namespace GladNet
{
	/// <summary>
	/// Contract for services that provide the ability to send payloads.
	/// </summary>
	/// <typeparam name="TPayloadBaseType">The payload's base type that is sent.</typeparam>
	public interface IPeerPayloadSendService<in TPayloadBaseType>
		where TPayloadBaseType : class
	{
		//TODO: We will implement a priority or immediate sending overloads in the future

		/// <summary>
		/// Sends the provided <see cref="payload"/>
		/// </summary>
		/// <typeparam name="TPayloadType">The type of payload.</typeparam>
		/// <param name="payload">The payload to send.</param>
		/// <param name="method">The delivery method for sending the message.</param>
		/// <returns>Indicates the result of the send message operation.</returns>
		Task<SendResult> SendMessage<TPayloadType>(TPayloadType payload, DeliveryMethod method)
			where TPayloadType : class, TPayloadBaseType;
	}
}
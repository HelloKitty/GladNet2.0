﻿using GladNet.Serializer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladNet.Common.Tests
{
	[TestFixture]
	public static class TestAttributeFunctionality
	{
		[Test]
		[TestCase(0)]
		[TestCase(-1)]
		[TestCase(int.MinValue)]
		public static void Test_MemberAttribute_Ctor_With_Invalid_Tags(int tagID)
		{
			//Should throw due to invalid tagID
			//See ctor of https://github.com/mgravell/protobuf-net/blob/e601b359c6ae56afc159754d29f5e7d0f05a01f5/protobuf-net/ProtoMemberAttribute.cs
			//attribute is based on protobuf-net specs.

			//arrange
			Assert.Throws<ArgumentException>(() => new GladNetMemberAttribute((GladNetPayloadDataIndex)tagID));
		}

		[Test]
		public static void Test_MemberAttribute_Ctor_With_Valid_Tags([Range((int)GladNetPayloadDataIndex.Index1, (int)GladNetPayloadDataIndex.Index38)] int tagID)
		{
			//arrange
			GladNetMemberAttribute gma = new GladNetMemberAttribute((GladNetPayloadDataIndex)tagID);

			//assert
			Assert.AreEqual(gma.TagID, tagID);
		}

		[Test]
		[TestCase(0)]
		[TestCase(-1)]
		[TestCase(int.MinValue)]
		public static void Test_IncludeAttribute_Ctor_With_Invalid_Tags(int tagID)
		{
			//Should throw due to invalid tagID
			//See ctor of https://github.com/mgravell/protobuf-net/blob/e601b359c6ae56afc159754d29f5e7d0f05a01f5/protobuf-net/ProtoMemberAttribute.cs
			//attribute is based on protobuf-net specs.

			//assert
			Assert.Throws<ArgumentException>(() => new GladNetSerializationIncludeAttribute((GladNetPayloadIncludeIndex)tagID, typeof(GladNetSerializationIncludeAttribute)));
		}

		[Test] //just picking a random Type to use for testing.
		public static void Test_IncludeAttribute_Ctor_With_Valid_Tags([Range((int)GladNetPayloadIncludeIndex.Index1, (int)GladNetPayloadIncludeIndex.Index38)] int tagID)
		{
			//arrange
			GladNetSerializationIncludeAttribute gma = new GladNetSerializationIncludeAttribute((GladNetPayloadIncludeIndex)tagID, typeof(GladNetSerializationIncludeAttribute));

			//assert
			Assert.AreEqual(gma.TagID, tagID);
			Assert.AreEqual(gma.TypeToWireTo, typeof(GladNetSerializationIncludeAttribute));
		}


		[Test]
		public static void Test_IncludeAttribute_Ctor_With_Null_Type([Range((int)GladNetPayloadIncludeIndex.Index1, (int)GladNetPayloadIncludeIndex.Index38)] int tagID)
		{
			//assert
			Assert.Throws<ArgumentNullException>(() => new GladNetSerializationIncludeAttribute((GladNetPayloadIncludeIndex)tagID, null));
		}
	}
}

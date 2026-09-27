using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020002F2 RID: 754
	internal class ObjectReader
	{
		// Token: 0x06001771 RID: 6001 RVA: 0x00053CC0 File Offset: 0x00051EC0
		public ObjectReader(BinaryFormatter formatter)
		{
			this._surrogateSelector = formatter.SurrogateSelector;
			this._context = formatter.Context;
			this._binder = formatter.Binder;
			this._manager = new ObjectManager(this._surrogateSelector, this._context);
			this._filterLevel = formatter.FilterLevel;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00053D3C File Offset: 0x00051F3C
		public void ReadObjectGraph(BinaryReader reader, bool readHeaders, out object result, out Header[] headers)
		{
			BinaryElement elem = (BinaryElement)reader.ReadByte();
			this.ReadObjectGraph(elem, reader, readHeaders, out result, out headers);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00053D5C File Offset: 0x00051F5C
		public void ReadObjectGraph(BinaryElement elem, BinaryReader reader, bool readHeaders, out object result, out Header[] headers)
		{
			headers = null;
			bool flag = this.ReadNextObject(elem, reader);
			if (flag)
			{
				do
				{
					if (readHeaders && headers == null)
					{
						headers = (Header[])this.CurrentObject;
					}
					else if (this._rootObjectID == 0L)
					{
						this._rootObjectID = this._lastObjectID;
					}
				}
				while (this.ReadNextObject(reader));
			}
			result = this._manager.GetObject(this._rootObjectID);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00053DD4 File Offset: 0x00051FD4
		private bool ReadNextObject(BinaryElement element, BinaryReader reader)
		{
			if (element == BinaryElement.End)
			{
				this._manager.DoFixups();
				this._manager.RaiseDeserializationEvent();
				return false;
			}
			long num;
			SerializationInfo info;
			this.ReadObject(element, reader, out num, out this._lastObject, out info);
			if (num != 0L)
			{
				this.RegisterObject(num, this._lastObject, info, 0L, null, null);
				this._lastObjectID = num;
			}
			return true;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00053E34 File Offset: 0x00052034
		public bool ReadNextObject(BinaryReader reader)
		{
			BinaryElement binaryElement = (BinaryElement)reader.ReadByte();
			if (binaryElement == BinaryElement.End)
			{
				this._manager.DoFixups();
				this._manager.RaiseDeserializationEvent();
				return false;
			}
			long num;
			SerializationInfo info;
			this.ReadObject(binaryElement, reader, out num, out this._lastObject, out info);
			if (num != 0L)
			{
				this.RegisterObject(num, this._lastObject, info, 0L, null, null);
				this._lastObjectID = num;
			}
			return true;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001776 RID: 6006 RVA: 0x00053E9C File Offset: 0x0005209C
		public object CurrentObject
		{
			get
			{
				return this._lastObject;
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00053EA4 File Offset: 0x000520A4
		private void ReadObject(BinaryElement element, BinaryReader reader, out long objectId, out object value, out SerializationInfo info)
		{
			switch (element)
			{
			case BinaryElement.RefTypeObject:
				this.ReadRefTypeObjectInstance(reader, out objectId, out value, out info);
				return;
			case BinaryElement.UntypedRuntimeObject:
				this.ReadObjectInstance(reader, true, false, out objectId, out value, out info);
				return;
			case BinaryElement.UntypedExternalObject:
				this.ReadObjectInstance(reader, false, false, out objectId, out value, out info);
				return;
			case BinaryElement.RuntimeObject:
				this.ReadObjectInstance(reader, true, true, out objectId, out value, out info);
				return;
			case BinaryElement.ExternalObject:
				this.ReadObjectInstance(reader, false, true, out objectId, out value, out info);
				return;
			case BinaryElement.String:
				info = null;
				this.ReadStringIntance(reader, out objectId, out value);
				return;
			case BinaryElement.GenericArray:
				info = null;
				this.ReadGenericArray(reader, out objectId, out value);
				return;
			case BinaryElement.BoxedPrimitiveTypeValue:
				value = this.ReadBoxedPrimitiveTypeValue(reader);
				objectId = 0L;
				info = null;
				return;
			case BinaryElement.NullValue:
				value = null;
				objectId = 0L;
				info = null;
				return;
			case BinaryElement.Assembly:
				this.ReadAssembly(reader);
				this.ReadObject((BinaryElement)reader.ReadByte(), reader, out objectId, out value, out info);
				return;
			case BinaryElement.ArrayFiller8b:
				value = new ObjectReader.ArrayNullFiller((int)reader.ReadByte());
				objectId = 0L;
				info = null;
				return;
			case BinaryElement.ArrayFiller32b:
				value = new ObjectReader.ArrayNullFiller(reader.ReadInt32());
				objectId = 0L;
				info = null;
				return;
			case BinaryElement.ArrayOfPrimitiveType:
				this.ReadArrayOfPrimitiveType(reader, out objectId, out value);
				info = null;
				return;
			case BinaryElement.ArrayOfObject:
				this.ReadArrayOfObject(reader, out objectId, out value);
				info = null;
				return;
			case BinaryElement.ArrayOfString:
				this.ReadArrayOfString(reader, out objectId, out value);
				info = null;
				return;
			}
			throw new SerializationException("Unexpected binary element: " + (int)element);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00054054 File Offset: 0x00052254
		private void ReadAssembly(BinaryReader reader)
		{
			long num = (long)((ulong)reader.ReadUInt32());
			string value = reader.ReadString();
			this._registeredAssemblies[num] = value;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00054084 File Offset: 0x00052284
		private void ReadObjectInstance(BinaryReader reader, bool isRuntimeObject, bool hasTypeInfo, out long objectId, out object value, out SerializationInfo info)
		{
			objectId = (long)((ulong)reader.ReadUInt32());
			ObjectReader.TypeMetadata metadata = this.ReadTypeMetadata(reader, isRuntimeObject, hasTypeInfo);
			this.ReadObjectContent(reader, metadata, objectId, out value, out info);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000540B4 File Offset: 0x000522B4
		private void ReadRefTypeObjectInstance(BinaryReader reader, out long objectId, out object value, out SerializationInfo info)
		{
			objectId = (long)((ulong)reader.ReadUInt32());
			long objectID = (long)((ulong)reader.ReadUInt32());
			object @object = this._manager.GetObject(objectID);
			if (@object == null)
			{
				throw new SerializationException("Invalid binary format");
			}
			ObjectReader.TypeMetadata metadata = (ObjectReader.TypeMetadata)this._typeMetadataCache[@object.GetType()];
			this.ReadObjectContent(reader, metadata, objectId, out value, out info);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00054114 File Offset: 0x00052314
		private void ReadObjectContent(BinaryReader reader, ObjectReader.TypeMetadata metadata, long objectId, out object objectInstance, out SerializationInfo info)
		{
			if (this._filterLevel == TypeFilterLevel.Low)
			{
				objectInstance = FormatterServices.GetSafeUninitializedObject(metadata.Type);
			}
			else
			{
				objectInstance = FormatterServices.GetUninitializedObject(metadata.Type);
			}
			this._manager.RaiseOnDeserializingEvent(objectInstance);
			info = ((!metadata.NeedsSerializationInfo) ? null : new SerializationInfo(metadata.Type, new FormatterConverter()));
			if (metadata.MemberNames != null)
			{
				for (int i = 0; i < metadata.FieldCount; i++)
				{
					this.ReadValue(reader, objectInstance, objectId, info, metadata.MemberTypes[i], metadata.MemberNames[i], null, null);
				}
			}
			else
			{
				for (int j = 0; j < metadata.FieldCount; j++)
				{
					this.ReadValue(reader, objectInstance, objectId, info, metadata.MemberTypes[j], metadata.MemberInfos[j].Name, metadata.MemberInfos[j], null);
				}
			}
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0005420C File Offset: 0x0005240C
		private void RegisterObject(long objectId, object objectInstance, SerializationInfo info, long parentObjectId, MemberInfo parentObjectMemeber, int[] indices)
		{
			if (parentObjectId == 0L)
			{
				indices = null;
			}
			if (!objectInstance.GetType().IsValueType || parentObjectId == 0L)
			{
				this._manager.RegisterObject(objectInstance, objectId, info, 0L, null, null);
			}
			else
			{
				if (indices != null)
				{
					indices = (int[])indices.Clone();
				}
				this._manager.RegisterObject(objectInstance, objectId, info, parentObjectId, parentObjectMemeber, indices);
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0005427C File Offset: 0x0005247C
		private void ReadStringIntance(BinaryReader reader, out long objectId, out object value)
		{
			objectId = (long)((ulong)reader.ReadUInt32());
			value = reader.ReadString();
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00054290 File Offset: 0x00052490
		private void ReadGenericArray(BinaryReader reader, out long objectId, out object val)
		{
			objectId = (long)((ulong)reader.ReadUInt32());
			reader.ReadByte();
			int num = reader.ReadInt32();
			bool flag = false;
			int[] array = new int[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = reader.ReadInt32();
				if (array[i] == 0)
				{
					flag = true;
				}
			}
			TypeTag code = (TypeTag)reader.ReadByte();
			Type type = this.ReadType(reader, code);
			Array array2 = Array.CreateInstance(type, array);
			if (flag)
			{
				val = array2;
				return;
			}
			int[] array3 = new int[num];
			for (int j = num - 1; j >= 0; j--)
			{
				array3[j] = array2.GetLowerBound(j);
			}
			bool flag2 = false;
			while (!flag2)
			{
				this.ReadValue(reader, array2, objectId, null, type, null, null, array3);
				int k = array2.Rank - 1;
				while (k >= 0)
				{
					array3[k]++;
					if (array3[k] > array2.GetUpperBound(k))
					{
						if (k > 0)
						{
							array3[k] = array2.GetLowerBound(k);
							k--;
							continue;
						}
						flag2 = true;
					}
					break;
				}
			}
			val = array2;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000543C0 File Offset: 0x000525C0
		private object ReadBoxedPrimitiveTypeValue(BinaryReader reader)
		{
			Type type = this.ReadType(reader, TypeTag.PrimitiveType);
			return ObjectReader.ReadPrimitiveTypeValue(reader, type);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000543E0 File Offset: 0x000525E0
		private void ReadArrayOfPrimitiveType(BinaryReader reader, out long objectId, out object val)
		{
			objectId = (long)((ulong)reader.ReadUInt32());
			int num = reader.ReadInt32();
			Type type = this.ReadType(reader, TypeTag.PrimitiveType);
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
			{
				bool[] array = new bool[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = reader.ReadBoolean();
				}
				val = array;
				return;
			}
			case TypeCode.Char:
			{
				char[] array2 = new char[num];
				int num2;
				for (int j = 0; j < num; j += num2)
				{
					num2 = reader.Read(array2, j, num - j);
					if (num2 == 0)
					{
						break;
					}
				}
				val = array2;
				return;
			}
			case TypeCode.SByte:
			{
				sbyte[] array3 = new sbyte[num];
				if (num > 2)
				{
					this.BlockRead(reader, array3, 1);
				}
				else
				{
					for (int k = 0; k < num; k++)
					{
						array3[k] = reader.ReadSByte();
					}
				}
				val = array3;
				return;
			}
			case TypeCode.Byte:
			{
				byte[] array4 = new byte[num];
				int num3;
				for (int l = 0; l < num; l += num3)
				{
					num3 = reader.Read(array4, l, num - l);
					if (num3 == 0)
					{
						break;
					}
				}
				val = array4;
				return;
			}
			case TypeCode.Int16:
			{
				short[] array5 = new short[num];
				if (num > 2)
				{
					this.BlockRead(reader, array5, 2);
				}
				else
				{
					for (int m = 0; m < num; m++)
					{
						array5[m] = reader.ReadInt16();
					}
				}
				val = array5;
				return;
			}
			case TypeCode.UInt16:
			{
				ushort[] array6 = new ushort[num];
				if (num > 2)
				{
					this.BlockRead(reader, array6, 2);
				}
				else
				{
					for (int n = 0; n < num; n++)
					{
						array6[n] = reader.ReadUInt16();
					}
				}
				val = array6;
				return;
			}
			case TypeCode.Int32:
			{
				int[] array7 = new int[num];
				if (num > 2)
				{
					this.BlockRead(reader, array7, 4);
				}
				else
				{
					for (int num4 = 0; num4 < num; num4++)
					{
						array7[num4] = reader.ReadInt32();
					}
				}
				val = array7;
				return;
			}
			case TypeCode.UInt32:
			{
				uint[] array8 = new uint[num];
				if (num > 2)
				{
					this.BlockRead(reader, array8, 4);
				}
				else
				{
					for (int num5 = 0; num5 < num; num5++)
					{
						array8[num5] = reader.ReadUInt32();
					}
				}
				val = array8;
				return;
			}
			case TypeCode.Int64:
			{
				long[] array9 = new long[num];
				if (num > 2)
				{
					this.BlockRead(reader, array9, 8);
				}
				else
				{
					for (int num6 = 0; num6 < num; num6++)
					{
						array9[num6] = reader.ReadInt64();
					}
				}
				val = array9;
				return;
			}
			case TypeCode.UInt64:
			{
				ulong[] array10 = new ulong[num];
				if (num > 2)
				{
					this.BlockRead(reader, array10, 8);
				}
				else
				{
					for (int num7 = 0; num7 < num; num7++)
					{
						array10[num7] = reader.ReadUInt64();
					}
				}
				val = array10;
				return;
			}
			case TypeCode.Single:
			{
				float[] array11 = new float[num];
				if (num > 2)
				{
					this.BlockRead(reader, array11, 4);
				}
				else
				{
					for (int num8 = 0; num8 < num; num8++)
					{
						array11[num8] = reader.ReadSingle();
					}
				}
				val = array11;
				return;
			}
			case TypeCode.Double:
			{
				double[] array12 = new double[num];
				if (num > 2)
				{
					this.BlockRead(reader, array12, 8);
				}
				else
				{
					for (int num9 = 0; num9 < num; num9++)
					{
						array12[num9] = reader.ReadDouble();
					}
				}
				val = array12;
				return;
			}
			case TypeCode.Decimal:
			{
				decimal[] array13 = new decimal[num];
				for (int num10 = 0; num10 < num; num10++)
				{
					array13[num10] = reader.ReadDecimal();
				}
				val = array13;
				return;
			}
			case TypeCode.DateTime:
			{
				DateTime[] array14 = new DateTime[num];
				for (int num11 = 0; num11 < num; num11++)
				{
					array14[num11] = DateTime.FromBinary(reader.ReadInt64());
				}
				val = array14;
				return;
			}
			case TypeCode.String:
			{
				string[] array15 = new string[num];
				for (int num12 = 0; num12 < num; num12++)
				{
					array15[num12] = reader.ReadString();
				}
				val = array15;
				return;
			}
			}
			if (type != typeof(TimeSpan))
			{
				throw new NotSupportedException("Unsupported primitive type: " + type.FullName);
			}
			TimeSpan[] array16 = new TimeSpan[num];
			for (int num13 = 0; num13 < num; num13++)
			{
				array16[num13] = new TimeSpan(reader.ReadInt64());
			}
			val = array16;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000548B0 File Offset: 0x00052AB0
		private void BlockRead(BinaryReader reader, Array array, int dataSize)
		{
			int i = Buffer.ByteLength(array);
			if (this.arrayBuffer == null || (i > this.arrayBuffer.Length && this.arrayBuffer.Length != this.ArrayBufferLength))
			{
				this.arrayBuffer = new byte[(i > this.ArrayBufferLength) ? this.ArrayBufferLength : i];
			}
			int num = 0;
			while (i > 0)
			{
				int num2 = (i >= this.arrayBuffer.Length) ? this.arrayBuffer.Length : i;
				int num3 = 0;
				do
				{
					int num4 = reader.Read(this.arrayBuffer, num3, num2 - num3);
					if (num4 == 0)
					{
						break;
					}
					num3 += num4;
				}
				while (num3 < num2);
				IL_A6:
				if (!BitConverter.IsLittleEndian && dataSize > 1)
				{
					BinaryCommon.SwapBytes(this.arrayBuffer, num2, dataSize);
				}
				Buffer.BlockCopy(this.arrayBuffer, 0, array, num, num2);
				i -= num2;
				num += num2;
				continue;
				goto IL_A6;
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000549A0 File Offset: 0x00052BA0
		private void ReadArrayOfObject(BinaryReader reader, out long objectId, out object array)
		{
			this.ReadSimpleArray(reader, typeof(object), out objectId, out array);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x000549B8 File Offset: 0x00052BB8
		private void ReadArrayOfString(BinaryReader reader, out long objectId, out object array)
		{
			this.ReadSimpleArray(reader, typeof(string), out objectId, out array);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000549D0 File Offset: 0x00052BD0
		private void ReadSimpleArray(BinaryReader reader, Type elementType, out long objectId, out object val)
		{
			objectId = (long)((ulong)reader.ReadUInt32());
			int num = reader.ReadInt32();
			int[] array = new int[1];
			Array array2 = Array.CreateInstance(elementType, num);
			for (int i = 0; i < num; i++)
			{
				array[0] = i;
				this.ReadValue(reader, array2, objectId, null, elementType, null, null, array);
				i = array[0];
			}
			val = array2;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00054A2C File Offset: 0x00052C2C
		private ObjectReader.TypeMetadata ReadTypeMetadata(BinaryReader reader, bool isRuntimeObject, bool hasTypeInfo)
		{
			ObjectReader.TypeMetadata typeMetadata = new ObjectReader.TypeMetadata();
			string text = reader.ReadString();
			int num = reader.ReadInt32();
			Type[] array = new Type[num];
			string[] array2 = new string[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = reader.ReadString();
			}
			if (hasTypeInfo)
			{
				TypeTag[] array3 = new TypeTag[num];
				for (int j = 0; j < num; j++)
				{
					array3[j] = (TypeTag)reader.ReadByte();
				}
				for (int k = 0; k < num; k++)
				{
					array[k] = this.ReadType(reader, array3[k]);
				}
			}
			if (!isRuntimeObject)
			{
				long assemblyId = (long)((ulong)reader.ReadUInt32());
				typeMetadata.Type = this.GetDeserializationType(assemblyId, text);
			}
			else
			{
				typeMetadata.Type = Type.GetType(text, true);
			}
			typeMetadata.MemberTypes = array;
			typeMetadata.MemberNames = array2;
			typeMetadata.FieldCount = array2.Length;
			if (this._surrogateSelector != null)
			{
				ISurrogateSelector surrogateSelector;
				ISerializationSurrogate surrogate = this._surrogateSelector.GetSurrogate(typeMetadata.Type, this._context, out surrogateSelector);
				typeMetadata.NeedsSerializationInfo = (surrogate != null);
			}
			if (!typeMetadata.NeedsSerializationInfo)
			{
				if (!typeMetadata.Type.IsSerializable)
				{
					throw new SerializationException("Serializable objects must be marked with the Serializable attribute");
				}
				typeMetadata.NeedsSerializationInfo = typeof(ISerializable).IsAssignableFrom(typeMetadata.Type);
				if (!typeMetadata.NeedsSerializationInfo)
				{
					typeMetadata.MemberInfos = new MemberInfo[num];
					for (int l = 0; l < num; l++)
					{
						FieldInfo fieldInfo = null;
						string text2 = array2[l];
						int num2 = text2.IndexOf('+');
						if (num2 != -1)
						{
							string b = array2[l].Substring(0, num2);
							text2 = array2[l].Substring(num2 + 1);
							for (Type baseType = typeMetadata.Type.BaseType; baseType != null; baseType = baseType.BaseType)
							{
								if (baseType.Name == b)
								{
									fieldInfo = baseType.GetField(text2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
									break;
								}
							}
						}
						else
						{
							fieldInfo = typeMetadata.Type.GetField(text2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						}
						if (fieldInfo == null)
						{
							throw new SerializationException("Field \"" + array2[l] + "\" not found in class " + typeMetadata.Type.FullName);
						}
						typeMetadata.MemberInfos[l] = fieldInfo;
						if (!hasTypeInfo)
						{
							array[l] = fieldInfo.FieldType;
						}
					}
					typeMetadata.MemberNames = null;
				}
			}
			if (!this._typeMetadataCache.ContainsKey(typeMetadata.Type))
			{
				this._typeMetadataCache[typeMetadata.Type] = typeMetadata;
			}
			return typeMetadata;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00054CD0 File Offset: 0x00052ED0
		private void ReadValue(BinaryReader reader, object parentObject, long parentObjectId, SerializationInfo info, Type valueType, string fieldName, MemberInfo memberInfo, int[] indices)
		{
			object obj;
			if (BinaryCommon.IsPrimitive(valueType))
			{
				obj = ObjectReader.ReadPrimitiveTypeValue(reader, valueType);
				this.SetObjectValue(parentObject, fieldName, memberInfo, info, obj, valueType, indices);
				return;
			}
			BinaryElement binaryElement = (BinaryElement)reader.ReadByte();
			if (binaryElement == BinaryElement.ObjectReference)
			{
				long childObjectId = (long)((ulong)reader.ReadUInt32());
				this.RecordFixup(parentObjectId, childObjectId, parentObject, info, fieldName, memberInfo, indices);
				return;
			}
			long num;
			SerializationInfo info2;
			this.ReadObject(binaryElement, reader, out num, out obj, out info2);
			bool flag = false;
			if (num != 0L)
			{
				if (obj.GetType().IsValueType)
				{
					this.RecordFixup(parentObjectId, num, parentObject, info, fieldName, memberInfo, indices);
					flag = true;
				}
				if (info == null && !(parentObject is Array))
				{
					this.RegisterObject(num, obj, info2, parentObjectId, memberInfo, null);
				}
				else
				{
					this.RegisterObject(num, obj, info2, parentObjectId, null, indices);
				}
			}
			if (!flag)
			{
				this.SetObjectValue(parentObject, fieldName, memberInfo, info, obj, valueType, indices);
			}
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00054DB8 File Offset: 0x00052FB8
		private void SetObjectValue(object parentObject, string fieldName, MemberInfo memberInfo, SerializationInfo info, object value, Type valueType, int[] indices)
		{
			if (value is IObjectReference)
			{
				value = ((IObjectReference)value).GetRealObject(this._context);
			}
			if (parentObject is Array)
			{
				if (value is ObjectReader.ArrayNullFiller)
				{
					int nullCount = ((ObjectReader.ArrayNullFiller)value).NullCount;
					indices[0] += nullCount - 1;
				}
				else
				{
					((Array)parentObject).SetValue(value, indices);
				}
			}
			else if (info != null)
			{
				info.AddValue(fieldName, value, valueType);
			}
			else if (memberInfo is FieldInfo)
			{
				((FieldInfo)memberInfo).SetValue(parentObject, value);
			}
			else
			{
				((PropertyInfo)memberInfo).SetValue(parentObject, value, null);
			}
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00054E78 File Offset: 0x00053078
		private void RecordFixup(long parentObjectId, long childObjectId, object parentObject, SerializationInfo info, string fieldName, MemberInfo memberInfo, int[] indices)
		{
			if (info != null)
			{
				this._manager.RecordDelayedFixup(parentObjectId, fieldName, childObjectId);
			}
			else if (parentObject is Array)
			{
				if (indices.Length == 1)
				{
					this._manager.RecordArrayElementFixup(parentObjectId, indices[0], childObjectId);
				}
				else
				{
					this._manager.RecordArrayElementFixup(parentObjectId, (int[])indices.Clone(), childObjectId);
				}
			}
			else
			{
				this._manager.RecordFixup(parentObjectId, memberInfo, childObjectId);
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00054EF8 File Offset: 0x000530F8
		private Type GetDeserializationType(long assemblyId, string className)
		{
			string text = (string)this._registeredAssemblies[assemblyId];
			Type type;
			if (this._binder != null)
			{
				type = this._binder.BindToType(text, className);
				if (type != null)
				{
					return type;
				}
			}
			Assembly assembly = Assembly.Load(text);
			type = assembly.GetType(className, true);
			if (type != null)
			{
				return type;
			}
			throw new SerializationException("Couldn't find type '" + className + "'.");
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00054F6C File Offset: 0x0005316C
		public Type ReadType(BinaryReader reader, TypeTag code)
		{
			switch (code)
			{
			case TypeTag.PrimitiveType:
				return BinaryCommon.GetTypeFromCode((int)reader.ReadByte());
			case TypeTag.String:
				return typeof(string);
			case TypeTag.ObjectType:
				return typeof(object);
			case TypeTag.RuntimeType:
			{
				string text = reader.ReadString();
				if (this._context.State == StreamingContextStates.Remoting)
				{
					if (text == "System.RuntimeType")
					{
						return typeof(MonoType);
					}
					if (text == "System.RuntimeType[]")
					{
						return typeof(MonoType[]);
					}
				}
				Type type = Type.GetType(text);
				if (type != null)
				{
					return type;
				}
				throw new SerializationException(string.Format("Could not find type '{0}'.", text));
			}
			case TypeTag.GenericType:
			{
				string className = reader.ReadString();
				long assemblyId = (long)((ulong)reader.ReadUInt32());
				return this.GetDeserializationType(assemblyId, className);
			}
			case TypeTag.ArrayOfObject:
				return typeof(object[]);
			case TypeTag.ArrayOfString:
				return typeof(string[]);
			case TypeTag.ArrayOfPrimitiveType:
			{
				Type typeFromCode = BinaryCommon.GetTypeFromCode((int)reader.ReadByte());
				return Type.GetType(typeFromCode.FullName + "[]");
			}
			default:
				throw new NotSupportedException("Unknow type tag");
			}
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00055098 File Offset: 0x00053298
		public static object ReadPrimitiveTypeValue(BinaryReader reader, Type type)
		{
			if (type == null)
			{
				return null;
			}
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				return reader.ReadBoolean();
			case TypeCode.Char:
				return reader.ReadChar();
			case TypeCode.SByte:
				return reader.ReadSByte();
			case TypeCode.Byte:
				return reader.ReadByte();
			case TypeCode.Int16:
				return reader.ReadInt16();
			case TypeCode.UInt16:
				return reader.ReadUInt16();
			case TypeCode.Int32:
				return reader.ReadInt32();
			case TypeCode.UInt32:
				return reader.ReadUInt32();
			case TypeCode.Int64:
				return reader.ReadInt64();
			case TypeCode.UInt64:
				return reader.ReadUInt64();
			case TypeCode.Single:
				return reader.ReadSingle();
			case TypeCode.Double:
				return reader.ReadDouble();
			case TypeCode.Decimal:
				return decimal.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
			case TypeCode.DateTime:
				return DateTime.FromBinary(reader.ReadInt64());
			case TypeCode.String:
				return reader.ReadString();
			}
			if (type == typeof(TimeSpan))
			{
				return new TimeSpan(reader.ReadInt64());
			}
			throw new NotSupportedException("Unsupported primitive type: " + type.FullName);
		}

		// Token: 0x04000C1C RID: 3100
		private ISurrogateSelector _surrogateSelector;

		// Token: 0x04000C1D RID: 3101
		private StreamingContext _context;

		// Token: 0x04000C1E RID: 3102
		private SerializationBinder _binder;

		// Token: 0x04000C1F RID: 3103
		private TypeFilterLevel _filterLevel;

		// Token: 0x04000C20 RID: 3104
		private ObjectManager _manager;

		// Token: 0x04000C21 RID: 3105
		private Hashtable _registeredAssemblies = new Hashtable();

		// Token: 0x04000C22 RID: 3106
		private Hashtable _typeMetadataCache = new Hashtable();

		// Token: 0x04000C23 RID: 3107
		private object _lastObject;

		// Token: 0x04000C24 RID: 3108
		private long _lastObjectID;

		// Token: 0x04000C25 RID: 3109
		private long _rootObjectID;

		// Token: 0x04000C26 RID: 3110
		private byte[] arrayBuffer;

		// Token: 0x04000C27 RID: 3111
		private int ArrayBufferLength = 4096;

		// Token: 0x020002F3 RID: 755
		private class ArrayNullFiller
		{
			// Token: 0x0600178C RID: 6028 RVA: 0x000551F8 File Offset: 0x000533F8
			public ArrayNullFiller(int count)
			{
				this.NullCount = count;
			}

			// Token: 0x04000C28 RID: 3112
			public int NullCount;
		}

		// Token: 0x020002F4 RID: 756
		private class TypeMetadata
		{
			// Token: 0x04000C29 RID: 3113
			public Type Type;

			// Token: 0x04000C2A RID: 3114
			public Type[] MemberTypes;

			// Token: 0x04000C2B RID: 3115
			public string[] MemberNames;

			// Token: 0x04000C2C RID: 3116
			public MemberInfo[] MemberInfos;

			// Token: 0x04000C2D RID: 3117
			public int FieldCount;

			// Token: 0x04000C2E RID: 3118
			public bool NeedsSerializationInfo;
		}
	}
}

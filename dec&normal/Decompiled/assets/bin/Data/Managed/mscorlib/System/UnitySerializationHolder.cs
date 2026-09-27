using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020003D9 RID: 985
	[Serializable]
	internal class UnitySerializationHolder : IObjectReference, ISerializable
	{
		// Token: 0x06001E8E RID: 7822 RVA: 0x00071E74 File Offset: 0x00070074
		private UnitySerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			this._data = info.GetString("Data");
			this._unityType = (UnitySerializationHolder.UnityType)info.GetInt32("UnityType");
			this._assemblyName = info.GetString("AssemblyName");
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00071EB0 File Offset: 0x000700B0
		public static void GetTypeData(Type instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", instance.FullName);
			info.AddValue("UnityType", 4);
			info.AddValue("AssemblyName", instance.Assembly.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00071F00 File Offset: 0x00070100
		public static void GetDBNullData(DBNull instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", null);
			info.AddValue("UnityType", 2);
			info.AddValue("AssemblyName", instance.GetType().Assembly.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00071F50 File Offset: 0x00070150
		public static void GetAssemblyData(Assembly instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", instance.FullName);
			info.AddValue("UnityType", 6);
			info.AddValue("AssemblyName", instance.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00071F90 File Offset: 0x00070190
		public static void GetModuleData(Module instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", instance.ScopeName);
			info.AddValue("UnityType", 5);
			info.AddValue("AssemblyName", instance.Assembly.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00071FE0 File Offset: 0x000701E0
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00071FE8 File Offset: 0x000701E8
		public virtual object GetRealObject(StreamingContext context)
		{
			switch (this._unityType)
			{
			case UnitySerializationHolder.UnityType.DBNull:
				return DBNull.Value;
			case UnitySerializationHolder.UnityType.Type:
			{
				Assembly assembly = Assembly.Load(this._assemblyName);
				return assembly.GetType(this._data);
			}
			case UnitySerializationHolder.UnityType.Module:
			{
				Assembly assembly2 = Assembly.Load(this._assemblyName);
				return assembly2.GetModule(this._data);
			}
			case UnitySerializationHolder.UnityType.Assembly:
				return Assembly.Load(this._data);
			}
			throw new NotSupportedException(Locale.GetText("UnitySerializationHolder does not support this type."));
		}

		// Token: 0x04000FB3 RID: 4019
		private string _data;

		// Token: 0x04000FB4 RID: 4020
		private UnitySerializationHolder.UnityType _unityType;

		// Token: 0x04000FB5 RID: 4021
		private string _assemblyName;

		// Token: 0x020003DA RID: 986
		private enum UnityType : byte
		{
			// Token: 0x04000FB7 RID: 4023
			DBNull = 2,
			// Token: 0x04000FB8 RID: 4024
			Type = 4,
			// Token: 0x04000FB9 RID: 4025
			Module,
			// Token: 0x04000FBA RID: 4026
			Assembly
		}
	}
}

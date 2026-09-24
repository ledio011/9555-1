using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	// Token: 0x020002CF RID: 719
	[ComVisible(true)]
	[Serializable]
	public class ObjRef : IObjectReference, ISerializable
	{
		// Token: 0x06001672 RID: 5746 RVA: 0x0004E720 File Offset: 0x0004C920
		public ObjRef()
		{
			this.UpdateChannelInfo();
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0004E730 File Offset: 0x0004C930
		internal ObjRef(string typeName, string uri, IChannelInfo cinfo)
		{
			this.uri = uri;
			this.channel_info = cinfo;
			this.typeInfo = new TypeInfo(Type.GetType(typeName, true));
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0004E758 File Offset: 0x0004C958
		internal ObjRef(Type type, string url, object remoteChannelData)
		{
			this.uri = url;
			this.typeInfo = new TypeInfo(type);
			if (remoteChannelData != null)
			{
				this.channel_info = new ChannelInfo(remoteChannelData);
			}
			this.flags |= ObjRef.WellKnowObjectRef;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0004E798 File Offset: 0x0004C998
		protected ObjRef(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				switch (name)
				{
				case "uri":
					this.uri = (string)enumerator.Value;
					continue;
				case "typeInfo":
					this.typeInfo = (IRemotingTypeInfo)enumerator.Value;
					continue;
				case "channelInfo":
					this.channel_info = (IChannelInfo)enumerator.Value;
					continue;
				case "envoyInfo":
					this.envoyInfo = (IEnvoyInfo)enumerator.Value;
					continue;
				case "fIsMarshalled":
				{
					object value = enumerator.Value;
					int num2;
					if (value is string)
					{
						num2 = ((IConvertible)value).ToInt32(null);
					}
					else
					{
						num2 = (int)value;
					}
					if (num2 == 0)
					{
						flag = false;
					}
					continue;
				}
				case "objrefFlags":
					this.flags = Convert.ToInt32(enumerator.Value);
					continue;
				}
				throw new NotSupportedException();
			}
			if (flag)
			{
				this.flags |= ObjRef.MarshalledObjectRef;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0004E948 File Offset: 0x0004CB48
		internal bool IsReferenceToWellKnow
		{
			get
			{
				return (this.flags & ObjRef.WellKnowObjectRef) > 0;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0004E95C File Offset: 0x0004CB5C
		public virtual IChannelInfo ChannelInfo
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.channel_info;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0004E964 File Offset: 0x0004CB64
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0004E96C File Offset: 0x0004CB6C
		public virtual IEnvoyInfo EnvoyInfo
		{
			get
			{
				return this.envoyInfo;
			}
			set
			{
				this.envoyInfo = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0004E978 File Offset: 0x0004CB78
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x0004E980 File Offset: 0x0004CB80
		public virtual IRemotingTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				this.typeInfo = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0004E98C File Offset: 0x0004CB8C
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x0004E994 File Offset: 0x0004CB94
		public virtual string URI
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0004E9A0 File Offset: 0x0004CBA0
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(base.GetType());
			info.AddValue("uri", this.uri);
			info.AddValue("typeInfo", this.typeInfo, typeof(IRemotingTypeInfo));
			info.AddValue("envoyInfo", this.envoyInfo, typeof(IEnvoyInfo));
			info.AddValue("channelInfo", this.channel_info, typeof(IChannelInfo));
			info.AddValue("objrefFlags", this.flags);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x0004EA2C File Offset: 0x0004CC2C
		public virtual object GetRealObject(StreamingContext context)
		{
			if ((this.flags & ObjRef.MarshalledObjectRef) > 0)
			{
				return RemotingServices.Unmarshal(this);
			}
			return this;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x0004EA48 File Offset: 0x0004CC48
		internal void UpdateChannelInfo()
		{
			this.channel_info = new ChannelInfo();
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0004EA58 File Offset: 0x0004CC58
		internal Type ServerType
		{
			get
			{
				if (this._serverType == null)
				{
					this._serverType = Type.GetType(this.typeInfo.TypeName);
				}
				return this._serverType;
			}
		}

		// Token: 0x04000B93 RID: 2963
		private IChannelInfo channel_info;

		// Token: 0x04000B94 RID: 2964
		private string uri;

		// Token: 0x04000B95 RID: 2965
		private IRemotingTypeInfo typeInfo;

		// Token: 0x04000B96 RID: 2966
		private IEnvoyInfo envoyInfo;

		// Token: 0x04000B97 RID: 2967
		private int flags;

		// Token: 0x04000B98 RID: 2968
		private Type _serverType;

		// Token: 0x04000B99 RID: 2969
		private static int MarshalledObjectRef = 1;

		// Token: 0x04000B9A RID: 2970
		private static int WellKnowObjectRef = 2;
	}
}

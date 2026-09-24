using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	internal sealed class MonoEvent : EventInfo, ISerializable
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x00042C8C File Offset: 0x00040E8C
		public override EventAttributes Attributes
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).attrs;
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00042CA8 File Offset: 0x00040EA8
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.add_method != null && eventInfo.add_method.IsPublic))
			{
				return eventInfo.add_method;
			}
			return null;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00042CE8 File Offset: 0x00040EE8
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.remove_method != null && eventInfo.remove_method.IsPublic))
			{
				return eventInfo.remove_method;
			}
			return null;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00042D28 File Offset: 0x00040F28
		public override Type DeclaringType
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).declaring_type;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x00042D44 File Offset: 0x00040F44
		public override Type ReflectedType
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).reflected_type;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00042D60 File Offset: 0x00040F60
		public override string Name
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).name;
			}
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00042D7C File Offset: 0x00040F7C
		public override string ToString()
		{
			return this.EventHandlerType + " " + this.Name;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00042D94 File Offset: 0x00040F94
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00042DA0 File Offset: 0x00040FA0
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00042DAC File Offset: 0x00040FAC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00042DB8 File Offset: 0x00040FB8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Event);
		}

		// Token: 0x04000902 RID: 2306
		private IntPtr klass;

		// Token: 0x04000903 RID: 2307
		private IntPtr handle;
	}
}

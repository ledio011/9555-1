using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020001C4 RID: 452
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EventInfo))]
	[ComVisible(true)]
	[Serializable]
	public abstract class EventInfo : MemberInfo, _EventInfo
	{
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600110D RID: 4365
		public abstract EventAttributes Attributes { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00042084 File Offset: 0x00040284
		public Type EventHandlerType
		{
			get
			{
				MethodInfo addMethod = this.GetAddMethod(true);
				ParameterInfo[] parameters = addMethod.GetParameters();
				if (parameters.Length > 0)
				{
					return parameters[0].ParameterType;
				}
				return null;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x000420B8 File Offset: 0x000402B8
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x06001110 RID: 4368
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x06001111 RID: 4369
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x04000894 RID: 2196
		private EventInfo.AddEventAdapter cached_add_event;

		// Token: 0x020001C5 RID: 453
		// (Invoke) Token: 0x06001113 RID: 4371
		private delegate void AddEventAdapter(object _this, Delegate dele);
	}
}

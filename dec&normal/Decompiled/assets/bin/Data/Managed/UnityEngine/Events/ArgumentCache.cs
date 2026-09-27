using System;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	internal class ArgumentCache
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00009408 File Offset: 0x00007608
		public Object unityObjectArgument
		{
			get
			{
				return this.m_ObjectArgument;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00009410 File Offset: 0x00007610
		public string unityObjectArgumentAssemblyTypeName
		{
			get
			{
				return this.m_ObjectArgumentAssemblyTypeName;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00009418 File Offset: 0x00007618
		public int intArgument
		{
			get
			{
				return this.m_IntArgument;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00009420 File Offset: 0x00007620
		public float floatArgument
		{
			get
			{
				return this.m_FloatArgument;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00009428 File Offset: 0x00007628
		public string stringArgument
		{
			get
			{
				return this.m_StringArgument;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00009430 File Offset: 0x00007630
		public bool boolArgument
		{
			get
			{
				return this.m_BoolArgument;
			}
		}

		// Token: 0x040000A0 RID: 160
		[SerializeField]
		[FormerlySerializedAs("objectArgument")]
		private Object m_ObjectArgument;

		// Token: 0x040000A1 RID: 161
		[SerializeField]
		[FormerlySerializedAs("objectArgumentAssemblyTypeName")]
		private string m_ObjectArgumentAssemblyTypeName;

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		[FormerlySerializedAs("intArgument")]
		private int m_IntArgument;

		// Token: 0x040000A3 RID: 163
		[FormerlySerializedAs("floatArgument")]
		[SerializeField]
		private float m_FloatArgument;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		[FormerlySerializedAs("stringArgument")]
		private string m_StringArgument;

		// Token: 0x040000A5 RID: 165
		[SerializeField]
		private bool m_BoolArgument;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000100 RID: 256
	public sealed class Shader : Object
	{
		// Token: 0x06000989 RID: 2441
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern Shader Find(string name);

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600098A RID: 2442
		public extern bool isSupported { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700021E RID: 542
		// (set) Token: 0x0600098B RID: 2443
		public static extern int globalMaximumLOD { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x0600098C RID: 2444 RVA: 0x00015448 File Offset: 0x00013648
		public static void SetGlobalColor(string propertyName, Color color)
		{
			Shader.SetGlobalColor(Shader.PropertyToID(propertyName), color);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00015458 File Offset: 0x00013658
		public static void SetGlobalColor(int nameID, Color color)
		{
			Shader.INTERNAL_CALL_SetGlobalColor(nameID, ref color);
		}

		// Token: 0x0600098E RID: 2446
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_CALL_SetGlobalColor(int nameID, ref Color color);

		// Token: 0x0600098F RID: 2447 RVA: 0x00015464 File Offset: 0x00013664
		public static void SetGlobalFloat(string propertyName, float value)
		{
			Shader.SetGlobalFloat(Shader.PropertyToID(propertyName), value);
		}

		// Token: 0x06000990 RID: 2448
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void SetGlobalFloat(int nameID, float value);

		// Token: 0x06000991 RID: 2449
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern int PropertyToID(string name);
	}
}

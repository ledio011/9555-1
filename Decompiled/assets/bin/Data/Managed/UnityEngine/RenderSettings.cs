using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000EA RID: 234
	public sealed class RenderSettings : Object
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00014AD4 File Offset: 0x00012CD4
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00014AEC File Offset: 0x00012CEC
		public static Color ambientLight
		{
			get
			{
				Color result;
				RenderSettings.INTERNAL_get_ambientLight(out result);
				return result;
			}
			set
			{
				RenderSettings.INTERNAL_set_ambientLight(ref value);
			}
		}

		// Token: 0x0600092F RID: 2351
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_get_ambientLight(out Color value);

		// Token: 0x06000930 RID: 2352
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern void INTERNAL_set_ambientLight(ref Color value);

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000931 RID: 2353
		// (set) Token: 0x06000932 RID: 2354
		public static extern Material skybox { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}

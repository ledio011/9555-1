using System;
using System.Runtime.CompilerServices;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200001C RID: 28
	public sealed class AssetBundle : Object
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600023C RID: 572
		public extern Object mainAsset { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x0600023D RID: 573
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[MethodImpl(4096)]
		public extern Object Load(string name, Type type);

		// Token: 0x0600023E RID: 574
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Object[] LoadAll(Type type);

		// Token: 0x0600023F RID: 575 RVA: 0x00006F00 File Offset: 0x00005100
		public Object[] LoadAll()
		{
			return this.LoadAll(typeof(Object));
		}

		// Token: 0x06000240 RID: 576
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Unload(bool unloadAllLoadedObjects);
	}
}

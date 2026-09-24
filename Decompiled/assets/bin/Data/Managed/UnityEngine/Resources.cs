using System;
using System.Runtime.CompilerServices;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000F0 RID: 240
	public sealed class Resources
	{
		// Token: 0x06000950 RID: 2384
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x06000951 RID: 2385 RVA: 0x00014C10 File Offset: 0x00012E10
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x06000952 RID: 2386
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[MethodImpl(4096)]
		public static extern Object Load(string path, Type systemTypeInstance);

		// Token: 0x06000953 RID: 2387
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern AsyncOperation UnloadUnusedAssets();
	}
}

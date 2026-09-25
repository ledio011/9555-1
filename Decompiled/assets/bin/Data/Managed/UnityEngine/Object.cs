using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000C5 RID: 197
	[StructLayout(0)]
	public class Object
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x00012FF4 File Offset: 0x000111F4
		public override bool Equals(object o)
		{
			return Object.CompareBaseObjects(this, o as Object);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00013004 File Offset: 0x00011204
		public override int GetHashCode()
		{
			return this.GetInstanceID();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001300C File Offset: 0x0001120C
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			return Object.CompareBaseObjectsInternal(lhs, rhs);
		}

		// Token: 0x0600080D RID: 2061
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern bool CompareBaseObjectsInternal([Writable] Object lhs, [Writable] Object rhs);

		// Token: 0x0600080E RID: 2062 RVA: 0x00013018 File Offset: 0x00011218
		[NotRenamed]
		public int GetInstanceID()
		{
			return this.m_UnityRuntimeReferenceData.instanceID;
		}

		// Token: 0x0600080F RID: 2063
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Object Internal_CloneSingle(Object data);

		// Token: 0x06000810 RID: 2064 RVA: 0x00013028 File Offset: 0x00011228
		private static Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot)
		{
			return Object.INTERNAL_CALL_Internal_InstantiateSingle(data, ref pos, ref rot);
		}

		// Token: 0x06000811 RID: 2065
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private static extern Object INTERNAL_CALL_Internal_InstantiateSingle(Object data, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x06000812 RID: 2066 RVA: 0x00013034 File Offset: 0x00011234
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The prefab you want to instantiate is null.");
			return Object.Internal_InstantiateSingle(original, position, rotation);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001304C File Offset: 0x0001124C
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The thing you want to instantiate is null.");
			return Object.Internal_CloneSingle(original);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00013060 File Offset: 0x00011260
		private static void CheckNullArgument(object arg, string message)
		{
			if (arg == null)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x06000815 RID: 2069
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x06000816 RID: 2070 RVA: 0x00013070 File Offset: 0x00011270
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float t = 0f;
			Object.Destroy(obj, t);
		}

		// Token: 0x06000817 RID: 2071
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x06000818 RID: 2072 RVA: 0x0001308C File Offset: 0x0001128C
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool allowDestroyingAssets = false;
			Object.DestroyImmediate(obj, allowDestroyingAssets);
		}

		// Token: 0x06000819 RID: 2073
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public static extern Object[] FindObjectsOfType(Type type);

		// Token: 0x0600081A RID: 2074 RVA: 0x000130A4 File Offset: 0x000112A4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type);
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600081B RID: 2075
		// (set) Token: 0x0600081C RID: 2076
		public extern string name { [WrapperlessIcall] [MethodImpl(4096)] get; [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x0600081D RID: 2077
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void DontDestroyOnLoad(Object target);

		// Token: 0x170001B7 RID: 439
		// (set) Token: 0x0600081E RID: 2078
		public extern HideFlags hideFlags { [WrapperlessIcall] [MethodImpl(4096)] set; }

		// Token: 0x0600081F RID: 2079
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public override extern string ToString();

		// Token: 0x06000820 RID: 2080 RVA: 0x000130C8 File Offset: 0x000112C8
		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000130D4 File Offset: 0x000112D4
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000130E0 File Offset: 0x000112E0
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x04000314 RID: 788
		private ReferenceData m_UnityRuntimeReferenceData;
	}
}

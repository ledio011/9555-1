using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000BA RID: 186
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x060007D6 RID: 2006
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern MonoBehaviour();

		// Token: 0x060007D7 RID: 2007
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void Invoke(string methodName, float time);

		// Token: 0x060007D8 RID: 2008
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void InvokeRepeating(string methodName, float time, float repeatRate);

		// Token: 0x060007D9 RID: 2009 RVA: 0x00012E9C File Offset: 0x0001109C
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return this.StartCoroutine_Auto(routine);
		}

		// Token: 0x060007DA RID: 2010
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Coroutine StartCoroutine_Auto(IEnumerator routine);

		// Token: 0x060007DB RID: 2011
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value);

		// Token: 0x060007DC RID: 2012 RVA: 0x00012EA8 File Offset: 0x000110A8
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object value = null;
			return this.StartCoroutine(methodName, value);
		}

		// Token: 0x060007DD RID: 2013
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void StopCoroutine(string methodName);

		// Token: 0x060007DE RID: 2014 RVA: 0x00012EC0 File Offset: 0x000110C0
		public void StopCoroutine(IEnumerator routine)
		{
			this.StopCoroutineViaEnumerator_Auto(routine);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00012ECC File Offset: 0x000110CC
		public void StopCoroutine(Coroutine routine)
		{
			this.StopCoroutine_Auto(routine);
		}

		// Token: 0x060007E0 RID: 2016
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern void StopCoroutineViaEnumerator_Auto(IEnumerator routine);

		// Token: 0x060007E1 RID: 2017
		[WrapperlessIcall]
		[MethodImpl(4096)]
		internal extern void StopCoroutine_Auto(Coroutine routine);

		// Token: 0x060007E2 RID: 2018
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void StopAllCoroutines();

		// Token: 0x060007E3 RID: 2019 RVA: 0x00012ED8 File Offset: 0x000110D8
		public static void print(object message)
		{
			Debug.Log(message);
		}

		// Token: 0x170001AB RID: 427
		// (set) Token: 0x060007E4 RID: 2020
		public extern bool useGUILayout { [WrapperlessIcall] [MethodImpl(4096)] set; }
	}
}

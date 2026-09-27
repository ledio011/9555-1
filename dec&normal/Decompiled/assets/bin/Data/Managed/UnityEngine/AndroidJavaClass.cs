using System;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	public class AndroidJavaClass : AndroidJavaObject
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00003474 File Offset: 0x00001674
		internal AndroidJavaClass(IntPtr jclass)
		{
			if (jclass == IntPtr.Zero)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = AndroidJNI.NewGlobalRef(jclass);
			this.m_jobject = IntPtr.Zero;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000034B0 File Offset: 0x000016B0
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000034C0 File Offset: 0x000016C0
		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			using (AndroidJavaObject androidJavaObject = AndroidJavaObject.FindClass(className))
			{
				this.m_jclass = AndroidJNI.NewGlobalRef(androidJavaObject.GetRawObject());
				this.m_jobject = IntPtr.Zero;
			}
		}
	}
}

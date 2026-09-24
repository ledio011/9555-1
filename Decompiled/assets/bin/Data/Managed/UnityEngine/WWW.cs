using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine
{
	// Token: 0x0200013E RID: 318
	public sealed class WWW : IDisposable
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x0001B0C4 File Offset: 0x000192C4
		public WWW(string url)
		{
			this.InitWWW(url, null, null);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001B0D8 File Offset: 0x000192D8
		public WWW(string url, WWWForm form)
		{
			string[] iHeaders = WWW.FlattenedHeadersFrom(form.headers);
			this.InitWWW(url, form.data, iHeaders);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001B108 File Offset: 0x00019308
		private static string[] FlattenedHeadersFrom(Hashtable headers)
		{
			if (headers == null)
			{
				return null;
			}
			string[] array = new string[headers.Count * 2];
			int num = 0;
			foreach (object obj in headers)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				array[num++] = dictionaryEntry.Key.ToString();
				array[num++] = dictionaryEntry.Value.ToString();
			}
			return array;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001B1A4 File Offset: 0x000193A4
		public void Dispose()
		{
			this.DestroyWWW(true);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0001B1B0 File Offset: 0x000193B0
		~WWW()
		{
			this.DestroyWWW(false);
		}

		// Token: 0x06000B80 RID: 2944
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern void DestroyWWW(bool cancel);

		// Token: 0x06000B81 RID: 2945
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public extern void InitWWW(string url, byte[] postData, string[] iHeaders);

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001B1E0 File Offset: 0x000193E0
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.ASCII;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B83 RID: 2947
		public extern byte[] bytes { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B84 RID: 2948
		public extern int size { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B85 RID: 2949
		public extern string error { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x06000B86 RID: 2950
		[WrapperlessIcall]
		[MethodImpl(4096)]
		private extern Texture2D GetTexture(bool markNonReadable);

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0001B1E8 File Offset: 0x000193E8
		public Texture2D texture
		{
			get
			{
				return this.GetTexture(false);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B88 RID: 2952
		public extern bool isDone { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B89 RID: 2953
		public extern float progress { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B8A RID: 2954
		public extern AssetBundle assetBundle { [WrapperlessIcall] [MethodImpl(4096)] get; }

		// Token: 0x040004FE RID: 1278
		internal IntPtr m_Ptr;
	}
}

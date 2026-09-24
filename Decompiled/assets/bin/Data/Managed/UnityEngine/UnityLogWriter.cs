using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine
{
	// Token: 0x02000131 RID: 305
	internal sealed class UnityLogWriter : TextWriter
	{
		// Token: 0x06000B13 RID: 2835
		[WrapperlessIcall]
		[MethodImpl(4096)]
		public static extern void WriteStringToUnityLog(string s);

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001A044 File Offset: 0x00018244
		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0001A050 File Offset: 0x00018250
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001A058 File Offset: 0x00018258
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001A068 File Offset: 0x00018268
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}
	}
}

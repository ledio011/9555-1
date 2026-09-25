using System;
using System.IO;
using System.Text;

namespace System
{
	// Token: 0x020000BF RID: 191
	public static class Console
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x00019CF8 File Offset: 0x00017EF8
		static Console()
		{
			if (Environment.IsRunningOnWindows)
			{
				Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
			}
			else
			{
				int num = 0;
				Encoding.InternalCodePage(ref num);
				if (num != -1 && ((num & 268435455) == 3 || (num & 268435456) != 0))
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.UTF8Unmarked);
				}
				else
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
				}
			}
			Console.SetEncodings(Console.inputEncoding, Console.outputEncoding);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00019D84 File Offset: 0x00017F84
		private static void SetEncodings(Encoding inputEncoding, Encoding outputEncoding)
		{
			Console.stderr = new UnexceptionalStreamWriter(Console.OpenStandardError(0), outputEncoding);
			((StreamWriter)Console.stderr).AutoFlush = true;
			Console.stderr = TextWriter.Synchronized(Console.stderr, true);
			Console.stdout = new UnexceptionalStreamWriter(Console.OpenStandardOutput(0), outputEncoding);
			((StreamWriter)Console.stdout).AutoFlush = true;
			Console.stdout = TextWriter.Synchronized(Console.stdout, true);
			Console.stdin = new UnexceptionalStreamReader(Console.OpenStandardInput(0), inputEncoding);
			Console.stdin = TextReader.Synchronized(Console.stdin);
			GC.SuppressFinalize(Console.stdout);
			GC.SuppressFinalize(Console.stderr);
			GC.SuppressFinalize(Console.stdin);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00019E34 File Offset: 0x00018034
		public static TextWriter Error
		{
			get
			{
				return Console.stderr;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00019E3C File Offset: 0x0001803C
		public static TextWriter Out
		{
			get
			{
				return Console.stdout;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00019E44 File Offset: 0x00018044
		private static Stream Open(IntPtr handle, FileAccess access, int bufferSize)
		{
			Stream result;
			try
			{
				result = new FileStream(handle, access, false, bufferSize, false, bufferSize == 0);
			}
			catch (IOException)
			{
				result = new NullStream();
			}
			return result;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00019E8C File Offset: 0x0001808C
		public static Stream OpenStandardError(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleError, FileAccess.Write, bufferSize);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00019E9C File Offset: 0x0001809C
		public static Stream OpenStandardInput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleInput, FileAccess.Read, bufferSize);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00019EAC File Offset: 0x000180AC
		public static Stream OpenStandardOutput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleOutput, FileAccess.Write, bufferSize);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00019EBC File Offset: 0x000180BC
		public static void SetOut(TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException("newOut");
			}
			Console.stdout = newOut;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00019ED8 File Offset: 0x000180D8
		public static void WriteLine(string value)
		{
			Console.stdout.WriteLine(value);
		}

		// Token: 0x04000268 RID: 616
		internal static TextWriter stdout;

		// Token: 0x04000269 RID: 617
		private static TextWriter stderr;

		// Token: 0x0400026A RID: 618
		private static TextReader stdin;

		// Token: 0x0400026B RID: 619
		private static Encoding inputEncoding;

		// Token: 0x0400026C RID: 620
		private static Encoding outputEncoding;
	}
}

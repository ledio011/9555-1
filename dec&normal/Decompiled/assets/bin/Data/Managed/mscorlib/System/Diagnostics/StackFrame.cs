using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x020000D9 RID: 217
	[MonoTODO("Serialized objects are not compatible with MS.NET")]
	[ComVisible(true)]
	[Serializable]
	public class StackFrame
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00020E88 File Offset: 0x0001F088
		public StackFrame()
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00020ED8 File Offset: 0x0001F0D8
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		// Token: 0x06000883 RID: 2179
		[MethodImpl(4096)]
		private static extern bool get_frame_info(int skip, bool needFileInfo, out MethodBase method, out int iloffset, out int native_offset, out string file, out int line, out int column);

		// Token: 0x06000884 RID: 2180 RVA: 0x00020F28 File Offset: 0x0001F128
		public virtual int GetFileLineNumber()
		{
			return this.lineNumber;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00020F30 File Offset: 0x0001F130
		public virtual string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00020F38 File Offset: 0x0001F138
		internal string GetSecureFileName()
		{
			string result = "<filename unknown>";
			if (this.fileName == null)
			{
				return result;
			}
			try
			{
				result = this.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return result;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00020F7C File Offset: 0x0001F17C
		public virtual int GetILOffset()
		{
			return this.ilOffset;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00020F84 File Offset: 0x0001F184
		public virtual MethodBase GetMethod()
		{
			return this.methodBase;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00020F8C File Offset: 0x0001F18C
		public virtual int GetNativeOffset()
		{
			return this.nativeOffset;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00020F94 File Offset: 0x0001F194
		internal string GetInternalMethodName()
		{
			return this.internalMethodName;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00020F9C File Offset: 0x0001F19C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.methodBase == null)
			{
				stringBuilder.Append(Locale.GetText("<unknown method>"));
			}
			else
			{
				stringBuilder.Append(this.methodBase.Name);
			}
			stringBuilder.Append(Locale.GetText(" at "));
			if (this.ilOffset == -1)
			{
				stringBuilder.Append(Locale.GetText("<unknown offset>"));
			}
			else
			{
				stringBuilder.Append(Locale.GetText("offset "));
				stringBuilder.Append(this.ilOffset);
			}
			stringBuilder.Append(Locale.GetText(" in file:line:column "));
			stringBuilder.Append(this.GetSecureFileName());
			stringBuilder.AppendFormat(":{0}:{1}", this.lineNumber, this.columnNumber);
			return stringBuilder.ToString();
		}

		// Token: 0x040002DF RID: 735
		public const int OFFSET_UNKNOWN = -1;

		// Token: 0x040002E0 RID: 736
		private int ilOffset = -1;

		// Token: 0x040002E1 RID: 737
		private int nativeOffset = -1;

		// Token: 0x040002E2 RID: 738
		private MethodBase methodBase;

		// Token: 0x040002E3 RID: 739
		private string fileName;

		// Token: 0x040002E4 RID: 740
		private int lineNumber;

		// Token: 0x040002E5 RID: 741
		private int columnNumber;

		// Token: 0x040002E6 RID: 742
		private string internalMethodName;
	}
}

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x020000DA RID: 218
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with .NET")]
	[Serializable]
	public class StackTrace
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x0002107C File Offset: 0x0001F27C
		public StackTrace()
		{
			this.init_frames(0, false);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002108C File Offset: 0x0001F28C
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			this.init_frames(skipFrames, fNeedFileInfo);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002109C File Offset: 0x0001F29C
		public StackTrace(Exception e, bool fNeedFileInfo) : this(e, 0, fNeedFileInfo)
		{
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000210A8 File Offset: 0x0001F2A8
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo) : this(e, skipFrames, fNeedFileInfo, false)
		{
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000210B4 File Offset: 0x0001F2B4
		internal StackTrace(Exception e, int skipFrames, bool fNeedFileInfo, bool returnNativeFrames)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			this.frames = StackTrace.get_trace(e, skipFrames, fNeedFileInfo);
			if (!returnNativeFrames)
			{
				bool flag = false;
				for (int i = 0; i < this.frames.Length; i++)
				{
					if (this.frames[i].GetMethod() == null)
					{
						flag = true;
					}
				}
				if (flag)
				{
					ArrayList arrayList = new ArrayList();
					for (int j = 0; j < this.frames.Length; j++)
					{
						if (this.frames[j].GetMethod() != null)
						{
							arrayList.Add(this.frames[j]);
						}
					}
					this.frames = (StackFrame[])arrayList.ToArray(typeof(StackFrame));
				}
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00021194 File Offset: 0x0001F394
		private void init_frames(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			ArrayList arrayList = new ArrayList();
			skipFrames += 2;
			StackFrame stackFrame;
			while ((stackFrame = new StackFrame(skipFrames, fNeedFileInfo)) != null && stackFrame.GetMethod() != null)
			{
				arrayList.Add(stackFrame);
				skipFrames++;
			}
			this.debug_info = fNeedFileInfo;
			this.frames = (StackFrame[])arrayList.ToArray(typeof(StackFrame));
		}

		// Token: 0x06000892 RID: 2194
		[MethodImpl(4096)]
		private static extern StackFrame[] get_trace(Exception e, int skipFrames, bool fNeedFileInfo);

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00021210 File Offset: 0x0001F410
		public virtual int FrameCount
		{
			get
			{
				return (this.frames != null) ? this.frames.Length : 0;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0002122C File Offset: 0x0001F42C
		public virtual StackFrame GetFrame(int index)
		{
			if (index < 0 || index >= this.FrameCount)
			{
				return null;
			}
			return this.frames[index];
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002124C File Offset: 0x0001F44C
		public override string ToString()
		{
			string value = string.Format("{0}   {1} ", Environment.NewLine, Locale.GetText("at"));
			string text = Locale.GetText("<unknown method>");
			string text2 = Locale.GetText(" in {0}:line {1}");
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.FrameCount; i++)
			{
				StackFrame frame = this.GetFrame(i);
				if (i > 0)
				{
					stringBuilder.Append(value);
				}
				else
				{
					stringBuilder.AppendFormat("   {0} ", Locale.GetText("at"));
				}
				MethodBase method = frame.GetMethod();
				if (method != null)
				{
					stringBuilder.AppendFormat("{0}.{1}", method.DeclaringType.FullName, method.Name);
					stringBuilder.Append("(");
					ParameterInfo[] parameters = method.GetParameters();
					for (int j = 0; j < parameters.Length; j++)
					{
						if (j > 0)
						{
							stringBuilder.Append(", ");
						}
						Type type = parameters[j].ParameterType;
						bool isByRef = type.IsByRef;
						if (isByRef)
						{
							type = type.GetElementType();
						}
						if (type.IsClass && type.Namespace != string.Empty)
						{
							stringBuilder.Append(type.Namespace);
							stringBuilder.Append(".");
						}
						stringBuilder.Append(type.Name);
						if (isByRef)
						{
							stringBuilder.Append(" ByRef");
						}
						stringBuilder.AppendFormat(" {0}", parameters[j].Name);
					}
					stringBuilder.Append(")");
				}
				else
				{
					stringBuilder.Append(text);
				}
				if (this.debug_info)
				{
					string secureFileName = frame.GetSecureFileName();
					if (secureFileName != "<filename unknown>")
					{
						stringBuilder.AppendFormat(text2, secureFileName, frame.GetFileLineNumber());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002E7 RID: 743
		public const int METHODS_TO_SKIP = 0;

		// Token: 0x040002E8 RID: 744
		private StackFrame[] frames;

		// Token: 0x040002E9 RID: 745
		private bool debug_info;
	}
}

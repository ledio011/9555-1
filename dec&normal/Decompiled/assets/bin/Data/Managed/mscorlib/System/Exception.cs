using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	// Token: 0x020000E7 RID: 231
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Exception))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public class Exception : _Exception, ISerializable
	{
		// Token: 0x0600092B RID: 2347 RVA: 0x00023A80 File Offset: 0x00021C80
		public Exception()
		{
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00023A94 File Offset: 0x00021C94
		public Exception(string message)
		{
			this.message = message;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00023AB0 File Offset: 0x00021CB0
		protected Exception(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.class_name = info.GetString("ClassName");
			this.message = info.GetString("Message");
			this.help_link = info.GetString("HelpURL");
			this.stack_trace = info.GetString("StackTraceString");
			this._remoteStackTraceString = info.GetString("RemoteStackTraceString");
			this.remote_stack_index = info.GetInt32("RemoteStackIndex");
			this.hresult = info.GetInt32("HResult");
			this.source = info.GetString("Source");
			this.inner_exception = (Exception)info.GetValue("InnerException", typeof(Exception));
			try
			{
				this._data = (IDictionary)info.GetValue("Data", typeof(IDictionary));
			}
			catch (SerializationException)
			{
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00023BC4 File Offset: 0x00021DC4
		public Exception(string message, Exception innerException)
		{
			this.inner_exception = innerException;
			this.message = message;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00023BE8 File Offset: 0x00021DE8
		public Exception InnerException
		{
			get
			{
				return this.inner_exception;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x00023BF0 File Offset: 0x00021DF0
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x00023BF8 File Offset: 0x00021DF8
		protected int HResult
		{
			get
			{
				return this.hresult;
			}
			set
			{
				this.hresult = value;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00023C04 File Offset: 0x00021E04
		internal void SetMessage(string s)
		{
			this.message = s;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00023C10 File Offset: 0x00021E10
		internal void SetStackTrace(string s)
		{
			this.stack_trace = s;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00023C1C File Offset: 0x00021E1C
		private string ClassName
		{
			get
			{
				if (this.class_name == null)
				{
					this.class_name = this.GetType().ToString();
				}
				return this.class_name;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00023C40 File Offset: 0x00021E40
		public virtual string Message
		{
			get
			{
				if (this.message == null)
				{
					this.message = string.Format(Locale.GetText("Exception of type '{0}' was thrown."), this.ClassName);
				}
				return this.message;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00023C70 File Offset: 0x00021E70
		public virtual string Source
		{
			get
			{
				if (this.source == null)
				{
					StackTrace stackTrace = new StackTrace(this, true);
					if (stackTrace.FrameCount > 0)
					{
						StackFrame frame = stackTrace.GetFrame(0);
						if (stackTrace != null)
						{
							MethodBase method = frame.GetMethod();
							if (method != null)
							{
								this.source = method.DeclaringType.Assembly.UnprotectedGetName().Name;
							}
						}
					}
				}
				return this.source;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00023CD8 File Offset: 0x00021ED8
		public virtual string StackTrace
		{
			get
			{
				if (this.stack_trace == null)
				{
					if (this.trace_ips == null)
					{
						return null;
					}
					StackTrace stackTrace = new StackTrace(this, 0, true, true);
					StringBuilder stringBuilder = new StringBuilder();
					string value = string.Format("{0}  {1} ", Environment.NewLine, Locale.GetText("at"));
					string text = Locale.GetText("<unknown method>");
					for (int i = 0; i < stackTrace.FrameCount; i++)
					{
						StackFrame frame = stackTrace.GetFrame(i);
						if (i == 0)
						{
							stringBuilder.AppendFormat("  {0} ", Locale.GetText("at"));
						}
						else
						{
							stringBuilder.Append(value);
						}
						if (frame.GetMethod() == null)
						{
							string internalMethodName = frame.GetInternalMethodName();
							if (internalMethodName != null)
							{
								stringBuilder.Append(internalMethodName);
							}
							else
							{
								stringBuilder.AppendFormat("<0x{0:x5}> {1}", frame.GetNativeOffset(), text);
							}
						}
						else
						{
							this.GetFullNameForStackTrace(stringBuilder, frame.GetMethod());
							if (frame.GetILOffset() == -1)
							{
								stringBuilder.AppendFormat(" <0x{0:x5}> ", frame.GetNativeOffset());
							}
							else
							{
								stringBuilder.AppendFormat(" [0x{0:x5}] ", frame.GetILOffset());
							}
							stringBuilder.AppendFormat("in {0}:{1} ", frame.GetSecureFileName(), frame.GetFileLineNumber());
						}
					}
					this.stack_trace = stringBuilder.ToString();
				}
				return this.stack_trace;
			}
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00023E4C File Offset: 0x0002204C
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ClassName", this.ClassName);
			info.AddValue("Message", this.message);
			info.AddValue("InnerException", this.inner_exception);
			info.AddValue("HelpURL", this.help_link);
			info.AddValue("StackTraceString", this.StackTrace);
			info.AddValue("RemoteStackTraceString", this._remoteStackTraceString);
			info.AddValue("RemoteStackIndex", this.remote_stack_index);
			info.AddValue("HResult", this.hresult);
			info.AddValue("Source", this.Source);
			info.AddValue("ExceptionMethod", null);
			info.AddValue("Data", this._data, typeof(IDictionary));
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00023F2C File Offset: 0x0002212C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ClassName);
			stringBuilder.Append(": ").Append(this.Message);
			if (this._remoteStackTraceString != null)
			{
				stringBuilder.Append(this._remoteStackTraceString);
			}
			if (this.inner_exception != null)
			{
				stringBuilder.Append(" ---> ").Append(this.inner_exception.ToString());
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Locale.GetText("  --- End of inner exception stack trace ---"));
			}
			if (this.StackTrace != null)
			{
				stringBuilder.Append(Environment.NewLine).Append(this.StackTrace);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00023FE0 File Offset: 0x000221E0
		internal void GetFullNameForStackTrace(StringBuilder sb, MethodBase mi)
		{
			ParameterInfo[] parameters = mi.GetParameters();
			sb.Append(mi.DeclaringType.ToString());
			sb.Append(".");
			sb.Append(mi.Name);
			if (mi.IsGenericMethod)
			{
				Type[] genericArguments = mi.GetGenericArguments();
				sb.Append("[");
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (i > 0)
					{
						sb.Append(",");
					}
					sb.Append(genericArguments[i].Name);
				}
				sb.Append("]");
			}
			sb.Append(" (");
			for (int j = 0; j < parameters.Length; j++)
			{
				if (j > 0)
				{
					sb.Append(", ");
				}
				Type parameterType = parameters[j].ParameterType;
				if (parameterType.IsClass && parameterType.Namespace != string.Empty)
				{
					sb.Append(parameterType.Namespace);
					sb.Append(".");
				}
				sb.Append(parameterType.Name);
				if (parameters[j].Name != null)
				{
					sb.Append(" ");
					sb.Append(parameters[j].Name);
				}
			}
			sb.Append(")");
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002413C File Offset: 0x0002233C
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400031B RID: 795
		private IntPtr[] trace_ips;

		// Token: 0x0400031C RID: 796
		private Exception inner_exception;

		// Token: 0x0400031D RID: 797
		internal string message;

		// Token: 0x0400031E RID: 798
		private string help_link;

		// Token: 0x0400031F RID: 799
		private string class_name;

		// Token: 0x04000320 RID: 800
		private string stack_trace;

		// Token: 0x04000321 RID: 801
		private string _remoteStackTraceString;

		// Token: 0x04000322 RID: 802
		private int remote_stack_index;

		// Token: 0x04000323 RID: 803
		internal int hresult = -2146233088;

		// Token: 0x04000324 RID: 804
		private string source;

		// Token: 0x04000325 RID: 805
		private IDictionary _data;
	}
}

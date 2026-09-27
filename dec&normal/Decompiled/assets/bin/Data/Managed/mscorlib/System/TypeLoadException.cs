using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020003D1 RID: 977
	[ComVisible(true)]
	[Serializable]
	public class TypeLoadException : SystemException
	{
		// Token: 0x06001E0F RID: 7695 RVA: 0x000707C0 File Offset: 0x0006E9C0
		public TypeLoadException() : base(Locale.GetText("A type load exception has occurred."))
		{
			base.HResult = -2146233054;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000707E0 File Offset: 0x0006E9E0
		public TypeLoadException(string message) : base(message)
		{
			base.HResult = -2146233054;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000707F4 File Offset: 0x0006E9F4
		public TypeLoadException(string message, Exception inner) : base(message, inner)
		{
			base.HResult = -2146233054;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0007080C File Offset: 0x0006EA0C
		internal TypeLoadException(string className, string assemblyName) : this()
		{
			this.className = className;
			this.assemblyName = assemblyName;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00070824 File Offset: 0x0006EA24
		protected TypeLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.className = info.GetString("TypeLoadClassName");
			this.assemblyName = info.GetString("TypeLoadAssemblyName");
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00070864 File Offset: 0x0006EA64
		public override string Message
		{
			get
			{
				if (this.className == null)
				{
					return base.Message;
				}
				if (this.assemblyName != null && this.assemblyName != string.Empty)
				{
					return string.Format("Could not load type '{0}' from assembly '{1}'.", this.className, this.assemblyName);
				}
				return string.Format("Could not load type '{0}'.", this.className);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x000708CC File Offset: 0x0006EACC
		public string TypeName
		{
			get
			{
				if (this.className == null)
				{
					return string.Empty;
				}
				return this.className;
			}
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000708E8 File Offset: 0x0006EAE8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("TypeLoadClassName", this.className, typeof(string));
			info.AddValue("TypeLoadAssemblyName", this.assemblyName, typeof(string));
			info.AddValue("TypeLoadMessageArg", string.Empty, typeof(string));
			info.AddValue("TypeLoadResourceID", 0, typeof(int));
		}

		// Token: 0x04000FA2 RID: 4002
		private const int Result = -2146233054;

		// Token: 0x04000FA3 RID: 4003
		private string className;

		// Token: 0x04000FA4 RID: 4004
		private string assemblyName;
	}
}

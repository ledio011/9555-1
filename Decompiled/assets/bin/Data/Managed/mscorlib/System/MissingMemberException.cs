using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000151 RID: 337
	[ComVisible(true)]
	[Serializable]
	public class MissingMemberException : MemberAccessException
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x00032164 File Offset: 0x00030364
		public MissingMemberException() : base(Locale.GetText("Cannot find the requested class member."))
		{
			base.HResult = -2146233070;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00032184 File Offset: 0x00030384
		public MissingMemberException(string message) : base(message)
		{
			base.HResult = -2146233070;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00032198 File Offset: 0x00030398
		protected MissingMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ClassName = info.GetString("MMClassName");
			this.MemberName = info.GetString("MMMemberName");
			this.Signature = (byte[])info.GetValue("MMSignature", typeof(byte[]));
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000321F0 File Offset: 0x000303F0
		public MissingMemberException(string className, string memberName)
		{
			this.ClassName = className;
			this.MemberName = memberName;
			base.HResult = -2146233070;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00032214 File Offset: 0x00030414
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("MMClassName", this.ClassName);
			info.AddValue("MMMemberName", this.MemberName);
			info.AddValue("MMSignature", this.Signature);
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00032254 File Offset: 0x00030454
		public override string Message
		{
			get
			{
				if (this.ClassName == null)
				{
					return base.Message;
				}
				string text = Locale.GetText("Member {0}.{1} not found.");
				return string.Format(text, this.ClassName, this.MemberName);
			}
		}

		// Token: 0x0400055C RID: 1372
		private const int Result = -2146233070;

		// Token: 0x0400055D RID: 1373
		protected string ClassName;

		// Token: 0x0400055E RID: 1374
		protected string MemberName;

		// Token: 0x0400055F RID: 1375
		protected byte[] Signature;
	}
}

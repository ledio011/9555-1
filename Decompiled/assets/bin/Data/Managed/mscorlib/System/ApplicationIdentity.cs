using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200005C RID: 92
	[ComVisible(false)]
	[Serializable]
	public sealed class ApplicationIdentity : ISerializable
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public ApplicationIdentity(string applicationIdentityFullName)
		{
			if (applicationIdentityFullName == null)
			{
				throw new ArgumentNullException("applicationIdentityFullName");
			}
			if (applicationIdentityFullName.IndexOf(", Culture=") == -1)
			{
				this._fullName = applicationIdentityFullName + ", Culture=neutral";
			}
			else
			{
				this._fullName = applicationIdentityFullName;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000E94C File Offset: 0x0000CB4C
		[MonoTODO("Missing serialization")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000E960 File Offset: 0x0000CB60
		public string FullName
		{
			get
			{
				return this._fullName;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000E968 File Offset: 0x0000CB68
		public override string ToString()
		{
			return this._fullName;
		}

		// Token: 0x04000181 RID: 385
		private string _fullName;

		// Token: 0x04000182 RID: 386
		private string _codeBase;
	}
}

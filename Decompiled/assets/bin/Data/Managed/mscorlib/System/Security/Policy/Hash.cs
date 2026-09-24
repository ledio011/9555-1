using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Security.Policy
{
	// Token: 0x0200035F RID: 863
	[ComVisible(true)]
	[Serializable]
	public sealed class Hash : ISerializable, IBuiltInEvidence
	{
		// Token: 0x060019AF RID: 6575 RVA: 0x0005ECE4 File Offset: 0x0005CEE4
		internal Hash()
		{
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0005ECEC File Offset: 0x0005CEEC
		internal Hash(SerializationInfo info, StreamingContext context)
		{
			this.data = (byte[])info.GetValue("RawData", typeof(byte[]));
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0005ED14 File Offset: 0x0005CF14
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("RawData", this.GetData());
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0005ED38 File Offset: 0x0005CF38
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = this.GetData();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			securityElement.AddChild(new SecurityElement("RawData", stringBuilder.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0005EDBC File Offset: 0x0005CFBC
		private byte[] GetData()
		{
			if (this.assembly == null && this.data == null)
			{
				string text = Locale.GetText("No assembly data.");
				throw new SecurityException(text);
			}
			if (this.data == null)
			{
				FileStream fileStream = new FileStream(this.assembly.Location, FileMode.Open, FileAccess.Read);
				this.data = new byte[fileStream.Length];
				fileStream.Read(this.data, 0, (int)fileStream.Length);
			}
			return this.data;
		}

		// Token: 0x04000E1B RID: 3611
		private Assembly assembly;

		// Token: 0x04000E1C RID: 3612
		private byte[] data;

		// Token: 0x04000E1D RID: 3613
		internal byte[] _md5;

		// Token: 0x04000E1E RID: 3614
		internal byte[] _sha1;
	}
}

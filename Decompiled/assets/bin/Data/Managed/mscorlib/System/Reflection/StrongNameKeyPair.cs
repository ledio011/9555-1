using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	// Token: 0x020001EF RID: 495
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : IDeserializationCallback, ISerializable
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x00044CF4 File Offset: 0x00042EF4
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
			this._keyPairContainer = info.GetString("_keyPairContainer");
			this._keyPairExported = info.GetBoolean("_keyPairExported");
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00044D6C File Offset: 0x00042F6C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_publicKey", this._publicKey, typeof(byte[]));
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray, typeof(byte[]));
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00044DD4 File Offset: 0x00042FD4
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00044DD8 File Offset: 0x00042FD8
		private RSA GetRSA()
		{
			if (this._rsa != null)
			{
				return this._rsa;
			}
			if (this._keyPairArray != null)
			{
				try
				{
					this._rsa = CryptoConvert.FromCapiKeyBlob(this._keyPairArray);
				}
				catch
				{
					this._keyPairArray = null;
				}
			}
			return this._rsa;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00044E3C File Offset: 0x0004303C
		internal StrongName StrongName()
		{
			RSA rsa = this.GetRSA();
			if (rsa != null)
			{
				return new StrongName(rsa);
			}
			if (this._publicKey != null)
			{
				return new StrongName(this._publicKey);
			}
			return null;
		}

		// Token: 0x04000962 RID: 2402
		private byte[] _publicKey;

		// Token: 0x04000963 RID: 2403
		private string _keyPairContainer;

		// Token: 0x04000964 RID: 2404
		private bool _keyPairExported;

		// Token: 0x04000965 RID: 2405
		private byte[] _keyPairArray;

		// Token: 0x04000966 RID: 2406
		[NonSerialized]
		private RSA _rsa;
	}
}

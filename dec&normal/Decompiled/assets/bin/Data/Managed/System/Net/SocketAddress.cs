using System;
using System.Net.Sockets;

namespace System.Net
{
	// Token: 0x0200004F RID: 79
	public class SocketAddress
	{
		// Token: 0x06000149 RID: 329 RVA: 0x000060A8 File Offset: 0x000042A8
		public SocketAddress(System.Net.Sockets.AddressFamily family, int size)
		{
			if (size < 2)
			{
				throw new ArgumentOutOfRangeException("size is too small");
			}
			this.data = new byte[size];
			this.data[0] = (byte)family;
			this.data[1] = (byte)(family >> 8);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000060E4 File Offset: 0x000042E4
		public System.Net.Sockets.AddressFamily Family
		{
			get
			{
				return (System.Net.Sockets.AddressFamily)((int)this.data[0] + ((int)this.data[1] << 8));
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000060FC File Offset: 0x000042FC
		public int Size
		{
			get
			{
				return this.data.Length;
			}
		}

		// Token: 0x17000054 RID: 84
		public byte this[int offset]
		{
			get
			{
				return this.data[offset];
			}
			set
			{
				this.data[offset] = value;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006120 File Offset: 0x00004320
		public override string ToString()
		{
			string text = ((System.Net.Sockets.AddressFamily)this.data[0]).ToString();
			int num = this.data.Length;
			string text2 = string.Concat(new object[]
			{
				text,
				":",
				num,
				":{"
			});
			for (int i = 2; i < num; i++)
			{
				int num2 = (int)this.data[i];
				text2 += num2;
				if (i < num - 1)
				{
					text2 += ",";
				}
			}
			return text2 + "}";
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000061C0 File Offset: 0x000043C0
		public override bool Equals(object obj)
		{
			SocketAddress socketAddress = obj as SocketAddress;
			if (socketAddress != null && socketAddress.data.Length == this.data.Length)
			{
				byte[] array = socketAddress.data;
				for (int i = 0; i < this.data.Length; i++)
				{
					if (array[i] != this.data[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006224 File Offset: 0x00004424
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.data.Length; i++)
			{
				num += (int)this.data[i] + i;
			}
			return num;
		}

		// Token: 0x04000813 RID: 2067
		private byte[] data;
	}
}

using System;

// Token: 0x02000809 RID: 2057
public class SocketReciveStream
{
	// Token: 0x0600315D RID: 12637 RVA: 0x000C0A44 File Offset: 0x000BEC44
	public SocketReciveStream(SocketInstance socket, uint bufferLen = 262144U, uint buffMaxLen = 524288U)
	{
		this.mSocket = socket;
		this.mBufferLen = bufferLen;
		this.mBufferMaxLen = buffMaxLen;
		this.mHead = 0U;
		this.mTail = 0U;
		this.mBuffer = new byte[this.mBufferLen];
		this.mTempBuffer = new byte[this.mBufferLen];
	}

	// Token: 0x0600315E RID: 12638 RVA: 0x000C0AA0 File Offset: 0x000BECA0
	public void InitStream(uint bufferLen = 262144U)
	{
		this.mHead = 0U;
		this.mTail = 0U;
		this.mBufferLen = bufferLen;
		this.mBuffer = new byte[this.mBufferLen];
		this.mTempBuffer = new byte[this.mBufferLen];
	}

	// Token: 0x0600315F RID: 12639 RVA: 0x000C0ADC File Offset: 0x000BECDC
	public void Clean()
	{
		this.mBuffer = null;
		this.mTempBuffer = null;
		this.mHead = 0U;
		this.mTail = 0U;
	}

	// Token: 0x06003160 RID: 12640 RVA: 0x000C0AFC File Offset: 0x000BECFC
	public uint Read(byte[] buff, uint len)
	{
		if (len > this.GetBuffLen())
		{
			return 0U;
		}
		int num = 0;
		while ((long)num < (long)((ulong)len))
		{
			buff[num] = this.mBuffer[(int)((UIntPtr)(this.mHead++))];
			if (this.mHead >= this.mBufferLen)
			{
				this.mHead -= this.mBufferLen;
			}
			num++;
		}
		return len;
	}

	// Token: 0x06003161 RID: 12641 RVA: 0x000C0B6C File Offset: 0x000BED6C
	public bool Peek(byte[] buff, uint len)
	{
		if (len == 0U)
		{
			return false;
		}
		if (len > this.GetBuffLen())
		{
			return false;
		}
		uint num = this.mHead;
		int num2 = 0;
		while ((long)num2 < (long)((ulong)len))
		{
			buff[num2] = this.mBuffer[(int)((UIntPtr)(num++))];
			if (num >= this.mBufferLen)
			{
				num -= this.mBufferLen;
			}
			num2++;
		}
		return true;
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x000C0BD0 File Offset: 0x000BEDD0
	public bool Skip(uint len)
	{
		if (len == 0U)
		{
			return false;
		}
		if (len > this.GetBuffLen())
		{
			return false;
		}
		this.mHead += len;
		if (this.mHead >= this.mBufferLen)
		{
			this.mHead -= this.mBufferLen;
		}
		return true;
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x000C0C28 File Offset: 0x000BEE28
	public uint Recive()
	{
		if (this.mSocket == null)
		{
			return 0U;
		}
		uint num = 0U;
		uint num2 = 0U;
		try
		{
			uint buffFree = this.GetBuffFree();
			if (buffFree != 0U)
			{
				num2 = this.mSocket.Recv(this.mTempBuffer, (int)buffFree, 0U);
				if (num2 == 4294967295U)
				{
					return uint.MaxValue;
				}
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					this.mBuffer[(int)((UIntPtr)(this.mTail++))] = this.mTempBuffer[num3];
					if (this.mTail >= this.mBufferLen)
					{
						this.mTail -= this.mBufferLen;
					}
					num3++;
				}
				num += num2;
			}
			if (num2 == buffFree)
			{
				uint num4 = this.mSocket.Avaiable();
				if (num4 > 0U)
				{
					if (!this.ExtendSize(num4 + 1U))
					{
						this.InitStream(262144U);
						return uint.MaxValue;
					}
					num2 = this.mSocket.Recv(this.mTempBuffer, (int)num4, 0U);
					if (num2 == 4294967295U)
					{
						return uint.MaxValue;
					}
					int num5 = 0;
					while ((long)num5 < (long)((ulong)num2))
					{
						this.mBuffer[(int)((UIntPtr)(this.mTail++))] = this.mTempBuffer[num5];
						if (this.mTail >= this.mBufferLen)
						{
							this.mTail -= this.mBufferLen;
						}
						num5++;
					}
					num += num2;
				}
			}
		}
		catch (Exception ex)
		{
		}
		return num;
	}

	// Token: 0x06003164 RID: 12644 RVA: 0x000C0DC8 File Offset: 0x000BEFC8
	public uint GetBuffLen()
	{
		if (this.mHead < this.mTail)
		{
			return this.mTail - this.mHead;
		}
		if (this.mHead > this.mTail)
		{
			return this.mBufferLen - this.mHead + this.mTail;
		}
		return 0U;
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x000C0E1C File Offset: 0x000BF01C
	public uint GetBuffFree()
	{
		if (this.mHead <= this.mTail)
		{
			return this.mBufferLen - this.mTail + this.mHead - 1U;
		}
		if (this.mHead > this.mTail)
		{
			return this.mHead - this.mTail - 1U;
		}
		return 0U;
	}

	// Token: 0x06003166 RID: 12646 RVA: 0x000C0E74 File Offset: 0x000BF074
	private bool ExtendSize(uint size)
	{
		uint num = Math.Max(this.mBufferLen, size) + this.mBufferLen;
		if (num >= this.mBufferMaxLen)
		{
			return false;
		}
		byte[] array = new byte[num];
		this.mTempBuffer = new byte[num];
		uint buffLen = this.GetBuffLen();
		if (array == null || this.mTempBuffer == null)
		{
			return false;
		}
		if (this.mHead < this.mTail)
		{
			int num2 = 0;
			while ((long)num2 < (long)((ulong)(this.mTail - this.mHead - 1U)))
			{
				array[num2] = this.mBuffer[(int)(checked((IntPtr)(unchecked((ulong)this.mHead + (ulong)((long)num2)))))];
				num2++;
			}
		}
		else
		{
			int num3 = 0;
			while ((long)num3 < (long)((ulong)(this.mBufferLen - this.mHead)))
			{
				array[num3] = this.mBuffer[(int)(checked((IntPtr)(unchecked((ulong)this.mHead + (ulong)((long)num3)))))];
				num3++;
			}
			int num4 = 0;
			while ((long)num4 < (long)((ulong)this.mTail))
			{
				array[(int)(checked((IntPtr)(unchecked((ulong)(this.mBufferLen - this.mHead) + (ulong)((long)num4)))))] = this.mBuffer[num4];
				num4++;
			}
		}
		this.mBuffer = array;
		this.mBufferLen = num;
		this.mHead = 0U;
		this.mTail = buffLen;
		return true;
	}

	// Token: 0x0400211B RID: 8475
	public const uint DEFAULT_SOCKET_RECIVE_BUFFER_SIZE = 262144U;

	// Token: 0x0400211C RID: 8476
	public const uint DEFAULT_SOCKET_RECIVE_BUFFER_MAX_SIZE = 524288U;

	// Token: 0x0400211D RID: 8477
	private const uint SOCKET_ERROR = 4294967295U;

	// Token: 0x0400211E RID: 8478
	private SocketInstance mSocket;

	// Token: 0x0400211F RID: 8479
	private uint mBufferLen;

	// Token: 0x04002120 RID: 8480
	private uint mBufferMaxLen;

	// Token: 0x04002121 RID: 8481
	private byte[] mBuffer;

	// Token: 0x04002122 RID: 8482
	private byte[] mTempBuffer;

	// Token: 0x04002123 RID: 8483
	private uint mHead;

	// Token: 0x04002124 RID: 8484
	private uint mTail;
}

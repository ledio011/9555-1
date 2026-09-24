using System;

// Token: 0x0200080A RID: 2058
public class SocketSendStream
{
	// Token: 0x06003167 RID: 12647 RVA: 0x000C0FB4 File Offset: 0x000BF1B4
	public SocketSendStream(SocketInstance socket, uint bufferLen = 8192U, uint buffMaxLen = 20480U)
	{
		this.mSocket = socket;
		this.mBufferLen = bufferLen;
		this.mBufferMaxLen = buffMaxLen;
		this.mHead = 0U;
		this.mTail = 0U;
		this.mBuffer = new byte[this.mBufferLen];
		this.mTempBuffer = new byte[this.mBufferLen];
	}

	// Token: 0x06003168 RID: 12648 RVA: 0x000C1010 File Offset: 0x000BF210
	public void Clean()
	{
		this.mBuffer = null;
		this.mTempBuffer = null;
		this.mHead = 0U;
		this.mTail = 0U;
	}

	// Token: 0x06003169 RID: 12649 RVA: 0x000C1030 File Offset: 0x000BF230
	public uint Write(byte[] buff, uint len)
	{
		uint num = (this.mHead > this.mTail) ? (this.mHead - this.mTail - 1U) : (this.mBufferLen - this.mTail + this.mHead - 1U);
		if (len >= num && !this.ExtendSize(len - num + 1U))
		{
			this.mHead = 0U;
			this.mTail = 0U;
			Log.WARING_MSG("Send pack is Max!!!!!");
			return 0U;
		}
		int num2 = 0;
		while ((long)num2 < (long)((ulong)len))
		{
			this.mBuffer[(int)((UIntPtr)(this.mTail++))] = buff[num2];
			if (this.mTail >= this.mBufferLen)
			{
				this.mTail -= this.mBufferLen;
			}
			num2++;
		}
		return len;
	}

	// Token: 0x0600316A RID: 12650 RVA: 0x000C1100 File Offset: 0x000BF300
	public uint Send()
	{
		if (this.mSocket == null)
		{
			return 0U;
		}
		uint num = 0U;
		try
		{
			for (uint buffLen = this.GetBuffLen(); buffLen > 0U; buffLen = this.GetBuffLen())
			{
				uint num2 = this.mHead;
				int num3 = 0;
				while ((long)num3 < (long)((ulong)buffLen))
				{
					this.mTempBuffer[num3] = this.mBuffer[(int)((UIntPtr)(num2++))];
					if (num2 >= this.mBufferLen)
					{
						num2 -= this.mBufferLen;
					}
					num3++;
				}
				uint num4 = this.mSocket.Send(this.mTempBuffer, (int)buffLen, 0);
				if (num4 == 4294967295U)
				{
					return uint.MaxValue;
				}
				num += num4;
				this.mHead += num4;
				if (this.mHead >= this.mBufferLen)
				{
					this.mHead -= this.mBufferLen;
				}
			}
		}
		catch (Exception ex)
		{
		}
		this.mHead = (this.mTail = 0U);
		return num;
	}

	// Token: 0x0600316B RID: 12651 RVA: 0x000C121C File Offset: 0x000BF41C
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

	// Token: 0x0600316C RID: 12652 RVA: 0x000C1270 File Offset: 0x000BF470
	private bool ExtendSize(uint size)
	{
		if (size >= this.mBufferLen)
		{
			return false;
		}
		uint num = this.mBufferLen >> 1;
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

	// Token: 0x04002125 RID: 8485
	public const uint DEFAULT_SOCKET_SEND_BUFFER_SIZE = 8192U;

	// Token: 0x04002126 RID: 8486
	public const uint DEFAULT_SOCKET_SEND_BUFFER_MAX_SIZE = 20480U;

	// Token: 0x04002127 RID: 8487
	private const uint SOCKET_ERROR = 4294967295U;

	// Token: 0x04002128 RID: 8488
	private SocketInstance mSocket;

	// Token: 0x04002129 RID: 8489
	private uint mBufferLen;

	// Token: 0x0400212A RID: 8490
	private uint mBufferMaxLen;

	// Token: 0x0400212B RID: 8491
	private byte[] mBuffer;

	// Token: 0x0400212C RID: 8492
	private byte[] mTempBuffer;

	// Token: 0x0400212D RID: 8493
	private uint mHead;

	// Token: 0x0400212E RID: 8494
	private uint mTail;
}

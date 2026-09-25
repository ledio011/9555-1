using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200032F RID: 815
	[ComVisible(true)]
	public class ToBase64Transform : IDisposable, ICryptoTransform
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x0005A454 File Offset: 0x00058654
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0005A464 File Offset: 0x00058664
		~ToBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x0005A494 File Offset: 0x00058694
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x0005A4B0 File Offset: 0x000586B0
		internal static void InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			byte[] encodeTable = Base64Constants.EncodeTable;
			int num = (int)inputBuffer[inputOffset];
			int num2 = (int)inputBuffer[inputOffset + 1];
			int num3 = (int)inputBuffer[inputOffset + 2];
			outputBuffer[outputOffset] = encodeTable[num >> 2];
			outputBuffer[outputOffset + 1] = encodeTable[(num << 4 & 48) | num2 >> 4];
			outputBuffer[outputOffset + 2] = encodeTable[(num2 << 2 & 60) | num3 >> 6];
			outputBuffer[outputOffset + 3] = encodeTable[num3 & 63];
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0005A50C File Offset: 0x0005870C
		internal static byte[] InternalTransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = 3;
			int num2 = 4;
			int num3 = inputCount / num;
			int num4 = inputCount % num;
			byte[] array = new byte[(inputCount == 0) ? 0 : ((inputCount + 2) / num * num2)];
			int num5 = 0;
			for (int i = 0; i < num3; i++)
			{
				ToBase64Transform.InternalTransformBlock(inputBuffer, inputOffset, num, array, num5);
				inputOffset += num;
				num5 += num2;
			}
			byte[] encodeTable = Base64Constants.EncodeTable;
			switch (num4)
			{
			case 1:
			{
				int num6 = (int)inputBuffer[inputOffset];
				array[num5] = encodeTable[num6 >> 2];
				array[num5 + 1] = encodeTable[num6 << 4 & 48];
				array[num5 + 2] = 61;
				array[num5 + 3] = 61;
				break;
			}
			case 2:
			{
				int num6 = (int)inputBuffer[inputOffset];
				int num7 = (int)inputBuffer[inputOffset + 1];
				array[num5] = encodeTable[num6 >> 2];
				array[num5 + 1] = encodeTable[(num6 << 4 & 48) | num7 >> 4];
				array[num5 + 2] = encodeTable[num7 << 2 & 60];
				array[num5 + 3] = 61;
				break;
			}
			}
			return array;
		}

		// Token: 0x04000D4B RID: 3403
		private const int inputBlockSize = 3;

		// Token: 0x04000D4C RID: 3404
		private const int outputBlockSize = 4;

		// Token: 0x04000D4D RID: 3405
		private bool m_disposed;
	}
}

using System;

namespace System.IO
{
	// Token: 0x02000132 RID: 306
	internal class NullStream : Stream
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0002E3D0 File Offset: 0x0002C5D0
		public override long Length
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002E3D4 File Offset: 0x0002C5D4
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0002E3D8 File Offset: 0x0002C5D8
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002E3DC File Offset: 0x0002C5DC
		public override void Flush()
		{
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		public override int Read(byte[] buffer, int offset, int count)
		{
			return 0;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002E3E4 File Offset: 0x0002C5E4
		public override int ReadByte()
		{
			return -1;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002E3E8 File Offset: 0x0002C5E8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return 0L;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002E3EC File Offset: 0x0002C5EC
		public override void SetLength(long value)
		{
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002E3F0 File Offset: 0x0002C5F0
		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
		public override void WriteByte(byte value)
		{
		}
	}
}

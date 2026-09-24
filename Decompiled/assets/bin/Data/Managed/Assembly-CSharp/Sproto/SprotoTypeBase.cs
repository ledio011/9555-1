using System;

namespace Sproto
{
	// Token: 0x02000811 RID: 2065
	public abstract class SprotoTypeBase
	{
		// Token: 0x06003199 RID: 12697 RVA: 0x000C1F04 File Offset: 0x000C0104
		public SprotoTypeBase(int max_field_count)
		{
			this.has_field = new SprotoTypeFieldOP(max_field_count);
			this.serialize = new SprotoTypeSerialize(max_field_count);
			this.deserialize = new SprotoTypeDeserialize();
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000C1F30 File Offset: 0x000C0130
		public SprotoTypeBase(int max_field_count, byte[] buffer)
		{
			this.has_field = new SprotoTypeFieldOP(max_field_count);
			this.serialize = new SprotoTypeSerialize(max_field_count);
			this.deserialize = new SprotoTypeDeserialize(buffer);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000C1F68 File Offset: 0x000C0168
		public int init(byte[] buffer, int offset = 0, int len = 0)
		{
			this.clear();
			this.deserialize.init(buffer, offset, len);
			this.decode();
			return this.deserialize.size();
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000C1F9C File Offset: 0x000C019C
		public long init(SprotoTypeReader reader)
		{
			this.clear();
			this.deserialize.init(reader);
			this.decode();
			return (long)this.deserialize.size();
		}

		// Token: 0x0600319D RID: 12701
		public abstract int encode(SprotoStream stream);

		// Token: 0x0600319E RID: 12702 RVA: 0x000C1FD0 File Offset: 0x000C01D0
		public byte[] encode()
		{
			SprotoStream sprotoStream = new SprotoStream();
			this.encode(sprotoStream);
			int position = sprotoStream.Position;
			byte[] array = new byte[position];
			sprotoStream.Seek(0, 0);
			sprotoStream.Read(array, 0, position);
			return array;
		}

		// Token: 0x0600319F RID: 12703
		protected abstract void decode();

		// Token: 0x060031A0 RID: 12704 RVA: 0x000C200C File Offset: 0x000C020C
		public void clear()
		{
			this.has_field.clear_field();
			this.deserialize.clear();
		}

		// Token: 0x0400213D RID: 8509
		protected SprotoTypeFieldOP has_field;

		// Token: 0x0400213E RID: 8510
		protected SprotoTypeSerialize serialize;

		// Token: 0x0400213F RID: 8511
		protected SprotoTypeDeserialize deserialize;
	}
}

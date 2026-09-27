using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000368 RID: 872
	public class count_down
	{
		// Token: 0x02000369 RID: 873
		public class request : SprotoTypeBase
		{
			// Token: 0x06001A0C RID: 6668 RVA: 0x000914F0 File Offset: 0x0008F6F0
			public request() : base(count_down.request.max_field_count)
			{
			}

			// Token: 0x06001A0D RID: 6669 RVA: 0x00091500 File Offset: 0x0008F700
			public request(byte[] buffer) : base(count_down.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x1700063F RID: 1599
			// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0009151C File Offset: 0x0008F71C
			// (set) Token: 0x06001A10 RID: 6672 RVA: 0x00091524 File Offset: 0x0008F724
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._type = value;
				}
			}

			// Token: 0x17000640 RID: 1600
			// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0009153C File Offset: 0x0008F73C
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000641 RID: 1601
			// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0009154C File Offset: 0x0008F74C
			// (set) Token: 0x06001A13 RID: 6675 RVA: 0x00091554 File Offset: 0x0008F754
			public long count_value
			{
				get
				{
					return this._count_value;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._count_value = value;
				}
			}

			// Token: 0x17000642 RID: 1602
			// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0009156C File Offset: 0x0008F76C
			public bool HasCount_value
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001A15 RID: 6677 RVA: 0x0009157C File Offset: 0x0008F77C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.count_value = this.deserialize.read_integer();
						}
					}
					else
					{
						this.type = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001A16 RID: 6678 RVA: 0x000915F4 File Offset: 0x0008F7F4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.type, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.count_value, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x040019B0 RID: 6576
			private static int max_field_count = 2;

			// Token: 0x040019B1 RID: 6577
			private long _type;

			// Token: 0x040019B2 RID: 6578
			private long _count_value;
		}
	}
}

using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200039F RID: 927
	public class equip_enhance
	{
		// Token: 0x020003A0 RID: 928
		public class request : SprotoTypeBase
		{
			// Token: 0x06001BC0 RID: 7104 RVA: 0x00094C28 File Offset: 0x00092E28
			public request() : base(equip_enhance.request.max_field_count)
			{
			}

			// Token: 0x06001BC1 RID: 7105 RVA: 0x00094C38 File Offset: 0x00092E38
			public request(byte[] buffer) : base(equip_enhance.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x00094C54 File Offset: 0x00092E54
			// (set) Token: 0x06001BC4 RID: 7108 RVA: 0x00094C5C File Offset: 0x00092E5C
			public long indexId
			{
				get
				{
					return this._indexId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._indexId = value;
				}
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x00094C74 File Offset: 0x00092E74
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x00094C84 File Offset: 0x00092E84
			// (set) Token: 0x06001BC7 RID: 7111 RVA: 0x00094C8C File Offset: 0x00092E8C
			public long level
			{
				get
				{
					return this._level;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._level = value;
				}
			}

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x00094CA4 File Offset: 0x00092EA4
			public bool HasLevel
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06001BC9 RID: 7113 RVA: 0x00094CB4 File Offset: 0x00092EB4
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
							this.level = this.deserialize.read_integer();
						}
					}
					else
					{
						this.indexId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06001BCA RID: 7114 RVA: 0x00094D2C File Offset: 0x00092F2C
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.level, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001A24 RID: 6692
			private static int max_field_count = 2;

			// Token: 0x04001A25 RID: 6693
			private long _indexId;

			// Token: 0x04001A26 RID: 6694
			private long _level;
		}
	}
}

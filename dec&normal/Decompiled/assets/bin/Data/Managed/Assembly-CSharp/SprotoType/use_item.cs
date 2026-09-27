using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200065B RID: 1627
	public class use_item
	{
		// Token: 0x0200065C RID: 1628
		public class request : SprotoTypeBase
		{
			// Token: 0x06002EFF RID: 12031 RVA: 0x000BAFA0 File Offset: 0x000B91A0
			public request() : base(use_item.request.max_field_count)
			{
			}

			// Token: 0x06002F00 RID: 12032 RVA: 0x000BAFB0 File Offset: 0x000B91B0
			public request(byte[] buffer) : base(use_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DD5 RID: 3541
			// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000BAFCC File Offset: 0x000B91CC
			// (set) Token: 0x06002F03 RID: 12035 RVA: 0x000BAFD4 File Offset: 0x000B91D4
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

			// Token: 0x17000DD6 RID: 3542
			// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000BAFEC File Offset: 0x000B91EC
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DD7 RID: 3543
			// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000BAFFC File Offset: 0x000B91FC
			// (set) Token: 0x06002F06 RID: 12038 RVA: 0x000BB004 File Offset: 0x000B9204
			public long x
			{
				get
				{
					return this._x;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._x = value;
				}
			}

			// Token: 0x17000DD8 RID: 3544
			// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000BB01C File Offset: 0x000B921C
			public bool HasX
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DD9 RID: 3545
			// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000BB02C File Offset: 0x000B922C
			// (set) Token: 0x06002F09 RID: 12041 RVA: 0x000BB034 File Offset: 0x000B9234
			public long z
			{
				get
				{
					return this._z;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._z = value;
				}
			}

			// Token: 0x17000DDA RID: 3546
			// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000BB04C File Offset: 0x000B924C
			public bool HasZ
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002F0B RID: 12043 RVA: 0x000BB05C File Offset: 0x000B925C
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.indexId = this.deserialize.read_integer();
						break;
					case 1:
						this.x = this.deserialize.read_integer();
						break;
					case 2:
						this.z = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002F0C RID: 12044 RVA: 0x000BB0F0 File Offset: 0x000B92F0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.x, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.z, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F4C RID: 8012
			private static int max_field_count = 3;

			// Token: 0x04001F4D RID: 8013
			private long _indexId;

			// Token: 0x04001F4E RID: 8014
			private long _x;

			// Token: 0x04001F4F RID: 8015
			private long _z;
		}
	}
}

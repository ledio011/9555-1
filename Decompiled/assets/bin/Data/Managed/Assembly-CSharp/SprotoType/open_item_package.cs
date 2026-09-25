using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200043C RID: 1084
	public class open_item_package
	{
		// Token: 0x0200043D RID: 1085
		public class request : SprotoTypeBase
		{
			// Token: 0x060021CB RID: 8651 RVA: 0x000A14E8 File Offset: 0x0009F6E8
			public request() : base(open_item_package.request.max_field_count)
			{
			}

			// Token: 0x060021CC RID: 8652 RVA: 0x000A14F8 File Offset: 0x0009F6F8
			public request(byte[] buffer) : base(open_item_package.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x060021CE RID: 8654 RVA: 0x000A1514 File Offset: 0x0009F714
			// (set) Token: 0x060021CF RID: 8655 RVA: 0x000A151C File Offset: 0x0009F71C
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

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000A1534 File Offset: 0x0009F734
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000987 RID: 2439
			// (get) Token: 0x060021D1 RID: 8657 RVA: 0x000A1544 File Offset: 0x0009F744
			// (set) Token: 0x060021D2 RID: 8658 RVA: 0x000A154C File Offset: 0x0009F74C
			public long indexId2
			{
				get
				{
					return this._indexId2;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._indexId2 = value;
				}
			}

			// Token: 0x17000988 RID: 2440
			// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000A1564 File Offset: 0x0009F764
			public bool HasIndexId2
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000989 RID: 2441
			// (get) Token: 0x060021D4 RID: 8660 RVA: 0x000A1574 File Offset: 0x0009F774
			// (set) Token: 0x060021D5 RID: 8661 RVA: 0x000A157C File Offset: 0x0009F77C
			public long count
			{
				get
				{
					return this._count;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._count = value;
				}
			}

			// Token: 0x1700098A RID: 2442
			// (get) Token: 0x060021D6 RID: 8662 RVA: 0x000A1594 File Offset: 0x0009F794
			public bool HasCount
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x060021D7 RID: 8663 RVA: 0x000A15A4 File Offset: 0x0009F7A4
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
						this.indexId2 = this.deserialize.read_integer();
						break;
					case 2:
						this.count = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060021D8 RID: 8664 RVA: 0x000A1638 File Offset: 0x0009F838
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.indexId2, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.count, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BD8 RID: 7128
			private static int max_field_count = 3;

			// Token: 0x04001BD9 RID: 7129
			private long _indexId;

			// Token: 0x04001BDA RID: 7130
			private long _indexId2;

			// Token: 0x04001BDB RID: 7131
			private long _count;
		}
	}
}

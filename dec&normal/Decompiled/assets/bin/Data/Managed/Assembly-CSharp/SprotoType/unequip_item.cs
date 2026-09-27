using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200062F RID: 1583
	public class unequip_item
	{
		// Token: 0x02000630 RID: 1584
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E0E RID: 11790 RVA: 0x000B92E8 File Offset: 0x000B74E8
			public request() : base(unequip_item.request.max_field_count)
			{
			}

			// Token: 0x06002E0F RID: 11791 RVA: 0x000B92F8 File Offset: 0x000B74F8
			public request(byte[] buffer) : base(unequip_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D8F RID: 3471
			// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000B9314 File Offset: 0x000B7514
			// (set) Token: 0x06002E12 RID: 11794 RVA: 0x000B931C File Offset: 0x000B751C
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

			// Token: 0x17000D90 RID: 3472
			// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000B9334 File Offset: 0x000B7534
			public bool HasIndexId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002E14 RID: 11796 RVA: 0x000B9344 File Offset: 0x000B7544
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						this.deserialize.read_unknow_data();
					}
					else
					{
						this.indexId = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002E15 RID: 11797 RVA: 0x000B93A0 File Offset: 0x000B75A0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.indexId, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F12 RID: 7954
			private static int max_field_count = 1;

			// Token: 0x04001F13 RID: 7955
			private long _indexId;
		}
	}
}

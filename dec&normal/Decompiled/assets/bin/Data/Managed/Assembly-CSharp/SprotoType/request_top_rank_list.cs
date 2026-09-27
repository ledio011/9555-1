using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020004D7 RID: 1239
	public class request_top_rank_list
	{
		// Token: 0x020004D8 RID: 1240
		public class request : SprotoTypeBase
		{
			// Token: 0x06002493 RID: 9363 RVA: 0x000A640C File Offset: 0x000A460C
			public request() : base(request_top_rank_list.request.max_field_count)
			{
			}

			// Token: 0x06002494 RID: 9364 RVA: 0x000A641C File Offset: 0x000A461C
			public request(byte[] buffer) : base(request_top_rank_list.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A23 RID: 2595
			// (get) Token: 0x06002496 RID: 9366 RVA: 0x000A6438 File Offset: 0x000A4638
			// (set) Token: 0x06002497 RID: 9367 RVA: 0x000A6440 File Offset: 0x000A4640
			public long sortType
			{
				get
				{
					return this._sortType;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._sortType = value;
				}
			}

			// Token: 0x17000A24 RID: 2596
			// (get) Token: 0x06002498 RID: 9368 RVA: 0x000A6458 File Offset: 0x000A4658
			public bool HasSortType
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002499 RID: 9369 RVA: 0x000A6468 File Offset: 0x000A4668
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
						this.sortType = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600249A RID: 9370 RVA: 0x000A64C4 File Offset: 0x000A46C4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.sortType, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C77 RID: 7287
			private static int max_field_count = 1;

			// Token: 0x04001C78 RID: 7288
			private long _sortType;
		}
	}
}

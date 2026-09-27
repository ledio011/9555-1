using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005FD RID: 1533
	public class sync_backpack_item
	{
		// Token: 0x020005FE RID: 1534
		public class request : SprotoTypeBase
		{
			// Token: 0x06002C60 RID: 11360 RVA: 0x000B5BE0 File Offset: 0x000B3DE0
			public request() : base(sync_backpack_item.request.max_field_count)
			{
			}

			// Token: 0x06002C61 RID: 11361 RVA: 0x000B5BF0 File Offset: 0x000B3DF0
			public request(byte[] buffer) : base(sync_backpack_item.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000CE3 RID: 3299
			// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000B5C0C File Offset: 0x000B3E0C
			// (set) Token: 0x06002C64 RID: 11364 RVA: 0x000B5C14 File Offset: 0x000B3E14
			public Dictionary<long, gameitem> gameitems
			{
				get
				{
					return this._gameitems;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._gameitems = value;
				}
			}

			// Token: 0x17000CE4 RID: 3300
			// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000B5C2C File Offset: 0x000B3E2C
			public bool HasGameitems
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002C66 RID: 11366 RVA: 0x000B5C3C File Offset: 0x000B3E3C
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
						this.gameitems = this.deserialize.read_map<long, gameitem>((gameitem v) => v.indexId);
					}
				}
			}

			// Token: 0x06002C67 RID: 11367 RVA: 0x000B5CB4 File Offset: 0x000B3EB4
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<long, gameitem>(this.gameitems, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E96 RID: 7830
			private static int max_field_count = 1;

			// Token: 0x04001E97 RID: 7831
			private Dictionary<long, gameitem> _gameitems;
		}
	}
}

using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200054D RID: 1357
	public class ret_mount_equip
	{
		// Token: 0x0200054E RID: 1358
		public class request : SprotoTypeBase
		{
			// Token: 0x06002787 RID: 10119 RVA: 0x000AC0DC File Offset: 0x000AA2DC
			public request() : base(ret_mount_equip.request.max_field_count)
			{
			}

			// Token: 0x06002788 RID: 10120 RVA: 0x000AC0EC File Offset: 0x000AA2EC
			public request(byte[] buffer) : base(ret_mount_equip.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B27 RID: 2855
			// (get) Token: 0x0600278A RID: 10122 RVA: 0x000AC108 File Offset: 0x000AA308
			// (set) Token: 0x0600278B RID: 10123 RVA: 0x000AC110 File Offset: 0x000AA310
			public Dictionary<string, mount> mount_info
			{
				get
				{
					return this._mount_info;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mount_info = value;
				}
			}

			// Token: 0x17000B28 RID: 2856
			// (get) Token: 0x0600278C RID: 10124 RVA: 0x000AC128 File Offset: 0x000AA328
			public bool HasMount_info
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B29 RID: 2857
			// (get) Token: 0x0600278D RID: 10125 RVA: 0x000AC138 File Offset: 0x000AA338
			// (set) Token: 0x0600278E RID: 10126 RVA: 0x000AC140 File Offset: 0x000AA340
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._ID = value;
				}
			}

			// Token: 0x17000B2A RID: 2858
			// (get) Token: 0x0600278F RID: 10127 RVA: 0x000AC158 File Offset: 0x000AA358
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002790 RID: 10128 RVA: 0x000AC168 File Offset: 0x000AA368
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
							this.ID = this.deserialize.read_string();
						}
					}
					else
					{
						this.mount_info = this.deserialize.read_map<string, mount>((mount v) => v.ID);
					}
				}
			}

			// Token: 0x06002791 RID: 10129 RVA: 0x000AC1FC File Offset: 0x000AA3FC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, mount>(this.mount_info, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_string(this.ID, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D40 RID: 7488
			private static int max_field_count = 2;

			// Token: 0x04001D41 RID: 7489
			private Dictionary<string, mount> _mount_info;

			// Token: 0x04001D42 RID: 7490
			private string _ID;
		}
	}
}

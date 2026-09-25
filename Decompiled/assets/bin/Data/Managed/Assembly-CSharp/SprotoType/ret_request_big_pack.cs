using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000565 RID: 1381
	public class ret_request_big_pack
	{
		// Token: 0x02000566 RID: 1382
		public class request : SprotoTypeBase
		{
			// Token: 0x0600281D RID: 10269 RVA: 0x000AD348 File Offset: 0x000AB548
			public request() : base(ret_request_big_pack.request.max_field_count)
			{
			}

			// Token: 0x0600281E RID: 10270 RVA: 0x000AD358 File Offset: 0x000AB558
			public request(byte[] buffer) : base(ret_request_big_pack.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B57 RID: 2903
			// (get) Token: 0x06002820 RID: 10272 RVA: 0x000AD374 File Offset: 0x000AB574
			// (set) Token: 0x06002821 RID: 10273 RVA: 0x000AD37C File Offset: 0x000AB57C
			public string ID
			{
				get
				{
					return this._ID;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._ID = value;
				}
			}

			// Token: 0x17000B58 RID: 2904
			// (get) Token: 0x06002822 RID: 10274 RVA: 0x000AD394 File Offset: 0x000AB594
			public bool HasID
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B59 RID: 2905
			// (get) Token: 0x06002823 RID: 10275 RVA: 0x000AD3A4 File Offset: 0x000AB5A4
			// (set) Token: 0x06002824 RID: 10276 RVA: 0x000AD3AC File Offset: 0x000AB5AC
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._state = value;
				}
			}

			// Token: 0x17000B5A RID: 2906
			// (get) Token: 0x06002825 RID: 10277 RVA: 0x000AD3C4 File Offset: 0x000AB5C4
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000B5B RID: 2907
			// (get) Token: 0x06002826 RID: 10278 RVA: 0x000AD3D4 File Offset: 0x000AB5D4
			// (set) Token: 0x06002827 RID: 10279 RVA: 0x000AD3DC File Offset: 0x000AB5DC
			public long end_time
			{
				get
				{
					return this._end_time;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._end_time = value;
				}
			}

			// Token: 0x17000B5C RID: 2908
			// (get) Token: 0x06002828 RID: 10280 RVA: 0x000AD3F4 File Offset: 0x000AB5F4
			public bool HasEnd_time
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000B5D RID: 2909
			// (get) Token: 0x06002829 RID: 10281 RVA: 0x000AD404 File Offset: 0x000AB604
			// (set) Token: 0x0600282A RID: 10282 RVA: 0x000AD40C File Offset: 0x000AB60C
			public Dictionary<string, special_big_pack> special_big_packs
			{
				get
				{
					return this._special_big_packs;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._special_big_packs = value;
				}
			}

			// Token: 0x17000B5E RID: 2910
			// (get) Token: 0x0600282B RID: 10283 RVA: 0x000AD424 File Offset: 0x000AB624
			public bool HasSpecial_big_packs
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x0600282C RID: 10284 RVA: 0x000AD434 File Offset: 0x000AB634
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.ID = this.deserialize.read_string();
						break;
					case 1:
						this.state = this.deserialize.read_integer();
						break;
					case 2:
						this.end_time = this.deserialize.read_integer();
						break;
					case 3:
						this.special_big_packs = this.deserialize.read_map<string, special_big_pack>((special_big_pack v) => v.ID);
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600282D RID: 10285 RVA: 0x000AD500 File Offset: 0x000AB700
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.ID, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.end_time, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_obj<string, special_big_pack>(this.special_big_packs, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D6A RID: 7530
			private static int max_field_count = 4;

			// Token: 0x04001D6B RID: 7531
			private string _ID;

			// Token: 0x04001D6C RID: 7532
			private long _state;

			// Token: 0x04001D6D RID: 7533
			private long _end_time;

			// Token: 0x04001D6E RID: 7534
			private Dictionary<string, special_big_pack> _special_big_packs;
		}
	}
}

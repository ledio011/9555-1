using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000605 RID: 1541
	public class sync_dance_state_info
	{
		// Token: 0x02000606 RID: 1542
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CAF RID: 11439 RVA: 0x000B6634 File Offset: 0x000B4834
			public request() : base(sync_dance_state_info.request.max_field_count)
			{
			}

			// Token: 0x06002CB0 RID: 11440 RVA: 0x000B6644 File Offset: 0x000B4844
			public request(byte[] buffer) : base(sync_dance_state_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D05 RID: 3333
			// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000B6660 File Offset: 0x000B4860
			// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x000B6668 File Offset: 0x000B4868
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._state = value;
				}
			}

			// Token: 0x17000D06 RID: 3334
			// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000B6680 File Offset: 0x000B4880
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D07 RID: 3335
			// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x000B6690 File Offset: 0x000B4890
			// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x000B6698 File Offset: 0x000B4898
			public long open
			{
				get
				{
					return this._open;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._open = value;
				}
			}

			// Token: 0x17000D08 RID: 3336
			// (get) Token: 0x06002CB7 RID: 11447 RVA: 0x000B66B0 File Offset: 0x000B48B0
			public bool HasOpen
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000D09 RID: 3337
			// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000B66C0 File Offset: 0x000B48C0
			// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000B66C8 File Offset: 0x000B48C8
			public Dictionary<long, dance_state_info> dance_state_info
			{
				get
				{
					return this._dance_state_info;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._dance_state_info = value;
				}
			}

			// Token: 0x17000D0A RID: 3338
			// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000B66E0 File Offset: 0x000B48E0
			public bool HasDance_state_info
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002CBB RID: 11451 RVA: 0x000B66F0 File Offset: 0x000B48F0
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.state = this.deserialize.read_integer();
						break;
					case 1:
						this.open = this.deserialize.read_integer();
						break;
					case 2:
						this.dance_state_info = this.deserialize.read_map<long, dance_state_info>((dance_state_info v) => v.uuid);
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002CBC RID: 11452 RVA: 0x000B67A0 File Offset: 0x000B49A0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.open, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_obj<long, dance_state_info>(this.dance_state_info, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EAF RID: 7855
			private static int max_field_count = 3;

			// Token: 0x04001EB0 RID: 7856
			private long _state;

			// Token: 0x04001EB1 RID: 7857
			private long _open;

			// Token: 0x04001EB2 RID: 7858
			private Dictionary<long, dance_state_info> _dance_state_info;
		}
	}
}

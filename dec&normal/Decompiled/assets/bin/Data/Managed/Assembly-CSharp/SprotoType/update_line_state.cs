using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000642 RID: 1602
	public class update_line_state
	{
		// Token: 0x02000643 RID: 1603
		public class request : SprotoTypeBase
		{
			// Token: 0x06002E6D RID: 11885 RVA: 0x000B9DF0 File Offset: 0x000B7FF0
			public request() : base(update_line_state.request.max_field_count)
			{
			}

			// Token: 0x06002E6E RID: 11886 RVA: 0x000B9E00 File Offset: 0x000B8000
			public request(byte[] buffer) : base(update_line_state.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DA7 RID: 3495
			// (get) Token: 0x06002E70 RID: 11888 RVA: 0x000B9E1C File Offset: 0x000B801C
			// (set) Token: 0x06002E71 RID: 11889 RVA: 0x000B9E24 File Offset: 0x000B8024
			public string mapInfoId
			{
				get
				{
					return this._mapInfoId;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._mapInfoId = value;
				}
			}

			// Token: 0x17000DA8 RID: 3496
			// (get) Token: 0x06002E72 RID: 11890 RVA: 0x000B9E3C File Offset: 0x000B803C
			public bool HasMapInfoId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000DA9 RID: 3497
			// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000B9E4C File Offset: 0x000B804C
			// (set) Token: 0x06002E74 RID: 11892 RVA: 0x000B9E54 File Offset: 0x000B8054
			public long line_count
			{
				get
				{
					return this._line_count;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._line_count = value;
				}
			}

			// Token: 0x17000DAA RID: 3498
			// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000B9E6C File Offset: 0x000B806C
			public bool HasLine_count
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000DAB RID: 3499
			// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000B9E7C File Offset: 0x000B807C
			// (set) Token: 0x06002E77 RID: 11895 RVA: 0x000B9E84 File Offset: 0x000B8084
			public List<long> line_states
			{
				get
				{
					return this._line_states;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._line_states = value;
				}
			}

			// Token: 0x17000DAC RID: 3500
			// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000B9E9C File Offset: 0x000B809C
			public bool HasLine_states
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002E79 RID: 11897 RVA: 0x000B9EAC File Offset: 0x000B80AC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.mapInfoId = this.deserialize.read_string();
						break;
					case 1:
						this.line_count = this.deserialize.read_integer();
						break;
					case 2:
						this.line_states = this.deserialize.read_integer_list();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002E7A RID: 11898 RVA: 0x000B9F40 File Offset: 0x000B8140
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.mapInfoId, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.line_count, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.line_states, 2);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F28 RID: 7976
			private static int max_field_count = 3;

			// Token: 0x04001F29 RID: 7977
			private string _mapInfoId;

			// Token: 0x04001F2A RID: 7978
			private long _line_count;

			// Token: 0x04001F2B RID: 7979
			private List<long> _line_states;
		}
	}
}

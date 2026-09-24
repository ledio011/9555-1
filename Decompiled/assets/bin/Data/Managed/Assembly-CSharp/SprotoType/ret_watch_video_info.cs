using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005AD RID: 1453
	public class ret_watch_video_info
	{
		// Token: 0x020005AE RID: 1454
		public class request : SprotoTypeBase
		{
			// Token: 0x060029EF RID: 10735 RVA: 0x000B0CE0 File Offset: 0x000AEEE0
			public request() : base(ret_watch_video_info.request.max_field_count)
			{
			}

			// Token: 0x060029F0 RID: 10736 RVA: 0x000B0CF0 File Offset: 0x000AEEF0
			public request(byte[] buffer) : base(ret_watch_video_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000BF1 RID: 3057
			// (get) Token: 0x060029F2 RID: 10738 RVA: 0x000B0D0C File Offset: 0x000AEF0C
			// (set) Token: 0x060029F3 RID: 10739 RVA: 0x000B0D14 File Offset: 0x000AEF14
			public long cur_times
			{
				get
				{
					return this._cur_times;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._cur_times = value;
				}
			}

			// Token: 0x17000BF2 RID: 3058
			// (get) Token: 0x060029F4 RID: 10740 RVA: 0x000B0D2C File Offset: 0x000AEF2C
			public bool HasCur_times
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000BF3 RID: 3059
			// (get) Token: 0x060029F5 RID: 10741 RVA: 0x000B0D3C File Offset: 0x000AEF3C
			// (set) Token: 0x060029F6 RID: 10742 RVA: 0x000B0D44 File Offset: 0x000AEF44
			public long max_times
			{
				get
				{
					return this._max_times;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._max_times = value;
				}
			}

			// Token: 0x17000BF4 RID: 3060
			// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000B0D5C File Offset: 0x000AEF5C
			public bool HasMax_times
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000BF5 RID: 3061
			// (get) Token: 0x060029F8 RID: 10744 RVA: 0x000B0D6C File Offset: 0x000AEF6C
			// (set) Token: 0x060029F9 RID: 10745 RVA: 0x000B0D74 File Offset: 0x000AEF74
			public long every_time
			{
				get
				{
					return this._every_time;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._every_time = value;
				}
			}

			// Token: 0x17000BF6 RID: 3062
			// (get) Token: 0x060029FA RID: 10746 RVA: 0x000B0D8C File Offset: 0x000AEF8C
			public bool HasEvery_time
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000BF7 RID: 3063
			// (get) Token: 0x060029FB RID: 10747 RVA: 0x000B0D9C File Offset: 0x000AEF9C
			// (set) Token: 0x060029FC RID: 10748 RVA: 0x000B0DA4 File Offset: 0x000AEFA4
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._state = value;
				}
			}

			// Token: 0x17000BF8 RID: 3064
			// (get) Token: 0x060029FD RID: 10749 RVA: 0x000B0DBC File Offset: 0x000AEFBC
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x060029FE RID: 10750 RVA: 0x000B0DCC File Offset: 0x000AEFCC
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.cur_times = this.deserialize.read_integer();
						break;
					case 1:
						this.max_times = this.deserialize.read_integer();
						break;
					case 2:
						this.every_time = this.deserialize.read_integer();
						break;
					case 3:
						this.state = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x060029FF RID: 10751 RVA: 0x000B0E78 File Offset: 0x000AF078
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.cur_times, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.max_times, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.every_time, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.state, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001DEE RID: 7662
			private static int max_field_count = 4;

			// Token: 0x04001DEF RID: 7663
			private long _cur_times;

			// Token: 0x04001DF0 RID: 7664
			private long _max_times;

			// Token: 0x04001DF1 RID: 7665
			private long _every_time;

			// Token: 0x04001DF2 RID: 7666
			private long _state;
		}
	}
}

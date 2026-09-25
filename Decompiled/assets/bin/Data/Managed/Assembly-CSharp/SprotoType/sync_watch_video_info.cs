using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000614 RID: 1556
	public class sync_watch_video_info
	{
		// Token: 0x02000615 RID: 1557
		public class request : SprotoTypeBase
		{
			// Token: 0x06002D10 RID: 11536 RVA: 0x000B7230 File Offset: 0x000B5430
			public request() : base(sync_watch_video_info.request.max_field_count)
			{
			}

			// Token: 0x06002D11 RID: 11537 RVA: 0x000B7240 File Offset: 0x000B5440
			public request(byte[] buffer) : base(sync_watch_video_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D23 RID: 3363
			// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000B725C File Offset: 0x000B545C
			// (set) Token: 0x06002D14 RID: 11540 RVA: 0x000B7264 File Offset: 0x000B5464
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

			// Token: 0x17000D24 RID: 3364
			// (get) Token: 0x06002D15 RID: 11541 RVA: 0x000B727C File Offset: 0x000B547C
			public bool HasCur_times
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000D25 RID: 3365
			// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000B728C File Offset: 0x000B548C
			// (set) Token: 0x06002D17 RID: 11543 RVA: 0x000B7294 File Offset: 0x000B5494
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

			// Token: 0x17000D26 RID: 3366
			// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000B72AC File Offset: 0x000B54AC
			public bool HasMax_times
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000D27 RID: 3367
			// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000B72BC File Offset: 0x000B54BC
			// (set) Token: 0x06002D1A RID: 11546 RVA: 0x000B72C4 File Offset: 0x000B54C4
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

			// Token: 0x17000D28 RID: 3368
			// (get) Token: 0x06002D1B RID: 11547 RVA: 0x000B72DC File Offset: 0x000B54DC
			public bool HasEvery_time
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x06002D1C RID: 11548 RVA: 0x000B72EC File Offset: 0x000B54EC
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
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x06002D1D RID: 11549 RVA: 0x000B7380 File Offset: 0x000B5580
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
				return this.serialize.close();
			}

			// Token: 0x04001ECB RID: 7883
			private static int max_field_count = 3;

			// Token: 0x04001ECC RID: 7884
			private long _cur_times;

			// Token: 0x04001ECD RID: 7885
			private long _max_times;

			// Token: 0x04001ECE RID: 7886
			private long _every_time;
		}
	}
}

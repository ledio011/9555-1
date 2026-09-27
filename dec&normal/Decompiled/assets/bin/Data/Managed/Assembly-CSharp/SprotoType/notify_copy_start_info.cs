using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000435 RID: 1077
	public class notify_copy_start_info
	{
		// Token: 0x02000436 RID: 1078
		public class request : SprotoTypeBase
		{
			// Token: 0x0600214E RID: 8526 RVA: 0x000A03DC File Offset: 0x0009E5DC
			public request() : base(notify_copy_start_info.request.max_field_count)
			{
			}

			// Token: 0x0600214F RID: 8527 RVA: 0x000A03EC File Offset: 0x0009E5EC
			public request(byte[] buffer) : base(notify_copy_start_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000941 RID: 2369
			// (get) Token: 0x06002151 RID: 8529 RVA: 0x000A0408 File Offset: 0x0009E608
			// (set) Token: 0x06002152 RID: 8530 RVA: 0x000A0410 File Offset: 0x0009E610
			public long end_time
			{
				get
				{
					return this._end_time;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._end_time = value;
				}
			}

			// Token: 0x17000942 RID: 2370
			// (get) Token: 0x06002153 RID: 8531 RVA: 0x000A0428 File Offset: 0x0009E628
			public bool HasEnd_time
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000943 RID: 2371
			// (get) Token: 0x06002154 RID: 8532 RVA: 0x000A0438 File Offset: 0x0009E638
			// (set) Token: 0x06002155 RID: 8533 RVA: 0x000A0440 File Offset: 0x0009E640
			public long type
			{
				get
				{
					return this._type;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._type = value;
				}
			}

			// Token: 0x17000944 RID: 2372
			// (get) Token: 0x06002156 RID: 8534 RVA: 0x000A0458 File Offset: 0x0009E658
			public bool HasType
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x17000945 RID: 2373
			// (get) Token: 0x06002157 RID: 8535 RVA: 0x000A0468 File Offset: 0x0009E668
			// (set) Token: 0x06002158 RID: 8536 RVA: 0x000A0470 File Offset: 0x0009E670
			public long wave_time
			{
				get
				{
					return this._wave_time;
				}
				set
				{
					this.has_field.set_field(2, true);
					this._wave_time = value;
				}
			}

			// Token: 0x17000946 RID: 2374
			// (get) Token: 0x06002159 RID: 8537 RVA: 0x000A0488 File Offset: 0x0009E688
			public bool HasWave_time
			{
				get
				{
					return this.has_field.has_field(2);
				}
			}

			// Token: 0x17000947 RID: 2375
			// (get) Token: 0x0600215A RID: 8538 RVA: 0x000A0498 File Offset: 0x0009E698
			// (set) Token: 0x0600215B RID: 8539 RVA: 0x000A04A0 File Offset: 0x0009E6A0
			public long curWave
			{
				get
				{
					return this._curWave;
				}
				set
				{
					this.has_field.set_field(3, true);
					this._curWave = value;
				}
			}

			// Token: 0x17000948 RID: 2376
			// (get) Token: 0x0600215C RID: 8540 RVA: 0x000A04B8 File Offset: 0x0009E6B8
			public bool HasCurWave
			{
				get
				{
					return this.has_field.has_field(3);
				}
			}

			// Token: 0x0600215D RID: 8541 RVA: 0x000A04C8 File Offset: 0x0009E6C8
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					switch (num)
					{
					case 0:
						this.end_time = this.deserialize.read_integer();
						break;
					case 1:
						this.type = this.deserialize.read_integer();
						break;
					case 2:
						this.wave_time = this.deserialize.read_integer();
						break;
					case 3:
						this.curWave = this.deserialize.read_integer();
						break;
					default:
						this.deserialize.read_unknow_data();
						break;
					}
				}
			}

			// Token: 0x0600215E RID: 8542 RVA: 0x000A0574 File Offset: 0x0009E774
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.end_time, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.type, 1);
				}
				if (this.has_field.has_field(2))
				{
					this.serialize.write_integer(this.wave_time, 2);
				}
				if (this.has_field.has_field(3))
				{
					this.serialize.write_integer(this.curWave, 3);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BB2 RID: 7090
			private static int max_field_count = 4;

			// Token: 0x04001BB3 RID: 7091
			private long _end_time;

			// Token: 0x04001BB4 RID: 7092
			private long _type;

			// Token: 0x04001BB5 RID: 7093
			private long _wave_time;

			// Token: 0x04001BB6 RID: 7094
			private long _curWave;
		}
	}
}

using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000668 RID: 1640
	public class watch_video_info
	{
		// Token: 0x02000669 RID: 1641
		public class request : SprotoTypeBase
		{
			// Token: 0x06002F77 RID: 12151 RVA: 0x000BBF04 File Offset: 0x000BA104
			public request() : base(watch_video_info.request.max_field_count)
			{
			}

			// Token: 0x06002F78 RID: 12152 RVA: 0x000BBF14 File Offset: 0x000BA114
			public request(byte[] buffer) : base(watch_video_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000E07 RID: 3591
			// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000BBF30 File Offset: 0x000BA130
			// (set) Token: 0x06002F7B RID: 12155 RVA: 0x000BBF38 File Offset: 0x000BA138
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

			// Token: 0x17000E08 RID: 3592
			// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000BBF50 File Offset: 0x000BA150
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002F7D RID: 12157 RVA: 0x000BBF60 File Offset: 0x000BA160
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
						this.state = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002F7E RID: 12158 RVA: 0x000BBFBC File Offset: 0x000BA1BC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.state, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F6D RID: 8045
			private static int max_field_count = 1;

			// Token: 0x04001F6E RID: 8046
			private long _state;
		}
	}
}

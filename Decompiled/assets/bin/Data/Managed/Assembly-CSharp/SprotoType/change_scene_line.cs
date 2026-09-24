using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000334 RID: 820
	public class change_scene_line
	{
		// Token: 0x02000335 RID: 821
		public class request : SprotoTypeBase
		{
			// Token: 0x06001784 RID: 6020 RVA: 0x0008BF08 File Offset: 0x0008A108
			public request() : base(change_scene_line.request.max_field_count)
			{
			}

			// Token: 0x06001785 RID: 6021 RVA: 0x0008BF18 File Offset: 0x0008A118
			public request(byte[] buffer) : base(change_scene_line.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000511 RID: 1297
			// (get) Token: 0x06001787 RID: 6023 RVA: 0x0008BF34 File Offset: 0x0008A134
			// (set) Token: 0x06001788 RID: 6024 RVA: 0x0008BF3C File Offset: 0x0008A13C
			public long line_index
			{
				get
				{
					return this._line_index;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._line_index = value;
				}
			}

			// Token: 0x17000512 RID: 1298
			// (get) Token: 0x06001789 RID: 6025 RVA: 0x0008BF54 File Offset: 0x0008A154
			public bool HasLine_index
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x0600178A RID: 6026 RVA: 0x0008BF64 File Offset: 0x0008A164
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
						this.line_index = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x0600178B RID: 6027 RVA: 0x0008BFC0 File Offset: 0x0008A1C0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.line_index, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x040018ED RID: 6381
			private static int max_field_count = 1;

			// Token: 0x040018EE RID: 6382
			private long _line_index;
		}
	}
}

using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200048C RID: 1164
	public class req_other_team
	{
		// Token: 0x0200048D RID: 1165
		public class request : SprotoTypeBase
		{
			// Token: 0x06002380 RID: 9088 RVA: 0x000A4804 File Offset: 0x000A2A04
			public request() : base(req_other_team.request.max_field_count)
			{
			}

			// Token: 0x06002381 RID: 9089 RVA: 0x000A4814 File Offset: 0x000A2A14
			public request(byte[] buffer) : base(req_other_team.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000A03 RID: 2563
			// (get) Token: 0x06002383 RID: 9091 RVA: 0x000A4830 File Offset: 0x000A2A30
			// (set) Token: 0x06002384 RID: 9092 RVA: 0x000A4838 File Offset: 0x000A2A38
			public long id
			{
				get
				{
					return this._id;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._id = value;
				}
			}

			// Token: 0x17000A04 RID: 2564
			// (get) Token: 0x06002385 RID: 9093 RVA: 0x000A4850 File Offset: 0x000A2A50
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002386 RID: 9094 RVA: 0x000A4860 File Offset: 0x000A2A60
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
						this.id = this.deserialize.read_integer();
					}
				}
			}

			// Token: 0x06002387 RID: 9095 RVA: 0x000A48BC File Offset: 0x000A2ABC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_integer(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001C41 RID: 7233
			private static int max_field_count = 1;

			// Token: 0x04001C42 RID: 7234
			private long _id;
		}
	}
}

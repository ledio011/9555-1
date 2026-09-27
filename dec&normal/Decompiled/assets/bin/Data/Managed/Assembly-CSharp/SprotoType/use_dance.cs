using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000657 RID: 1623
	public class use_dance
	{
		// Token: 0x02000658 RID: 1624
		public class request : SprotoTypeBase
		{
			// Token: 0x06002EE7 RID: 12007 RVA: 0x000BACB0 File Offset: 0x000B8EB0
			public request() : base(use_dance.request.max_field_count)
			{
			}

			// Token: 0x06002EE8 RID: 12008 RVA: 0x000BACC0 File Offset: 0x000B8EC0
			public request(byte[] buffer) : base(use_dance.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DCD RID: 3533
			// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000BACDC File Offset: 0x000B8EDC
			// (set) Token: 0x06002EEB RID: 12011 RVA: 0x000BACE4 File Offset: 0x000B8EE4
			public string id
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

			// Token: 0x17000DCE RID: 3534
			// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000BACFC File Offset: 0x000B8EFC
			public bool HasId
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002EED RID: 12013 RVA: 0x000BAD0C File Offset: 0x000B8F0C
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
						this.id = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002EEE RID: 12014 RVA: 0x000BAD68 File Offset: 0x000B8F68
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.id, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F46 RID: 8006
			private static int max_field_count = 1;

			// Token: 0x04001F47 RID: 8007
			private string _id;
		}
	}
}

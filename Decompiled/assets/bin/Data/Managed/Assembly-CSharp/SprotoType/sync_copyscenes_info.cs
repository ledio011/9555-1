using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000603 RID: 1539
	public class sync_copyscenes_info
	{
		// Token: 0x02000604 RID: 1540
		public class request : SprotoTypeBase
		{
			// Token: 0x06002CA5 RID: 11429 RVA: 0x000B6508 File Offset: 0x000B4708
			public request() : base(sync_copyscenes_info.request.max_field_count)
			{
			}

			// Token: 0x06002CA6 RID: 11430 RVA: 0x000B6518 File Offset: 0x000B4718
			public request(byte[] buffer) : base(sync_copyscenes_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000D03 RID: 3331
			// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000B6534 File Offset: 0x000B4734
			// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x000B653C File Offset: 0x000B473C
			public Dictionary<string, copyscene_info> copyscenes
			{
				get
				{
					return this._copyscenes;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._copyscenes = value;
				}
			}

			// Token: 0x17000D04 RID: 3332
			// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000B6554 File Offset: 0x000B4754
			public bool HasCopyscenes
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002CAB RID: 11435 RVA: 0x000B6564 File Offset: 0x000B4764
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
						this.copyscenes = this.deserialize.read_map<string, copyscene_info>((copyscene_info v) => v.ID);
					}
				}
			}

			// Token: 0x06002CAC RID: 11436 RVA: 0x000B65DC File Offset: 0x000B47DC
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, copyscene_info>(this.copyscenes, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001EAC RID: 7852
			private static int max_field_count = 1;

			// Token: 0x04001EAD RID: 7853
			private Dictionary<string, copyscene_info> _copyscenes;
		}
	}
}

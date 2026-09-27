using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200065F RID: 1631
	public class use_skill_buff
	{
		// Token: 0x02000660 RID: 1632
		public class request : SprotoTypeBase
		{
			// Token: 0x06002F14 RID: 12052 RVA: 0x000BB210 File Offset: 0x000B9410
			public request() : base(use_skill_buff.request.max_field_count)
			{
			}

			// Token: 0x06002F15 RID: 12053 RVA: 0x000BB220 File Offset: 0x000B9420
			public request(byte[] buffer) : base(use_skill_buff.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000DDB RID: 3547
			// (get) Token: 0x06002F17 RID: 12055 RVA: 0x000BB23C File Offset: 0x000B943C
			// (set) Token: 0x06002F18 RID: 12056 RVA: 0x000BB244 File Offset: 0x000B9444
			public List<buff> buffs
			{
				get
				{
					return this._buffs;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._buffs = value;
				}
			}

			// Token: 0x17000DDC RID: 3548
			// (get) Token: 0x06002F19 RID: 12057 RVA: 0x000BB25C File Offset: 0x000B945C
			public bool HasBuffs
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002F1A RID: 12058 RVA: 0x000BB26C File Offset: 0x000B946C
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
						this.buffs = this.deserialize.read_obj_list<buff>();
					}
				}
			}

			// Token: 0x06002F1B RID: 12059 RVA: 0x000BB2C8 File Offset: 0x000B94C8
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<buff>(this.buffs, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001F51 RID: 8017
			private static int max_field_count = 1;

			// Token: 0x04001F52 RID: 8018
			private List<buff> _buffs;
		}
	}
}

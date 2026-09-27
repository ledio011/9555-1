using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x020002E2 RID: 738
	public class accept_damge
	{
		// Token: 0x020002E3 RID: 739
		public class request : SprotoTypeBase
		{
			// Token: 0x060014A5 RID: 5285 RVA: 0x00086050 File Offset: 0x00084250
			public request() : base(accept_damge.request.max_field_count)
			{
			}

			// Token: 0x060014A6 RID: 5286 RVA: 0x00086060 File Offset: 0x00084260
			public request(byte[] buffer) : base(accept_damge.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x170003DB RID: 987
			// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0008607C File Offset: 0x0008427C
			// (set) Token: 0x060014A9 RID: 5289 RVA: 0x00086084 File Offset: 0x00084284
			public List<acceptdamge> damges
			{
				get
				{
					return this._damges;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._damges = value;
				}
			}

			// Token: 0x170003DC RID: 988
			// (get) Token: 0x060014AA RID: 5290 RVA: 0x0008609C File Offset: 0x0008429C
			public bool HasDamges
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060014AB RID: 5291 RVA: 0x000860AC File Offset: 0x000842AC
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
						this.damges = this.deserialize.read_obj_list<acceptdamge>();
					}
				}
			}

			// Token: 0x060014AC RID: 5292 RVA: 0x00086108 File Offset: 0x00084308
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<acceptdamge>(this.damges, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001823 RID: 6179
			private static int max_field_count = 1;

			// Token: 0x04001824 RID: 6180
			private List<acceptdamge> _damges;
		}
	}
}

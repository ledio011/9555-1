using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000438 RID: 1080
	public class npc_create
	{
		// Token: 0x02000439 RID: 1081
		public class request : SprotoTypeBase
		{
			// Token: 0x060021B9 RID: 8633 RVA: 0x000A12D8 File Offset: 0x0009F4D8
			public request() : base(npc_create.request.max_field_count)
			{
			}

			// Token: 0x060021BA RID: 8634 RVA: 0x000A12E8 File Offset: 0x0009F4E8
			public request(byte[] buffer) : base(npc_create.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x060021BC RID: 8636 RVA: 0x000A1304 File Offset: 0x0009F504
			// (set) Token: 0x060021BD RID: 8637 RVA: 0x000A130C File Offset: 0x0009F50C
			public npc_attribute npc_attribute
			{
				get
				{
					return this._npc_attribute;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._npc_attribute = value;
				}
			}

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x060021BE RID: 8638 RVA: 0x000A1324 File Offset: 0x0009F524
			public bool HasNpc_attribute
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x060021BF RID: 8639 RVA: 0x000A1334 File Offset: 0x0009F534
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
						this.npc_attribute = this.deserialize.read_obj<npc_attribute>();
					}
				}
			}

			// Token: 0x060021C0 RID: 8640 RVA: 0x000A1390 File Offset: 0x0009F590
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj(this.npc_attribute, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001BD4 RID: 7124
			private static int max_field_count = 1;

			// Token: 0x04001BD5 RID: 7125
			private npc_attribute _npc_attribute;
		}
	}
}

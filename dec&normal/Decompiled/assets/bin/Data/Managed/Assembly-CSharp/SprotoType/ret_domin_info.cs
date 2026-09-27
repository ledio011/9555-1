using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000517 RID: 1303
	public class ret_domin_info
	{
		// Token: 0x02000518 RID: 1304
		public class request : SprotoTypeBase
		{
			// Token: 0x0600261F RID: 9759 RVA: 0x000A9434 File Offset: 0x000A7634
			public request() : base(ret_domin_info.request.max_field_count)
			{
			}

			// Token: 0x06002620 RID: 9760 RVA: 0x000A9444 File Offset: 0x000A7644
			public request(byte[] buffer) : base(ret_domin_info.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x06002622 RID: 9762 RVA: 0x000A9460 File Offset: 0x000A7660
			// (set) Token: 0x06002623 RID: 9763 RVA: 0x000A9468 File Offset: 0x000A7668
			public Dictionary<string, domin_info> domin_infos
			{
				get
				{
					return this._domin_infos;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._domin_infos = value;
				}
			}

			// Token: 0x17000AAA RID: 2730
			// (get) Token: 0x06002624 RID: 9764 RVA: 0x000A9480 File Offset: 0x000A7680
			public bool HasDomin_infos
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000AAB RID: 2731
			// (get) Token: 0x06002625 RID: 9765 RVA: 0x000A9490 File Offset: 0x000A7690
			// (set) Token: 0x06002626 RID: 9766 RVA: 0x000A9498 File Offset: 0x000A7698
			public Dictionary<long, character_look> characters
			{
				get
				{
					return this._characters;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._characters = value;
				}
			}

			// Token: 0x17000AAC RID: 2732
			// (get) Token: 0x06002627 RID: 9767 RVA: 0x000A94B0 File Offset: 0x000A76B0
			public bool HasCharacters
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x06002628 RID: 9768 RVA: 0x000A94C0 File Offset: 0x000A76C0
			protected override void decode()
			{
				int num;
				while ((num = this.deserialize.read_tag()) != -1)
				{
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							this.deserialize.read_unknow_data();
						}
						else
						{
							this.characters = this.deserialize.read_map<long, character_look>((character_look v) => v.id);
						}
					}
					else
					{
						this.domin_infos = this.deserialize.read_map<string, domin_info>((domin_info v) => v.id);
					}
				}
			}

			// Token: 0x06002629 RID: 9769 RVA: 0x000A9574 File Offset: 0x000A7774
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_obj<string, domin_info>(this.domin_infos, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_obj<long, character_look>(this.characters, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001CDD RID: 7389
			private static int max_field_count = 2;

			// Token: 0x04001CDE RID: 7390
			private Dictionary<string, domin_info> _domin_infos;

			// Token: 0x04001CDF RID: 7391
			private Dictionary<long, character_look> _characters;
		}
	}
}

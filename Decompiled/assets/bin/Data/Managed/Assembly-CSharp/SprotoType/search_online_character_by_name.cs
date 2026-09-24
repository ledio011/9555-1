using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020005BA RID: 1466
	public class search_online_character_by_name
	{
		// Token: 0x020005BB RID: 1467
		public class request : SprotoTypeBase
		{
			// Token: 0x06002A5B RID: 10843 RVA: 0x000B1AB0 File Offset: 0x000AFCB0
			public request() : base(search_online_character_by_name.request.max_field_count)
			{
			}

			// Token: 0x06002A5C RID: 10844 RVA: 0x000B1AC0 File Offset: 0x000AFCC0
			public request(byte[] buffer) : base(search_online_character_by_name.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000C1B RID: 3099
			// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000B1ADC File Offset: 0x000AFCDC
			// (set) Token: 0x06002A5F RID: 10847 RVA: 0x000B1AE4 File Offset: 0x000AFCE4
			public string name
			{
				get
				{
					return this._name;
				}
				set
				{
					this.has_field.set_field(0, true);
					this._name = value;
				}
			}

			// Token: 0x17000C1C RID: 3100
			// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000B1AFC File Offset: 0x000AFCFC
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x06002A61 RID: 10849 RVA: 0x000B1B0C File Offset: 0x000AFD0C
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
						this.name = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x06002A62 RID: 10850 RVA: 0x000B1B68 File Offset: 0x000AFD68
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.name, 0);
				}
				return this.serialize.close();
			}

			// Token: 0x04001E0B RID: 7691
			private static int max_field_count = 1;

			// Token: 0x04001E0C RID: 7692
			private string _name;
		}
	}
}

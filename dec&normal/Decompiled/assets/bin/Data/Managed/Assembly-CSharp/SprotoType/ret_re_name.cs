using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x0200055D RID: 1373
	public class ret_re_name
	{
		// Token: 0x0200055E RID: 1374
		public class request : SprotoTypeBase
		{
			// Token: 0x060027E2 RID: 10210 RVA: 0x000ACBDC File Offset: 0x000AADDC
			public request() : base(ret_re_name.request.max_field_count)
			{
			}

			// Token: 0x060027E3 RID: 10211 RVA: 0x000ACBEC File Offset: 0x000AADEC
			public request(byte[] buffer) : base(ret_re_name.request.max_field_count, buffer)
			{
				this.decode();
			}

			// Token: 0x17000B41 RID: 2881
			// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000ACC08 File Offset: 0x000AAE08
			// (set) Token: 0x060027E6 RID: 10214 RVA: 0x000ACC10 File Offset: 0x000AAE10
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

			// Token: 0x17000B42 RID: 2882
			// (get) Token: 0x060027E7 RID: 10215 RVA: 0x000ACC28 File Offset: 0x000AAE28
			public bool HasName
			{
				get
				{
					return this.has_field.has_field(0);
				}
			}

			// Token: 0x17000B43 RID: 2883
			// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000ACC38 File Offset: 0x000AAE38
			// (set) Token: 0x060027E9 RID: 10217 RVA: 0x000ACC40 File Offset: 0x000AAE40
			public long state
			{
				get
				{
					return this._state;
				}
				set
				{
					this.has_field.set_field(1, true);
					this._state = value;
				}
			}

			// Token: 0x17000B44 RID: 2884
			// (get) Token: 0x060027EA RID: 10218 RVA: 0x000ACC58 File Offset: 0x000AAE58
			public bool HasState
			{
				get
				{
					return this.has_field.has_field(1);
				}
			}

			// Token: 0x060027EB RID: 10219 RVA: 0x000ACC68 File Offset: 0x000AAE68
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
							this.state = this.deserialize.read_integer();
						}
					}
					else
					{
						this.name = this.deserialize.read_string();
					}
				}
			}

			// Token: 0x060027EC RID: 10220 RVA: 0x000ACCE0 File Offset: 0x000AAEE0
			public override int encode(SprotoStream stream)
			{
				this.serialize.open(stream);
				if (this.has_field.has_field(0))
				{
					this.serialize.write_string(this.name, 0);
				}
				if (this.has_field.has_field(1))
				{
					this.serialize.write_integer(this.state, 1);
				}
				return this.serialize.close();
			}

			// Token: 0x04001D59 RID: 7513
			private static int max_field_count = 2;

			// Token: 0x04001D5A RID: 7514
			private string _name;

			// Token: 0x04001D5B RID: 7515
			private long _state;
		}
	}
}

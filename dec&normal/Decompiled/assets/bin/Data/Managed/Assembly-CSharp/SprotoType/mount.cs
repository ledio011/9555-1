using System;
using System.Collections.Generic;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000418 RID: 1048
	public class mount : SprotoTypeBase
	{
		// Token: 0x06002083 RID: 8323 RVA: 0x0009EAA8 File Offset: 0x0009CCA8
		public mount() : base(mount.max_field_count)
		{
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0009EAB8 File Offset: 0x0009CCB8
		public mount(byte[] buffer) : base(mount.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x0009EAD4 File Offset: 0x0009CCD4
		// (set) Token: 0x06002087 RID: 8327 RVA: 0x0009EADC File Offset: 0x0009CCDC
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this.has_field.set_field(0, true);
				this._ID = value;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0009EAF4 File Offset: 0x0009CCF4
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x0009EB04 File Offset: 0x0009CD04
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x0009EB0C File Offset: 0x0009CD0C
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

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x0009EB24 File Offset: 0x0009CD24
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x0009EB34 File Offset: 0x0009CD34
		// (set) Token: 0x0600208D RID: 8333 RVA: 0x0009EB3C File Offset: 0x0009CD3C
		public Dictionary<string, color> colors
		{
			get
			{
				return this._colors;
			}
			set
			{
				this.has_field.set_field(2, true);
				this._colors = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0009EB54 File Offset: 0x0009CD54
		public bool HasColors
		{
			get
			{
				return this.has_field.has_field(2);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x0009EB64 File Offset: 0x0009CD64
		// (set) Token: 0x06002090 RID: 8336 RVA: 0x0009EB6C File Offset: 0x0009CD6C
		public string select
		{
			get
			{
				return this._select;
			}
			set
			{
				this.has_field.set_field(3, true);
				this._select = value;
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x0009EB84 File Offset: 0x0009CD84
		public bool HasSelect
		{
			get
			{
				return this.has_field.has_field(3);
			}
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0009EB94 File Offset: 0x0009CD94
		protected override void decode()
		{
			int num;
			while ((num = this.deserialize.read_tag()) != -1)
			{
				switch (num)
				{
				case 0:
					this.ID = this.deserialize.read_string();
					break;
				case 1:
					this.state = this.deserialize.read_integer();
					break;
				case 2:
					this.colors = this.deserialize.read_map<string, color>((color v) => v.ID);
					break;
				case 3:
					this.select = this.deserialize.read_string();
					break;
				default:
					this.deserialize.read_unknow_data();
					break;
				}
			}
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0009EC60 File Offset: 0x0009CE60
		public override int encode(SprotoStream stream)
		{
			this.serialize.open(stream);
			if (this.has_field.has_field(0))
			{
				this.serialize.write_string(this.ID, 0);
			}
			if (this.has_field.has_field(1))
			{
				this.serialize.write_integer(this.state, 1);
			}
			if (this.has_field.has_field(2))
			{
				this.serialize.write_obj<string, color>(this.colors, 2);
			}
			if (this.has_field.has_field(3))
			{
				this.serialize.write_string(this.select, 3);
			}
			return this.serialize.close();
		}

		// Token: 0x04001B7D RID: 7037
		private static int max_field_count = 4;

		// Token: 0x04001B7E RID: 7038
		private string _ID;

		// Token: 0x04001B7F RID: 7039
		private long _state;

		// Token: 0x04001B80 RID: 7040
		private Dictionary<string, color> _colors;

		// Token: 0x04001B81 RID: 7041
		private string _select;
	}
}

using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x02000405 RID: 1029
	public class level_pack : SprotoTypeBase
	{
		// Token: 0x06001FD1 RID: 8145 RVA: 0x0009D3AC File Offset: 0x0009B5AC
		public level_pack() : base(level_pack.max_field_count)
		{
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0009D3BC File Offset: 0x0009B5BC
		public level_pack(byte[] buffer) : base(level_pack.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0009D3D8 File Offset: 0x0009B5D8
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
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

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x0009D3F8 File Offset: 0x0009B5F8
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0009D408 File Offset: 0x0009B608
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x0009D410 File Offset: 0x0009B610
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

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0009D428 File Offset: 0x0009B628
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0009D438 File Offset: 0x0009B638
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
					this.ID = this.deserialize.read_string();
				}
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0009D4B0 File Offset: 0x0009B6B0
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
			return this.serialize.close();
		}

		// Token: 0x04001B4B RID: 6987
		private static int max_field_count = 2;

		// Token: 0x04001B4C RID: 6988
		private string _ID;

		// Token: 0x04001B4D RID: 6989
		private long _state;
	}
}

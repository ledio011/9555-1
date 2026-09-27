using System;
using Sproto;

namespace SprotoType
{
	// Token: 0x020003B4 RID: 948
	public class function_info : SprotoTypeBase
	{
		// Token: 0x06001CB3 RID: 7347 RVA: 0x00096C44 File Offset: 0x00094E44
		public function_info() : base(function_info.max_field_count)
		{
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00096C54 File Offset: 0x00094E54
		public function_info(byte[] buffer) : base(function_info.max_field_count, buffer)
		{
			this.decode();
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00096C70 File Offset: 0x00094E70
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x00096C78 File Offset: 0x00094E78
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

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00096C90 File Offset: 0x00094E90
		public bool HasID
		{
			get
			{
				return this.has_field.has_field(0);
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x00096CA0 File Offset: 0x00094EA0
		// (set) Token: 0x06001CBA RID: 7354 RVA: 0x00096CA8 File Offset: 0x00094EA8
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

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00096CC0 File Offset: 0x00094EC0
		public bool HasState
		{
			get
			{
				return this.has_field.has_field(1);
			}
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x00096CD0 File Offset: 0x00094ED0
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

		// Token: 0x06001CBD RID: 7357 RVA: 0x00096D48 File Offset: 0x00094F48
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

		// Token: 0x04001A6A RID: 6762
		private static int max_field_count = 2;

		// Token: 0x04001A6B RID: 6763
		private string _ID;

		// Token: 0x04001A6C RID: 6764
		private long _state;
	}
}

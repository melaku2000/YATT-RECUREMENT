namespace YattCommon.Enums.Extensions
{
	public class StringValue : Attribute
	{
		public string Value { get; private set; }

		public StringValue(string value)
		{
			Value = value;
		}
	}
}

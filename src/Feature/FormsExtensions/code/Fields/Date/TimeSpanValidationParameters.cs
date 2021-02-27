namespace Feature.FormsExtensions.Fields.Date
{
    public class TimeSpanValidationParameters
    {
        public string Unit { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public string ValidationType { get; set; }
    }
}
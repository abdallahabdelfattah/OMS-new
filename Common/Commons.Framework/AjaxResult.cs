namespace Commons.Framework
{
    public class AjaxResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public object Value { get; set; }
    }
}

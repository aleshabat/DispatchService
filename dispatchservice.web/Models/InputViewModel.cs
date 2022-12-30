
namespace dispatchservice.web.Models
{
    public class InputViewModel : HtmlElement
    {
        public enum InputType
        {
            TextBox,
            DatePicker,
            DateTimePicker,
            TimePicker
        }

        public InputViewModel(){}
        public InputViewModel(string name, string caption, string value, InputType type)
        {
            Name = name;
            Value = value;
            Type = type;
            Caption = caption;
        }

        public string Value { get; set; }
        public InputType Type { get; set; }

    }
}
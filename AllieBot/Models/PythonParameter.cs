namespace AllieBot.Models
{
    public class PythonParameter
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }

        public PythonParameter()
        {
            Name = "";
            Value = "";
        }

        public PythonParameter(string name, dynamic value)
        {
            Name = name;
            Value = value;
        }
    }
}

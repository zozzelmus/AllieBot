namespace AllieBot.Models
{
    public class PythonParamter
    {
        public string Name { get; set; }
        public dynamic Value { get; set; }

        public PythonParamter()
        {
            Name = "";
            Value = "";
        }

        public PythonParamter(string name, dynamic value)
        {
            Name = name;
            Value = value;
        }
    }
}

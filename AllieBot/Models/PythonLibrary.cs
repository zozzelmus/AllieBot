using Microsoft.Scripting.Hosting;

namespace AllieBot.Models
{
    public class PythonLibrary
    {
        public string Name { get; set; }
        public CompiledCode Code { get; set; }

        public PythonLibrary(string name, CompiledCode code)
        {
            Name = name;
            Code = code;
        }
    }
}

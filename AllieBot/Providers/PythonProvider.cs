using Python.Runtime;


namespace AllieBot.Providers
{
    public class PythonProvider
    {
        public string RunScript(string script)
        {
            Runtime.PythonDLL = @"../../Python311";
            PythonEngine.Initialize();

            using (Py.GIL())
            {
                var pythonScript = Py.Import("helloWorld");
                var result = pythonScript.InvokeMethod("hello");

                return result.ToString() ?? string.Empty;
            }
        }
    }
}

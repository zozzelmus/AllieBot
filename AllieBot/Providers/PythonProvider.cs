using Python.Runtime;


namespace AllieBot.Providers
{
    public class PythonProvider
    {
        public PythonProvider()
        {
            Runtime.PythonDLL = @"C:\Python311\python311.dll";
            PythonEngine.Initialize();
        }

        public string RunScript(string script)
        {
            using (Py.GIL())
            {
                var pythonScript = Py.Import("./PyScripts/helloWorld");
                var result = pythonScript.InvokeMethod("hello");

                return result.ToString() ?? string.Empty;
            }
        }
    }
}

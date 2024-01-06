using Python.Runtime;
using System.Security.Cryptography.X509Certificates;

namespace AllieBot.Providers
{
    public class PythonProvider
    {
        public PyObject pyOb;
        public PythonProvider()
        {
            Runtime.PythonDLL = "C:/Python311/python311.dll";
            PythonEngine.Initialize();
            var pyOb = Py.Import("helloWorld.py");
        }

        public async Task<string> RunScript(string script)
        {
            try
            {
                using (Py.GIL())
                {
                    var result = pyOb.InvokeMethod("hello");
                    return result.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
            }
        }
    }
}

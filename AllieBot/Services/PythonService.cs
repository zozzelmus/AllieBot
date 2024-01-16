using System;
using AllieBot.Models;
using Microsoft.Scripting.Hosting;

namespace AllieBot.Services
{
    public class PythonService
    {
        private ScriptEngine engine;
        List<PythonLibrary> pylibs;

        /// <summary>
        /// Constructor for a new python provider. Declared as singleton when project is created.
        /// </summary>
        public PythonService()
        {
            //create engine
            engine = IronPython.Hosting.Python.CreateEngine();

            //setup python context
            pylibs = new List<PythonLibrary>();
            foreach (string file in Directory.EnumerateFiles(@"Providers\pyScripts"))
            {
                var sourcepy = File.ReadAllText(file);
                pylibs.Add(new PythonLibrary(file.Split('.')[0].Split('\\')[2], engine.CreateScriptSourceFromString(sourcepy).Compile()));
            }
        }

        /// <summary>
        /// Used to return a variable from a manually inputed python script.
        /// </summary>
        /// <param name="code">Raw Script as String</param>
        /// <param name="vars">List of variables (Variable name, variable value)</param>
        /// <param name="retVarName">Name of the variable in which you want to return from</param>
        /// <returns>Stringified version of variable result from passed script.</returns>
        // EX of this method being used:
        // List<PythonParamter> pyPar = new List<PythonParamter>() 
        // {
        //     new PythonParamter()
        //     {
        //         Name = "x",
        //         Value = "5"
        //     },
        //     new PythonParamter()
        //     {
        //         Name= "y",
        //         Value = "6"
        //     }
        // };
        //Console.WriteLine(RunScriptRaw("final = str(int(x) + int(y))", pyPar, "final"));

        public dynamic RunScriptRaw(string code, List<PythonParameter> vars, string retVarName)
        {
            //create scope
            var scope = engine.CreateScope();

            //handles the parameters sent to the python script
            if (vars.Count > 0)
            {

                foreach (PythonParameter var in vars)
                {
                    scope.SetVariable(var.Name, var.Value);
                }
            }

            //execute script
            engine.CreateScriptSourceFromString(code).Execute(scope);

            //return execution
            return scope.GetVariable<dynamic>(retVarName);
        }

        public dynamic RunScript(string libName, List<PythonParameter> vars, string retVarName)
        {
            var scope = engine.CreateScope();

            if (vars.Count > 0)
            {

                foreach (PythonParameter var in vars)
                {
                    scope.SetVariable(var.Name, var.Value);
                }
            }

            var script = pylibs.Where(x => x.Name == libName).FirstOrDefault();

            if (script is null)
                throw new NullReferenceException("Could not find lib");
            try
            {
                script.Code.Execute(scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return scope.GetVariable<dynamic>(retVarName);
        }

    }
}

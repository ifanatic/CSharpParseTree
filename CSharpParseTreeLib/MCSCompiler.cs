using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Mono.CSharp;
using System.Reflection;

namespace CSharpParseTreeLib
{
    public class ClassDriverNotFoundException : ApplicationException
    {
        public ClassDriverNotFoundException() : base("ClassDriverNotFoundException") { }
        public ClassDriverNotFoundException(string message) : base(message) { }
    }
    
    public class MCSCompiler : ICompiler
    {
        private Assembly _dmcsAssembly = null;
        Type _driverType = null;
        ModuleContainer _treeRoot = null;


        public ModuleContainer TreeRoot
        {
            get { return _treeRoot; }
        }

        public bool SuccessfulCreated
        {
            get;
            private set;
        }

        private void ExtractTreeRoot()
        {
            RootContext rootContext = new RootContext();
            Type rootContextType = rootContext.GetType()/*ReflectionUtils.ExtractTypeByName(_dmcsAssembly, "Mono.CSharp.RootContext")*/;
            _treeRoot = (ModuleContainer)rootContextType.InvokeMember("root", BindingFlags.GetField | BindingFlags.Instance
                | BindingFlags.NonPublic | BindingFlags.Static, null, rootContext, null);
        }

        public MCSCompiler(string assemblyFileName)
        {
            _dmcsAssembly = Assembly.LoadFile(assemblyFileName);

            _driverType = ReflectionUtils.ExtractTypeByName(_dmcsAssembly, "Mono.CSharp.Driver");

            if (_driverType == null)
            {
                SuccessfulCreated = false;
                
                throw new ClassDriverNotFoundException(String.Format("В сборке \"{0}\" не найден тип драйвера компиляции", assemblyFileName));
            }

            SuccessfulCreated = true;
        }

        public bool Compile(string fileName)
        {
            EmptyReportPrinter report = new EmptyReportPrinter();
           
            return Compile(fileName, report);
        }

        public bool Compile(string fileName, ReportPrinter reportPrinter)
        {
            if (_driverType == null)
            {
                throw new ClassDriverNotFoundException();
            }

            CompilerCallableEntryPoint.Reset();

            string[] args = new string[] { fileName, "--parse" };

            object[] oargs = new object[] { args, false, reportPrinter };

            object driver = _driverType.InvokeMember("Create", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod,
                null, null, oargs);

            if (driver == null)
            {
                throw new ClassDriverNotFoundException("Не могу создать объект драйвера компиляции");
            }

            bool result = (bool)_driverType.InvokeMember("Compile", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                | BindingFlags.Instance, null, driver, null);
            if (!result || (reportPrinter.ErrorsCount > 0))
            {
                _treeRoot = null;
                return false;
            }

            ExtractTreeRoot();

            return true;
        }
    }
}

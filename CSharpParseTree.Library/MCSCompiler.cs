using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Mono.CSharp;
using System.Reflection;

namespace CSharpParseTree.Library
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
        ReportPrinter _reporter = new EmptyReportPrinter();

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

        public MCSCompiler(string assemblyFileName, ReportPrinter reporter)
        {
            // Загружаем сборку компилятора
            _dmcsAssembly = Assembly.LoadFile(assemblyFileName);

            // Пробуем получить тип драйвера компиляции
            _driverType = ReflectionUtils.ExtractTypeByName(_dmcsAssembly, "Mono.CSharp.Driver");

            // Если не удалось
            if (_driverType == null)
            {
                SuccessfulCreated = false;
                
                throw new ClassDriverNotFoundException(String.Format("В сборке \"{0}\" не найден тип драйвера компиляции", assemblyFileName));
            }

            SuccessfulCreated = true;

            _reporter = reporter;
        }

        public bool Compile(string fileName)
        {
            if (_driverType == null)
            {
                throw new ClassDriverNotFoundException();
            }

            // Сбрасываем внутреннее состояние компилятора
            CompilerCallableEntryPoint.Reset();

            // Формируем параметры командной строки
            // Первый - это имя файла, второй - что необходимо выполнить только разбор
            string[] args = new string[] { fileName, "--parse" };

            object[] oargs = new object[] { args, false, _reporter };

            // Создаем объект драйвера
            object driver = _driverType.InvokeMember("Create", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod,
                null, null, oargs);

            if (driver == null)
            {
                throw new ClassDriverNotFoundException("Не могу создать объект драйвера компиляции");
            }

            // И компилируем
            bool result = (bool)_driverType.InvokeMember("Compile", BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase
                | BindingFlags.Instance, null, driver, null);

            if (!result || (_reporter.ErrorsCount > 0))
            {
                _treeRoot = null;
                return false;
            }

            // Достаем дерево разбора
            ExtractTreeRoot();

            return true;
        }
    }
}

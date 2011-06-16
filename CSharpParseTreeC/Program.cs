using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using CSharpParseTree;
using CSharpParseTree.Common;
using CSharpParseTreeLib;

namespace CSharpParseTreeC
{
    class Program
    {
        struct Args
        {
            public string Project;
            public string OutputFile;
            public string Format;
            public bool ShowHelp;
        }
        
        static void PrintHelpMessage()
        {
            Console.WriteLine("CSharpParseTree - справка");
            Console.WriteLine("-p (--project)=\"<путь к папке проекта>\"");
            Console.WriteLine("-o (--output)=\"<путь к выходному файлу>\"");
            Console.WriteLine("-f (--format)=\"(xml|json|plain)\" - формат вывода");
            Console.WriteLine("-h (--help)  - показать это сообщение");
        }

        static Args CreateArgs(string[] args)
        {
            Args result = new Args();
            result.ShowHelp = false;

            try
            {
                foreach (string arg in args)
                {
                    if (arg.StartsWith("-p="))
                    {
                        result.Project = arg.Remove(0, 3);
                    }
                    else if (arg.StartsWith("--project="))
                    {
                        result.Project = arg.Remove(0, 10);
                    }
                    else if (arg.StartsWith("-o="))
                    {
                        result.OutputFile = arg.Remove(0, 3);
                    }
                    else if (arg.StartsWith("--output="))
                    {
                        result.OutputFile = arg.Remove(0, 9);
                    }
                    else if (arg.StartsWith("-f="))
                    {
                        result.Format = arg.Remove(0, 3);
                    }
                    else if (arg.StartsWith("--format="))
                    {
                        result.Format = arg.Remove(0, 9);
                    }
                    else if (arg.StartsWith("-h"))
                    {
                        result.ShowHelp = true;
                    }
                    else if (arg.StartsWith("--help"))
                    {
                        result.ShowHelp = true;
                    }
                }
            }
            catch
            {
                result.ShowHelp = true;
            }

            return result;
        }

        static void PrintError(string message)
        {
            Console.WriteLine("ОШИБКА: " + message);
        }

        static bool NeedPrintHelpMessage(Args args)
        {
            if (args.ShowHelp == true)
            {
                return true;
            }

            if ((args.Format == null) ||
                (args.OutputFile == null) ||
                (args.Project == null))
            {
                return true;
            }

            return false;
        }

        static IFormatBuilder CreateBuilder(string type, Stream stream)
        {
            if (type.Equals("xml", StringComparison.CurrentCultureIgnoreCase))
            {
                return new XMLFormatBuilder(stream);
            }
            else if (type.Equals("json", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new NotImplementedException("Поддержка JSON пока не реализована");
            }
            else if (type.Equals("plain", StringComparison.CurrentCultureIgnoreCase))
            {
                throw new NotImplementedException("Поддержка Plain Text пока не реализована");
            }
            else
            {
                return null;
            }
        }
        
        static void Main(string[] args)
        {
            Args arguments = CreateArgs(args);

            if(NeedPrintHelpMessage(arguments))
            {
                PrintHelpMessage();
                Console.ReadLine();
                return;
            }

            if (!Directory.Exists(arguments.Project))
            {
                PrintError("Папка " + "\"" + arguments.Project +"\" не найдена" );
                Console.ReadLine();
                return;
            }

            DirectoryProject project = new DirectoryProject(arguments.Project, "cs");

            Stream outputStream = null;
            IFormatBuilder formatter = null;

            ProgressIndicator progressIndicator = new ProgressIndicator(10, 1);

            try
            {
                outputStream = File.Open(arguments.OutputFile, FileMode.Create, FileAccess.Write);

                formatter = CreateBuilder(arguments.Format, outputStream);
                if (formatter == null)
                {
                    PrintHelpMessage();
                    throw new Exception();
                }

                RemovePathProcessor removePathProcessor = new RemovePathProcessor(arguments.Project);
                List<IStringProcessor> processors = new List<IStringProcessor>();
                processors.Add(removePathProcessor);

                StreamReportPrinter reporter = new StreamReportPrinter(Console.Out);

                FormatTextProjectProcessor projectProcessor = new FormatTextProjectProcessor(reporter, formatter, processors);
                projectProcessor.OnProgress += new ProgressHandler(progressIndicator.OnProgress);

                Console.WriteLine("Начало обработки файлов");
                Console.Write("Прогресс: ");
                Console.SetCursorPosition(0, 2);

                projectProcessor.Process(project);

            }
            catch(Exception ex)
            {
                PrintError(ex.Message);

                if (outputStream != null)
                {
                    outputStream.Close();
                }
                Console.ReadLine();
                return;
            }
            finally
            {
                if (outputStream != null)
                {
                    outputStream.Close();
                }
            }

            Console.WriteLine("Обработка файлов завершена");

            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharpParseTree.Common
{
    public class SourceFile
    {
        public string FileName { get; private set; }
        public string FileFullPath { get; private set; }

        public SourceFile(FileInfo fileInfo)
        {
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("Файл " + fileInfo.FullName + " не найден");
            }

            FileName = fileInfo.Name;
            FileFullPath = fileInfo.FullName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTree.Common
{
    public delegate void ProgressHandler(int percent);
    
    public abstract class AbstractProjectProcessor
    {
        protected virtual void DoProgress(int percent)
        {
            if (OnProgress != null)
            {
                CurrentProgress = percent;
                OnProgress(percent);
            }
        }

        public virtual void Process(SourceCodeProject project) { }

        public ProgressHandler OnProgress;
        public int CurrentProgress { get; private set; }
        
    }
}

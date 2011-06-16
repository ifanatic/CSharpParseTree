using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpParseTreeC
{
    class ProgressIndicator
    {
        private int _left;
        private int _top;

        private const int AREA_SIZE = 5;

        public ProgressIndicator(int left, int top)
        {
            _left = left;
            _top = top;
        }

        public void OnProgress(int percent)
        {
            int currentLeft = Console.CursorLeft;
            int currentTop = Console.CursorTop;

            Console.SetCursorPosition(_left, _top);
            Console.Write("{0}%", percent);

            for (int i = (percent.ToString().Length + 1); i <= AREA_SIZE; ++i)
            {
                Console.Write(" ");
            }

            Console.SetCursorPosition(currentLeft, currentTop);
        }
    }
}

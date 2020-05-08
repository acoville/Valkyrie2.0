using System;

namespace Valkyrie.Controls
{

    //=============================================================

    public class Command
    {
        internal string commandString_ = "";
        public string CommandString
        {
            get => commandString_;
            set => commandString_ = value;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Valkyrie.Controls
{
    public interface IController
    {
        Command SendCommand();
    }
}

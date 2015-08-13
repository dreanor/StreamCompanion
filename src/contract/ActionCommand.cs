using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.gmail.mikeundead.streamcompanion.contract
{
    public class ActionCommand
    {
        public ActionCommand(Action action)
        {
            action();
        }

        public ActionCommand(Action action, Func<bool> canExecute)
        {
            if (canExecute())
            {
                action();
            }
        }
    }
}

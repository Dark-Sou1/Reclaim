using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Scripting;

namespace Giacomo
{
    public interface IStatObject
    {
        public abstract Stats GetStats();
    }
}

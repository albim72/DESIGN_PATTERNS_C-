using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    internal class SingletonTest
    {
        private static readonly SingletonTest _instance = new SingletonTest();
        public static SingletonTest Instance {
            get { return _instance; }
        }
    }
}

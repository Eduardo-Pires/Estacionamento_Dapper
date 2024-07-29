using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace estacionamento.Repositorios
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class IgnoreDapperAttribute : Attribute
    {
        public IgnoreDapperAttribute()
        {

        }

    }
}
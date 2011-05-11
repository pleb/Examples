using System;

namespace NinjectExample.Web.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AlphaAttribute : Attribute
    {

    }
}
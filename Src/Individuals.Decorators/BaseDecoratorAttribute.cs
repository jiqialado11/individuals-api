using System;

namespace Individuals.Decorators
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true)]
    public  class BaseDecoratorAttribute : Attribute
    {
        public BaseDecoratorAttribute(Type decoratorType)
        {
            DecoratorType = decoratorType;
        }

        public Type DecoratorType { get; }
    }
}

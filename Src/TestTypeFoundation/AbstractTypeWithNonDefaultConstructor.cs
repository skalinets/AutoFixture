using System;

namespace Ploeh.TestTypeFoundation
{
    public abstract class AbstractTypeWithNonDefaultConstructor<T>
    {
        private readonly T property;

        protected AbstractTypeWithNonDefaultConstructor(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.property = value;
        }

        public abstract string StringProperty { get; }

        public T Property
        {
            get { return this.property; }
        }
    }
}

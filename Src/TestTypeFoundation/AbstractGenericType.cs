namespace Ploeh.TestTypeFoundation
{
    public abstract class AbstractGenericType<T>
    {
        private readonly T t;

        protected AbstractGenericType(T t)
        {
            this.t = t;
        }

        public virtual T Value { get { return this.t; }}
    }
}
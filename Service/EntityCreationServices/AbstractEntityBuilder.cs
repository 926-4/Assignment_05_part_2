namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public abstract class AbstractEntityBuilder<T, S>
        where S : T, new()
    {
        protected T instance = new S();
        public virtual AbstractEntityBuilder<T, S> Begin()
        {
            instance = new S();
            return this;
        }
        public T End()
        {
            T aux = instance;
            instance = new S();
            return aux;
        }
    }
}

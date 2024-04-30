namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    internal abstract class AbstractEntityFactory<T, S>
        where S : T
    {
        protected T instance = default(S);
        public virtual AbstractEntityFactory<T, S> Begin()
        {
            instance = default(S);
            return this;
        }
        public T End()
        {
            T aux = instance;
            instance = default(S);
            return aux;
        }
    }
}

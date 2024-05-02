namespace UBB_SE_2024_Team_42.Service.EntityCreationServices
{
    public abstract class AbstractEntityBuilder<Interface, Implementation>
        where Implementation : Interface, new()
    {
        protected Interface instance = new Implementation();
        public virtual AbstractEntityBuilder<Interface, Implementation> Begin()
        {
            instance = new Implementation();
            return this;
        }
        public Interface End()
        {
            Interface returnedObject = instance;
            instance = new Implementation();
            return returnedObject;
        }
    }
}

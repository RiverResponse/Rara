namespace Messages
{

    public class InstantiateEntityMessage
    {

        public InstantiateEntityMessage(EntityBase entity)
        {
            Entity = entity;
        }

        public EntityBase Entity { get; }
    }
}

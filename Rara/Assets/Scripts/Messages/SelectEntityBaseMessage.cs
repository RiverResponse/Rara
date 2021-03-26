namespace Messages
{

    public class SelectEntityBaseMessage
    {

        public EntityBase Entity { get; }
        

        public SelectEntityBaseMessage(EntityBase entity)
        {
            Entity = entity;
        }
    }
}

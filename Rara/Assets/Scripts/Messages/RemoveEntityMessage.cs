namespace Messages
{

    public class RemoveEntityMessage
    {

        public EntityBase EntityBase { get; private set; }


        public RemoveEntityMessage(EntityBase entityBase)
        {
            EntityBase = entityBase;
        }
    }
}
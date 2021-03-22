
public class ChooseEntityMessage
{
    public EntityBase Entity { get; private set; }
    public ChooseEntityMessage(EntityBase entity)
    {
        Entity = entity;
    }
}

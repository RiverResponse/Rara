using System.Linq;
using UniRx;

public class EntityBase
{
    public ReactiveCollection<EntityBehaviourBase> Behaviours = new ReactiveCollection<EntityBehaviourBase>();
    public ReactiveProperty<EntityData> EntityData = new ReactiveProperty<EntityData>();
    public StringReactiveProperty Name = new StringReactiveProperty();
    public StringReactiveProperty Description = new StringReactiveProperty();

    public EntityBase()
    {
        Name.Value = "New entity";
        Description.Value = "New entity's description";
    }

    protected virtual void TapAction()
    {
    }

    public void AddBehavior(EntityBehaviourBase behaviour)
    {
        var canBeAdded = EntityBehaviourCanBeAdded(behaviour);

        if (canBeAdded)
        {
            Behaviours.Add(behaviour);
            //TODO:Show added message
        }
        else
        {
            //TODO:Each behavior can be added once
        }
    }

    public bool EntityBehaviourCanBeAdded(EntityBehaviourBase behaviour)
    {
        return !Behaviours.Any(b => b.Type == behaviour.Type);
    }

    public void RemoveBehavior(EntityBehaviourBase behaviour)
    {
        if (Behaviours.Contains(behaviour))
        {
            Behaviours.Remove(behaviour);
            //TODO:Show removed message
        }
        else
        {
            //TODO:Show error
        }
    }

    public (bool, string) Validate()
    {
        (bool isValid, string problems) result;
        result.isValid = true;
        result.problems = "";


        return result;
    }
}
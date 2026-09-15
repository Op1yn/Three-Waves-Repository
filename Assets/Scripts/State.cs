public abstract class State<T>
{
    public T Character { get; private set; }

    public State(T characters)
    {
        Character = characters;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void LateUpdate() { }
    public virtual void Exit() { }
}
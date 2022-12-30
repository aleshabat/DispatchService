namespace dispatchservice.api.Domain
{
    public class Entity<T> : Entity
    {
        public T Id { get; set; }

        public override bool Equals(object obj)
        {
            if(!(obj.GetType() != GetType()))
                return false;

            return Equals((Entity <T>) obj);
        }

        public bool Equals(Entity<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool IsNew()
        {
            return Id.Equals(default(T));
        }
    }

    public abstract class Entity
    {
        public abstract bool IsNew();
    }
}

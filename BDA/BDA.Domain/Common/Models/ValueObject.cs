namespace BDA.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var valueObject = (ValueObject)obj;
        
        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return Equals(left, right);
    }
    
    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !Equals(left, right);
    }
    
    public bool Equals(ValueObject? other)
    {
       return Equals((object?)other);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(o => o?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}

using System;

namespace Feature.FormsExtensions.Business.FieldBindings
{
    public class FieldBindingTokenKey : IEquatable<FieldBindingTokenKey>
    {
        public FieldBindingTokenKey(string category, string id, string label)
        {
            Id = id;
            Label = label;
            Category = category;
        }

        public FieldBindingTokenKey(string id) : this(id,id,id)
        {
        }

        public string Id { get; }
        public string Label { get; }
        public string Category { get; }

        public bool Equals(FieldBindingTokenKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FieldBindingTokenKey) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
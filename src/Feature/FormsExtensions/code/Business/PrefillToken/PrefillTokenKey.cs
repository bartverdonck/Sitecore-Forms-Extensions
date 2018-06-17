using System;

namespace Feature.FormsExtensions.Business.PrefillToken
{
    public class PrefillTokenKey : IEquatable<PrefillTokenKey>
    {
        public PrefillTokenKey(string id, string label)
        {
            Id = id;
            Label = label;
        }

        public PrefillTokenKey(string id) : this(id,id)
        {
        }

        public string Id { get; }
        public string Label { get; }

        public bool Equals(PrefillTokenKey other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PrefillTokenKey) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
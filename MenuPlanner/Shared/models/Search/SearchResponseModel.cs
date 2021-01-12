using System.Collections.Generic;
namespace Shared.models.Search
{
    public class SearchResponseModel<TResult>
    {

        public List<TResult> Result {get; set;}

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
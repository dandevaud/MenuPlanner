using System.Collections.Generic;
namespace Shared.models.Search
{
    public class SearchResponseModel<TResult>
    {

        public List<TResult> Result {get; set;}
        public string OrderBy {get; set;}

    }
}
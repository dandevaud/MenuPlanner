using System;

namespace Shared.models.Search
{
    public abstract class SearchRequestModel
    {
        public string Filter {get; set;}
        public string Name { get; set; }
        public bool OrderBy {get; set;}
    }
}

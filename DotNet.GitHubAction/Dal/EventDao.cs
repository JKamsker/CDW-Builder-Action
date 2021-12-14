using DotNet.GitHubAction.Models.Database;

using MongoDB.Driver;

using System;
using System.Collections.Generic;

namespace DotNet.GitHubAction.Dal
{
    public class EventDao : DaoBase<WorkshopEvent>
    {
        public EventDao(IMongoCollection<WorkshopEvent> collection) : base(collection)
        {
        }

        internal IAsyncEnumerable<WorkshopEvent> FindByDateAsync(DateTimeOffset eventDate)
        {
            throw new NotImplementedException();
        }
    }
}

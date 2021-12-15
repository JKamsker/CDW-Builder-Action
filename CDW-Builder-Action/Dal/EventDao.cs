using CDW_Builder_Action.Models.Database;

using MongoDB.Driver;

using System;
using System.Linq;
using System.Collections.Generic;

using Rnd.MongoDb;
using System.Threading.Tasks;

namespace CDW_Builder_Action.Dal
{
    public class EventDao : DaoBase<WorkshopEvent>
    {
        public EventDao(IMongoCollection<WorkshopEvent> collection) : base(collection)
        {
        }

        internal async Task<WorkshopEvent?> FindByDateAsync(DateTimeOffset eventDate)
        {
            return await Collection
                 .FindAsync(x => x.EventDate == eventDate)
                 .EnumerateAsync()
                 .FirstOrDefaultAsync();
        }

        internal async Task InsertAsync(WorkshopEvent dbEvent)
        {
            await Collection.InsertOneAsync(dbEvent);
        }

        internal async Task UpdateAsync(WorkshopEvent dbEvent)
        {
            await Collection.ReplaceOneAsync(x => x.Id == dbEvent.Id, dbEvent);
        }
    }
}
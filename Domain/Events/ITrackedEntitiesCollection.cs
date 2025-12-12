using Domain.Models;

namespace Domain.Events
{
    public interface ITrackedEntitiesCollection
    {
        void AppendEntityToTracker(MainEntity entity);
        IEnumerable<MainEntity> GetTrackedEntities();
        void ClearAll();
    }

    public class TrackedEntitesCollection : ITrackedEntitiesCollection
    {
        private readonly HashSet<MainEntity> _trackedEntities = new();

        public void ClearAll()
        {
            foreach (MainEntity entity in _trackedEntities)
            {
                entity.ClearDomainEvents();
            }
            _trackedEntities.Clear();
        }

        public void AppendEntityToTracker(MainEntity entity)
        {
            _trackedEntities.Add(entity);
        }

        public IEnumerable<MainEntity> GetTrackedEntities()
        {
            return _trackedEntities;    
        }
    }

}

using System;
using System.Threading.Tasks;
using MenuPlanner.Server.Data;
using MenuPlanner.Shared.Models;

namespace MenuPlanner.Server.Logic.EntityUpdater
{
    public abstract class EntityUpdater
    {
        private readonly MenuPlannerContext _context;

        public EntityUpdater(MenuPlannerContext context)
        {
            _context = context;
        }

        protected void SaveChanges()
        {
            lock (_context)
            {
                _context.SaveChanges();
            }

        }

        public async Task<bool> DeleteEntity<T>(Guid id) where T : Entity
        {
            
            var ent = await _context.FindAsync<T>(id);
            if (ent == null) return false;
            _context.Remove<T>(ent);
            SaveChanges();
            return true;
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using MenuPlanner.Server.Data;
using MenuPlanner.Server.Extension.EntityFramework;
using MenuPlanner.Shared.models;
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
               
                    DetachAllUnchangedEntities();
                    if (!_context.ChangeTracker.Entries().IsNullOrEmpty())
                    {
                    _context.SaveChanges();
                }
            }

        }

        protected void DetachAllUnchangedEntities()
        {
            DetachUnchanged<Ingredient>();
            DetachUnchanged<MenuIngredient>();
            DetachUnchanged<Image>();
            DetachUnchanged<Comment>();

        }


        protected void DetachUnchanged<T>() where T : Identifier
        {
            _context.ChangeTracker.Entries<T>().ToList().ForEach(entry =>
            {
                entry?.DetachUnchanged();
            });
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

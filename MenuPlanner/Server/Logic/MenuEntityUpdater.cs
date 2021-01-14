using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.SqlImplementation;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Logic
{
    public class MenuEntityUpdater
    {

        public delegate Boolean ProvidedContains(Guid guid);

        public delegate Task RemoveFromContext<T>(Guid guid);

        public delegate void DetachEntity(Guid guid);

        private readonly MenuPlannerContext _context;

        public MenuEntityUpdater(MenuPlannerContext dbContext)
        {
            _context = dbContext;
        }


        /// <summary>Updates the menu in the Database.</summary>
        /// <param name="menu">The menu provided (new values)</param>
        /// <param name="entityInDatabase">The entity in database (old Values)</param>
        public async Task UpdateMenuInContext(Menu menu, Menu entityInDatabase)
        {
            //load images & Menuingredients
            await LoadMenuSubEntities(entityInDatabase);

            //Get IDs of provided entities
            var providedImages = menu.Images.Select(
                i => i.ImageId).ToList();
            var providedMenuIngredients = menu.Ingredients.Select(i => i.Id).ToList();


            //Remove all deleted Images from DB
            DeleteRemovedEntitiesFromMenu<Image>(
                entityInDatabase.Images,
                (Image i) => i.ImageId,
                (Guid guid) => providedImages.Contains(guid),
                RemoveSubEntities<Image>(_context.Image, providedImages));

            //Remove all deleted MenuIngredients from DB
            DeleteRemovedEntitiesFromMenu<MenuIngredient>(
                entityInDatabase.Ingredients,
                (MenuIngredient i) => i.Id,
                (Guid guid) => providedMenuIngredients.Contains(guid),
                RemoveSubEntities<MenuIngredient>(_context.MenuIngredients, providedImages));


            providedMenuIngredients.ForEach(menuIngr =>
                DetachEntityFromContext<MenuIngredient>(_context.MenuIngredients).Invoke(menuIngr)
            );
            providedImages.ForEach(imageProv =>
                DetachEntityFromContext<Image>(_context.Image).Invoke(imageProv)
            );

            _context.Entry(entityInDatabase).State = EntityState.Detached;
            _context.Update(menu).CurrentValues.SetValues(menu);


            await _context.SaveChangesAsync();
        }

        private RemoveFromContext<T> RemoveSubEntities<T>(DbSet<T> dbSet, List<Guid> providedList) where T : class
        {
            return async (Guid guid) =>
            {
                var menuEntity = dbSet.FindAsync(guid);
                providedList.Remove(guid);
                dbSet.Remove(await menuEntity);
            };
        }

        private DetachEntity DetachEntityFromContext<T>(DbSet<T> dbSet) where T : class
        {
            return (Guid guid) =>
            {
                var entity = dbSet.Find(guid);
                if (entity != null)
                {
                    _context.Entry(entity).State = EntityState.Detached;
                }
            };
        }
        private void DeleteRemovedEntitiesFromMenu<T>(ICollection<T> entities, Func<T, Guid> selector, ProvidedContains providedContains, RemoveFromContext<T> removeFromContext)
        {
            entities
                .Select(selector)
                .Where(guid => !providedContains(guid))
                .ToList()
                .ForEach(async id => { await removeFromContext(id); });
        }

        private async Task LoadMenuSubEntities(Menu foundMenu)
        {
            await _context.Entry(foundMenu).Collection(m => m.Images).LoadAsync();
            await _context.Entry(foundMenu).Collection(m => m.Ingredients).LoadAsync();
        }
    }
}

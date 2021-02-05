// <copyright file="IngredientsController.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Shared.models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Server.Logic.EntityUpdater
{
    /// <summary>Class used to update Menu entities in Database</summary>
    public class MenuEntityUpdater : EntityUpdater, IMenuEntityUpdater
    {
        public delegate bool ProvidedContains(Guid guid);
        public delegate Task RemoveFromContext<T>(Guid guid);
        public delegate void DetachEntity(Guid guid);
        private readonly MenuPlannerContext _context;

        public MenuEntityUpdater(MenuPlannerContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        

        /// <summary>Updates the menu in the Database.</summary>
        /// <param name="menu">The menu provided (new values)</param>
        /// <param name="entityInDatabase">The entity in database (old Values)</param>
        public async Task UpdateMenuInContext(Menu menu)
        {
            var entityInDatabase = await _context.Menus.FindAsync(menu.Id);
            if (entityInDatabase == null)
            {
                await CreateMenuInContext(menu);
                return;
            }
            //load images & MenuIngredients
            await LoadMenuSubEntities(entityInDatabase);

            //Get IDs of provided entities
            var providedImages = menu.Images.Select(
                i => i.ImageId).ToList();
            var providedMenuIngredients = menu.Ingredients.Select(i => i.Id).ToList();

            //Remove all deleted Images from DB
            await DeleteRemovedEntitiesFromMenu<Image>(
                entityInDatabase.Images,
                (Image i) => i.ImageId,
                (Guid guid) => providedImages.Contains(guid),
                RemoveSubEntities<Image>(_context.Images, providedImages));

            //Remove all deleted MenuIngredients from DB
           await  DeleteRemovedEntitiesFromMenu<MenuIngredient>(
                entityInDatabase.Ingredients,
                (MenuIngredient i) => i.Id,
                (Guid guid) => providedMenuIngredients.Contains(guid),
                RemoveSubEntities<MenuIngredient>(_context.MenuIngredients, providedMenuIngredients));

            menu.Ingredients.Select(i => i.Ingredient).ToList()
                .ForEach( ing => 
                    DetachEntityFromContext<Ingredient>(_context.Ingredients).Invoke(ing.Id)
                );
            providedMenuIngredients.ForEach(menuIngr =>
                DetachEntityFromContext<MenuIngredient>(_context.MenuIngredients).Invoke(menuIngr)
            );
            providedImages.ForEach(imageProv =>
                DetachEntityFromContext<Image>(_context.Images).Invoke(imageProv)
            );

            _context.Entry(entityInDatabase).State = EntityState.Detached;
            UnLoadMenuSubEntities(entityInDatabase);
            UnLoadMenuSubEntities(menu);
            _context.Update(menu).CurrentValues.SetValues(menu);

            SaveChanges();
        }

        private async Task CreateMenuInContext(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            foreach (var menuIngredient in menu.Ingredients)
            {
                _context.Entry(menuIngredient.Ingredient).State = EntityState.Detached;
            }

            SaveChanges();
        }

        /// <summary>
        /// Deletes the menu from database.
        /// </summary>
        /// <param name="menuIn">The menu to delete</param>
        public async Task DeleteMenuFromDatabase(Menu menuIn)
        {
            var menu = await _context.Menus.FindAsync(menuIn.Id);
            if (menu == null)
            {
                return;
            }
            await LoadMenuSubEntities(menu);
            menu.Ingredients.ToList().ForEach(mi => RemoveSubEntities<MenuIngredient>(_context.MenuIngredients,null).Invoke(mi.Id));
            menu.Images.ToList().ForEach(i => RemoveSubEntities<Image>(_context.Images, null).Invoke(i.ImageId));
            _context.Menus.Remove(menu);
            SaveChanges();
        }

        private RemoveFromContext<T> RemoveSubEntities<T>(DbSet<T> dbSet, List<Guid> providedList) where T : class
        {
            return async (Guid guid) =>
            {
                var menuEntity = dbSet.FindAsync(guid);
                providedList?.Remove(guid);
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
                    var entry = _context.ChangeTracker.Entries<T>()
                        .FirstOrDefault(ent => ent.Entity.Equals(entity));
                    if (entry != null) entry.State = EntityState.Detached;
                }
            };
        }
        private async Task DeleteRemovedEntitiesFromMenu<T>(ICollection<T> entities, Func<T, Guid> selector, ProvidedContains providedContains, RemoveFromContext<T> removeFromContext)
        {
            var toDelete = entities
                .Select(selector)
                .Where(guid => !providedContains(guid))
                .ToList();
            foreach (var id in toDelete)
            {
                await removeFromContext(id);
            }

           
        }

        /// <summary>
        /// Loads the menu sub entities such as MenuIngredient and Images.
        /// </summary>
        /// <param name="foundMenu">The menu to Load the entities from</param>
        private async Task LoadMenuSubEntities(Menu foundMenu)
        {
            await _context.Entry(foundMenu).Collection(m => m.Images).LoadAsync();
            await _context.Entry(foundMenu).Collection(m => m.Ingredients).LoadAsync();
            var toLoad = foundMenu.Ingredients.ToList();
            foreach (var mi in toLoad)
            {
                await _context.Entry(mi).Reference(i => i.Ingredient).LoadAsync();
            }

        }

        private void UnLoadMenuSubEntities(Menu foundMenu)
        {
            foundMenu.Ingredients.ToList().ForEach(mi =>
            {
                var entity = _context.ChangeTracker.Entries<Ingredient>().FirstOrDefault(i => mi.Ingredient.Id.Equals(i.Entity.Id));
                if (entity != null)
                {
                    if (entity.State == EntityState.Unchanged)
                    {
                        entity.State = EntityState.Detached;
                    }
                }
            });

            foundMenu.Ingredients.ToList().ForEach(mi =>
            {
                var entity = _context.ChangeTracker.Entries<MenuIngredient>().FirstOrDefault(i => mi.Id.Equals(i.Entity.Id));
                if (entity != null)
                {
                    if (entity.State == EntityState.Unchanged)
                    {
                        entity.State = EntityState.Detached;
                    }
                }
            });

            foundMenu.Images.ToList().ForEach(mi =>
            {
                var entity = _context.ChangeTracker.Entries<Image>().FirstOrDefault(i => mi.ImageId.Equals(i.Entity.ImageId));
                if (entity != null)
                {
                    if (entity.State == EntityState.Unchanged)
                    {
                        entity.State = EntityState.Detached;
                    }
                }
            });
        }
    }
}

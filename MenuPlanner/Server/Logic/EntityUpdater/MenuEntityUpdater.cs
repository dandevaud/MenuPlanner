// <copyright file="MenuEntityUpdater.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MenuPlanner.Server.Contracts.Blob;
using MenuPlanner.Server.Contracts.Logic;
using MenuPlanner.Server.Data;
using MenuPlanner.Server.Extension.EntityFramework;
using MenuPlanner.Shared.Extension;
using MenuPlanner.Shared.models;
using MenuPlanner.Shared.Models;
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
        private readonly IPictureHandler _pictureHandler;

        public MenuEntityUpdater(MenuPlannerContext dbContext, IPictureHandler pictureHandler) : base(dbContext)
        {
            _context = dbContext;
            _pictureHandler = pictureHandler;
        }



        /// <summary>Updates the menu in the Database.</summary>
        /// <param name="menu">The menu provided (new values)</param>
        /// <param name="entityInDatabase">The entity in database (old Values)</param>
        public async Task UpdateMenuInContext(Menu menu)
        {
            var entityInDatabase = await _context.Menus.
                Include(m => m.Images).
                Include(m => m.Tags).
                Include(m => m.Ingredients).
                ThenInclude(mi => mi.Ingredient).
                FirstOrDefaultAsync(ei => ei.Id.Equals(menu.Id));
            if (entityInDatabase == null)
            {
                await CreateMenuInContext(menu);
                return;
            }


            await HandleSubEntities(menu, entityInDatabase);

            _context.Entry(entityInDatabase).State = EntityState.Detached;


            DetachAllUnchangedEntities();


            _context.Update(entityInDatabase).CurrentValues.SetValues(menu);

            menu.Images.ToList().ForEach(i =>
                {
                    if (i.Id.Equals(Guid.Empty) || !entityInDatabase.Images.Any(im => im.Id.Equals(i.Id)))
                    {
                        entityInDatabase.Images.Add(i);
                    }
                }
            );

            SaveChanges();
            await HandleImages(entityInDatabase);
            SaveChanges();
        }

        private async Task HandleSubEntities(Menu menu, Menu entityInDatabase)
        {
            //load images & MenuIngredients
            await LoadMenuSubEntities(entityInDatabase);
            HandleNewTags(menu, entityInDatabase);

            HandleNewAndChangedMenuIngredients(menu, entityInDatabase);

            //Get IDs of provided entities
            await HandleDeletedSubEntities(menu, entityInDatabase);
        }

        private void HandleNewTags(Menu menu, Menu entityInDatabase)
        {
            var newTags = menu.Tags.Where(i => !entityInDatabase.Tags.Any(t => t.Id.Equals(i.Id))).ToList();
            newTags.ForEach(tag => entityInDatabase.Tags.Add(tag));
        }

        private async Task HandleDeletedSubEntities(Menu menu, Menu entityInDatabase)
        {
            var providedImages = menu.Images.Select(
                i => i.Id).ToList();
            var providedMenuIngredients = menu.Ingredients.Select(i => i.Id).ToList();

            var providedTags = menu.Tags.Select(t => t.Id).ToList();

            await RemoveAndDeleteImages(menu, providedImages, entityInDatabase);

            //Remove all deleted MenuIngredients from DB
            await DeleteRemovedEntitiesFromMenu<MenuIngredient>(
                entityInDatabase.Ingredients,
                (MenuIngredient i) => i.Id,
                (Guid guid) => providedMenuIngredients.Contains(guid),
                RemoveSubEntities<MenuIngredient>(_context.MenuIngredients, providedMenuIngredients));

            //Remove all deleted MenuIngredients from DB
            await DeleteRemovedEntitiesFromMenu<Tag>(
                entityInDatabase.Tags,
                (Tag i) => i.Id,
                (Guid guid) => providedTags.Contains(guid),
                RemoveSubEntities<Tag>(_context.Tags, providedTags));


        }

        private void HandleNewAndChangedMenuIngredients(Menu menu, Menu entityInDatabase)
        {

            HandleNewMenuIngredient(menu, entityInDatabase);


            HandleChangedMenuIngredient(menu, entityInDatabase);


        }

        private void HandleNewMenuIngredient(Menu menu, Menu entityInDatabase)
        {
            var newIngredients =
                menu.Ingredients.Where(i => !entityInDatabase.Ingredients.Any(mi => mi.Id.Equals(i.Id))).ToList();
            HandleDuplicateIngredient(newIngredients).ForEach(mi => entityInDatabase.Ingredients.Add(mi));

        }

        private List<MenuIngredient> HandleDuplicateIngredient(List<MenuIngredient> menuIngredients)
        {
            var duplicates = menuIngredients.Select(mi => mi.Ingredient).GroupBy(i => i.Name).Where(g => g.Count() > 1).SelectMany(g => g).ToList();
            menuIngredients.ForEach(mi =>
            {
                if (_context.ChangeTracker.Entries<Ingredient>().Any(i => i.Entity.Id.Equals(mi.Ingredient.Id)))
                {
                    mi.Ingredient = _context.ChangeTracker.Entries<Ingredient>()
                        .FirstOrDefault(i => i.Entity.Id.Equals(mi.Ingredient.Id))?.Entity;
                }
                else if (duplicates.Contains(mi.Ingredient))
                {
                    mi.Ingredient = duplicates.First(i => i.Id.Equals(mi.Ingredient.Id));
                }
            });
            return menuIngredients;

        }

        private void HandleChangedMenuIngredient(Menu menu, Menu entityInDatabase)
        {
            var changedIngredients = menu.Ingredients.Where(i => entityInDatabase.Ingredients.Any(mi => mi.Id.Equals(i.Id) && !mi.Id.Equals(Guid.Empty)))
                .Where(mi =>
                    !mi.ContentEquals(entityInDatabase.Ingredients.SingleOrDefault(i => i.Id.Equals(mi.Id)))).ToList();

            changedIngredients.ForEach(mi =>
            {
                var entity = _context.ChangeTracker.Entries<MenuIngredient>().SingleOrDefault(i => i.Entity.Id.Equals(mi.Id));
                var ingredientEntity = _context.ChangeTracker.Entries<Ingredient>()
                    .SingleOrDefault(ing => ing.Entity.Id.Equals(mi.Ingredient.Id));
                ingredientEntity?.DetachUnchanged();
                entity?.CurrentValues.SetValues(mi);
            });
        }

        private async Task RemoveAndDeleteImages(Menu menu, List<Guid> providedImages, Menu entityInDatabase)
        {
            var removeImages = RemoveSubEntities<Image>(_context.Images, providedImages);
            removeImages += DeleteImages(menu.Id);

            //Remove all deleted Images from DB
            await DeleteRemovedEntitiesFromMenu<Image>(
                entityInDatabase.Images,
                (Image i) => i.Id,
                (Guid guid) => providedImages.Contains(guid),
                removeImages
            );
        }

        private async Task HandleImages(Menu menu)
        {
            foreach (var menuImage in menu.Images)
            {
                if (!_pictureHandler.ImageExists(menu.Id, menuImage))
                {
                    menuImage.Path = await _pictureHandler.SaveImage(menu.Id, menuImage);
                }
            }


        }

        private async Task CreateMenuInContext(Menu menu)
        {
            await _context.Ingredients.LoadAsync();
            menu.Ingredients = HandleDuplicateIngredient(menu.Ingredients.ToList());
            await _context.Menus.AddAsync(menu);

            SaveChanges();
            var images = menu.Images;
            await SaveImages(menu, images);
            SaveChanges();


        }

        private async Task SaveImages(Menu menu, ICollection<Image> images)
        {
            foreach (var image in images)
            {
                image.Path = await _pictureHandler.SaveImage(menu.Id, image);
            }
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
            menu.Ingredients.ToList().ForEach(mi => RemoveSubEntities<MenuIngredient>(_context.MenuIngredients, null).Invoke(mi.Id));
            menu.Images.ToList().ForEach(i => RemoveSubEntities<Image>(_context.Images, null).Invoke(i.Id));
            menu.Comments.ToList().ForEach(i => RemoveSubEntities<Comment>(_context.Comments, null).Invoke(i.Id));
            _pictureHandler.DeleteImage(menu.Id, Guid.Empty);
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

        private RemoveFromContext<Image> DeleteImages(Guid menuId)
        {
            return async (Guid guid) =>
            {
                await Task.CompletedTask;
                _pictureHandler.DeleteImage(menuId, guid);
            };
        }

        private DetachEntity DetachEntityFromContext<T>(DbSet<T> dbSet) where T : Identifier
        {
            return (Guid guid) =>
            {

                var entry = _context.ChangeTracker.Entries<T>()
                    .FirstOrDefault(ent => ent.Entity.Id.Equals(guid));
                entry?.DetachUnchanged();

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
            //await _context.Entry(foundMenu).Collection(m => m.Images).LoadAsync();
            //await _context.Entry(foundMenu).Collection(m => m.Ingredients).LoadAsync();
            var toLoad = foundMenu.Ingredients.ToList();
            foreach (var mi in toLoad)
            {
                await _context.Entry(mi).Reference(i => i.Ingredient).LoadAsync();
            }

        }



    }
}

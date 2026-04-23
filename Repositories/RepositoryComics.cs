using Microsoft.EntityFrameworkCore;
using PracticandoExamenViernes.Data;
using PracticandoExamenViernes.Models;

namespace PracticandoExamenViernes.Repositories
{
    public class RepositoryComics
    {

        private readonly ComicsContext context;

        public RepositoryComics( ComicsContext context)
        {
            this.context = context;
        }

        public async Task<List<Comic>> GetComicsAsync()
        {

            return await this.context.comics.ToListAsync();
        }
        public async Task<Comic> GetComicAsync(int id)
        {
            return await this.context.comics.FirstOrDefaultAsync(x => x.Id==id);
        }

        public  async Task UpdateComicAsync(Comic comic)
        {

             this.context.comics.Update(comic);

            await this.context.SaveChangesAsync();

        }

        public async Task CreateComicAsync(Comic comic)
        {
        
            await this.context.comics.AddAsync(comic);

            await this.context.SaveChangesAsync();

        }
        public async Task RemoveComicAsync(int id)
        {

            Comic comic= await this.GetComicAsync(id);
             this.context.Remove(comic);
            await this.context.SaveChangesAsync();

        }


    }
}
